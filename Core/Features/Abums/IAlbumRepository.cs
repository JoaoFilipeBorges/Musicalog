namespace Core.Features.Abums
{
    public interface IAlbumRepository
    {
        Task<Page<Album>> Search(int pageNumber, int pageSize, string title, string artistName, CancellationToken cancellationToken = default);
        
        Task Save(Album album, CancellationToken cancellationToken = default);

        Task Update(Album album, CancellationToken cancellationToken = default);

        Task Delete(Guid id, CancellationToken cancellationToken = default);

        Task<Album> GetById(Guid id, CancellationToken cancellationToken = default);

    }
}
