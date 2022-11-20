using FluentValidation;
using PawMate.Image.API.Models;

namespace PawMate.Image.API.Infastructure.Validation
{
    public class UploadImageModelValidator : AbstractValidator<UploadImageModel>
    {
        public UploadImageModelValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.UserName).NotNull().NotEmpty();

            RuleFor(x => x.Image).NotNull();
        }
    }
}
