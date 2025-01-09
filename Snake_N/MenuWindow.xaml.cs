using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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

namespace Snake_N
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public string PlayerName;
        public MenuWindow()
        {
            SplashScreen splash = new SplashScreen("Reso/logo.png");
            splash.Show(true);
            Thread.Sleep(1000);
            InitializeComponent();
            PlayerName = Player.Text;
        }

        private void ContinueGameButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerName = Player.Text;
            MessageBox.Show("В оригинале через данный элемент меню реализовывалась функция паузы: то есть ты выходил в меню и потом мог вернуться. После перезагрузки устройства прогресс не сохранялся. Данная кнопка находится здесь из уважения к оригиналу", "Snake_N", MessageBoxButton.OK, MessageBoxImage.Information);
            GameWindow window = new GameWindow(PlayerName);
            window.Show();
            Close();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerName = Player.Text;
            GameWindow window = new GameWindow(PlayerName);
            window.Show();
            Close();
        }

        private void LevelButton_Click(object sender, RoutedEventArgs e)
        {
            LevelWindow window = new LevelWindow();
            window.Show();
            Close();
        }

        private void MazesButton_Click(object sender, RoutedEventArgs e)
        {
            MazesWindow window = new MazesWindow();
            window.Show();
            Close();
        }

        private void RecordsButton_Click(object sender, RoutedEventArgs e)
        {
            RecordsWindow window = new RecordsWindow();
            window.Show();
            Close();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Удлинить змею, направляя её к пище, используя стрелки. Змею нельзя остановить или вернуть назад. Стены и хвост не задевать", "Snake_N", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
            => Close();
    }
}
