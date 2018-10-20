namespace TicTacToe.Services.Services
{
    using System.Collections.Generic;
    using TicTacToe.Domain.Interfaces;

    public class TicTacToeCommonServices : ITicTacToeCommonServices
    {
        public List<List<int>> GetLines()
        {
            return new List<List<int>>
            {
               new List<int> {0, 1, 2},
               new List<int> {3, 4, 5},
               new List<int> {6, 7, 8},
               new List<int> {0, 3, 6},
               new List<int> {1, 4, 7},
               new List<int> {2, 5, 8},
               new List<int> {0, 4, 8},
               new List<int> {2, 4, 6},
            };
        }
    }
}
