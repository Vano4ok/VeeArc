using MediatR;

namespace VeeArc.Application.Feature.TestModels.Create;

public record CreateTestModelCommand : IRequest<int>
{
    public required string Title { get; init; }

    public required string Text { get; init; }
}

public class CreateTestModelCommandHandler : IRequestHandler<CreateTestModelCommand, int>
{
    public Task<int> Handle(CreateTestModelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
