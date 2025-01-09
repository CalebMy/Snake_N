using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace Snake_N
{
    /// <summary>
    /// Логика взаимодействия для RecordsWindow.xaml
    /// </summary>
    public partial class RecordsWindow : Window
    {
        //private readonly SnakeHighscore HighscoreList;
        public RecordsWindow()
        {
            InitializeComponent();
            SnakeHighscore.LoadHighscoreList();
            //List<SnakeHighscore> records;
            //records.Add(new SnakeHighscore { PlayerName = playerName, Score = scores });
            //RecordsDataGrid.ItemsSource = records;
            //XElement ScoreList = XElement.Load("snake_highscorelist.xml");
            //RecordsDataGrid.DataContext = ScoreList;
            RecordsDataGrid.ItemsSource = SnakeHighscore.HighscoreList.OrderByDescending(x => x.Score).ToList();
        }

    }
}
