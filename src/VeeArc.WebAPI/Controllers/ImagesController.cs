using Microsoft.AspNetCore.Mvc;
using VeeArc.Application.Feature.Images.Create;

namespace VeeArc.WebAPI.Controllers
{
    [Route("images")]
    public class ImagesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(IFormFile image)
        {
            var command = new CreateImageCommand()
            {
                Image = image,
            };

            string imageUrl = await Mediator.Send(command);

            return Ok(imageUrl);
        }
    }
}
