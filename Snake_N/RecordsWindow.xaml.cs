using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace Snake_N
{
    /// <summary>
    /// Логика взаимодействия для RecordsWindow.xaml
    /// </summary>
    public partial class RecordsWindow : Window
    {
        private readonly SnakeHighscore HighscoreList;
        public RecordsWindow()
        {
            InitializeComponent();
            SnakeHighscore.LoadHighscoreList();
            List<SnakeHighscore> records;
            
                //records.Add(new SnakeHighscore { PlayerName = playerName, Score = scores });
            //RecordsDataGrid.ItemsSource = records;
        
        
        }

    }
}
