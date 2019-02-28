using System.Windows;
using System.Windows.Controls;

namespace GameOfLife_XAML
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _game;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            CellContainer.RowDefinitions.Clear();
            CellContainer.ColumnDefinitions.Clear();
            for (int i = 0; i < 32; i++)
            {
                CellContainer.RowDefinitions.Add(new RowDefinition());
                CellContainer.ColumnDefinitions.Add(new ColumnDefinition());
            }
            _game = new Game(32, 32, CellContainer);
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            _game.IsRunning = !_game.IsRunning;
        }

        private void btnRandomize_Click(object sender, RoutedEventArgs e)
        {
            _game.Randomize();
        }
    }
}
