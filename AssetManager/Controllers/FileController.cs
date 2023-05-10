using AssetManager.Database;
using AssetManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AssetManager.Controllers
{
    /// <summary>
    /// The controller responsible for file uploads and listings
    /// </summary>
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

        /// <summary>
        /// Allows users to upload files specified in form-data/files
        /// </summary>
        /// <param name="files">The List of files that should be uploaded</param>
        /// <returns>Nothing :) (hopefully)</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult> AddFile(List<IFormFile> files)
        {
            User? user = dbContext.Users.FirstOrDefault(a => a.Name == User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (user is null)
                return Unauthorized();

            if (files.Count == 0)
                return BadRequest("Expected at least 1 file");

            long maxSize = configuration.GetValue<long>("MaxFileSize", 20000000);

            if (files.Any(file => file.Length >= maxSize))
                return BadRequest($"Maximum file size is {maxSize} bytes");

            using MD5 md5 = MD5.Create();

            foreach (IFormFile file in files)
            {
                await using Stream stream = file.OpenReadStream();
                string? hash = Convert.ToHexString(await md5.ComputeHashAsync(stream));

                if (String.IsNullOrEmpty(hash))
                    return Problem("Internal server error");

                stream.Seek(0, SeekOrigin.Begin);

                File entry = new()
                {
                    Md5 = hash,
                    Name = file.FileName,
                    User = user
                };

                await using FileStream fileStream = IOFile.Create($"./data/{entry.Guid}.{file.FileName.Split(".").Last()}");
                await stream.CopyToAsync(fileStream);

                dbContext.Files.Add(entry);
            }
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Returns all of the Files the user has Uploaded
        /// </summary>
        /// <returns>A list of the Files the user has Uploaded</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(File), 200)]
        public async Task<ActionResult<IEnumerable<File>>> GetUsersItems()
        {
            User? user = dbContext.Users.FirstOrDefault(a => a.Name == User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (user is null)
                return Unauthorized();

            return Ok(await dbContext.Files.Where(a => a.User.Id == user.Id).ToListAsync());
        }
    }
}