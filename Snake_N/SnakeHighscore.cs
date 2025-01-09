using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System;

public class SnakeHighscore : ISnakeHighscore
{
    public string PlayerName { get; set; }
    public int Score { get; set; }

    public static List<SnakeHighscore> HighscoreList = new List<SnakeHighscore>();

    static SnakeHighscore()
    {
        LoadHighscoreList();
    }

    public const int MaxHighscoreListEntryCount = 10;

    public static void LoadHighscoreList()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "snake_highscorelist.xml");
        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<SnakeHighscore>), new XmlRootAttribute("SnakeHighscores"));
            using (FileStream reader = new FileStream(filePath, FileMode.Open))
            {
                try
                {
                    HighscoreList = (List<SnakeHighscore>)serializer.Deserialize(reader);
                }
                catch (Exception ex)
                {
                    HighscoreList = new List<SnakeHighscore>();
                    // Log the exception
                }
            }
        }
        else
        {
            HighscoreList = new List<SnakeHighscore>();
        }
    }

    public static void SaveHighscoreList()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "snake_highscorelist.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(List<SnakeHighscore>), new XmlRootAttribute("SnakeHighscores"));
        using (FileStream writer = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(writer, HighscoreList);
        }
    }

    public static void HighscoreUpdate(int currentScore, string _name)
    {
        // Check if the player already has an entry
        var existingEntry = HighscoreList.FirstOrDefault(x => x.PlayerName == _name);

        int newIndex = 0;
        bool insertNewScore = false;

        if (HighscoreList.Any())
        {
            var sortedScores = HighscoreList.OrderByDescending(x => x.Score).ToList();
            for (int i = 0; i < sortedScores.Count; i++)
            {
                var score = sortedScores[i];
                if (currentScore > score.Score)
                {
                    newIndex = HighscoreList.IndexOf(score);
                    insertNewScore = true;
                    break;
                }
                else if (score.PlayerName == _name)
                {
                    // If the new score is less than or equal to the existing score, don't insert
                    insertNewScore = false;
                    break;
                }
            }
            if (newIndex >= HighscoreList.Count)
            {
                newIndex = HighscoreList.Count;
                insertNewScore = true;
            }
        }
        else
        {
            newIndex = 0;
            insertNewScore = true;
        }

        if (insertNewScore)
        {
            // If the player already has an entry, remove it
            if (existingEntry != null)
            {
                HighscoreList.Remove(existingEntry);
            }
            HighscoreList.Insert(newIndex, new SnakeHighscore()
            {
                PlayerName = _name,
                Score = currentScore
            });
            // Ensure the list doesn't exceed the maximum number of entries
            while (HighscoreList.Count > MaxHighscoreListEntryCount)
                HighscoreList.RemoveAt(MaxHighscoreListEntryCount);
            SaveHighscoreList();
        }
    }
    //public static void HighscoreUpdate(int currentScore, string _name)
    //{
    //    int newIndex = 0;
    //    if (HighscoreList.Any())
    //    {
    //        var sortedScores = HighscoreList.OrderByDescending(x => x.Score).ToList();
    //        for (int i = 0; i < sortedScores.Count; i++)
    //        {
    //            var score = sortedScores[i];
    //            if (currentScore > score.Score)
    //            {
    //                newIndex = HighscoreList.IndexOf(score);
    //                break;
    //            }
    //        }
    //        if (newIndex >= HighscoreList.Count)
    //        {
    //            newIndex = HighscoreList.Count;
    //        }
    //    }
    //    else
    //    {
    //        newIndex = 0;
    //    }
    //    HighscoreList.Insert(newIndex, new SnakeHighscore()
    //    {
    //        PlayerName = _name,
    //        Score = currentScore
    //    });
    //    while (HighscoreList.Count > MaxHighscoreListEntryCount)
    //        HighscoreList.RemoveAt(MaxHighscoreListEntryCount);
    //    SaveHighscoreList();
    //}
}

//public class SnakeHighscore : ISnakeHighscore
//{
//    public string PlayerName { get; set; }
//    public int Score { get; set; }
//    //public static ObservableCollection<SnakeHighscore> HighscoreList{get; set; } = new ObservableCollection<SnakeHighscore>();
//    public static List<SnakeHighscore> HighscoreList = new List<SnakeHighscore>();
//    public const int MaxHighscoreListEntryCount = 10;

//    public static void LoadHighscoreList()
//    {
//        if (File.Exists("snake_highscorelist.xml"))
//        {
//            XmlSerializer serializer = new XmlSerializer(typeof(List<SnakeHighscore>), new XmlRootAttribute("SnakeHighscores"));
//            using (FileStream reader = new FileStream("snake_highscorelist.xml", FileMode.Open))
//            {
//                HighscoreList = (List<SnakeHighscore>)serializer.Deserialize(reader);
//            }
//        }
//    }

//    public static void SaveHighscoreList()
//    {
//        XmlSerializer serializer = new XmlSerializer(typeof(List<SnakeHighscore>), new XmlRootAttribute("SnakeHighscores"));
//        using (FileStream writer = new FileStream("snake_highscorelist.xml", FileMode.Create))
//        {
//            serializer.Serialize(writer, HighscoreList);
//        }
//    }
//    //public static void LoadHighscoreList()
//    //{
//    //    if (File.Exists("snake_highscorelist.xml"))
//    //    {
//    //        XmlSerializer serializer = new XmlSerializer(typeof(List<SnakeHighscore>));
//    //        using (FileStream reader = new FileStream("snake_highscorelist.xml", FileMode.Open))
//    //        {
//    //            HighscoreList = (List<SnakeHighscore>)serializer.Deserialize(reader);
//    //        }
//    //    }
//    //}
//    //public static void SaveHighscoreList()
//    //{
//    //    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<SnakeHighscore>));
//    //    using (FileStream writer = new FileStream("snake_highscorelist.xml", FileMode.Create))
//    //    {

//    //        serializer.Serialize(writer, HighscoreList);
//    //    }
//    //}
//    public static void HighscoreUpdate(int currentScore, string _name)
//    {
//        int newIndex = 0;
//        if ((HighscoreList.Count > 0) && (currentScore < HighscoreList.Max(x => x.Score)))
//        {
//            SnakeHighscore justAbove = HighscoreList.OrderByDescending(x => x.Score).First(x => x.Score >= currentScore);
//            if (justAbove != null)
//                newIndex = HighscoreList.IndexOf(justAbove) + 1;
//        }
//        HighscoreList.Insert(newIndex, new SnakeHighscore()
//        {
//            PlayerName = _name,
//            Score = currentScore
//        });
//        while (HighscoreList.Count > MaxHighscoreListEntryCount)
//            HighscoreList.RemoveAt(MaxHighscoreListEntryCount - 1);
//    }

//    static SnakeHighscore()
//    {
//        LoadHighscoreList();
//    }
//}
