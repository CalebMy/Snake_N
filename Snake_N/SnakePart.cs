using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static Snake_N.GameWindow;

public class SnakePart
{
    public UIElement UiElement { get; set; }
    public Point Position { get; set; }
    public bool IsHead { get; set; }
    const int SnakeSquareSize = 20;
    private SolidColorBrush snakeBodyBrush = Brushes.Green;
    private SolidColorBrush snakeHeadBrush = Brushes.YellowGreen;
    private List<SnakePart> snakeParts = new List<SnakePart>();

    //private void DrawSnake()
    //{
    //    foreach (SnakePart snakePart in snakeParts)
    //    {
    //        if (snakePart.UiElement == null)
    //        {
    //            snakePart.UiElement = new Rectangle()
    //            {
    //                Width = SnakeSquareSize,
    //                Height = SnakeSquareSize,
    //                Fill = (snakePart.IsHead ? snakeHeadBrush : snakeBodyBrush)
    //            };
    //            Pole.Children.Add(snakePart.UiElement);
    //            Canvas.SetTop(snakePart.UiElement, snakePart.Position.Y);
    //            Canvas.SetLeft(snakePart.UiElement, snakePart.Position.X);
    //        }
    //    }
    //}
    public enum SnakeDirection { Left, Right, Up, Down };
    private SnakeDirection snakeDirection = SnakeDirection.Right;
    private int snakeLength;

    //private void MoveSnake()
    //{
    //    // Remove the last part of the snake, in preparation of the new part added below  
    //    while (snakeParts.Count >= snakeLength)
    //    {
    //        Pole.Children.Remove(snakeParts[0].UiElement);
    //        snakeParts.RemoveAt(0);
    //    }
    //    // Next up, we'll add a new element to the snake, which will be the (new) head  
    //    // Therefore, we mark all existing parts as non-head (body) elements and then  
    //    // we make sure that they use the body brush  
    //    foreach (SnakePart snakePart in snakeParts)
    //    {
    //        (snakePart.UiElement as Rectangle).Fill = snakeBodyBrush;
    //        snakePart.IsHead = false;
    //    }

    //    // Determine in which direction to expand the snake, based on the current direction  
    //    SnakePart snakeHead = snakeParts[snakeParts.Count - 1];
    //    double nextX = snakeHead.Position.X;
    //    double nextY = snakeHead.Position.Y;
    //    switch (snakeDirection)
    //    {
    //        case SnakeDirection.Left:
    //            nextX -= SnakeSquareSize;
    //            break;
    //        case SnakeDirection.Right:
    //            nextX += SnakeSquareSize;
    //            break;
    //        case SnakeDirection.Up:
    //            nextY -= SnakeSquareSize;
    //            break;
    //        case SnakeDirection.Down:
    //            nextY += SnakeSquareSize;
    //            break;
    //    }

    //    // Now add the new head part to our list of snake parts...  
    //    snakeParts.Add(new SnakePart()
    //    {
    //        Position = new Point(nextX, nextY),
    //        IsHead = true
    //    });
    //    //... and then have it drawn!  
    //    DrawSnake();
    //    // We'll get to this later...  
    //    //DoCollisionCheck();          
    //}
}
