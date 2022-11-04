using CSharpFunctionalExtensions;
using MediatR;
using PawMate.Image.API.Infastructure.Repository;

namespace PawMate.Image.API.Commands
{
    public class DeleteImageCommand : IRequest<Result>
    {
        public string Url { get; init; }

        public DeleteImageCommand(string url)
        {
            Url = url;
        }
    }

    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, Result>
    {
        private readonly IImageRepository _imageRepository;

        public DeleteImageCommandHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Result> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            return await _imageRepository.DeleteImage(request.Url, cancellationToken);
        }
    }
}
