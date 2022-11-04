using CSharpFunctionalExtensions;
using MediatR;
using PawMate.Image.API.Domain;
using PawMate.Image.API.Infastructure.Helper;
using PawMate.Image.API.Infastructure.Repository;
using PawMate.Image.API.Models;

namespace PawMate.Image.API.Commands
{
    public class UpdateImageCommand : IRequest<Result<string>>
    {
        public UpdateImageModel Model { get; init; }

        public UpdateImageCommand(UpdateImageModel model)
        {
            Model = model;
        }
    }

    public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand, Result<string>>
    {
        private readonly IImageRepository _imageRepository;

        public UpdateImageCommandHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Result<string>> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {
            ImageEntity imageEntity = ImageMapper.Map(request.Model);
            return await _imageRepository.UpdateImage(imageEntity, cancellationToken);
        }
    }
}
