using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using WMPLib;

namespace Midterm_Project
{
    public class Final_Project
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var database = DataBaseReader.LoadDatabase("AudioDatabase.csv");
            
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
                Console.WriteLine("10. Add to Database");
                Console.WriteLine("11. Remove from Database");
                Console.WriteLine("12. Play Audio");
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
                    case "10":
                        AddEntry(database);
                        break;
                    case "11":
                        RemoveEntry(database);
                        break;
                    case "12":
                        PlayAudio(database);
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
                    return track.Rating;  // Track uses Rating (double)
                if (item is AudioBook audiobook)
                    return audiobook.ThumbsUp;  // AudioBook uses thumbs up/down for rating
                if (item is Podcast podcast)
                    return podcast.Rating;  // Podcast uses 1-10 as the rating
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

        //Stretch Goal 1
        static void AddEntry(List<object> database)
        {
            
            using StreamWriter writer = new StreamWriter("AudioDatabase.csv", append: true);
            
            Console.Clear();
            Console.WriteLine("\tWhat Would You Like to Add?");
            Console.WriteLine("1 - Track\n2 - Audio Book\n3 - Podcast");
            switch(Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Whats the Title?");
                    string title = Console.ReadLine();
                    Console.WriteLine("By Who?");
                    string artist = Console.ReadLine();
                    Console.WriteLine("In what Album?");
                    string album = Console.ReadLine();
                    Console.WriteLine("What Year was it Made?");
                    int year = int.Parse(Console.ReadLine());
                    Console.WriteLine("Duration in Minutes?");
                    int dur = int.Parse(Console.ReadLine());
                    Console.WriteLine("Rating?");
                    double onefive = double.Parse(Console.ReadLine());
                    Console.WriteLine("Mp3 Name?");
                    string sound = Console.ReadLine();
                    database.Add(new Track(title, artist, album, year, dur, onefive, sound));
                    writer.WriteLine($"Track:,{title},{artist},{album},{year},{dur},{onefive},{sound}");
                    break;
                case "2":
                    Console.WriteLine("Whats the Title?");
                    title = Console.ReadLine();
                    Console.WriteLine("By Who?");
                    artist = Console.ReadLine();
                    Console.WriteLine("What Year was it Made?");
                    year = int.Parse(Console.ReadLine());
                    Console.WriteLine("Duration in Minutes?");
                    dur = int.Parse(Console.ReadLine());
                    Console.WriteLine("Rating?\nHow Many Thumbs Up? :)");
                    int thumbsUp = int.Parse(Console.ReadLine());
                    Console.WriteLine("How Many Thumbs Down? :(");
                    int thumbsDown = int.Parse(Console.ReadLine());
                    database.Add(new AudioBook(title, artist, year, dur, thumbsUp, thumbsDown));
                    writer.WriteLine($"AudioBook:,{title},{artist},{year},{dur},{thumbsUp},{thumbsDown}");
                    break;
                case "3":
                    Console.WriteLine("Whats the Title?");
                    title = Console.ReadLine();
                    Console.WriteLine("By Who?");
                    artist = Console.ReadLine();
                    Console.WriteLine("What Year was it Made?");
                    year = int.Parse(Console.ReadLine());
                    Console.WriteLine("Duration in Minutes?");
                    dur = int.Parse(Console.ReadLine());
                    Console.WriteLine("Rating?");
                    int oneten = int.Parse(Console.ReadLine());
                    database.Add(new Podcast(title, artist, year, dur, oneten));
                    writer.WriteLine($"Podcast:,{title},{artist},{year},{dur},{oneten}");
                    break;
                default:
                    Console.WriteLine("Invaild; Choose 1-3");
                    break;
            }
            Console.WriteLine("Successfully Added!!!");

        }

        static void RemoveEntry(List<object> database)
        {
            Console.Clear();
            
            for (int i = 1; i <= database.Count; i++)
            {
                Console.WriteLine($"{i}: {database[i-1]}");
            }
            Console.WriteLine("What would you like to remove?");
            database.RemoveAt(int.Parse(Console.ReadLine())-1);
            Console.WriteLine("Successfully Removed!");
            DataBaseUpdate(database);
        }

        static void DataBaseUpdate(List<object> database)
        {
            using StreamWriter update = new StreamWriter("AudioDatabase.csv");
            foreach (var item in database)
            {
               
                        if (item is Track track)
                        {
                            update.WriteLine($"Track:, {track.Title}, {track.Artist}, {track.Album}, {track.Year}, {track.Duration}, {track.Rating}");
                        }

                        if (item is AudioBook book)
                        {
                            update.WriteLine($"AudioBook:, {book.Title}, {book.Author}, {book.Year}, {book.Duration}, {book.ThumbsUp}, {book.ThumbsDown}");
                        }
                       
                        if (item is Podcast podcast)
                        {
                            update.WriteLine($"Podcast:, {podcast.Title}, {podcast.Creator}, {podcast.Year}, {podcast.Duration}, {podcast.Rating}");
                        }
                        
                
            }
            
        }

        static void PlayAudio(List<object> database)
        {
            Console.Clear();
            WindowsMediaPlayer sound = new WindowsMediaPlayer();
            int j = 1;
            for (int i = 0; i < database.Count; i++)
            {
                if (database[i] is Track track)
                {
                    Console.WriteLine($"{j}: {database[i]}");
                    j++;
                }
            }
            Console.WriteLine("What would you like to Play?");
            object playing = database[int.Parse(Console.ReadLine()) - 1];
            if (playing is Track t)
            {
                sound.URL = t.Sound;
                Console.WriteLine($"Playing {t.Title} by {t.Artist}");
            }
            sound.controls.play();


            Console.WriteLine("Press 1 to Stop");
            if(Console.ReadLine() == "1")
            {
                sound.controls.stop();
            }
            
            


        }

    }
}