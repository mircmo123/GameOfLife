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
        public Cell[,] Field { get; set; }
        public int AliveCells { get => _aliveCells; set => _aliveCells = value; }

        private int _aliveCells = 0;
        public bool IsRunning
        {
            get => _timer.IsEnabled;
            set => _timer.IsEnabled = value;
        }

        private DispatcherTimer _timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(0.2)
        };

        public Game(int rows, int columns, Grid container)
        {
            Random rnd = new Random();
            Rows = rows;
            Columns = columns;
            Field = new Cell[rows, columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    var cell = new Cell(i, j)
                    {
                        Alive = rnd.Next(2) == 1,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch, 
                        Cursor = System.Windows.Input.Cursors.Hand
                    };
                    container.Children.Add(cell);
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    Field[i, j] = cell;
                }
            }
            _timer.Tick += GameTick;
        }

        private void GameTick(object sender, EventArgs e)
        {
            var tempField = new bool[Rows,Columns];
            ForeachCell(cell => {
                var gonnabealive = cell.Alive;
                var aliveNeighbours = AliveNeighbours(cell);
                if (aliveNeighbours < 2)
                    gonnabealive = false;
                else if(aliveNeighbours > 2)
                    gonnabealive = aliveNeighbours > 3 ? false : true;
                tempField[cell.Row, cell.Column] = gonnabealive;
            });
            ForeachCell(c => c.Alive = tempField[c.Row, c.Column]);
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

        private int AliveNeighbours(Cell cell)
        {
            var count = 0;
            for (int i = cell.Row - 1; i < cell.Row + 1; i++)
            {
                for (int j = cell.Column - 1; j < cell.Column + 1; j++)
                {
                    if (i < 0 || j < 0 || i > this.Rows || j > this.Columns)
                        continue;
                    if (i == cell.Row && j == cell.Column)
                        continue;
                    if (Field[i, j].Alive) count++;
                }
            }
            return count;
        }
    }
}
