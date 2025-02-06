namespace FileManagementService.Service;

public interface IFileManagementServce
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