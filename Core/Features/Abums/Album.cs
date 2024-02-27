namespace Core.Features.Abums
{
    public class Album
    {
        /// <summary>
        /// Id generated at the application layer. This is the id that's presented to the client.
        /// At the DB level this is a candidate key.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The album title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Artist Name
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// The album type/format
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// The album stock
        /// </summary>
        public int Stock {  get; set; }

        /// <summary>
        /// Base64 string of the cover image
        /// </summary>
        public string Cover { get; set; }
    }
}
