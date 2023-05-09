using AssetManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MainDatabase>((optionsBuilder) => { optionsBuilder.UseSqlite("Datasource=./MainDatabase.sqlite"); });

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

app.Run();
