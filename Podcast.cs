using System;
public class Podcast
{
    public string Title
    {
        get { };
        set { };
    }

    public string Creator
    {
        get { };
        set { };
    }

    public int Year
    {
        get { };
        set { };
    }

    public double Duration
    {
        get { };
        set { };
    }

    public int ThumbsUp
    {
        get { };
        set { };
    }

    public int ThumbsDown
    {
        get { };
        set { };
    }

    public override string ToString()
    {
        return $"Podcast: {Title} by {Creator}, Year: ({Year}), Duration: {Duration} Minutes, Rating: {ThumbsUp} Thumbs Up / {ThumbsDown} Thumbs Down";
    }
}

