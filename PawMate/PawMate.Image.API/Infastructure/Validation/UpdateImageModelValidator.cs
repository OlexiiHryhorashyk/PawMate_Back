using FluentValidation;
using PawMate.Image.API.Models;

namespace PawMate.Image.API.Infastructure.Validation
{
    public class UpdateImageModelValidator : AbstractValidator<UpdateImageModel>
    {
        public UpdateImageModelValidator()
        {
            RuleFor(x => x.OldImageUrl).NotNull().NotEmpty();

            RuleFor(x => x.NewImage).NotNull();
        }
    }
}
