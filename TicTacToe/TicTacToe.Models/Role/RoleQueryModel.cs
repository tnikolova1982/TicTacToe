namespace TicTacToe.Models.Role
{
    using System.ComponentModel;
    using TicTacToe.Models.Search;

    public class RoleQueryModel : SearchQuery
    {
        [DisplayName("Наименование")]
        public string Name { get; set; }
    }
}
