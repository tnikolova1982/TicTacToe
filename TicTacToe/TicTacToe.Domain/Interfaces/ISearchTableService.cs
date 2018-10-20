namespace TicTacToe.Domain.Interfaces
{
    using System.Collections.Generic;
    using TicTacToe.Models.Search;

    public interface ISearchTableService
    {
        IEnumerable<TTable> Search<TQuery, TTable>(TQuery model, ISearchable<TQuery, TTable> searchable)
            where TQuery : SearchQuery;
    }
}
