using System;
public class Track
{
    public string Title
    {
        get { };
        set { };
    }

    public string Artist
    {
        get { };
        set { };
    }
    public string Album
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
    public decimal Rating
    {
        get { };
        set { };
    }

    public override string ToString()
    {
        return $"Track: {Title} by {Artist}, Album: {Album}, Year: ({Year}), Duration: {Duration} Minutes, Rating: {Rating} Stars";
    }
}

