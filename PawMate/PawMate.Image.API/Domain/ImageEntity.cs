namespace PawMate.Image.API.Domain
{
    public class ImageEntity
    {
        public string Path { get; init; }

        public string ImageName { get; init; }

        public IFormFile ImageFile { get; init; }
    }
}
