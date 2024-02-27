using Core.Exceptions;
using Core.Features.Abums.CreateAlbum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Abums.UpdateAlbum
{
    public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand>
    {
        private readonly IAlbumRepository albumRepository;

        public UpdateAlbumCommandHandler(IAlbumRepository albumRepository)
        {
            this.albumRepository = albumRepository;
        }
        public async Task Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = await this.albumRepository.GetById(request.Id, cancellationToken);
            if(album == null)
            {
                throw new ResourceNotFoundException($"Could not find album with id: {request.Id}");
            }

            request.Document.ApplyTo(album);

            await this.albumRepository.Update(album, cancellationToken);
        }
    }
}
