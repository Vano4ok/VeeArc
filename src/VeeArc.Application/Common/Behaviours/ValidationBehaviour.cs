using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = VeeArc.Application.Common.Exceptions.ValidationException;

namespace VeeArc.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        ValidationResult[] validationResults = await GetValidationResults(context, cancellationToken);
        List<ValidationFailure> failures = GetValidationFailures(validationResults);

        ThrowIfValidationFailuresExist(failures);

        return await next();
    }

    private async Task<ValidationResult[]> GetValidationResults(ValidationContext<TRequest> context, CancellationToken cancellationToken)
    {
        ValidationResult[] validationResults = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        return validationResults;
    }

    private static List<ValidationFailure> GetValidationFailures(ValidationResult[] validationResults)
    {
        List<ValidationFailure> failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        return failures;
    }

    private static void ThrowIfValidationFailuresExist(List<ValidationFailure> failures)
    {
        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
    }
}