namespace TicTacToe.Domain.Interfaces
{
    using System.Collections.Generic;

    public interface ISearchable<in TQuery, out TTable>
    {
        IEnumerable<TTable> Search(TQuery model);

        int CountResults(TQuery model);
    }
}
