using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLife_XAML
{
    internal class Cell : Grid
    {
        public int Row { get; set; }
        public int Column { get; set; }

        private bool _alive;

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            MouseLeftButtonUp += Cell_MouseLeftButtonUp;
            MouseEnter += Cell_Hover;
            MouseLeave += Cell_MouseLeave;
        }

        private void Cell_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Alive = Alive;
        }

        private void Cell_Hover(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Background = Alive ? Brushes.DarkGray : Brushes.WhiteSmoke;
        }

        private void Cell_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Alive = !Alive;
        }

        public bool Alive
        {
            get => _alive;
            set
            {
                _alive = value;
                Background = value ? Brushes.Black : Brushes.White;
            }
        }
    }
}