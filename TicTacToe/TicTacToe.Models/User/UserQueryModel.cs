namespace TicTacToe.Models.User
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using TicTacToe.Models.Search;

    public class UserQueryModel : SearchQuery
    {
        [DisplayName("Наименование")]
        public string FullName { get; set; }

        [DisplayName("Потребителско име")]
        public string UserName { get; set; }

        [DisplayName("Електронна поща")]
        public string Email { get; set; }

        public Guid? RoleId { get; set; }

        [DisplayName("Роля")]
        public IEnumerable<KeyValuePair<string, string>> RoleIdDropDown { get; set; }
    }
}
