using Core;
using Core.Features.Abums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class AlbumRepositoryEF : DbContext, IAlbumRepository
    {
        public AlbumRepositoryEF(DbContextOptions<AlbumRepositoryEF> options)
            : base(options)
        {
            
        }

        public Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Album> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Save(Album album, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Page<Album>> Search(int pageNumber, int pageSize, string title, string artistName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Update(Album album, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
