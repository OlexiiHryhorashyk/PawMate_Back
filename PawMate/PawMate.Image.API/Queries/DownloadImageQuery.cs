using Amazon.S3.Model;
using CSharpFunctionalExtensions;
using MediatR;
using PawMate.Image.API.Infastructure.Helper;
using PawMate.Image.API.Infastructure.Repository;
using PawMate.Image.API.Models;

namespace PawMate.Image.API.Queries
{
    public class DownloadImageQuery : IRequest<Result<DownloadImageModel>>
    {
        public string Url { get; init; }

        public DownloadImageQuery(string url)
        {
            Url = url;
        }
    }

    public class DownloadImageQueryHandler : IRequestHandler<DownloadImageQuery, Result<DownloadImageModel>>
    {
        private readonly IImageRepository _imageRepository;

        public DownloadImageQueryHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Result<DownloadImageModel>> Handle(DownloadImageQuery request, CancellationToken cancellationToken)
        {
            Result<byte[]> imageData = await _imageRepository.GetImage(request.Url, cancellationToken);

            if(imageData.IsFailure)
            {
                return Result.Failure<DownloadImageModel>(imageData.Error);
            }

            Result<S3Object> imageMetadata = await _imageRepository.GetImageMetadate(request.Url, cancellationToken);

            if(imageMetadata.IsFailure)
            {
                return Result.Failure<DownloadImageModel>(imageMetadata.Error);
            }

            DownloadImageModel result = new()
            {
                ImageName = S3UrlHelper.GetFileNameFromUrl(imageMetadata.Value.Key),
                ImageData = imageData.Value
            };

            return Result.Success<DownloadImageModel>(result);
        }
    }
}
