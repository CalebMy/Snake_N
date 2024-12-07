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
}
