using MediatR;

namespace Core.Features.Abums.CreateAlbum
{
    public class CreateAlbumCommand : IRequest<Guid>
    {
        public string Title { get; }
        public string ArtistName { get; }
        public Type Type { get; }
        public int Stock { get; }

        public string Cover { get; set; }

        public CreateAlbumCommand(string title, string artistName, Type type, int stock, string cover)
        {;
            this.Title = title;
            this.ArtistName = artistName;
            this.Type = type;
            this.Stock = stock;
            this.Cover = cover;
        }

        
    }
}
