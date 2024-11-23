using System;
public class AudioBook
{
    public string Title
    {
        get { };
        set { };
    }

    public string Author
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
        return $"AudioBook: {Title} by {Artist}, Year: ({Year}), Duration: {Duration} Minutes, Rating: {ThumbsUp} Thumbs Up / {ThumbsDown} Thumbs Down";
    }
}

