using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Snake_N.GameWindow;

public class SnakePart
{
    public UIElement UiElement { get; set; }
    public Point Position { get; set; }
    public bool IsHead { get; set; }
    public Random rnd = new Random();
    public const int SnakeSquareSize = 20;
    public const int SnakeStartLength = 3;
    public enum SnakeStartSpeed
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
    public const int SnakeSpeedThreshold = 100;
    public SolidColorBrush snakeBodyBrush = Brushes.Green;
    public SolidColorBrush snakeHeadBrush = Brushes.YellowGreen;
    public List<SnakePart> snakeParts = new List<SnakePart>();
    public UIElement snakeFood = null;
    public SolidColorBrush foodBrush = Brushes.Red;

    const int MaxHighscoreListEntryCount = 10;


    public void DrawSnake(Canvas Pole)
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
    public SnakeDirection snakeDirection = SnakeDirection.Right;
    public int snakeLength;
    public int currentScore = 0;

    public void MoveSnake(DispatcherTimer timer, TextBlock Score, Canvas Pole)
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
        DrawSnake(Pole);
        // Мы вернемся к этому позже...  
        DoCollisionCheck(timer, Score, Pole);
    }
    public void EatSnakeFood(DispatcherTimer timer, TextBlock Score, Canvas Pole)
    {
        snakeLength++;
        currentScore++;
        Score.Text = currentScore.ToString("D4");
        int timerInterval = Math.Max(SnakeSpeedThreshold, (int)timer.Interval.TotalMilliseconds - (currentScore * 2));
        timer.Interval = TimeSpan.FromMilliseconds(timerInterval);
        Pole.Children.Remove(snakeFood);
        DrawSnakeFood(Pole);
    }
    public void DoCollisionCheck(DispatcherTimer timer, TextBlock Score, Canvas Pole)
    {
        SnakePart snakeHead = snakeParts[snakeParts.Count - 1];

        if ((snakeHead.Position.X == Canvas.GetLeft(snakeFood)) && (snakeHead.Position.Y == Canvas.GetTop(snakeFood)))
        {
            EatSnakeFood(timer, Score, Pole);
            return;
        }

        if ((snakeHead.Position.Y < 0) || (snakeHead.Position.Y >= Pole.ActualHeight) ||
        (snakeHead.Position.X < 0) || (snakeHead.Position.X >= Pole.ActualWidth))
        {
            EndGame(timer);
        }

        foreach (SnakePart snakeBodyPart in snakeParts.Take(snakeParts.Count - 1))
        {
            if ((snakeHead.Position.X == snakeBodyPart.Position.X) && (snakeHead.Position.Y == snakeBodyPart.Position.Y))
                EndGame(timer);
        }
    }

    public Point GetNextFoodPosition(Canvas Pole)
    {
        int maxX = (int)(Pole.ActualWidth / SnakeSquareSize);
        int maxY = (int)(Pole.ActualHeight / SnakeSquareSize);
        int foodX = rnd.Next(0, maxX) * SnakeSquareSize;
        int foodY = rnd.Next(0, maxY) * SnakeSquareSize;

        foreach (SnakePart snakePart in snakeParts)
        {
            if ((snakePart.Position.X == foodX) && (snakePart.Position.Y == foodY))
                return GetNextFoodPosition(Pole);
        }

        return new Point(foodX, foodY);
    }

    public void DrawSnakeFood(Canvas Pole)
    {
        Point foodPosition = GetNextFoodPosition(Pole);
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
    public void BtnAddToHighscoreList(object sender, RoutedEventArgs e)
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
    public void EndGame(DispatcherTimer timer)
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
