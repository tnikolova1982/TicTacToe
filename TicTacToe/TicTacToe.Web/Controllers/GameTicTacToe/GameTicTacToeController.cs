namespace TicTacToe.Controllers.GameTicTacToe
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using TicTacToe.Core.Logger;
    using TicTacToe.Data.Context;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Infrastructure.Extensions;
    using TicTacToe.Models;
    using TicTacToe.Models.Game;

    public class GameTicTacToeController : BaseController
    {
        private readonly IContextManager contextManager;
        private readonly ITicTacToeGameService ticTacToeGameService;
        private readonly ITicTacToeAIService ticTacToeAIService;

        public GameTicTacToeController(ILogger logger, IContextManager contextManager, ITicTacToeGameService ticTacToeGameService, ITicTacToeAIService ticTacToeAIService)
            : base(logger)
        {
            this.contextManager = contextManager;
            this.ticTacToeGameService = ticTacToeGameService;
            this.ticTacToeAIService = ticTacToeAIService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            // TODO - Check If User have a saved game in DB
            var game = ticTacToeGameService.InitializeTicTacToeGame();
            PrepareViewBags();

            return View(game);
        }

        [HttpPost]
        public ActionResult GameResult(Game ticTacToeGame, string userAction)
        {
            string userLetter = ticTacToeGame.UserLetterId == GameLetters.X ? "X" : "O";
            string aiLetter = ticTacToeGame.UserLetterId == GameLetters.X ? "O" : "X";

            if (userAction == "startFirst")
            {
                ticTacToeGame = ticTacToeGameService.StartTicTacToeGame(ticTacToeGame);

                return View(ticTacToeGame);
            }
            else if (userAction == "startSecond")
            {
                ticTacToeGame = ticTacToeGameService.StartTicTacToeGame(ticTacToeGame);

                var aiMove = ticTacToeAIService.AIMove(ticTacToeGame.Borad, userLetter, aiLetter, ticTacToeGame.MovesCount);

                ticTacToeGame.MovesCount += 1;
                ticTacToeGame.Borad = aiMove;

                return View(ticTacToeGame);
            }
            else if (userAction == "userPlay")
            {
                ticTacToeGame.MovesCount += 1;

                if (ticTacToeGame.MovesCount > 3) {
                    // check if user won
                    var userWon = ticTacToeGameService.CalculateWinner(ticTacToeGame.Borad);

                    if (userWon != null)
                    {
                        ticTacToeGame.Borad = userWon;
                        SetTheGameStatus(ticTacToeGame, userLetter, aiLetter);
                        DisableBoard(ticTacToeGame.Borad);

                        return View(ticTacToeGame);
                    }
                    else if (ticTacToeGame.MovesCount == 9)
                    {
                        SetTheGameStatus(ticTacToeGame, userLetter, aiLetter);
                        DisableBoard(ticTacToeGame.Borad);
                        return View(ticTacToeGame);
                    }
                }

                // if not make a AI move
                var aiMove = ticTacToeAIService.AIMove(ticTacToeGame.Borad, userLetter, aiLetter, ticTacToeGame.MovesCount);

                ticTacToeGame.MovesCount += 1;
                ticTacToeGame.Borad = aiMove;

                if (ticTacToeGame.MovesCount > 3)
                {
                    // check if AI won
                    var aiWon = ticTacToeGameService.CalculateWinner(ticTacToeGame.Borad);

                    if (aiWon != null)
                    {
                        ticTacToeGame.Borad = aiWon;
                        SetTheGameStatus(ticTacToeGame, userLetter, aiLetter);
                        DisableBoard(ticTacToeGame.Borad);

                        return View(ticTacToeGame);
                    }
                    else if (ticTacToeGame.MovesCount == 9)
                    {
                        SetTheGameStatus(ticTacToeGame, userLetter, aiLetter);
                        DisableBoard(ticTacToeGame.Borad);

                        return View(ticTacToeGame);
                    }
                }
            }

            return View(ticTacToeGame);
        }

        private void SetTheGameStatus(Game game, string userLetter, string aiLetter)
        {
            var box = game.Borad.GameBoard.FirstOrDefault(e => e.IsWinBox == true);

            if (box != null)
            {
                if (box.Value == userLetter)
                {
                    game.GameStatus = new Nomenclature
                    {
                        Id = GameStatus.Win,
                        Name = "ВИЕ СПЕЧЕЛИХТЕ"
                    };
                }
                else
                {
                    game.GameStatus = new Nomenclature
                    {
                        Id = GameStatus.Lose,
                        Name = "ВИЕ ЗАГУБИХТЕ"
                    };
                }
            }
            else
            {
                game.GameStatus = new Nomenclature
                {
                    Id = GameStatus.Draw,
                    Name = "РАВЕНСТВО"
                };
            }

        }

        private void DisableBoard(Board board)
        {
            foreach (var item in board.GameBoard.Where(e => e.IsEnabled == true))
            {
                item.IsEnabled = false;
            };
        }

        private void PrepareViewBags()
        {
            using (contextManager.NewTransaction())
            {
                var gameLatters = ticTacToeGameService.GetGameLetters().AsSelectListItems().ToList();
                var gameLevels = ticTacToeGameService.GetGameLevels().AsSelectListItems().ToList();

                ViewBag.GameLetters = gameLatters;
                ViewBag.GameLevels = gameLevels;
            }
        }
    }
}