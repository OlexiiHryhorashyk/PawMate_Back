using CSharpFunctionalExtensions;
using MediatR;
using PawMate.Image.API.Domain;
using PawMate.Image.API.Infastructure.Helper;
using PawMate.Image.API.Infastructure.Repository;
using PawMate.Image.API.Models;

namespace PawMate.Image.API.Commands
{
    public class UploadImageCommand : IRequest<Result<string>>
    {
        public UploadImageModel Model { get; init; }

        public UploadImageCommand(UploadImageModel model)
        {
            Model = model;
        }
    }

    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Result<string>>
    {
        private readonly IImageRepository _imageRepository;

        public UploadImageCommandHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Result<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            ImageEntity imageEntity = ImageMapper.Map(request.Model);
            return await _imageRepository.AddImage(imageEntity, cancellationToken);
        }
    }
}
