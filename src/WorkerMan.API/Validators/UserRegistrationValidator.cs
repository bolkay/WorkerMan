using FluentValidation;
using WorkerMan.Contracts.DTOs;

namespace WorkerMan.API.Validators
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationDTO>
    {
        public UserRegistrationValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Provide your first name.");

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Provide your last name.");

            RuleFor(x => x.Email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .NotNull()
                .WithMessage("Please provide your email address.");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please provide your password.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Must be the same as password.");

        }
    }
}
