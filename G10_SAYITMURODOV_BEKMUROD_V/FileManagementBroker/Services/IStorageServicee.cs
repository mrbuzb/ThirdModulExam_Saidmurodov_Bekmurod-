using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementBroker.Services;

public interface IStorageServicee
{
    Task CreateFolderAsync(string folderPath);
    Task UploadFileAsync(string folderPath, Stream stream); 
    Task UploadFileWithChunksAsync(string? folderPath, Stream stream);
    Task UploafFilesAsync(List<Stream> streams, string? folderPath);
    Task DeleteFileAsync(string filePath);
    Task DeleteFolderAsync(string folderPath);
    Task<Stream> DownloadFileAsync(string filePath);
    Task<Stream> DownloadFolderAzZipAsync(string folderPath);
    Task<string> GetContentOfTxtFileAsync(string filePath);
    Task UpdateContentOfTxtFileAsync(string filePath, string newContent);
    Task<List<string>> GetAllInFolderPathAsync(string? folderPath);

}
