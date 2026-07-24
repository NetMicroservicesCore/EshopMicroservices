using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using NSubstitute;

namespace Catalog.API.Tests;

public class ValidationBehaviorTests
{
    [Fact]
    public async Task Handle_WithNoValidators_ShouldCallNext()
    {
        var validators = Enumerable.Empty<IValidator<IRequest<Unit>>>();
        var behavior = new ValidationBehavior<IRequest<Unit>, Unit>(validators);
        var next = Substitute.For<RequestHandlerDelegate<Unit>>();
        next().Returns(Unit.Value);

        var result = await behavior.Handle(Substitute.For<IRequest<Unit>>(), next, CancellationToken.None);

        Assert.Equal(Unit.Value, result);
        await next.Received(1)();
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldCallNext()
    {
        var validator = Substitute.For<IValidator<TestCommand>>();
        validator.ValidateAsync(Arg.Any<ValidationContext<TestCommand>>(), Arg.Any<CancellationToken>())
            .Returns(new FluentValidation.Results.ValidationResult());

        var validators = new[] { validator };
        var behavior = new ValidationBehavior<TestCommand, Unit>(validators);
        var next = Substitute.For<RequestHandlerDelegate<Unit>>();
        next().Returns(Unit.Value);

        var result = await behavior.Handle(new TestCommand(), next, CancellationToken.None);

        Assert.Equal(Unit.Value, result);
        await next.Received(1)();
    }

    [Fact]
    public async Task Handle_WithInvalidData_ShouldThrowValidationException()
    {
        var validator = Substitute.For<IValidator<TestCommand>>();
        var failures = new List<FluentValidation.Results.ValidationFailure>
        {
            new("Name", "Name is required")
        };
        validator.ValidateAsync(Arg.Any<ValidationContext<TestCommand>>(), Arg.Any<CancellationToken>())
            .Returns(new FluentValidation.Results.ValidationResult(failures));

        var validators = new[] { validator };
        var behavior = new ValidationBehavior<TestCommand, Unit>(validators);

        await Assert.ThrowsAsync<ValidationException>(() =>
            behavior.Handle(new TestCommand(), Substitute.For<RequestHandlerDelegate<Unit>>(), CancellationToken.None));
    }

    public record TestCommand : IRequest<Unit>;
}
