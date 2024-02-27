using MediatR;

namespace Core.Features.Abums.GetAllAlbums
{
    public class GetAllAlbumsQuery : IRequest<Page<Album>>
    {
        public int Page { get; }

        public int PageSize { get; }

        public string Title { get; }

        public string ArtistName { get; }


        public GetAllAlbumsQuery(int? page, int? pageSize, string title, string artistName)
        {
            this.Page = page ?? Constants.DefaultPagination.Page;
            this.PageSize = pageSize ?? Constants.DefaultPagination.PageSize;
            this.Title = title;
            this.ArtistName = artistName;
        }
    }
}
