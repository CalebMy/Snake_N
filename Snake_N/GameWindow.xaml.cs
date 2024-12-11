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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake_N
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        //private Random rnd = new Random();
        //const int SnakeSquareSize = 20;
        //const int SnakeStartLength = 3;
        //private enum SnakeStartSpeed
        //{
        //    One = 100,
        //    Two = 200,
        //    Three = 300,
        //    Four = 400,
        //    Five = 500,
        //    Six = 600,
        //    Seven = 700,
        //    Eight = 800,
        //    Nine = 900
        //};
        //const int SnakeSpeedThreshold = 100;

        //private SolidColorBrush snakeBodyBrush = Brushes.Green;
        //private SolidColorBrush snakeHeadBrush = Brushes.YellowGreen;
        //private List<SnakePart> snakeParts = new List<SnakePart>();
        //private UIElement snakeFood = null;
        //private SolidColorBrush foodBrush = Brushes.Red;
        //const int MaxHighscoreListEntryCount = 10;
        //DispatcherTimer timer = new DispatcherTimer();

        //private void DrawSnake()
        //{
        //    foreach (SnakePart snakePart in snakeParts)
        //    {
        //        if (snakePart.UiElement == null)
        //        {
        //            snakePart.UiElement = new Rectangle()
        //            {
        //               Width = SnakeSquareSize,
        //                Height = SnakeSquareSize,
        //                Fill = (snakePart.IsHead ? snakeHeadBrush : snakeBodyBrush)
        //            };
        //            Pole.Children.Add(snakePart.UiElement);
        //            Canvas.SetTop(snakePart.UiElement, snakePart.Position.Y);
        //            Canvas.SetLeft(snakePart.UiElement, snakePart.Position.X);
        //        }
        //    }
        //}
        //public enum SnakeDirection { Left, Right, Up, Down };
        //private SnakeDirection snakeDirection = SnakeDirection.Right;
        //private int snakeLength;
        //private int currentScore = 0;
        //private void MoveSnake()
        //{
        // Удалите последнюю часть змеи, чтобы подготовить новую часть, добавленную ниже  
        //    while (snakeParts.Count >= snakeLength)
        //    {
        //        Pole.Children.Remove(snakeParts[0].UiElement);
        //        snakeParts.RemoveAt(0);
        //    }
        // Далее мы добавим к змее новый элемент, которым будет (новая) голова  
        // Поэтому мы помечаем все существующие детали как элементы, не относящиеся к головке (корпусу), а затем  
        // мы следим за тем, чтобы они пользовались щеткой для тела
        //    foreach (SnakePart snakePart in snakeParts)
        //    {
        //        (snakePart.UiElement as Rectangle).Fill = snakeBodyBrush;
        //        snakePart.IsHead = false;
        //   }

        // Определите, в каком направлении развернуть змейку, основываясь на текущем направлении  
        //    SnakePart snakeHead = snakeParts[snakeParts.Count - 1];
        //    double nextX = snakeHead.Position.X;
        //   double nextY = snakeHead.Position.Y;
        //   switch (snakeDirection)
        //   {
        //       case SnakeDirection.Left:
        //           nextX -= SnakeSquareSize;
        //          break;
        //      case SnakeDirection.Right:
        //          nextX += SnakeSquareSize;
        //          break;
        //     case SnakeDirection.Up:
        //         nextY -= SnakeSquareSize;
        //         break;
        //     case SnakeDirection.Down:
        //         nextY += SnakeSquareSize;
        //        break;
        //}

        // Теперь добавьте новую головную часть к нашему списку деталей змеи...  
        //snakeParts.Add(new SnakePart()
        //{
        //    Position = new Point(nextX, nextY),
        //    IsHead = true
        //});
        //.. а потом пусть это будет нарисовано!  
        //DrawSnake();
        //// Мы вернемся к этому позже...  
        //DoCollisionCheck();          
        //}
        //private SolidColorBrush snakeBodyBrush = Brushes.Green;
        //private SolidColorBrush snakeHeadBrush = Brushes.YellowGreen;
        //private List<SnakePart> snakeParts = new List<SnakePart>();
        //private UIElement snakeFood = null;
        //private SolidColorBrush foodBrush = Brushes.Red;
        SnakePart Palka = new SnakePart();
        DispatcherTimer timer = new DispatcherTimer();
        private void StartNewGame()
        {
            foreach (SnakePart snakeBodyPart in Palka.snakeParts)
            {
                if (snakeBodyPart.UiElement != null)
                    Pole.Children.Remove(snakeBodyPart.UiElement);
            }
            Palka.snakeParts.Clear();
            if (Palka.snakeFood != null)
                Pole.Children.Remove(Palka.snakeFood);

            Palka.currentScore = 0;
            Score.Text = Palka.currentScore.ToString("D4");
            Palka.snakeLength = SnakePart.SnakeStartLength;
            Palka.snakeDirection = SnakePart.SnakeDirection.Right;
            Palka.snakeParts.Add(new SnakePart() { Position = new Point(SnakePart.SnakeSquareSize * 5, SnakePart.SnakeSquareSize * 5) });
            timer.Interval = TimeSpan.FromMilliseconds((int)SnakePart.SnakeStartSpeed.One);

            // Draw the snake  
            Palka.DrawSnake(Pole);
            Palka.DrawSnakeFood(Pole);

            // Go!          
            timer.IsEnabled = true;

        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            SnakePart.SnakeDirection originalSnakeDirection = Palka.snakeDirection;
            if (timer.IsEnabled)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        if (Palka.snakeDirection != SnakePart.SnakeDirection.Down)
                            Palka.snakeDirection = SnakePart.SnakeDirection.Up;
                        break;
                    case Key.Down:
                        if (Palka.snakeDirection != SnakePart.SnakeDirection.Up)
                            Palka.snakeDirection = SnakePart.SnakeDirection.Down;
                        break;
                    case Key.Left:
                        if (Palka.snakeDirection != SnakePart.SnakeDirection.Right)
                            Palka.snakeDirection = SnakePart.SnakeDirection.Left;
                        break;
                    case Key.Right:
                        if (Palka.snakeDirection != SnakePart.SnakeDirection.Left)
                            Palka.snakeDirection = SnakePart.SnakeDirection.Right;
                        break;
                    case Key.Space:
                        StartNewGame();
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Up:
                        if (Palka.snakeDirection != SnakePart.SnakeDirection.Down)
                        {
                            MessageBox.Show("Думал, что самый умный?)\nЧто змея будет двигать несмотря на конец игры?!))\nЧтобы начать новую игру, НУЖНО БЫЛО НАЖАТЬ ПРОБЕЛ!!!\n\nНо я нажал за тебя) *Тык", "Snake_N|Bug", MessageBoxButton.OK, MessageBoxImage.Information);
                            StartNewGame();
                        }    
                        break;
                    case Key.Down:
                        if (Palka.snakeDirection != SnakePart.SnakeDirection.Up)
                        {
                            MessageBox.Show("Думал, что самый умный?)\nЧто змея будет двигать несмотря на конец игры?!))\nЧтобы начать новую игру, НУЖНО БЫЛО НАЖАТЬ ПРОБЕЛ!!!\n\nНо я нажал за тебя) *Тык", "Snake_N|Bug", MessageBoxButton.OK, MessageBoxImage.Information);
                            StartNewGame();
                        }
                        break;
                    case Key.Left:
                        if (Palka.snakeDirection != SnakePart.SnakeDirection.Right)
                        {
                            MessageBox.Show("Думал, что самый умный?)\nЧто змея будет двигать несмотря на конец игры?!))\nЧтобы начать новую игру, НУЖНО БЫЛО НАЖАТЬ ПРОБЕЛ!!!\n\nНо я нажал за тебя) *Тык", "Snake_N|Bug", MessageBoxButton.OK, MessageBoxImage.Information);
                            StartNewGame();
                        }    
                        break;
                    case Key.Right:
                        if (Palka.snakeDirection != SnakePart.SnakeDirection.Left)
                        {
                            MessageBox.Show("Думал, что самый умный?)\nЧто змея будет двигать несмотря на конец игры?!))\nЧтобы начать новую игру, НУЖНО БЫЛО НАЖАТЬ ПРОБЕЛ!!!\n\nНо я нажал за тебя) *Тык", "Snake_N|Bug", MessageBoxButton.OK, MessageBoxImage.Information);
                            StartNewGame();
                        }
                        break;
                    case Key.Space:
                        StartNewGame();
                        break;
                }
                    
            }
            if (Palka.snakeDirection != originalSnakeDirection)
                Palka.MoveSnake(timer, Score, Pole);
        }

        public GameWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            StartNewGame();
            SnakeHighscore.LoadHighscoreList();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Palka.MoveSnake(timer, Score, Pole);
        }
    }
}
