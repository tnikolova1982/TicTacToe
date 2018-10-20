namespace TicTacToe.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TicTacToe.Infrastructure.CustomAttributes;

    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Трите имена")]
        [Required(ErrorMessage = "Полето е задължително")]
        public string FullName { get; set; }

        public virtual string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Електронна поща")]
        [EmailAddress(ErrorMessage = "Невалидна \"Eлектронна поща\"")]
        [ConfigRegularExpression("ValidateEmailRegex", ErrorMessage = "Невалидна \"Eлектронна поща\"")]
        public string Email { get; set; }

        public bool IsNew()
        {
            return Id == default(Guid);
        }
    }
}