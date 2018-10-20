namespace TicTacToe.Models
{
    using System;

    public class DomainEntity
    {
        public Guid? Id { get; set; }

        public bool IsNew()
        {
            return Id == default(Guid);
        }
    }
}
