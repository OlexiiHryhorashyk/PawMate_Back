using Amazon.S3;
using Amazon.S3.Model;
using CSharpFunctionalExtensions;

namespace PawMate.Image.API.Infastructure.Storage;

public interface IStorage
{
    Task<Result<byte[]>> Download(string path, CancellationToken cancellationToken);

    Task<Result<string>> Upload(IFormFile file, string path, CancellationToken cancellationToken);

    Task<Result> Delete(string path, CancellationToken cancellationToken);

    Task<Result<S3Object>> FileMetadata(string path, CancellationToken cancellationToken);
}

public class Storage : IStorage
{
    private readonly IAmazonS3 _client;
    private readonly string _bucketName;

    public Storage(IAmazonS3 client, string bucketName)
    {
        _client = client;
        _bucketName = bucketName;
    }

    public async Task<Result> Delete(string path, CancellationToken cancellationToken)
    {
        try
        {
            DeleteObjectRequest request = new()
            {
                BucketName = _bucketName,
                Key = path
            };
            DeleteObjectResponse response = await _client.DeleteObjectAsync(request, cancellationToken);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return Result.Success();
            }
            else
            {
                return Result.Failure($"Failed to delete file. Status code: {response.HttpStatusCode}");
            }           
        }
        catch (Exception ex)
        {
            return Result.Failure<string>(ex.Message);
        }

    }


    public async Task<Result<byte[]>> Download(string path, CancellationToken cancellationToken)
    {       
        try
        {
            GetObjectRequest request = new()
            {
                BucketName = _bucketName,
                Key = path
            };
            using (GetObjectResponse response = await _client.GetObjectAsync(request, cancellationToken))
            {
                if(response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    MemoryStream memoryStream = new();
                    await response.ResponseStream.CopyToAsync(memoryStream);
                    return Result.Success<byte[]>(memoryStream.ToArray());
                }
                else
                {
                    return Result.Failure<byte[]>($"Failed to download file. Status code: {response.HttpStatusCode}");
                }
            }               
        }
        catch(Exception ex)
        {
            return Result.Failure<byte[]>(ex.Message);
        }
    }

    public async Task<Result<S3Object>> FileMetadata(string path, CancellationToken cancellationToken)
    {
        try
        {
            ListObjectsRequest request = new()
            {
                BucketName = _bucketName,
                Prefix = path,
                MaxKeys = 1,                
            };

            ListObjectsResponse responce = await _client.ListObjectsAsync(request, cancellationToken);

            if(responce.HttpStatusCode == System.Net.HttpStatusCode.OK && responce.S3Objects.Count != 0)
            {
                S3Object fileMeatadata = responce.S3Objects.First();

                return Result.Success<S3Object>(fileMeatadata);
            }
            else
            {
                return Result.Failure<S3Object>("Cannot find such file");
            }
        }
        catch(Exception ex)
        {
            return Result.Failure<S3Object>(ex.Message);
        }
    }

    public async Task<Result<string>> Upload(IFormFile file, string path, CancellationToken cancellationToken)
    {
        try
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                PutObjectRequest request = new()
                {
                    InputStream = memoryStream,
                    BucketName = _bucketName,
                    Key = path,
                    ContentType = file.ContentType
                };

                PutObjectResponse response = await _client.PutObjectAsync(request, cancellationToken);

                if(response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Result.Success<string>($"https://{_bucketName}.s3.{_client.Config.RegionEndpoint.SystemName}.amazonaws.com/{path}");
                }
                else
                {
                    return Result.Failure<string>($"Failed to upload file. Status code: {response.HttpStatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            return Result.Failure<string>(ex.Message);
        }
    }
}
