namespace TicTacToe.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditUserViewModel : UserViewModel
    {
        [Display(Name = "Роля")]
        [Required(ErrorMessage = "Полето е задължително")]
        public Guid? RoleId { get; set; }
    }
}