using System;
using System.Globalization;
public class Track
{

    private string title;
    private string creator;
    private string album;
    private int year;
    private double duration;
    private double rating;
    private string sound;

    public Track()
    { }

    public Track(string title, string creator, string album, int year, double duration, double rating, string sound)
    {
        Title = title;
        Artist = creator;
        Album = album;
        Year = year;
        Duration = duration;
        Rating = rating;
        Sound = sound;
    }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public int Year { get; set; }
    public double Duration { get; set; }
    public double Rating { get; set; }
    public string Sound { get; set; }

    public override string ToString()
    {
        return $"Track: {Title} by {Artist}, Album: {Album}, Year: ({Year}), Duration: {Duration} Minutes, Rating: {Rating} Stars";
    }

    public string DataBaseWriter()
    {
        return $"Track:, {Title}, {Artist}, {Album}, {Year}, {Duration}, {Rating}, {Sound}";
    }
}

