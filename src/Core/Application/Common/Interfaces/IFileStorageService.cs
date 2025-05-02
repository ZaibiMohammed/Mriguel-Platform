namespace AlloVoisinClone.Application.Common.Interfaces
{
    /// <summary>
    /// Interface for file storage service
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// Uploads a file
        /// </summary>
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
        
        /// <summary>
        /// Deletes a file
        /// </summary>
        Task DeleteFileAsync(string fileUrl);
        
        /// <summary>
        /// Gets a file's URL
        /// </summary>
        string GetFileUrl(string fileName);
    }
}
