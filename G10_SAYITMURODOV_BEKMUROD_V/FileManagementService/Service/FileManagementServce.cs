using FileManagementBroker.Services;

namespace FileManagementService.Service;

public class FileManagementServce : IFileManagementServce
{
    private readonly IStorageServicee _storageService;

    public FileManagementServce(IStorageServicee storageService)
    {
        _storageService = storageService;
    }

    public async Task CreateFolderAsync(string folderPath)
    {
        await _storageService.CreateFolderAsync(folderPath);
    }

    public async Task DeleteFileAsync(string filePath)
    {
        await _storageService.DeleteFileAsync(filePath);
    }

    public async Task DeleteFolderAsync(string folderPath)
    {
        await _storageService.DeleteFolderAsync(folderPath);
    }

    public async Task<Stream> DownloadFileAsync(string filePath)
    {
        return await _storageService.DownloadFileAsync(filePath);
    }

    public async Task<Stream> DownloadFolderAzZipAsync(string folderPath)
    {
        return await _storageService.DownloadFolderAzZipAsync(folderPath);
    }

    public async Task<List<string>> GetAllInFolderPathAsync(string? folderPath)
    {
        return await _storageService.GetAllInFolderPathAsync(folderPath);
    }

    public async Task<string> GetContentOfTxtFileAsync(string filePath)
    {
        return await _storageService.GetContentOfTxtFileAsync(filePath);
    }

    public async Task UpdateContentOfTxtFileAsync(string filePath, string newContent)
    {
        await UpdateContentOfTxtFileAsync(filePath, newContent);
    }

    public async Task UploadFileAsync(string folderPath, Stream stream)
    {
        await _storageService.UploadFileAsync(folderPath, stream);    
    }

    public async Task UploadFileWithChunksAsync(string? folderPath, Stream stream)
    {
        await _storageService.UploadFileWithChunksAsync(folderPath,stream);
    }

    public async Task UploafFilesAsync(List<Stream> streams, string? folderPath)
    {
        await _storageService.UploafFilesAsync(streams, folderPath);
    }
}
