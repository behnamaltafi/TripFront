public interface IFileStorageService
{
    /// <summary>
    /// Saves a file to the storage system
    /// </summary>
    /// <param name="file">The file to save</param>
    /// <param name="subDirectory">Optional subdirectory within the storage</param>
    /// <returns>Relative URL path to access the file</returns>
    Task<string> SaveFileAsync(IFormFile file, string subDirectory = null);

    /// <summary>
    /// Deletes a file from storage
    /// </summary>
    /// <param name="filePath">Relative file path to delete</param>
    Task DeleteFileAsync(string filePath);

    /// <summary>
    /// Retrieves a file stream
    /// </summary>
    /// <param name="filePath">Relative file path</param>
    /// <returns>File stream</returns>
    Task<Stream> GetFileAsync(string filePath);
}