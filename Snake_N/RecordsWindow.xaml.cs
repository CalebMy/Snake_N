using System;
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
            // Timer generating random BestLap double values from 1.0 to 4.0 every 5 seconds
            DispatcherTimer randomlyUpdateDriverBestLapTimer = new DispatcherTimer();
            randomlyUpdateDriverBestLapTimer.Interval = TimeSpan.FromSeconds(5);
            randomlyUpdateDriverBestLapTimer.Tick += RandomlyUpdateDriverBestLapTimerOnTick;

            SnakeHighscore = new SnakeHighscore();

            Driver driver = new Driver { BestLap = 1.2, Name = "Meyer", StartNr = 1 };
            driverViewModel.DriverList.Add(driver);

            driver = new Driver { BestLap = 1.4, Name = "Sand", StartNr = 2 };
            driverViewModel.HighscoreList.Add(driver);

            driver = new Driver { BestLap = 1.5, Name = "Huntelaar", StartNr = 3 };
            driverViewModel.HighscoreList.Add(driver);

            this.DataContext = driverViewModel;

            InitializeComponent();

            randomlyUpdateDriverBestLapTimer.Start();
        }
        private void RandomlyUpdateDriverBestLapTimerOnTick(object sender, EventArgs eventArgs)
        {
            // Important to declare Random object not in the loop because it will generate the same random double for each driver
            Random random = new Random();

            foreach (var driver in driverViewModel.DriverList)
            {
                // Random double from 1.0 - 4.0 (Source code from https://stackoverflow.com/questions/1064901/random-number-between-2-double-numbers)
                driver.BestLap = random.NextDouble() * (4.0 - 1.0) + 1.0;
            }
        }

    }
}
