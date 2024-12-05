using System;
using System.ComponentModel.Design;
public class Podcast
{
    
    private string title;
    private string creator;
    private int year;
    private double duration;
    private int rating;

    
    
    public Podcast(string title, string creator, int year, double duration, int rating)
    {
        Title = title;
        Creator = creator;
        Year = year;
        Duration = duration;
        Rating = rating;
    }
    public string Title { get; set; }
    public string Creator { get; set; }
    public int Year { get; set; }
    public double Duration { get; set; }
    
    public int Rating { get; set; }

    public override string ToString()
    {
        return $"Podcast: {Title} by {Creator}, Year: ({Year}), Duration: {Duration} Minutes, Rating: {Rating}/10";
    }

    public string DataBaseWriter()
    {
        return $"Podcast:, {Title}, {Creator}, {Year}, {Duration}, {Rating}";
    }
}

