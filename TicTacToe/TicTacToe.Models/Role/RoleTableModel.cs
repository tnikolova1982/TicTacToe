namespace TicTacToe.Models.Role
{
    using System;
    using System.ComponentModel;

    public class RoleTableModel
    {
        public Guid Id { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}
