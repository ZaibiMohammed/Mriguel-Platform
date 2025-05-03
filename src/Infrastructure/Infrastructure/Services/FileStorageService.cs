using Amazon.S3;
using Amazon.S3.Model;
using Mriguel.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Mriguel.Infrastructure.Services
{
    /// <summary>
    /// Service for storing files on Amazon S3
    /// </summary>
    public class FileStorageService : IFileStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private readonly string _cdnUrl;

        public FileStorageService(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:S3:BucketName"] ?? throw new ArgumentNullException("AWS:S3:BucketName");
            _cdnUrl = configuration["AWS:CloudFront:Url"] ?? configuration["AWS:S3:BucketUrl"] ?? throw new ArgumentNullException("AWS:S3:BucketUrl");
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            // Generate a unique file name to avoid conflicts
            var uniqueFileName = $"{Guid.NewGuid()}-{fileName}";
            
            // Upload the file to S3
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = uniqueFileName,
                InputStream = fileStream,
                ContentType = contentType,
                CannedACL = S3CannedACL.PublicRead
            };
            
            await _s3Client.PutObjectAsync(putRequest);
            
            // Return the URL to the uploaded file
            return GetFileUrl(uniqueFileName);
        }

        public async Task DeleteFileAsync(string fileUrl)
        {
            // Extract the file name from the URL
            var fileName = Path.GetFileName(new Uri(fileUrl).AbsolutePath);
            
            // Delete the file from S3
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };
            
            await _s3Client.DeleteObjectAsync(deleteRequest);
        }

        public string GetFileUrl(string fileName)
        {
            return $"{_cdnUrl.TrimEnd('/')}/{fileName}";
        }
    }
}
