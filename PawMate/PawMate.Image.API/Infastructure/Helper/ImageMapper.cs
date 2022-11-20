using PawMate.Image.API.Domain;
using PawMate.Image.API.Models;

namespace PawMate.Image.API.Infastructure.Helper
{
    public static class ImageMapper
    {
        public static ImageEntity Map(UploadImageModel model)
        {
            return new ImageEntity()
            {
                Path = S3UrlHelper.GetPath(model.Id, model.UserName, model.Image.FileName),
                ImageName = model.Image.FileName,
                ImageFile = model.Image
            };
        }

        public static ImageEntity Map(UpdateImageModel model)
        {
            return new ImageEntity()
            {
                Path = S3UrlHelper.GetPathFromUrl(model.OldImageUrl),
                ImageName = S3UrlHelper.GetFileNameFromUrl(model.OldImageUrl),
                ImageFile = model.NewImage
            };
        }
    }
}
