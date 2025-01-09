using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Snake_N
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public static string p_name;
        public GameWindow(string name)
        {
            p_name = name;
            InitializeComponent();
            MessageBox.Show(p_name, "Snake_N", MessageBoxButton.OK, MessageBoxImage.Information);
            timer.Tick += new EventHandler(Timer_Tick);
            StartNewGame();
        }
        SnakePart Palka = new SnakePart(p_name);
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
            timer.Interval = TimeSpan.FromMilliseconds((int)SnakePart.SnakeStartSpeed.Five);
            
            Palka.DrawSnake(Pole);
            Palka.DrawSnakeFood(Pole);
     
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
                    case Key.Escape:
                        Palka.EndGame(timer);
                        MenuWindow window = new MenuWindow();
                        window.Show();
                        Close();
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
                    case Key.Escape:
                        Palka.EndGame(timer);
                        MenuWindow window = new MenuWindow();
                        window.Show();
                        Close();
                        break;
                }
                    
            }
            if (Palka.snakeDirection != originalSnakeDirection)
                Palka.MoveSnake(timer, Score, Pole);
        }

        //public GameWindow()
        //{
        //    InitializeComponent();
        //    timer.Tick += new EventHandler(timer_Tick);
        //    StartNewGame();
        //}

        private void Timer_Tick(object sender, EventArgs e)
        {
            Palka.MoveSnake(timer, Score, Pole);
        }
    }
}
