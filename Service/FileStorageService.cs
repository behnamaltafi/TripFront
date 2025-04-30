
public class FileStorageService : IFileStorageService
{
    private readonly ILogger<FileStorageService> _logger;
    private readonly string _storagePath;

    public FileStorageService(ILogger<FileStorageService> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _storagePath = Path.Combine(env.ContentRootPath, "uploads");

        // Ensure the upload directory exists
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<string> SaveFileAsync(IFormFile file, string subDirectory)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is empty or null");
        }

        // Validate file size (e.g., 10MB max)
        if (file.Length > 10 * 1024 * 1024)
        {
            throw new ArgumentException("File size exceeds 10MB limit");
        }

        // Create safe file name
        var originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
        var fileExtension = Path.GetExtension(file.FileName);
        var safeFileName = $"{Guid.NewGuid()}{fileExtension}";

        // Create subdirectory if specified
        var uploadPath = string.IsNullOrEmpty(subDirectory)
            ? _storagePath
            : Path.Combine(_storagePath, subDirectory);

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var filePath = Path.Combine(uploadPath, safeFileName);

        try
        {
            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _logger.LogInformation($"File saved successfully: {filePath}");

            // Return relative path for web access
            return string.IsNullOrEmpty(subDirectory)
                ? $"/uploads/{safeFileName}"
                : $"/uploads/{subDirectory}/{safeFileName}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error saving file {originalFileName}");
            throw new Exception("Error saving file", ex);
        }
    }

    public async Task DeleteFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return;
        }

        try
        {
            var physicalPath = Path.Combine(_storagePath, filePath.TrimStart('/'));
            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
                _logger.LogInformation($"File deleted: {physicalPath}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting file {filePath}");
            throw new Exception("Error deleting file", ex);
        }
    }

    public async Task<Stream> GetFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path is required");
        }

        var physicalPath = Path.Combine(_storagePath, filePath.TrimStart('/'));

        if (!File.Exists(physicalPath))
        {
            throw new FileNotFoundException("File not found", physicalPath);
        }

        return new FileStream(physicalPath, FileMode.Open, FileAccess.Read);
    }
}