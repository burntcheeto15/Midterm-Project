using System.Collections.Generic;
using System.Linq;

namespace Midterm_Project
{
    public class Final_Project
    {
        public static void Main(string[] args)
        {
            var database = DataBaseReader.LoadDatabase("File.txt");
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Media Database!");
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Print all entries");
                Console.WriteLine("2. Print only the Tracks");
                Console.WriteLine("3. Print only the Audio Books");
                Console.WriteLine("4. Print only the Podcasts");
                Console.WriteLine("5. Print entries by artist/author/creator");
                Console.WriteLine("6. Sort entries by rating (Descending)");
                Console.WriteLine("7. Sort entries by year (Ascending)");
                Console.WriteLine("8. Sort entries by title (Alphabetically)");
                Console.WriteLine("9. Print entries from a given year onward");
                Console.WriteLine("0. Quit");
                Console.Write("\nEnter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PrintAllEntries(database);
                        break;
                    case "2":
                        PrintByType(database, "track");
                        break;
                    case "3":
                        PrintByType(database, "audiobook");
                        break;
                    case "4":
                        PrintByType(database, "podcast");
                        break;
                    case "5":
                        SearchByCreator(database);
                        break;
                    case "6":
                        SortByRatingDescending(database);
                        break;
                    case "7":
                        SortByYearAscending(database);
                        break;
                    case "8":
                        SortByTitleAlphabetical(database);
                        break;
                    case "9":
                        PrintEntriesFromYear(database);
                        break;
                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress Enter To Return");
                Console.ReadLine();
            }
        }

        //Part 3.1 - Print out all entries in the database
        static void PrintAllEntries(List<object> database)
        {
            Console.WriteLine("\nAll Entries:");
            foreach (var item in database)
            {
                Console.WriteLine(item.ToString());
            }
        }

        //Part 3.2, 3.3, 3.4 - Print out only a certain type 
        static void PrintByType(List<object> database, string type)
        {
            var filteredList = database.Where(item => item.GetType().Name.ToLower() == type.ToLower()).ToList();
            Console.WriteLine($"\n{type} Entries:");
            foreach (var item in filteredList)
            {
                Console.WriteLine(item.ToString());
            }
        }

        //Part 3.5 - Print out all entries with a given artist/author/creator (not case sensitive)
        static void SearchByCreator(List<object> database)
        {
            Console.Write("Enter the name of the artist/author/creator: ");
            string searchTerm = Console.ReadLine().ToLower();

            var results = database.Where(item =>
            {
                if (item is Track track)
                    return track.Artist.ToLower().Contains(searchTerm);
                if (item is AudioBook audiobook)
                    return audiobook.Author.ToLower().Contains(searchTerm);
                if (item is Podcast podcast)
                    return podcast.Creator.ToLower().Contains(searchTerm);
                return false;
            }).ToList();

            Console.WriteLine("\nSearch Results:");
            foreach (var result in results)
            {
                Console.WriteLine(result.ToString());
            }
        }

        //Part 3.6 - Sort all entries by their rating in descending order
        static void SortByRatingDescending(List<object> database)
        {
            
            // Use LINQ to sort by rating, different for each type of object
            var sortedList = database.OrderByDescending(item =>
            {
                if (item is Track track)
                    return track.Rating;  // Track uses Rating (decimal)
                if (item is AudioBook audiobook)
                    return audiobook.ThumbsUp;  // AudioBook uses thumbs up/down for rating
                if (item is Podcast podcast)
                    return podcast.ThumbsUp;  // Podcast uses ThumbsUp as the rating
                return 0;
            }).ToList();  // Convert back to List after sorting

            // Print the sorted entries
            Console.WriteLine("\nSorted by Rating (Descending):");
            foreach (var item in sortedList)
            {
                Console.WriteLine(item.ToString());  // Print each entry in the sorted list
            }
            
        }

        //Part 3.7 - Sort all entries in ascending order based on their year
        static void SortByYearAscending(List<object> database)
        {
            var sortedList = database.OrderBy(item =>
            {
                if (item is Track track) return track.Year;
                if (item is AudioBook audiobook) return audiobook.Year;
                if (item is Podcast podcast) return podcast.Year;
                return 0;
            }).ToList();

            Console.WriteLine("\nSorted by Year (Ascending):");
            foreach (var item in sortedList)
            {
                Console.WriteLine(item.ToString());
            }
        }

        //Part 3.8 - Sort all entries by the lexicographical order of their title
        static void SortByTitleAlphabetical(List<object> database)
        {
            var sortedList = database.OrderBy(item =>
            {
                if (item is Track track) return track.Title;
                if (item is AudioBook audiobook) return audiobook.Title;
                if (item is Podcast podcast) return podcast.Title;
                return "";
            }).ToList();

            Console.WriteLine("\nSorted by Title (Alphabetically):");
            foreach (var item in sortedList)
            {
                Console.WriteLine(item.ToString());
            }
        }

        //Part 3.9 - Print out all entries released on or after a given year
        static void PrintEntriesFromYear(List<object> database)
        {
            Console.Write("Enter the year: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                var results = database.Where(item =>
                {
                    if (item is Track track) return track.Year >= year;
                    if (item is AudioBook audiobook) return audiobook.Year >= year;
                    if (item is Podcast podcast) return podcast.Year >= year;
                    return false;
                }).ToList();

                Console.WriteLine($"\nEntries from {year} onward:");
                foreach (var result in results)
                {
                    Console.WriteLine(result.ToString());
                }
            }
            else
            {
                Console.WriteLine("Invalid year entered.");
            }
        }
    }
}