namespace TicTacToe.Infrastructure.Misc.IgnoreProperty
{
    using System;

    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreProperty : Attribute
    {
    }
}
