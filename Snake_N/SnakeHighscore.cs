using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
public class SnakeHighscore : ISnakeHighscore
{
    public string PlayerName { get; set; }
    public int Score { get; set; }
    public static ObservableCollection<SnakeHighscore> HighscoreList
    {get; set; } = new ObservableCollection<SnakeHighscore>();
    public const int MaxHighscoreListEntryCount = 10;
    public static void LoadHighscoreList()
    {
        if (File.Exists("snake_highscorelist.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<SnakeHighscore>));
            using (Stream reader = new FileStream("snake_highscorelist.xml", FileMode.Open))
            {
                List<SnakeHighscore> tempList = (List<SnakeHighscore>)serializer.Deserialize(reader);
                HighscoreList.Clear();
                foreach (var item in tempList.OrderByDescending(x => x.Score))
                HighscoreList.Add(item);
            }
        }
    }
    public static void SaveHighscoreList()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<SnakeHighscore>));
        using (Stream writer = new FileStream("snake_highscorelist.xml", FileMode.Create))
        {
            serializer.Serialize(writer, HighscoreList);
        }
    }
    public static void HighscoreUpdate(int currentScore, string _name)
    {
        int newIndex = 0;
        if ((HighscoreList.Count > 0) && (currentScore < HighscoreList.Max(x => x.Score)))
        {
            SnakeHighscore justAbove = HighscoreList.OrderByDescending(x => x.Score).First(x => x.Score >= currentScore);
            if (justAbove != null)
                newIndex = HighscoreList.IndexOf(justAbove) + 1;
        }
        HighscoreList.Insert(newIndex, new SnakeHighscore()
        {
            PlayerName = _name,
            Score = currentScore
        });
        while (HighscoreList.Count > MaxHighscoreListEntryCount)
            HighscoreList.RemoveAt(MaxHighscoreListEntryCount);

        SaveHighscoreList();
    }
}
