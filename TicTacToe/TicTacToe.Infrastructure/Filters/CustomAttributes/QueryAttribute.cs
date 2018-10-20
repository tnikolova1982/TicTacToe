namespace TicTacToe.Infrastructure.Filters.CustomAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class QueryAttribute : Attribute
    {
        public virtual bool IsVisibleInView { get; set; }
    }
}
