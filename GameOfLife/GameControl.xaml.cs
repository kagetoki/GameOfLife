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
using Game;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl, IDisposable
    {
        private ConwayGame _game;
        private static SolidColorBrush AliveColor = new SolidColorBrush(new Color { A = 255, R = 11, B = 11, G = 255 });
        private static SolidColorBrush DeadColor = new SolidColorBrush(new Color { A = 255, R = 11, B = 11, G = 11 });
        private const int CELL_SIZE = 20;
        public GameControl()
        {
            _game = new ConwayGame(50,50, 100);
            InitializeComponent();
            DrawGrid();
            _game.GenerationChanged += RedrawGrid;
        }

        private void DrawGrid()
        {
            for (int i = 0; i < _game.Field.Width; i++)
            {
                this.GameGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(CELL_SIZE) });
            }
            for(int j = 0; j < _game.Field.Height; j++)
            {
                this.GameGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CELL_SIZE)});
            }
            for(int x = 0; x < _game.Field.Width; x++)
            {
                for(int y = 0; y < _game.Field.Height; y++)
                {
                    var btn = CellButton(_game, x, y);
                    this.GameGrid.Children.Add(btn);
                    Grid.SetColumn(btn, x);
                    Grid.SetRow(btn, y);
                }
            }
        }
        private void CleanCells()
        {
            for (int x = 0; x < _game.Field.Width; x++)
            {
                for (int y = 0; y < _game.Field.Height; y++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        var btn = GameGrid.Children.Cast<UIElement>().
                          FirstOrDefault(e => Grid.GetColumn(e) == x && Grid.GetRow(e) == y) as Button;
                        btn.Background = _game.Field[x, y].IsAlive ? AliveColor : DeadColor;
                    }, System.Windows.Threading.DispatcherPriority.Send);
                }
            }
            
        }
        private void RedrawGrid(object sender, NewGenerationEventArgs args)
        {
            foreach(var cell in args.ChangedCells)
            {
                Dispatcher.Invoke(() =>
                {
                    var btn = GameGrid.Children.Cast<UIElement>().
                      FirstOrDefault(e => Grid.GetColumn(e) == cell.X && Grid.GetRow(e) == cell.Y) as Button;
                    btn.Background = cell.IsAlive ? AliveColor : DeadColor;
                }, System.Windows.Threading.DispatcherPriority.Send);
            }
        }

        private Button CellButton(ConwayGame game, int xIndex, int yIndex)
        {
            var button = new Button();
            button.Background = DeadColor;
            button.Height = CELL_SIZE;
            button.Width = CELL_SIZE;
            button.Click += (s, e) => 
            {
                game.Field[xIndex, yIndex].IsAlive = !game.Field[xIndex, yIndex].IsAlive;
                button.Background = game.Field[xIndex, yIndex].IsAlive ? AliveColor : DeadColor;
                
            };
            return button;
        }

        public void Dispose()
        {
            if(_game != null)
            {
                _game.Dispose();
            }
        }

        private void StartGameBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_game.IsStarted)
            {
                StartGameBtn.Content = "Start";
                _game.Stop();
            }
            else
            {
                StartGameBtn.Content = "Stop";
                _game.Start();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _game.Stop();
            for (int x = 0; x < _game.Field.Width; x++)
            {
                for (int y = 0; y < _game.Field.Height; y++)
                {
                    _game.Field[x, y].IsAlive = false;
                }
            }
            CleanCells();
            this.StartGameBtn.Content = "Start";
        }
    }
}
