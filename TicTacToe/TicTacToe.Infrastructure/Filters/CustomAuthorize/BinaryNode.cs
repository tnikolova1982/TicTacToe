namespace TicTacToe.Infrastructure.Filters.CustomAuthorize
{
    internal abstract class BinaryNode : Node
    {
        private readonly Node leftExpression;
        private readonly Node rightExpression;

        protected BinaryNode(Node leftExpression, Node rightExpression)
        {
            this.leftExpression = leftExpression;
            this.rightExpression = rightExpression;
        }

        public Node LeftExpression
        {
            get { return leftExpression; }
        }

        public Node RightExpression
        {
            get { return rightExpression; }
        }
    }
}
