using MediatR;


namespace VeeArc.Application.Feature.TestModels.Update;

public record UpdateTestModelCommand : IRequest<int>
{
    public required int Id { get; init; }

    public required string Title { get; init; }

    public string? Text { get; init; }

    public int? PageIndex { get; init; }
}

public class UpdateTestModelCommandHandler : IRequestHandler<UpdateTestModelCommand, int>
{
    public Task<int> Handle(UpdateTestModelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
