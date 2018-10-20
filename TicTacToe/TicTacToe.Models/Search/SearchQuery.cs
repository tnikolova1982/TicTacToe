namespace TicTacToe.Models.Search
{
    using TicTacToe.Infrastructure.Filters.CustomAttributes;

    public class SearchQuery
    {
        [Query(IsVisibleInView = false)]
        public int Limit { get; set; }

        [Query(IsVisibleInView = false)]
        public int Offset { get; set; }

        [Query(IsVisibleInView = false)]
        public string Sort { get; set; }

        [Query(IsVisibleInView = false)]
        public string SortDir { get; set; }
    }
}
