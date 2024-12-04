using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public static class DataBaseReader
{
    public static List<object> LoadDatabase(string filePath)
    {
        var database = new List<object>();
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File does not exist", filePath);
        }
        foreach (var line in File.ReadLines(filePath))
        {
            var columns = line.Split(',');
            if (columns[0].Trim('\"') == "Track:")
            {
                var track = new Track("", "", "", 0, 0, 0, "")
                {
                    Title = columns[1].Trim('\"'),
                    Artist = columns[2].Trim('\"'),
                    Album = columns[3].Trim('\"'),
                    Year = int.Parse(columns[4]),
                    Duration = double.Parse(columns[5]),
                    Rating = double.Parse(columns[6]),
                    Sound = columns[7]
                };
                database.Add(track);
            }
            else if (columns[0].Trim('\"') == "AudioBook:")
            {
                var audiobook = new AudioBook("","",0,0,0,0)
                {
                    Title = columns[1].Trim('\"'),
                    Author = columns[2].Trim('\"'),
                    Year = int.Parse(columns[3]),
                    Duration = double.Parse(columns[4]),
                    ThumbsUp = int.Parse(columns[5]),
                    ThumbsDown = int.Parse(columns[6])
                };
                database.Add(audiobook);
            }
            else if (columns[0].Trim('\"') == "Podcast:")
            {
                var podcast = new Podcast("", "", 0, 0, 0)
                {
                    Title = columns[1].Trim('\"'),
                    Creator = columns[2].Trim('\"'),
                    Year = int.Parse(columns[3]),
                    Duration = double.Parse(columns[4]),
                    Rating = int.Parse(columns[5]),
                    
                };
                database.Add(podcast);
            }
        }
        return database;
    }
}
