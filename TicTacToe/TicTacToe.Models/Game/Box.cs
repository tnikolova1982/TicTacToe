namespace TicTacToe.Models.Game
{
    public class Box
    {
        public string Value { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsWinBox { get; set; }

        public int Positon { get; set; }

        public override bool Equals(object obj)
        {
            var box = obj as Box;

            if (box == null)
            {
                return false;
            }
            
            return (this.Value == box.Value) && (this.IsEnabled == box.IsEnabled) && (this.IsWinBox == box.IsWinBox) && (this.Positon == box.Positon);
        }
    }
}
