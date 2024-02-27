namespace Core
{
    public class Page<T>
    {
        /// <summary>
        /// Number of the retrieved page.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Number of pages that the action can request.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Total number of items in all the pages of the response.
        /// </summary>
        public int TotalItems { get; }

        /// <summary>
        /// Entries in the page of the response. By default, pages have 60 items.
        /// </summary>
        public IEnumerable<T> Entries { get; }

        public Page(int number, int totalItems, int totalPages, IEnumerable<T> entries)
        {
            Number = number;
            TotalItems = totalItems;
            Entries = entries;
            TotalPages = totalPages;
        }
    }
}
