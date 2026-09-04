using FluentValidation;

namespace Ordering.Application.Validators;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(o => o.Id)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        RuleFor(o => o.UserName)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters");
        RuleFor(o => o.TotalPrice)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be positive");
        RuleFor(o => o.EmailAddress)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .EmailAddress().WithMessage("{PropoertyName} must be a valid email address");
        RuleFor(o => o.FirstName)
            .NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(o => o.LastName)
            .NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(o => o.CardNumber)
            .CreditCard().When(o => !string.IsNullOrEmpty(o.CardNumber))
            .WithMessage("{PropertyName} must be a valid credit card number");
        RuleFor(o => o.Expiration)
            .Matches(@"^(0[1-9][1[0.2])\/?([0-9]{2})$")
            .When(o => !string.IsNullOrEmpty(o.Expiration))
            .WithMessage("{PropertyName} must be in MM/YY format");
        RuleFor(o => o.Expiration)
            .Matches(@"^\d{3,4}$")
            .When(o => !string.IsNullOrEmpty(o.Cvv))
            .WithMessage("{PropertyName} must be 3 or 4 digits");
    }
}
