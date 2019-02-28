using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GameOfLife_XAML
{
    class Game
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Cell[,] Field { get; set; }

        public bool IsRunning
        {
            get => _timer.IsEnabled;
            set => _timer.IsEnabled = value;
        }

        private DispatcherTimer _timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(0.1)
        };

        public Game(int rows, int columns, Grid container)
        {
            Rows = rows;
            Columns = columns;
            Field = new Cell[rows, columns];
            ForeachCell((i, j) => {
                var cell = new Cell()
                {
                    Alive = false,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch, 
                    Cursor = System.Windows.Input.Cursors.Hand
                };
                container.Children.Add(cell);
                Grid.SetRow(cell, i);
                Grid.SetColumn(cell, j);
                Field[i, j] = cell;
            });
            _timer.Tick += GameTick;
        }

        internal void Randomize()
        {
            Random rnd = new Random();
            ForeachCell((i, j) => Field[i, j].Alive = rnd.Next(2) == 1);
        }

        private void GameTick(object sender, EventArgs e)
        {
            var tempField = new bool[Rows,Columns];
            ForeachCell((i, j) => {
                var cell = Field[i, j];
                var gonnabealive = cell.Alive;
                var aliveNeighbours = AliveNeighbours(i,j);
                if (aliveNeighbours < 2)
                    gonnabealive = false;
                else if(aliveNeighbours > 2)
                    gonnabealive = aliveNeighbours > 3 ? false : true;
                tempField[i,j] = gonnabealive;
            });
            ForeachCell((i,j)=> Field[i,j].Alive = tempField[i,j]);
        }

        private void ForeachCell(Action<int,int> action)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    action(i,j);
                }
            }
        }

        private int AliveNeighbours(int row, int column)
        {
            var count = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = column - 1; j <= column + 1; j++)
                {
                    if (i < 0 || j < 0 || i >= this.Rows - 1 || j >= this.Columns - 1)
                        continue;
                    if (i == row && j == column)
                        continue;
                    if (Field[i, j].Alive) count++;
                }
            }
            return count;
        }
    }
}
