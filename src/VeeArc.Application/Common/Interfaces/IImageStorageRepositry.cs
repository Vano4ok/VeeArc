using Microsoft.AspNetCore.Http;

namespace VeeArc.Application.Common.Interfaces;

public interface IImageStorageRepositry
{
    Task<string> UploadImage(IFormFile image, CancellationToken cancellationToken);
}
