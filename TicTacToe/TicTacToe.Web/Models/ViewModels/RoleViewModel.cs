namespace TicTacToe.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RoleViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        [Display(Name = "Име на роля")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Изберете поне едно право от таблицата!")]
        public List<Guid> ActivitiesIds { get; set; }

        public bool IsNew()
        {
            return Id == default(Guid);
        }
    }
}