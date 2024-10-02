using System;
using System.Reflection;

namespace Midterm_Project
{
    public class Book
    {
        private string title;
        private string author;
        private string genre;
        private int pageLength;
        private int yearPublished;
        private bool checkedOut;
        public Book(string title, string author, string genre, int pageLength, int yearPublished, bool checkedOut)
        {
            title = Title;
            author = Author;
            genre = Genre;
            pageLength = PageLength;
            yearPublished = YearPublished;
            checkedOut = CheckedOut;
        }

        public override string ToString()
        {
            return $"{Title} by {Author} ({YearPublished}) | Genre: {Genre} Page Count: {PageLength} | Checked Out: {CheckedOut}";
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Title cannot be null or empty");
                }
                title = value;
            }
        }

        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Author's Name cannot be null or empty");
                }
                author = value;
            }
        }

        public string Genre
        {
            get
            {
                return genre;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Genre cannot be null or empty");
                }
                genre = value;
            }
        }

        public int PageLength
        {
            get
            {
                return pageLength;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Page Number cannot be 0 or less");
                }
                pageLength = value;
            }
        }

        public int YearPublished
        {
            get
            {
                return yearPublished;
            }
            set
            {
                if (value <= 1455 || value > DateTime.Now.Year)
                {
                    throw new Exception("Year cannot be before 868 or in the future");
                }
                yearPublished = value;
            }
        }

        public bool CheckedOut
        {
            get
            {
                return checkedOut;
            }
            set
            {
                checkedOut = value;
            }
        }
    }
}
