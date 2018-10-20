namespace TicTacToe.Services.Services
{
    using System.Linq;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Infrastructure.Random;
    using TicTacToe.Models.Game;

    public class TicTacToeAIService : ITicTacToeAIService
    {
        private readonly ITicTacToeCommonServices ticTacToeCommonServices;

        public TicTacToeAIService(ITicTacToeCommonServices ticTacToeCommonServices)
        {
            this.ticTacToeCommonServices = ticTacToeCommonServices;
        }

        public Board AIMove(Board board, string userLetter, string aiLetter, int movesCount)
        {
            Board newBoard = null;

            if (movesCount > 2)
            {
                newBoard = AIMoveIfAIIsAboutToWin(board, userLetter, aiLetter);

                if (newBoard == null)
                {
                    newBoard = AIMoveIfUserIsAboutToWin(board, userLetter, aiLetter);
                }
            }

            if (newBoard == null)
            {
                newBoard = AIMoveAtRandomPosition(board, aiLetter);
            }

            return newBoard;
        }

        private Board AIMoveIfUserIsAboutToWin(Board board, string userLetter, string aiLetter)
        {
            var lines = ticTacToeCommonServices.GetLines();

            int countUserLettersOnLine = 0;
            int countEmptyBoxesOnLine = 0;
            int? positonOfEmptyBox = null;
            bool isAIMakeMove = false;


            for (int index = 0; index < lines.Count; index++)
            {
                var line = lines[index];

                countUserLettersOnLine = 0;
                countEmptyBoxesOnLine = 0;
                positonOfEmptyBox = null;

                for (int lineIndex = 0; lineIndex < line.Count; lineIndex++)
                {
                    if (board.GameBoard[line[lineIndex]].Value == userLetter)
                    {
                        countUserLettersOnLine++;
                    }
                    else if (board.GameBoard[line[lineIndex]].Value == null)
                    {
                        countEmptyBoxesOnLine++;
                        positonOfEmptyBox = line[lineIndex];
                    }

                    if (countEmptyBoxesOnLine == 2)
                    {
                        break;
                    }
                    else if ((countUserLettersOnLine == 2 && lineIndex == line.Count - 1) && positonOfEmptyBox != null)
                    {
                        board.GameBoard[positonOfEmptyBox.Value].Value = aiLetter;
                        board.GameBoard[positonOfEmptyBox.Value].IsEnabled = false;
                        isAIMakeMove = true;
                    }
                }

                if (isAIMakeMove)
                {
                    break;
                }

            }

            return isAIMakeMove ? board : null;
        }

        private Board AIMoveIfAIIsAboutToWin(Board board, string userLetter, string aiLetter)
        {
            var lines = ticTacToeCommonServices.GetLines();

            int countAiLettersOnLine = 0;
            int countEmptyBoxesOnLine = 0;
            int? positonOfEmptyBox = null;
            bool isAIMakeMove = false;


            for (int index = 0; index < lines.Count; index++)
            {
                var line = lines[index];

                countAiLettersOnLine = 0;
                countEmptyBoxesOnLine = 0;
                positonOfEmptyBox = null;

                for (int lineIndex = 0; lineIndex < line.Count; lineIndex++)
                {
                    if (board.GameBoard[line[lineIndex]].Value == aiLetter)
                    {
                        countAiLettersOnLine++;
                    }
                    else if (board.GameBoard[line[lineIndex]].Value == null)
                    {
                        countEmptyBoxesOnLine++;
                        positonOfEmptyBox = line[lineIndex];
                    }

                    if (countEmptyBoxesOnLine == 2)
                    {
                        break;
                    }
                    else if ((countAiLettersOnLine == 2 && lineIndex == line.Count - 1) && positonOfEmptyBox != null)
                    {
                        board.GameBoard[positonOfEmptyBox.Value].Value = aiLetter;
                        board.GameBoard[positonOfEmptyBox.Value].IsEnabled = false;
                        isAIMakeMove = true;
                    }
                }

                if (isAIMakeMove)
                {
                    break;
                }
            }

            return isAIMakeMove ? board : null;
        }

        private Board AIMoveAtRandomPosition(Board board, string aiLetter)
        {
            var emptyBoxes = board.GameBoard.Where(e => e.Value == null).ToList();

            var index = RandomProvider.Instance.Next(emptyBoxes.Count);

            board.GameBoard[emptyBoxes[index].Positon].Value = aiLetter;
            board.GameBoard[emptyBoxes[index].Positon].IsEnabled = false;

            return board;
        }
    }
}
