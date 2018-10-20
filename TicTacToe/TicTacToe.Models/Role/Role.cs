namespace TicTacToe.Models.Role
{
    using System;

    public class Role : DomainEntity
    {
        public string Name { get; set; }

        public Guid[] ActivitiesIds { get; set; }
    }
}
