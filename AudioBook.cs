using System;
public class AudioBook
{

    private string title;
    private string creator;
    private int year;
    private double duration;
    private int thumbsup;
    private int thumbsub;

    
    public AudioBook(string title, string creator, int year, double duration, int thumbsup, int thumbsdown)
    {
        Title = title;
        Author = creator;
        Year = year;
        Duration = duration;
        ThumbsUp = thumbsup;
        ThumbsDown = thumbsdown;
        
    }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public double Duration { get; set; }
    public int ThumbsUp { get; set; }
    public int ThumbsDown { get; set; }

    public override string ToString()
    {
        return $"AudioBook: {Title} by {Author}, Year: ({Year}), Duration: {Duration} Minutes, Rating: {ThumbsUp} Thumbs Up / {ThumbsDown} Thumbs Down";
    }
    
    public string DataBaseWriter()
    {
        return $"AudioBook:, {Title}, {Author}, {Year}, {Duration}, {ThumbsUp}, {ThumbsDown}";
    }
}

