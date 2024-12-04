using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using WMPLib;
using System;

namespace FinalProject
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
            string search = "";
            while (string.IsNullOrEmpty(search))
            {
                Console.Write("Enter the name of the artist/author/creator: ");
                search = Console.ReadLine().Trim().ToLower();
                if (string.IsNullOrEmpty(search))
                {
                    Console.WriteLine("Input cannot be empty. Please enter a valid name.");
                }
            }

            var results = database.Where(item =>
            {
                if (item is Track track)
                    return track.Artist.ToLower().Contains(search);
                if (item is AudioBook audiobook)
                    return audiobook.Author.ToLower().Contains(search);
                if (item is Podcast podcast)
                    return podcast.Creator.ToLower().Contains(search);
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
            var sortedList = database.OrderByDescending(item =>
            {
                if (item is Track track)
                    return track.Rating;
                if (item is AudioBook audiobook)
                    return audiobook.ThumbsUp;
                if (item is Podcast podcast)
                    return podcast.Rating;
                return 0;
            }).ToList();

            Console.WriteLine("\nSorted by Rating (Descending):");
            foreach (var item in sortedList)
            {
                Console.WriteLine(item.ToString());
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
            string choice = Console.ReadLine();
            while (choice != "1" && choice != "2" && choice != "3")
            {
                Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                choice = Console.ReadLine();
            }

            string title, artist, album, sound;
            int year, dur, thumbsUp, thumbsDown, pcRating;
            double rating;

            switch (choice)
            {
                case "1":
                    title = ValidStringInput("What's the Title?");
                    artist = ValidStringInput("By Who?");
                    album = ValidStringInput("In what Album?");
                    year = ValidIntegerInput("What Year was it Made?", 1900, DateTime.Now.Year);
                    dur = ValidIntegerInput("Duration in Minutes?", 1, int.MaxValue);
                    rating = ValidDoubleInput("Rating (1-5)?", 0, 5);
                    sound = ValidStringInput("Mp3 Name?");

                    database.Add(new Track(title, artist, album, year, dur, rating, sound));
                    writer.WriteLine($"Track:,{title},{artist},{album},{year},{dur},{rating},{sound}");
                    Console.WriteLine("Successfully Added!!!");
                    break;

                case "2":
                    title = ValidStringInput("What's the Title?");
                    artist = ValidStringInput("By Who?");
                    year = ValidIntegerInput("What Year was it Made?", 1900, DateTime.Now.Year);
                    dur = ValidIntegerInput("Duration in Minutes?", 1, int.MaxValue);
                    thumbsUp = ValidIntegerInput("How Many Thumbs Up? :)", 0, int.MaxValue);
                    thumbsDown = ValidIntegerInput("How Many Thumbs Down? :(", 0, int.MaxValue);

                    database.Add(new AudioBook(title, artist, year, dur, thumbsUp, thumbsDown));
                    writer.WriteLine($"AudioBook:,{title},{artist},{year},{dur},{thumbsUp},{thumbsDown}");
                    Console.WriteLine("Successfully Added!!!");
                    break;

                case "3":
                    title = ValidStringInput("What's the Title?");
                    artist = ValidStringInput("By Who?");
                    year = ValidIntegerInput("What Year was it Made?", 1900, DateTime.Now.Year);
                    dur = ValidIntegerInput("Duration in Minutes?", 1, int.MaxValue);
                    pcRating = ValidIntegerInput("Rating (1-10)?", 0, 10);

                    database.Add(new Podcast(title, artist, year, dur, pcRating));
                    writer.WriteLine($"Podcast:,{title},{artist},{year},{dur},{pcRating}");
                    Console.WriteLine("Successfully Added!!!");
                    break;
            }

        }

        static void RemoveEntry(List<object> database)
        {
            Console.Clear();
            for (int i = 1; i <= database.Count; i++)
            {
                Console.WriteLine($"{i}: {database[i - 1]}");
            }

            int indexToRemove = -1;
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine("What would you like to remove? (Enter the number of the entry)");

                if (int.TryParse(Console.ReadLine(), out indexToRemove))
                {
                    if (indexToRemove >= 1 && indexToRemove <= database.Count)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid index. Please enter a valid number corresponding to the entries.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
            database.RemoveAt(indexToRemove - 1);
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
                            update.WriteLine($"Track:, {track.Title}, {track.Artist}, {track.Album}, {track.Year}, {track.Duration}, {track.Rating}, {track.Sound}");
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
            int trackToPlay = -1;
            bool validTrackChoice = false;

            while (!validTrackChoice)
            {
                Console.WriteLine("What would you like to Play? (Enter the number of the track)");

                if (int.TryParse(Console.ReadLine(), out trackToPlay))
                {
                    if (trackToPlay >= 1 && trackToPlay < j)
                    {
                        validTrackChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a number corresponding to the available tracks.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid track number.");
                }
            }

            object playing = database[trackToPlay - 1];
            if (playing is Track t)
            {
                sound.URL = t.Sound;
                Console.WriteLine($"Playing {t.Title} by {t.Artist}");
            }
            sound.controls.play();

            string stopInput = "";
            while (stopInput != "1")
            {
                Console.WriteLine("Press 1 to stop the audio.");
                stopInput = Console.ReadLine();

                if (stopInput != "1")
                {
                    Console.WriteLine("Invalid input. Please press 1 to stop.");
                }
            }
            sound.controls.stop();
            Console.WriteLine("Audio stopped.");
        }

        static string ValidStringInput(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine($"Input cannot be empty. Please enter a valid value.");
                input = Console.ReadLine();
            }
            return input;
        }

        static int ValidIntegerInput(string prompt, int minValue, int maxValue)
        {
            int input;
            Console.WriteLine(prompt);
            while (!int.TryParse(Console.ReadLine(), out input) || input < minValue || input > maxValue)
            {
                Console.WriteLine($"Invalid input ({input}). Please enter a valid input between {minValue} and {maxValue}.");
            }
            return input;
        }

        static double ValidDoubleInput(string prompt, double minValue, double maxValue)
        {
            double input;
            Console.WriteLine(prompt);
            while (!double.TryParse(Console.ReadLine(), out input) || input < minValue || input > maxValue)
            {
                Console.WriteLine($"Invalid input ({input}). Please enter a valid input between {minValue} and {maxValue}.");
            }
            return input;
        }

    }
}