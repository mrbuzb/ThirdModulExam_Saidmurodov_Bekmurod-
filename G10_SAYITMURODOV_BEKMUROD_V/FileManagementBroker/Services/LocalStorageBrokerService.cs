using System.IO.Compression;

namespace FileManagementBroker.Services;

public class LocalStorageBrokerService : IStorageServicee
{

    private readonly string _storagePath;
    public LocalStorageBrokerService()
    {
        _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "LocalStoragePath");
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }


    public async Task CreateFolderAsync(string folderPath)
    {
        var path = Path.Combine(_storagePath, folderPath);

        if (Directory.Exists(path))
        {
            throw new Exception("This Folder Already Exists");
        }

        var parent = Directory.GetParent(path);
        if (!Directory.Exists(parent.FullName))
        {
            throw new Exception("Parent Folder Not Found");
        }

        Directory.CreateDirectory(path);
    }

    public async Task DeleteFileAsync(string filePath)
    {
        var path = Path.Combine(_storagePath, filePath);
        if (!File.Exists(path))
        {
            throw new Exception("File Not Found To Delete");
        }
        File.Delete(path);
    }

    public async Task DeleteFolderAsync(string folderPath)
    {
        var path = Path.Combine(_storagePath, folderPath);
        if (!Directory.Exists(path))
        {
            throw new Exception("Folder Not Found To Deletw");
        }
        Directory.Delete(path, true);
    }

    public async Task<Stream> DownloadFileAsync(string filePath)
    {
        var path = Path.Combine(_storagePath, filePath);

        if (!File.Exists(path))
        {
            throw new Exception("File Not Found To Download");
        }

        return new FileStream(path, FileMode.Open, FileAccess.Read);
    }

    public async Task<Stream> DownloadFolderAzZipAsync(string folderPath)
    {
        var path = Path.Combine(_storagePath, folderPath);
        if (Path.GetExtension(path) != string.Empty)
        {
            throw new Exception("Folder Path Is Not a Folder");
        }
        if (!Directory.Exists(_storagePath))
        {
            throw new Exception("Folder Not Found");
        }

        var zipPath = path + ".zip";

        ZipFile.CreateFromDirectory(path, zipPath);

        return new FileStream(zipPath, FileMode.Open, FileAccess.Read);

    }

    public async Task<List<string>> GetAllInFolderPathAsync(string? folderPath)
    {
        folderPath = folderPath ?? string.Empty;
        var path = Path.Combine(_storagePath, folderPath);
        var info = Directory.EnumerateFileSystemEntries(path);
        return info.Select(inf => inf.Remove(0, Directory.GetParent(inf).FullName.Length + 1)).ToList();
    }

    public async Task<string> GetContentOfTxtFileAsync(string filePath)
    {
        var path = Path.Combine(_storagePath, filePath);
        var textInFile = string.Empty;
        if (!File.Exists(path))
        {
            throw new Exception("FIle Not Found");
        }
        using (var sr = new StreamReader(path))
        {
            textInFile = sr.ReadToEnd();
        }
        return textInFile;
    }

    public async Task UpdateContentOfTxtFileAsync(string filePath, string newContent)
    {
        var path = Path.Combine(_storagePath, filePath);
        if (!File.Exists(path))
        {
            throw new Exception("File Not Found");
        }
        using (var sw = new StreamWriter(path, true))
        {
            sw.Write(newContent);
        }
    }

    public async Task UploadFileAsync(string folderPath, Stream stream)
    {
        var path = Path.Combine(_storagePath, folderPath);
        if (File.Exists(path))
        {
            throw new Exception("File Already Exists");
        }

        using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            stream.CopyTo(fileStream);
        }

    }

    public async Task UploadFileWithChunksAsync(string? folderPath, Stream stream)
    {
        folderPath = folderPath ?? string.Empty;
        var path = Path.Combine(_storagePath, folderPath);
        if (File.Exists(path))
        {
            throw new Exception("File Already Exists");
        }
        var buffer = new byte[1024 * 1024 * 100];

        using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            while (true)
            {
                var readed = stream.Read(buffer, 0, buffer.Length);
                if (readed <= 0)
                {
                    break;
                }
                fileStream.Write(buffer, 0, readed);
            }
        }
    }

    public async Task UploafFilesAsync(List<Stream> streams, string? folderPath)
    {
        folderPath = folderPath ?? string.Empty;
        var path = Path.Combine(_storagePath, folderPath);
        foreach (var stream in streams)
        {
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }
    }
}
