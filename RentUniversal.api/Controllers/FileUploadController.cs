// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
//
// namespace RentUniversal.api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class FileUploadController : ControllerBase
//     {
//         private readonly string _uploadRoot;
//
//         public FileUploadController(IConfiguration configuration)
//         {
//             // Læs upload-roden fra appsettings.json
//             _uploadRoot = configuration["UploadSettings:RootPath"]
//                           ?? throw new InvalidOperationException("Upload root path is not configured.");
//         }
//          
//         /// <summary>
//         /// Upload en enkelt fil til en specifik undermappe.
//         /// Eksempel: POST /api/FileUpload/upload/profile
//         /// </summary>
//         [HttpPost("upload/{folder}")]
//         public async Task<IActionResult> UploadFile(
//             string folder,
//             [FromForm] IFormFile file)
//         {
//             if (file == null || file.Length == 0)
//                 return BadRequest("Ingen fil modtaget.");
//
//             // Sørg for at mappenavnet ikke indeholder slashes osv.
//             var safeFolderName = folder.Replace("..", string.Empty)
//                                        .Replace("/", string.Empty)
//                                        .Replace("\\", string.Empty);
//
//             // Byg fuld sti: root + undermappe
//             var targetFolder = Path.Combine(_uploadRoot, safeFolderName);
//
//             if (!Directory.Exists(targetFolder))
//             {
//                 Directory.CreateDirectory(targetFolder);
//             }
//
//             // Brug kun filnavnet (ingen path fra klienten)
//             var safeFileName = Path.GetFileName(file.FileName);
//
//             // Her kan du fx tilføje timestamp for at undgå duplikerede navne
//             var fileNameWithTimestamp =
//                 $"{Path.GetFileNameWithoutExtension(safeFileName)}_{DateTime.UtcNow:yyyyMMddHHmmss}{Path.GetExtension(safeFileName)}";
//
//             var fullPath = Path.Combine(targetFolder, fileNameWithTimestamp);
//
//             // Gem filen på serveren
//             await using (var stream = new FileStream(fullPath, FileMode.Create))
//             {
//                 await file.CopyToAsync(stream);
//             }
//
//             // Returner fx relativ sti, som du senere kan gemme i databasen
//             var relativePath = Path.Combine(safeFolderName, fileNameWithTimestamp);
//
//             return Ok(new
//             {
//                 message = "Fil uploadet",
//                 fileName = fileNameWithTimestamp,
//                 folder = safeFolderName,
//                 path = relativePath
//             });
//         }
//     }
// }
