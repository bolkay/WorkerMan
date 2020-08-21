using FluentValidation;
using WorkerMan.API.Configuration;
using WorkerMan.Contracts.DTOs;

namespace WorkerMan.API.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginDTO>
    {
        private readonly GlobalWorkerManOptions globalWorkerManOptions;

        public UserLoginValidator(GlobalWorkerManOptions globalWorkerManOptions)
        {
            RuleFor(x => x.Email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .NotNull()
                .NotEmpty()
                .WithMessage("You must supply a valid email address.");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(globalWorkerManOptions.PasswordLength)
                .WithMessage("You must provide a valid password");

            this.globalWorkerManOptions = globalWorkerManOptions;
        }
    }
}
