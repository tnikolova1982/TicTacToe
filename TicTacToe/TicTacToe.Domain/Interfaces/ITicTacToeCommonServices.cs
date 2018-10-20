namespace TicTacToe.Domain.Interfaces
{
    using System.Collections.Generic;

    public interface ITicTacToeCommonServices
    {
        List<List<int>> GetLines();
    }
}
