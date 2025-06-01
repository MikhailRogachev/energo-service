using energo.customer.api.Dto;
using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace energo.customer.api.Validators;

public class CustomerValidator : AbstractValidator<CustomerDto>
{
    private readonly int _minLength = 3;
    private readonly int _maxLength = 40;
    protected override bool PreValidate(ValidationContext<CustomerDto> context, ValidationResult result)
    {
        if (context.InstanceToValidate == null)
        {
            result.Errors.Add(new ValidationFailure("", $"The customer is not found.")
            {
                CustomState = HttpStatusCode.NotFound
            });

            return false;
        }

        return base.PreValidate(context, result);
    }

    public CustomerValidator()
    {
        RuleFor(p => p.CustomerName)
            .NotEmpty()
            .WithMessage(p => "The CustomerName can't be empty.")
            .WithState(p => HttpStatusCode.BadRequest);

        RuleFor(p => p.CustomerName)
            .Length(_minLength, _maxLength)
            .WithMessage(p => $"The CustomerName lenght must have from {_maxLength} to {_maxLength} simbols.")
            .WithState(p => HttpStatusCode.BadRequest);
    }
}
