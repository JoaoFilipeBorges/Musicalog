using MediatR;

namespace Core.Features.Abums.DeleteAlbum
{
    public class DeleteAlbumCommand : IRequest
    {

        public Guid Id { get; set; }
        public DeleteAlbumCommand(Guid id)
        {
            this.Id = id;
        }
    }
}
