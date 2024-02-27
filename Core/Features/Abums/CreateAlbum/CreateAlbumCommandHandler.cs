using Core.Exceptions;
using MediatR;

namespace Core.Features.Abums.CreateAlbum
{
    public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, Guid>
    {
        private readonly IAlbumRepository albumRepository;

        public CreateAlbumCommandHandler(IAlbumRepository albumRepository)
        {
            this.albumRepository = albumRepository;
        }

        public async Task<Guid> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var validationErrors = request.Validate();
            if (validationErrors.Any())
            {
                throw new ValidationException("", validationErrors);
            }

            var album = new Album
            {
                Id = Guid.NewGuid(),
                ArtistName = request.ArtistName,
                Title = request.Title,
                Type = request.Type,
                Stock = request.Stock,
                Cover = request.Cover,
            };

            await albumRepository.Save(album);

            return album.Id;
            

        }

    }
}
