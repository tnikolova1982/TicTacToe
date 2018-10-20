namespace TicTacToe.Tests.Services
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using TicTacToe.Models.Game;
    using TicTacToe.Services.Services;

    [TestClass]
    public class TicTacToeAIServiceTest
    {
        private static TicTacToeAIService ticTacToeAIService;

        private static TicTacToeCommonServices ticTacToeCommonServices;

        private const int TicTacToeBoardSize = 9;

        private string userLetter = "X";

        private string aiLetter = "O";

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            ticTacToeCommonServices = new TicTacToeCommonServices();
            ticTacToeAIService = new TicTacToeAIService(ticTacToeCommonServices);
        }

        [TestMethod]
        public void AIBlockMoveIfUserIsAboutToWinHorizontal()
        {
            // Arrange
            var board = InitializeBoardWhereUserIsAboutToMakeHorizontalWin(userLetter, aiLetter);
            var expected = ExpectedResultWhereUserIsAboutToMakeHorizontalWin(userLetter, aiLetter);
            int movesCount = 3;

            // Act
            var result = ticTacToeAIService.AIMove(board, userLetter, aiLetter, movesCount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.GameBoard.Count, result.GameBoard.Count);
            CollectionAssert.AreEqual(expected.GameBoard, result.GameBoard);
        }

        [TestMethod]
        public void AIBlockMoveIfUserIsAboutToWinVertical()
        {
            // Arrange
            var board = InitializeBoardWhereUserIsAboutToMakeVerticalWin(userLetter, aiLetter);
            var expected = ExpectedResultWhereUserIsAboutToMakeVerticalWin(userLetter, aiLetter);
            int movesCount = 3;

            // Act
            var result = ticTacToeAIService.AIMove(board, userLetter, aiLetter, movesCount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.GameBoard.Count, result.GameBoard.Count);
            CollectionAssert.AreEqual(expected.GameBoard, result.GameBoard);
        }

        [TestMethod]
        public void AIBlockMoveIfUserIsAboutToWinFirstDiagnalWin()
        {
            // Arrange
            var board = InitializeBoardWhereUserIsAboutToMakeFirstDiagonalWin(userLetter, aiLetter);
            var expected = ExpectedResultWhereUserIsAboutToMakeFirstDiagnalWin(userLetter, aiLetter);
            int movesCount = 3;

            // Act
            var result = ticTacToeAIService.AIMove(board, userLetter, aiLetter, movesCount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.GameBoard.Count, result.GameBoard.Count);
            CollectionAssert.AreEqual(expected.GameBoard, result.GameBoard);
        }

        [TestMethod]
        public void AIBlockMoveIfUserIsAboutToWinSecondDiagnalWin()
        {
            // Arrange
            var board = InitializeBoardWhereUserIsAboutToMakeSecondDiagonalWin(userLetter, aiLetter);
            var expected = ExpectedResultWhereUserIsAboutToMakeSecondDiagonalWin(userLetter, aiLetter);
            int movesCount = 3;

            // Act
            var result = ticTacToeAIService.AIMove(board, userLetter, aiLetter, movesCount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.GameBoard.Count, result.GameBoard.Count);
            CollectionAssert.AreEqual(expected.GameBoard, result.GameBoard);
        }

        [TestMethod]
        public void AIWinMoveIfAIIsAboutToWinOnHorizontalLine()
        {
            // Arrange
            var board = InitializeBoardWhereAIIsAboutToWinOnHorizontalLine(userLetter, aiLetter);
            var expected = ExpectedResultAIIsAboutToWinOnHorizontalLine(userLetter, aiLetter);
            int movesCount = 4;

            // Act
            var result = ticTacToeAIService.AIMove(board, userLetter, aiLetter, movesCount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.GameBoard.Count, result.GameBoard.Count);
            CollectionAssert.AreEqual(expected.GameBoard, result.GameBoard);
        }

        [TestMethod]
        public void AIWinIfAIIsAboutToWinOnVerticalLine()
        {
            // Arrange
            var board = InitializeBoardWhereAIIsAboutToWinOnVerticalLine(userLetter, aiLetter);
            var expected = ExpectedResultWhereAIIsAboutToWinOnVerticalLine(userLetter, aiLetter);
            int movesCount = 4;

            // Act
            var result = ticTacToeAIService.AIMove(board, userLetter, aiLetter, movesCount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.GameBoard.Count, result.GameBoard.Count);
            CollectionAssert.AreEqual(expected.GameBoard, result.GameBoard);
        }

        [TestMethod]
        public void AIWinIfAIIsAboutToWinOnFirstDiagonalLine()
        {
            // Arrange
            var board = InitializeBoardWhereAIIsAboutToWinOnFirstDiagonalLine(userLetter, aiLetter);
            var expected = ExpectedResultWhereAIIsAboutToWinOnFirstDiagonalLine(userLetter, aiLetter);
            int movesCount = 3;

            // Act
            var result = ticTacToeAIService.AIMove(board, userLetter, aiLetter, movesCount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.GameBoard.Count, result.GameBoard.Count);
            CollectionAssert.AreEqual(expected.GameBoard, result.GameBoard);
        }

        [TestMethod]
        public void AIWinIfAIIsAboutToWinOnSecondDiagonalLine()
        {
            // Arrange
            var board = InitializeBoardWhereAIIsAboutToWinOnSecondDiagonalLine(userLetter, aiLetter);
            var expected = ExpectedResultWhereAIIsAboutToWinOnSecondDiagonalLine(userLetter, aiLetter);
            int movesCount = 3;

            // Act
            var result = ticTacToeAIService.AIMove(board, userLetter, aiLetter, movesCount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.GameBoard.Count, result.GameBoard.Count);
            CollectionAssert.AreEqual(expected.GameBoard, result.GameBoard);
        }

        [TestMethod]
        public void IfAIIsNotWiningOrLosingMakeARandomMove()
        {
            // Arrange
            var board = InitalizeBoardWhereAiIsNotWiningOrLosing(userLetter, aiLetter);
            int movesCount = 1;

            // Act
            var result = ticTacToeAIService.AIMove(board, userLetter, aiLetter, movesCount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(TicTacToeBoardSize, result.GameBoard.Count);
            Assert.AreEqual(2, result.GameBoard.Where(e => e.Value != null).Count());
            Assert.AreEqual(1, result.GameBoard.Where(e => e.Value == aiLetter).Count());
        }

        // Initalize
        private Board InitializeBoardWhereUserIsAboutToMakeHorizontalWin(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = false, Positon = 0, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 2, Value = userLetter });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 3 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 4, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 6 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 8 });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board InitializeBoardWhereUserIsAboutToMakeVerticalWin(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = false, Positon = 0, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 2 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 3, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 4, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 6 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 8 });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board InitializeBoardWhereUserIsAboutToMakeFirstDiagonalWin(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = false, Positon = 0, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 2 });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 3 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 4 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 6, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 8, Value = userLetter });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board InitializeBoardWhereUserIsAboutToMakeSecondDiagonalWin(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = true, Positon = 0 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 2, Value = userLetter });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 3 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 4 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 6, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 8, Value = aiLetter });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board InitializeBoardWhereAIIsAboutToWinOnHorizontalLine(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = true, Positon = 0 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 1, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 2, Value = aiLetter });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 3, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 4, });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 6 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 7, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 8 });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board InitializeBoardWhereAIIsAboutToWinOnVerticalLine(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = true, Positon = 0 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 1, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 2 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 3, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 4, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 6 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 8, Value = userLetter });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board InitializeBoardWhereAIIsAboutToWinOnFirstDiagonalLine(string userLetter, string aiLetter)
        {

            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = false, Positon = 0, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 2 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 3, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 4 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 6, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 8, Value = aiLetter });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board InitializeBoardWhereAIIsAboutToWinOnSecondDiagonalLine(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = true, Positon = 0 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 2, Value = aiLetter });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 3 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 4 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 6, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 7, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 8, Value = userLetter });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board InitalizeBoardWhereAiIsNotWiningOrLosing(string userLetter, string aiLetter)
        {

            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = true, Positon = 0 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 2, Value = userLetter });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 3 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 4 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 6 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 8 });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        // Expected Result
        private Board ExpectedResultWhereUserIsAboutToMakeHorizontalWin(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = false, Positon = 0, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 1, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 2, Value = userLetter });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 3 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 4, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 6 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 8 });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board ExpectedResultWhereUserIsAboutToMakeVerticalWin(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = false, Positon = 0, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 2 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 3, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 4, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 6, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 8 });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board ExpectedResultWhereUserIsAboutToMakeFirstDiagnalWin(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = false, Positon = 0, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 2 });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 3 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 4, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 6, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 8, Value = userLetter });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board ExpectedResultWhereUserIsAboutToMakeSecondDiagonalWin(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = true, Positon = 0 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 2, Value = userLetter });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 3 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 4, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 6, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 8, Value = aiLetter });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board ExpectedResultAIIsAboutToWinOnHorizontalLine(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = false, Positon = 0, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 1, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 2, Value = aiLetter });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 3, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 4 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 6 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 7, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 8 });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board ExpectedResultWhereAIIsAboutToWinOnVerticalLine(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = true, Positon = 0 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 1, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 2 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 3, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 4, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 6 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 7, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 8, Value = userLetter });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board ExpectedResultWhereAIIsAboutToWinOnFirstDiagonalLine(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = false, Positon = 0, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 2 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 3, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 4, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 6, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 7 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 8, Value = aiLetter });

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        private Board ExpectedResultWhereAIIsAboutToWinOnSecondDiagonalLine(string userLetter, string aiLetter)
        {
            var gameBoard = new List<Box>();

            gameBoard.Add(new Box { IsEnabled = true, Positon = 0 });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 1 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 2, Value = aiLetter });

            gameBoard.Add(new Box { IsEnabled = true, Positon = 3 });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 4, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = true, Positon = 5 });

            gameBoard.Add(new Box { IsEnabled = false, Positon = 6, Value = aiLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 7, Value = userLetter });
            gameBoard.Add(new Box { IsEnabled = false, Positon = 8, Value = userLetter });

            return new Board
            {
                GameBoard = gameBoard
            };
        }
    }
}
