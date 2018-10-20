namespace TicTacToe.Models.User
{
    using System;
    using System.ComponentModel;

    public class UserTableModel
    {
        public Guid Id { get; set; }

        [DisplayName("Наименование")]
        public string FullName { get; set; }

        [DisplayName("Потребителско име")]
        public string UserName { get; set; }

        [DisplayName("Електронна поща")]
        public string Email { get; set; }

        [DisplayName("Роля")]
        public string Role { get; set; }
    }
}
