namespace TicTacToe.Domain.Interfaces
{
    using TicTacToe.Models.Game;

    public interface ITicTacToeAIService
    {
        Board AIMove(Board board, string userLetter, string aiLetter, int movesCount);
    }
}
