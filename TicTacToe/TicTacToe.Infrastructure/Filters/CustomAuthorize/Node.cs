﻿namespace TicTacToe.Infrastructure.Filters.CustomAuthorize
{
    using System.Security.Principal;

    internal abstract class Node
    {
        public abstract bool Eval(IPrincipal principal);
    }
}
