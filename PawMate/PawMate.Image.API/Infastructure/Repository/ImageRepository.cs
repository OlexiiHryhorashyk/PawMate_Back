using Amazon.S3.Model;
using CSharpFunctionalExtensions;
using PawMate.Image.API.Domain;
using PawMate.Image.API.Infastructure.Helper;
using PawMate.Image.API.Infastructure.Storage;

namespace PawMate.Image.API.Infastructure.Repository
{
    public interface IImageRepository
    {
        Task<Result<byte[]>> GetImage(string url, CancellationToken cancellationToken);

        Task<Result<string>> AddImage(ImageEntity image, CancellationToken cancellation);

        Task<Result<string>> UpdateImage(ImageEntity image, CancellationToken cancellationToken);

        Task<Result> DeleteImage(string url, CancellationToken cancellation);

        Task<Result<S3Object>> GetImageMetadate(string url, CancellationToken cancellationToken);
    }

    public class ImageRepository : IImageRepository
    {
        private readonly IStorage _storage;

        public ImageRepository(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<Result<string>> AddImage(ImageEntity image, CancellationToken cancellation)
        {
            if(await IsImageExist(image.Path, cancellation))
            {
                return Result.Failure<string>("Image with same name already exist");
            }

            return await _storage.Upload(image.ImageFile, image.Path, cancellation);
        }

        public async Task<Result> DeleteImage(string url, CancellationToken cancellation)
        {
            string path = S3UrlHelper.GetPathFromUrl(url);
            if (!(await IsImageExist(path, cancellation)))
            {
                return Result.Failure<string>("Image does not exist");
            }

            return await _storage.Delete(path, cancellation);
        }

        public async Task<Result<byte[]>> GetImage(string url, CancellationToken cancellationToken)
        {
            string path = S3UrlHelper.GetPathFromUrl(url);
            if (!(await IsImageExist(path, cancellationToken)))
            {
                return Result.Failure<byte[]>("Image does not exist");
            }

            return await _storage.Download(path, cancellationToken);
        }

        public async Task<Result<S3Object>> GetImageMetadate(string url, CancellationToken cancellationToken)
        {
            string path = S3UrlHelper.GetPathFromUrl(url);
            return await _storage.FileMetadata(path, cancellationToken);
        }

        public async Task<Result<string>> UpdateImage(ImageEntity image, CancellationToken cancellationToken)
        {
            if (!(await IsImageExist(image.Path, cancellationToken)))
            {
                return Result.Failure<string>("Image does not exist");
            }

            Result deleteResult = await _storage.Delete(image.Path, cancellationToken);

            if (deleteResult.IsSuccess)
            {
                return await _storage.Upload(image.ImageFile, image.Path, cancellationToken);
            }
            else
            {
                return Result.Failure<string>($"Failed to update image. {deleteResult.Error}");
            }
        }

        private async Task<bool> IsImageExist(string path, CancellationToken cancellation)
        {
            Result<S3Object> result = await _storage.FileMetadata(path, cancellation);

            if (result.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
