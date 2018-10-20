namespace TicTacToe.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using TicTacToe.Infrastructure.CustomAttributes;

    public class RegisterUserViewModel : UserViewModel
    {
        [Display(Name = "Потребителско име")]
        [Required(ErrorMessage = "Полето е задължително")]
        [System.Web.Mvc.Remote("VerifyUserName", "ManageUser", ErrorMessage = "Съществува такова потребителско име!")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        [ConfigRegularExpression("ValidatePassword", ErrorMessage = "Новата парола трябва да се състои от минимум 8 символа, които трябва да включват задължително главна, малка буква и число.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [DataType(DataType.Password)]
        [Display(Name = "Потвърди парола")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат!")]
        public string ConfrimPassword { get; set; }
    }
}