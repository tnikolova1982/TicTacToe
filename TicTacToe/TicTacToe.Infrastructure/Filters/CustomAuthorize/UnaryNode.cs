namespace TicTacToe.Infrastructure.Filters.CustomAuthorize
{
    internal abstract class UnaryNode : Node
    {
        private readonly Node expression;

        protected UnaryNode(Node expression)
        {
            this.expression = expression;
        }

        public Node Expression
        {
            get { return expression; }
        }
    }
}
