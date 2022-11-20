using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PawMate.Image.API.Commands;
using PawMate.Image.API.Models;
using PawMate.Image.API.Queries;

namespace PawMate.Image.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Download")]
        public async Task<IActionResult> DownloadImage(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("Image url cannot be not null of whitespace");
            }

            Result<DownloadImageModel> downloading = await _mediator.Send(new DownloadImageQuery(url));

            if (downloading.IsFailure)
            {
                return Problem(detail: downloading.Error, statusCode: StatusCodes.Status500InternalServerError);
            }

            return File(downloading.Value.ImageData, "application/octet-stream", downloading.Value.ImageName);
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageModel request)
        {
            Result<string> uploading = await _mediator.Send(new UploadImageCommand(request));

            if(uploading.IsFailure)
            {
                return Problem(detail: uploading.Error, statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok(uploading.Value);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateImage([FromForm] UpdateImageModel request)
        {
            Result<string> updating = await _mediator.Send(new UpdateImageCommand(request));

            if(updating.IsFailure)
            {
                return Problem(detail: updating.Error, statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok(updating.Value);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteImage(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("Image url cannot be not null of whitespace");
            }

            Result deleting = await _mediator.Send(new DeleteImageCommand(url));

            if(deleting.IsFailure)
            {
                return Problem(detail: deleting.Error, statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
