using FileManagementService.Service;
using Microsoft.AspNetCore.Mvc;

namespace G10_SAYITMURODOV_BEKMUROD_V.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagementController : ControllerBase
    {
        private readonly IFileManagementServce _fileManagementServce;

        public FileManagementController(IFileManagementServce fileManagementServce)
        {
            _fileManagementServce = fileManagementServce;
        }

        [HttpPost("createFolderAsync")]
        public async Task CreateFolderAsync(string folderPath)
        {
            await _fileManagementServce.CreateFolderAsync(folderPath);
        }
        [HttpPost("uploadFileAsync")]
        public async Task UploadFileAsync(string folderPath, IFormFile file)
        {
            folderPath += file.FileName;
            using (var stream = file.OpenReadStream())
            {
                await _fileManagementServce.UploadFileAsync(folderPath, stream);
            }
        }
        [HttpPost("uploadFileWithChunksAsync")]
        public async Task UploadFileWithChunksAsync(string? folderPath, IFormFile file)
        {
            folderPath += file.FileName;
            using (var stream = file.OpenReadStream())
            {
                await _fileManagementServce.UploadFileAsync(folderPath, stream);
            }
        }
        [HttpPost("uploafFilesAsync")]
        public async Task UploafFilesAsync(List<IFormFile> files, string? folderPath)
        {
            
            foreach (var file in files)
            {
                using (var stream = file.OpenReadStream())
                {
                    folderPath += file.FileName;
                    await _fileManagementServce.UploadFileAsync(folderPath, stream);
                }
            }
        }
        [HttpDelete("deleteFileAsync")]
        public async Task DeleteFileAsync(string filePath)
        {
            await _fileManagementServce.DeleteFileAsync(filePath);
        }
        [HttpDelete("deleteFolderAsync")]
        public async Task DeleteFolderAsync(string folderPath)
        {
            await _fileManagementServce.DeleteFolderAsync(folderPath);
        }
        [HttpGet("downloadFileAsync")]
        public async Task<FileStreamResult> DownloadFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("Error");
            }
            var fileName = Path.GetFileName(filePath);
            var stream =await _fileManagementServce.DownloadFileAsync(filePath);
            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = fileName,
            };
        }
        [HttpGet("downloadFolderAzZipAsync")]
        public async  Task<FileStreamResult> DownloadFolderAzZipAsync(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                throw new Exception("Error");
            }
            var fileName = Path.GetFileName(folderPath);
            var stream = await _fileManagementServce.DownloadFolderAzZipAsync(folderPath);
            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = fileName+".zip",
            };
        }
        [HttpGet("getContentOfTxtFileAsync")]
        public async Task<string> GetContentOfTxtFileAsync(string filePath)
        {
            return await _fileManagementServce.GetContentOfTxtFileAsync(filePath);
        }
        [HttpPut("updateContentOfTxtFileAsync")]
        public async Task UpdateContentOfTxtFileAsync(string filePath, string newContent)
        {
            await _fileManagementServce.UpdateContentOfTxtFileAsync(filePath, newContent);
        }
        [HttpGet("getAllInFolderPathAsync")]
        public async Task<List<string>> GetAllInFolderPathAsync(string? folderPath)
        {
            return await _fileManagementServce.GetAllInFolderPathAsync(folderPath);
        }


    }
}
