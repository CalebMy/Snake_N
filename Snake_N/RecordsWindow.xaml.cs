using System.Windows;

namespace Snake_N
{
    /// <summary>
    /// Логика взаимодействия для RecordsWindow.xaml
    /// </summary>
    public partial class RecordsWindow : Window
    {
        public RecordsWindow()
        {
            InitializeComponent();
            SnakeHighscore.LoadHighscoreList();
        }

    }
}
