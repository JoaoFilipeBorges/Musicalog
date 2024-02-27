using Core.Exceptions;
using MediatR;

namespace Core.Features.Abums.GetAllAlbums
{
    public class GetAllAlbumsQueryHandler : IRequestHandler<GetAllAlbumsQuery, Page<Album>>
    {
        private readonly IAlbumRepository albumRepository;

        public GetAllAlbumsQueryHandler(IAlbumRepository albumRepository)
        {
            this.albumRepository = albumRepository;
        }

        public async Task<Page<Album>> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
        {
            var validationErrors = request.Validate();
            if (validationErrors.Any())
            {
                throw new ValidationException("", validationErrors);
            }

            return await this.albumRepository.Search(request.Page, request.PageSize, request.Title, request.ArtistName, cancellationToken);
        }
    }
}
