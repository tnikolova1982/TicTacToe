namespace TicTacToe.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class LogInViewModel
    {
        [Required(ErrorMessage = "Полето е задължително!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}