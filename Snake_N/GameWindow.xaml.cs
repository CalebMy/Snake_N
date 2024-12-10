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
        private Random rnd = new Random();
        //const int SnakeStartSpeed = 400;
        const int SnakeSquareSize = 20;
        const int SnakeStartLength = 3;
        private enum SnakeStartSpeed
        {
            One = 100,
            Two = 200,
            Three = 300,
            Four = 400,
            Five = 500,
            Six = 600,
            Seven = 700,
            Eight = 800,
            Nine = 900
        };
        const int SnakeSpeedThreshold = 100;

        private SolidColorBrush snakeBodyBrush = Brushes.Green;
        private SolidColorBrush snakeHeadBrush = Brushes.YellowGreen;
        private List<SnakePart> snakeParts = new List<SnakePart>();
        private UIElement snakeFood = null;
        private SolidColorBrush foodBrush = Brushes.Red;
        const int MaxHighscoreListEntryCount = 10;
        DispatcherTimer timer = new DispatcherTimer();
        
        private void DrawSnake()
        {
            foreach (SnakePart snakePart in snakeParts)
            {
                if (snakePart.UiElement == null)
                {
                    snakePart.UiElement = new Rectangle()
                    {
                        Width = SnakeSquareSize,
                        Height = SnakeSquareSize,
                        Fill = (snakePart.IsHead ? snakeHeadBrush : snakeBodyBrush)
                    };
                    Pole.Children.Add(snakePart.UiElement);
                    Canvas.SetTop(snakePart.UiElement, snakePart.Position.Y);
                    Canvas.SetLeft(snakePart.UiElement, snakePart.Position.X);
                }
            }
        }
        public enum SnakeDirection { Left, Right, Up, Down };
        private SnakeDirection snakeDirection = SnakeDirection.Right;
        private int snakeLength;
        private int currentScore = 0;
        private void MoveSnake()
        {
            // Удалите последнюю часть змеи, чтобы подготовить новую часть, добавленную ниже  
            while (snakeParts.Count >= snakeLength)
            {
                Pole.Children.Remove(snakeParts[0].UiElement);
                snakeParts.RemoveAt(0);
            }
            // Далее мы добавим к змее новый элемент, которым будет (новая) голова  
            // Поэтому мы помечаем все существующие детали как элементы, не относящиеся к головке (корпусу), а затем  
            // мы следим за тем, чтобы они пользовались щеткой для тела
            foreach (SnakePart snakePart in snakeParts)
            {
                (snakePart.UiElement as Rectangle).Fill = snakeBodyBrush;
                snakePart.IsHead = false;
            }

            // Определите, в каком направлении развернуть змейку, основываясь на текущем направлении  
            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];
            double nextX = snakeHead.Position.X;
            double nextY = snakeHead.Position.Y;
            switch (snakeDirection)
            {
                case SnakeDirection.Left:
                    nextX -= SnakeSquareSize;
                    break;
                case SnakeDirection.Right:
                    nextX += SnakeSquareSize;
                    break;
                case SnakeDirection.Up:
                    nextY -= SnakeSquareSize;
                    break;
                case SnakeDirection.Down:
                    nextY += SnakeSquareSize;
                    break;
            }

            // Теперь добавьте новую головную часть к нашему списку деталей змеи...  
            snakeParts.Add(new SnakePart()
            {
                Position = new Point(nextX, nextY),
                IsHead = true
            });
            //.. а потом пусть это будет нарисовано!  
            DrawSnake();
            // Мы вернемся к этому позже...  
            DoCollisionCheck();          
        }

        private void StartNewGame()
        {
            foreach (SnakePart snakeBodyPart in snakeParts)
            {
                if (snakeBodyPart.UiElement != null)
                    Pole.Children.Remove(snakeBodyPart.UiElement);
            }
            snakeParts.Clear();
            if (snakeFood != null)
                Pole.Children.Remove(snakeFood);

            currentScore = 0;
            snakeLength = SnakeStartLength;
            snakeDirection = SnakeDirection.Right;
            snakeParts.Add(new SnakePart() { Position = new Point(SnakeSquareSize * 5, SnakeSquareSize * 5) });
            timer.Interval = TimeSpan.FromMilliseconds((int)SnakeStartSpeed.Four);

            // Draw the snake  
            DrawSnake();
            DrawSnakeFood();

            // Go!          
            timer.IsEnabled = true;

        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            SnakeDirection originalSnakeDirection = snakeDirection;
            switch (e.Key)
            {
                case Key.Up:
                    if (snakeDirection != SnakeDirection.Down)
                        snakeDirection = SnakeDirection.Up;
                    break;
                case Key.Down:
                    if (snakeDirection != SnakeDirection.Up)
                        snakeDirection = SnakeDirection.Down;
                    break;
                case Key.Left:
                    if (snakeDirection != SnakeDirection.Right)
                        snakeDirection = SnakeDirection.Left;
                    break;
                case Key.Right:
                    if (snakeDirection != SnakeDirection.Left)
                        snakeDirection = SnakeDirection.Right;
                    break;
                case Key.Space:
                    StartNewGame();
                    break;
            }
            if (snakeDirection != originalSnakeDirection)
                MoveSnake();
        }
        //// This list describes the Bonus Red pieces of Food on the Canvas
        //private readonly List<Point> _bonusPoints = new List<Point>();

        //// This list describes the body of the snake on the Canvas
        //private readonly List<Point> _snakePoints = new List<Point>();

        //private readonly Brush _snakeColor = Brushes.Green;
        //private enum SnakeSize
        //{
        //    Thin = 4,
        //    Normal = 6,
        //    Thick = 8
        //};
        //private enum Movingdirection
        //{
        //    Upwards = 8,
        //    Downwards = 2,
        //    Toleft = 4,
        //    Toright = 6
        //};

        ////TimeSpan values
        //private enum GameSpeed
        //{
        //    Fast = 1,
        //    Moderate = 10000,
        //    Slow = 50000,
        //    DamnSlow = 500000
        //};

        //private readonly Point _startingPoint = new Point(100, 100);
        //private Point _currentPosition = new Point();

        //// Movement direction initialisation
        //private int _direction = 0;

        ///* Placeholder for the previous movement direction
        // * The snake needs this to avoid its own body.  */
        //private int _previousDirection = 0;

        ///* Here user can change the size of the snake. 
        // * Possible sizes are THIN, NORMAL and THICK */
        //private readonly int _headSize = (int)SnakeSize.Thick;

        //private int _length = 100;
        //private int _score = 0;
        //private readonly Random _rnd = new Random();
        public GameWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            StartNewGame();
            SnakeHighscore.LoadHighscoreList();
            /* Here user can change the speed of the snake. 
             * Possible speeds are FAST, MODERATE, SLOW and DAMNSLOW */
            //timer.Interval = new TimeSpan((int)GameSpeed.Moderate);
            //timer.Start();

            //KeyDown += new KeyEventHandler(OnButtonKeyDown);
            //PaintSnake(_startingPoint);
            //_currentPosition = _startingPoint;

            //// Instantiate Food Objects
            //for (var n = 0; n < 10; n++)
            //{
            //    PaintBonus(n);
            //}
        }
        //private void PaintSnake(Point currentposition)
        //{

        //    // This method is used to paint a frame of the snake´s body each time it is called.

        //    Ellipse newEllipse = new Ellipse
        //    {
        //        Fill = _snakeColor,
        //        Width = _headSize,
        //        Height = _headSize
        //    };

        //    Canvas.SetTop(newEllipse, currentposition.Y);
        //    Canvas.SetLeft(newEllipse, currentposition.X);

        //    int count = Pole.Children.Count;
        //    Pole.Children.Add(newEllipse);
        //    _snakePoints.Add(currentposition);

        //    // Restrict the tail of the snake
        //    if (count > _length)
        //    {
        //        Pole.Children.RemoveAt(count - _length + 9);
        //        _snakePoints.RemoveAt(count - _length);
        //    }
        //}

        //private void PaintBonus(int index)
        //{
        //    Point bonusPoint = new Point(_rnd.Next(1, 352), _rnd.Next(1, 264));

        //    Ellipse newEllipse = new Ellipse
        //    {
        //        Fill = Brushes.Red,
        //        Width = _headSize,
        //        Height = _headSize
        //    };

        //    Canvas.SetTop(newEllipse, bonusPoint.Y);
        //    Canvas.SetLeft(newEllipse, bonusPoint.X);
        //    Pole.Children.Insert(index, newEllipse);
        //    _bonusPoints.Insert(index, bonusPoint);

        //}
        private void EatSnakeFood()
        {
            snakeLength++;
            currentScore++;
            Score.Text = currentScore.ToString("D4");
            int timerInterval = Math.Max(SnakeSpeedThreshold, (int)timer.Interval.TotalMilliseconds - (currentScore * 2));
            timer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            Pole.Children.Remove(snakeFood);
            DrawSnakeFood();
        }
        private void DoCollisionCheck()
        {
            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];

            if ((snakeHead.Position.X == Canvas.GetLeft(snakeFood)) && (snakeHead.Position.Y == Canvas.GetTop(snakeFood)))
            {
                EatSnakeFood();
                return;
            }

            if ((snakeHead.Position.Y < 0) || (snakeHead.Position.Y >= Pole.ActualHeight) ||
            (snakeHead.Position.X < 0) || (snakeHead.Position.X >= Pole.ActualWidth))
            {
                EndGame();
            }

            foreach (SnakePart snakeBodyPart in snakeParts.Take(snakeParts.Count - 1))
            {
                if ((snakeHead.Position.X == snakeBodyPart.Position.X) && (snakeHead.Position.Y == snakeBodyPart.Position.Y))
                    EndGame();
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
        }

        private Point GetNextFoodPosition()
        {
            int maxX = (int)(Pole.ActualWidth / SnakeSquareSize);
            int maxY = (int)(Pole.ActualHeight / SnakeSquareSize);
            int foodX = rnd.Next(0, maxX) * SnakeSquareSize;
            int foodY = rnd.Next(0, maxY) * SnakeSquareSize;

            foreach (SnakePart snakePart in snakeParts)
            {
                if ((snakePart.Position.X == foodX) && (snakePart.Position.Y == foodY))
                    return GetNextFoodPosition();
            }

            return new Point(foodX, foodY);
        }

        private void DrawSnakeFood()
        {
            Point foodPosition = GetNextFoodPosition();
            snakeFood = new Ellipse()
            {
                Width = SnakeSquareSize,
                Height = SnakeSquareSize,
                Fill = foodBrush
            };
            Pole.Children.Add(snakeFood);
            Canvas.SetTop(snakeFood, foodPosition.Y);
            Canvas.SetLeft(snakeFood, foodPosition.X);
        }
        //private void OnButtonKeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.Key)
        //    {
        //        case Key.Down:
        //            if (_previousDirection != (int)Movingdirection.Upwards)
        //                _direction = (int)Movingdirection.Downwards;
        //            break;
        //        case Key.Up:
        //            if (_previousDirection != (int)Movingdirection.Downwards)
        //                _direction = (int)Movingdirection.Upwards;
        //            break;
        //        case Key.Left:
        //            if (_previousDirection != (int)Movingdirection.Toright)
        //                _direction = (int)Movingdirection.Toleft;
        //            break;
        //        case Key.Right:
        //            if (_previousDirection != (int)Movingdirection.Toleft)
        //                _direction = (int)Movingdirection.Toright;
        //            break;

        //    }
        //    _previousDirection = _direction;

        //}
        //private void GameOver()
        //{
        //    //bool isNewHighscore = false;
        //    if (_score > 0)
        //    {
        //        int lowestHighscore = SnakeHighscore.HighscoreList.Count > 0 ? SnakeHighscore.HighscoreList.Min(x => x.Score) : 0;
        //        if (_score > lowestHighscore)
        //        {
        //            SnakeHighscore.SaveHighscoreList();
        //            //isNewHighscore = true;
        //        }
        //    }
        //    //if (!isNewHighscore)
        //    //{
        //    //    tbFinalScore.Text = currentScore.ToString();
        //   // }
        //    MessageBox.Show($@"You Lose! Your score is {_score}", "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);

        //    Close();
        //}
        private void BtnAddToHighscoreList(object sender, RoutedEventArgs e)
        {
            int newIndex = 0;
            // Where should the new entry be inserted?
            if ((SnakeHighscore.HighscoreList.Count > 0) && (currentScore < SnakeHighscore.HighscoreList.Max(x => x.Score)))
            {
                SnakeHighscore justAbove = SnakeHighscore.HighscoreList.OrderByDescending(x => x.Score).First(x => x.Score >= currentScore);
                if (justAbove != null)
                    newIndex = SnakeHighscore.HighscoreList.IndexOf(justAbove) + 1;
            }
            // Create & insert the new entry
            SnakeHighscore.HighscoreList.Insert(newIndex, new SnakeHighscore()
            {
                PlayerName = "bayIgor",
                Score = currentScore
            });
            // Make sure that the amount of entries does not exceed the maximum
            while (SnakeHighscore.HighscoreList.Count > MaxHighscoreListEntryCount)
                SnakeHighscore.HighscoreList.RemoveAt(MaxHighscoreListEntryCount);

            SnakeHighscore.SaveHighscoreList();
        }
        private void EndGame()
        {
            timer.IsEnabled = false;
            if (currentScore > 0)
            {
                int lowestHighscore = (SnakeHighscore.HighscoreList.Count > 0 ? SnakeHighscore.HighscoreList.Min(x => x.Score) : 0);
            }
            timer.IsEnabled = false;
            MessageBox.Show("КОНЕЦ ИГРЫ!\n\nЧтобы начать новую игру, нажми Пробел", "Snake_N", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
