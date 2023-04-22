using MediatR;
using Microsoft.AspNetCore.Http;
using VeeArc.Application.Common.Interfaces;

namespace VeeArc.Application.Feature.Images.Create;

public record CreateImageCommand : IRequest<string>
{
    public required IFormFile Image { get; init; }
}

public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, string>
{
    private readonly IImageStorageRepositry _imageStorageRepositry;

    public CreateImageCommandHandler(IImageStorageRepositry imageStorageRepositry)
    {
        _imageStorageRepositry = imageStorageRepositry;
    }

    public async Task<string> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        string url = await _imageStorageRepositry.UploadImage(request.Image, cancellationToken);

        return url;
    }
}
