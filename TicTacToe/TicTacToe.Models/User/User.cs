namespace TicTacToe.Models.User
{
    using System;

    public class User : UserPrincipal
    {
        public string FullName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public Guid? RoleId { get; set; }
    }
}
