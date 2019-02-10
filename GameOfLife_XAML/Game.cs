using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GameOfLife_XAML
{
    class Game
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Cell[,] Field { get; }
        public bool IsRunning
        {
            get => _timer.IsEnabled;
            set => _timer.IsEnabled = value;
        }

        private DispatcherTimer _timer = new DispatcherTimer();
        private bool _isRunning;

        public Game(int rows, int columns, Grid container)
        {
            Rows = rows;
            Columns = columns;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    var cell = new Cell(i, j)
                    {
                        Alive = false,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };
                    container.Children.Add(cell);
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                }
            }
        }

        private void ForeachCell(Action<Cell> action)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    action(Field[i, j]);
                }
            }
        }

        private void ForeachCellNeighbour(Cell cell, Action<Cell> action)
        {
            for (int i = cell.Row - 1; i < cell.Row + 1; i++)
            {
                for (int j = cell.Column - 1; j < cell.Column + 1; j++)
                {
                    if (i < 0 || j < 0 || i > this.Columns || j > this.Rows)
                        continue;
                    Console.WriteLine($"{i} - {j}");
                }
            }
        }
    }
}
