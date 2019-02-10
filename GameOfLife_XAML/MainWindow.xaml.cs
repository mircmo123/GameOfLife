using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
            for (int i = 0; i < 16; i++)
            {
                CellContainer.RowDefinitions.Add(new RowDefinition());
                CellContainer.ColumnDefinitions.Add(new ColumnDefinition());
            }
            _game = new Game(16, 16, CellContainer);
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            _game.IsRunning = !_game.IsRunning;
        }
    }
}
