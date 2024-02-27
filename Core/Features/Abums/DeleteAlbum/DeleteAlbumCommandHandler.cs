using MediatR;

namespace Core.Features.Abums.DeleteAlbum
{
    public class DeleteAlbumCommandHandler : IRequestHandler<DeleteAlbumCommand>
    {
        private readonly IAlbumRepository repository;

        public DeleteAlbumCommandHandler(IAlbumRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
        {
            await this.repository.Delete(request.Id, cancellationToken);
        }
    }
}
