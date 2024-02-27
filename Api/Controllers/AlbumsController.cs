using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Features.Abums.GetAllAlbums;
using Core.Features.Abums;
using Microsoft.AspNetCore.JsonPatch;
using Core.Features.Abums.CreateAlbum;
using Core.Features.Abums.DeleteAlbum;
using Core.Features.Abums.UpdateAlbum;
using Core.Exceptions;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AlbumsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums(
            [FromQuery] int? pageNumber = default, 
            [FromQuery] int? pageSize = default, 
            [FromQuery] string title = default, 
            [FromQuery] string artistName = default,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var getAllAlbumsQuery = new GetAllAlbumsQuery(pageNumber, pageSize, title, artistName);
                var albums = await mediator.Send(getAllAlbumsQuery, cancellationToken);

                return this.Ok(albums);
            }
            catch (ValidationException ex)
            {
                return this.BadRequest(ex.ValidationErrors);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> CreateAlbum([FromBody] CreateAlbumCommand album, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await mediator.Send(album, cancellationToken);

                //This should return a location header with the location for the newly created resource.
                return this.Ok();
            }
            catch (ValidationException ex)
            {
                return this.BadRequest(ex.ValidationErrors);
            }
            catch (ResourceAlreadyExistsException ex)
            {
                return this.Conflict(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAlbum(
            [FromRoute] Guid id,
            [FromBody] JsonPatchDocument<Album> patchDocument,
            CancellationToken cancellation = default)
        {
            try
            {
                if (Guid.Empty == id)
                {
                    return this.BadRequest("Must be a valid uuid");
                }

                var command = new UpdateAlbumCommand(id, patchDocument);
                await mediator.Send(command, cancellation);
            }
            catch (ResourceNotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch(ResourceAlreadyExistsException ex)
            {
                return this.Conflict(ex.Message);
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            if(Guid.Empty == id)
            {
                return this.BadRequest("Must be a valid uuid");
            }

            await mediator.Send(new DeleteAlbumCommand(id), cancellationToken);

            return this.NoContent();
        }
    }
}
