using System;
using System.Windows;
using System.Windows.Controls;

namespace GameOfLife_XAML
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int rows = 42, columns = 42;
        private Game _game;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            CellContainer.RowDefinitions.Clear();
            CellContainer.ColumnDefinitions.Clear();
            for (int i = 0; i < columns; i++)
                CellContainer.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < rows; i++)
                CellContainer.RowDefinitions.Add(new RowDefinition());
            _game = new Game(rows, columns, CellContainer, TimeSpan.FromMilliseconds(150));
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            _game.Clear();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            _game.IsRunning = !_game.IsRunning;
            btnPlay.Content = _game.IsRunning ? "Pause" : "Play";
        }
    }
}
