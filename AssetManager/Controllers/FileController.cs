using System.Security.Cryptography;
using AssetManager.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AssetManager.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class FileController : ControllerBase
	{
		private readonly IConfiguration configuration;
		private readonly MainDatabase dbContext;
		public FileController(IConfiguration configuration, MainDatabase dbContext)
		{
			this.configuration = configuration;
			this.dbContext = dbContext;
		}

		[HttpGet]
		public ActionResult<IEnumerable<File>> GetAll()
		{
			return Ok(dbContext.Files.AsQueryable());
		}

		[HttpPost]
		public async Task<ActionResult> AddFile(List<IFormFile> files)
		{
			if (files.Count == 0)
				return BadRequest("Expected at least 1 file");

			long maxSize = configuration.GetValue<long>("MaxFileSize", 20000000);

			if (files.Any(file => file.Length >= maxSize))
			{
				return BadRequest($"Maximum file size is {maxSize} bytes");
			}

			using MD5 md5 = MD5.Create();

			foreach (IFormFile file in files)
			{
				await using Stream stream = file.OpenReadStream();
				string? hash = Convert.ToHexString(await md5.ComputeHashAsync(stream));

				if (String.IsNullOrEmpty(hash))
					return Problem("Internal server error");

				stream.Seek(0, SeekOrigin.Begin);

				File entry = new File
				{
					Md5 = hash,
					Name = file.FileName
				};

				await using FileStream fileStream = IOFile.Create($"./data/{entry.Uuid}.{file.FileName.Split(".").Last()}");
				await stream.CopyToAsync(fileStream);

				dbContext.Files.Add(entry);
				await dbContext.SaveChangesAsync();
			}
			
			return Ok();
		}
	}
}
