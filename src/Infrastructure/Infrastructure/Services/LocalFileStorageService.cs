using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mriguel.Application.Common.Interfaces;

namespace Mriguel.Infrastructure.Services
{
    /// <summary>
    /// Local file storage implementation for development purposes
    /// </summary>
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly ILogger<LocalFileStorageService> _logger;
        private readonly string _storageBasePath;

        public LocalFileStorageService(ILogger<LocalFileStorageService> logger)
        {
            _logger = logger;
            _storageBasePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(_storageBasePath))
            {
                Directory.CreateDirectory(_storageBasePath);
            }
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            try
            {
                string filePath = Path.Combine(_storageBasePath, fileName);
                using (var fileWriter = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(fileWriter);
                }
                return $"/uploads/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file {FileName}", fileName);
                throw;
            }
        }

        public Task DeleteFileAsync(string fileUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(fileUrl))
                    return Task.CompletedTask;
                
                string fileName = Path.GetFileName(fileUrl);
                string filePath = Path.Combine(_storageBasePath, fileName);
                
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file {FileUrl}", fileUrl);
                return Task.CompletedTask;
            }
        }

        public string GetFileUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;
            
            return $"/uploads/{fileName}";
        }
    }
}
