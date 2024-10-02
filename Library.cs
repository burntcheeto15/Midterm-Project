using System;

namespace Midterm_Project
{
    public class Library
    {
        
        private static void Main()
        {


            const string file = "Library Books.csv";
            if (!File.Exists(file))
            {
                Console.WriteLine("File doesn't Exist");
            }
            int lineCount = GetLineCount(file);

            Book[] library = new Book[lineCount - 1];
            try
            {
                using StreamReader reader = new StreamReader(file);
                reader.ReadLine();
                for (int i = 0; i < lineCount; i++)
                {
                    string line = reader.ReadLine();
                    string[] col = line.Split(",");

                    string author = col[0];
                    string title = col[1];
                    int yearPublished = int.Parse(col[2]);
                    string genre = col[3];
                    int pageLength = int.Parse(col[4]);
                    bool checkedOut = bool.Parse(col[5]);

                    library[i] = new Book(title, author, genre, pageLength, yearPublished, checkedOut);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error reading");
                Console.WriteLine(ex);
            }

            Console.WriteLine("Books in the Library");
            for (int i = 0; i < lineCount - 1; i++)
            {
                Console.WriteLine(library[i]);
            }
        }

        private static int GetLineCount(string file)
        {
            using StreamReader reader = new StreamReader(file);
            int lines = 0;
            while (!reader.EndOfStream)
            {
                reader.ReadLine();
                lines++;
            }
            return lines;
        }


    }
}
