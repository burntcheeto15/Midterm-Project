using System;
using System.Collections.Generic;
using System.IO;

public static class DataBaseReader
{
    public static List<object> LoadDatabase(string filePath)
    {
        var database = new List<object>();
        foreach (var line in File.ReadLines(filePath))
        {
            var columns = line.Split(',');
            if (columns[0].Trim('\"') == "track")
            {
                var track = new Track
                {
                    Title = columns[1].Trim('\"'),
                    Artist = columns[2].Trim('\"'),
                    Album = columns[3].Trim('\"'),
                    Year = int.Parse(columns[4]),
                    Duration = double.Parse(columns[5]),
                    Rating = decimal.Parse(columns[6])
                };
                database.Add(track);
            }
            else if (columns[0].Trim('\"') == "audiobook")
            {
                var audiobook = new AudioBook
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
            else if (columns[0].Trim('\"') == "podcast")
            {
                var podcast = new Podcast
                {
                    Title = columns[1].Trim('\"'),
                    Creator = columns[2].Trim('\"'),
                    Year = int.Parse(columns[3]),
                    Duration = double.Parse(columns[4]),
                    ThumbsUp = int.Parse(columns[5]),
                    ThumbsDown = int.Parse(columns[6])
                };
                database.Add(podcast);
            }
        }
        return database;
    }
}
