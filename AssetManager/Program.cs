using AssetManager.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Xml.XPath;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MainDatabase>((optionsBuilder) => optionsBuilder.UseSqlite("Datasource=./MainDatabase.sqlite"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer((optionsBuilder) =>
{
	optionsBuilder.TokenValidationParameters = new()
	{
		ValidateAudience = true,
		ValidateIssuer = true,
		ValidateLifetime = false,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["JWT:Issuer"],
		ValidAudience = builder.Configuration["JWT:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"] ?? "scream"))
	};
});

builder.Services.AddCors(corsOptionsFactory => corsOptionsFactory.AddDefaultPolicy(corsPolicyOptionFactory => corsPolicyOptionFactory.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

if (builder.Environment.IsDevelopment())
{
	builder.Services.AddSwaggerGen(optionsBuilder => optionsBuilder.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "documentation.xml")));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Configure MainDB
IServiceScope scope = app.Services.CreateScope();

//Ensure data directory exists
if (!Directory.Exists("./data"))
{
	Directory.CreateDirectory("./data");
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

//Static file root to read the files back
app.UseStaticFiles(new StaticFileOptions()
{
	FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "data")),
	RequestPath = "/Content",
	ServeUnknownFileTypes = true
});

scope.ServiceProvider.GetRequiredService<MainDatabase>().Database.EnsureCreated();

app.MapControllers();
app.UseCors();

app.Run();