using AssetManager.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Security.Claims;

namespace AssetManager.Controllers
{
    /// <summary>
    /// The controller responsible for returning content to the user
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly MainDatabase dbContext;

        public ContentController(MainDatabase dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Returns the file located under the guid returned by the File endpoints
        /// </summary>
        /// <param name="guid"> The guid of the sought after file</param>
        /// <returns>The sought after file</returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(typeof(FileStream), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetFile(Guid guid)
        {
            File? result = dbContext.Files.FirstOrDefault(a => a.Guid == guid);
            if (result is null)
                return NotFound();

            string contentType = Path.GetExtension(result.Name);
            FileStream file = IOFile.OpenRead($"./data/{result.Guid}{contentType}");

            _ = new FileExtensionContentTypeProvider().TryGetContentType(contentType, out string? what);

            return File(file, what ?? "text/plain", fileDownloadName: result.Name);
        }
    }
}