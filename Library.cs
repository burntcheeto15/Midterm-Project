using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

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
                for (int i = 0; i < lineCount - 1; i++)
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
            catch
            {
                Console.WriteLine("Error reading");
                return;
            }

            openMenu(library);



        }

        public static void openMenu(Book[] library)
        {
            Console.Clear();
            Console.WriteLine(" Library Database");
            Console.WriteLine("------------------");

            Console.WriteLine(" 1 - All Books");
            Console.WriteLine(" 2 - All Available Books");
            Console.WriteLine(" 3 - Search by Author");
            Console.WriteLine(" 4 - Search by Year");
            Console.WriteLine(" 5 - Search by Title");
            Console.WriteLine(" 6 - Sort Library by Pages");
            Console.WriteLine(" 7 - Check Out Book");
            Console.WriteLine(" 8 - Return Book");
            Console.WriteLine(" 9 - Sort Library by Title");
            Console.WriteLine(" 10 - Donate Book");
            Console.WriteLine(" 11 - Quit");
            Console.WriteLine();


            int menuChoice = int.Parse(Console.ReadLine());
            if (menuChoice < 12 && menuChoice > 0)
            {
                if (menuChoice == 1)
                {
                    printAll(library);
                }
                if (menuChoice == 2)
                {
                    printAvaiable(library);
                }
                if (menuChoice == 3)
                {
                    searchAuthor(library);
                }
                if (menuChoice == 4)
                {
                    searchYear(library);
                }
                if (menuChoice == 5)
                {
                    searchTitle(library);
                }
                if (menuChoice == 6)
                {
                    printByPageLength(library);
                }
                if (menuChoice == 7)
                {
                    checkoutBook(library);
                }
                if (menuChoice == 8)
                {
                    returnBook(library);
                }
                if (menuChoice == 9)
                {
                    sortByTitle(library);
                }
                if (menuChoice == 10)
                {
                    //Still Have to do This
                    //donateBook(library);
                    donateBook(library);
                }
                if (menuChoice == 11)
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                throw new Exception("Input must be 1-10");
            }
        }

        public static void printAll(Book[] library)
        {
            Console.Clear();
            const string file = "Library Books.csv";
            Console.WriteLine("Books in the Library: ");
            for (int i = 0; i < library.Length; i++)
            {
                Console.WriteLine(library[i]);
            }

            Console.WriteLine();
            Console.WriteLine(" 1 - Return");
            int returnMenu = int.Parse(Console.ReadLine());

            if (returnMenu == 1)
            {
                openMenu(library);
            }
        }

        public static void printAvaiable(Book[] library)
        {
            Console.Clear();
            const string file = "Library Books.csv";
            Console.WriteLine("Books Avaiable: ");
            for (int i = 0; i < GetLineCount(file) - 1; i++)
            {
                if (library[i].CheckedOut == false)
                {
                    Console.WriteLine(library[i]);
                }
            }

            Console.WriteLine();
            Console.WriteLine(" 1 - Return");
            int returnMenu = int.Parse(Console.ReadLine());

            if (returnMenu == 1)
            {
                openMenu(library);
            }
        }

        public static void searchAuthor(Book[] library)
        {
            Console.Clear();
            const string file = "Library Books.csv";
            Console.Write("Search Author's Last Name: ");
            string authorSearch = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine($"Books by {authorSearch}: ");
            for (int i = 0; i < GetLineCount(file) - 1; i++)
            {
                if (library[i].Author.ToLower().Equals(authorSearch.ToLower()))
                {
                    Console.WriteLine(library[i]);
                }
            }

            Console.WriteLine();
            Console.WriteLine(" 1 - Return");
            Console.WriteLine(" 2 - Search Again");
            int returnMenu = int.Parse(Console.ReadLine());

            if (returnMenu == 1)
            {
                openMenu(library);
            }
            if (returnMenu == 2)
            {
                searchAuthor(library);
            }
        }

        public static void searchYear(Book[] library)
        {
            Console.Clear();
            const string file = "Library Books.csv";
            Console.Write("Search Books Published On or After: ");
            int minYear = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine($"Books Published On or After {minYear}: ");
            for (int i = 0; i < GetLineCount(file) - 1; i++)
            {
                if (library[i].YearPublished >= minYear)
                {
                    Console.WriteLine(library[i]);
                }
            }

            Console.WriteLine();
            Console.WriteLine(" 1 - Return");
            Console.WriteLine(" 2 - Search Again");
            int returnMenu = int.Parse(Console.ReadLine());

            if (returnMenu == 1)
            {
                openMenu(library);
            }
            if (returnMenu == 2)
            {
                searchYear(library);
            }
        }

        public static void searchTitle(Book[] library)
        {
            Console.Clear();
            const string file = "Library Books.csv";
            Console.Write("Search Book's Title: ");
            string titleSearch = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine($"Books called {titleSearch}: ");
            for (int i = 0; i < GetLineCount(file) - 1; i++)
            {
                if (library[i].Title.ToLower().Equals(titleSearch.ToLower()))
                {
                    Console.WriteLine(library[i]);
                }
            }

            Console.WriteLine();
            Console.WriteLine(" 1 - Return");
            Console.WriteLine(" 2 - Search Again");
            int returnMenu = int.Parse(Console.ReadLine());

            if (returnMenu == 1)
            {
                openMenu(library);
            }
            if (returnMenu == 2)
            {
                searchTitle(library);
            }
        }


        //Need to fix this sorting too
        public static void printByPageLength(Book[] library)
        {
            Console.Clear();
            const string file = "Library Books.csv";
            Console.WriteLine("Books in the Library: ");

            var sortedBooks = library.OrderBy(book => book.PageLength);

            Console.WriteLine("Books sorted by page count:");
            foreach (var book in sortedBooks)
            {
                Console.WriteLine($"{book.PageLength} pages {book.Title} by {book.Author}, ({book.YearPublished}) | Genre: {book.Genre} | Checked Out: {book.CheckedOut}");
            }
            /*
            int minPage = library[0].PageLength;
            int indexOfMin = 0;


            for (int i = 0; i < library.Length - 1; i++)
            {
                minPage = library[i].PageLength;
                for (int j = i; j < library.Length; j++)
                {
                    if (library[j].PageLength < minPage)
                    {
                        minPage = library[j].PageLength;
                        indexOfMin = j;
                    }
                    Console.WriteLine(minPage + ":" + indexOfMin);
                }
                (library[indexOfMin], library[i]) = (library[i], library[indexOfMin]);
            }


            for (int i = 0; i < GetLineCount(file) - 1; i++)
            {
                Console.WriteLine(library[i]);
            }

            Console.WriteLine();
            */
            Console.WriteLine("\n 1 - Return");
            int returnMenu = int.Parse(Console.ReadLine());

            if (returnMenu == 1)
            {
                openMenu(library);
            }
        }

        //Need to fix this sorting
        public static void sortByTitle(Book[] library)
        {
            Console.Clear();
            const string file = "Library Books.csv";
            Console.WriteLine("Books in the Library: ");
            int minPage = library[0].PageLength;
            int indexOfMin = 0;

            var sortedBooks = library.OrderBy(book => book.Title);

            Console.WriteLine("Books sorted by page count:");
            foreach (var book in sortedBooks)
            {
                Console.WriteLine($"{book.Title} by {book.Author}, ({book.YearPublished}) | Genre: {book.Genre} {book.PageLength} pages | Checked Out: {book.CheckedOut}");
            }
            /*
            for (int i = 0; i < library.Length - 1; i++)
            {
                minPage = library[i].PageLength;
                for (int j = i; j < library.Length; j++)
                {
                    if (library[j].PageLength < minPage)
                    {
                        minPage = library[j].PageLength;
                        indexOfMin = j;
                    }
                    Console.WriteLine(minPage + ":" + indexOfMin);
                }
                (library[indexOfMin], library[i]) = (library[i], library[indexOfMin]);
            }


            for (int i = 0; i < GetLineCount(file) - 1; i++)
            {
                Console.WriteLine(library[i]);
            }
            */
            Console.WriteLine();
            Console.WriteLine("\n 1 - Return");
            int returnMenu = int.Parse(Console.ReadLine());

            if (returnMenu == 1)
            {
                openMenu(library);
            }
        }

        public static void checkoutBook(Book[] library)
        {
            Console.Clear();
            const string file = "Library Books.csv";
            Console.WriteLine("Books in the Library: ");
            for (int i = 0; i < GetLineCount(file) - 1; i++)
            {
                Console.WriteLine((i + 1) + " - " + library[i]);
            }

            Console.WriteLine();
            Console.Write("Which Book Would You Like to Check Out? : ");
            int pickBook = int.Parse(Console.ReadLine()) - 1;
            if (!library[pickBook].CheckedOut)
            {
                confirmCheckout(ref library[pickBook], library);
            }
            else
            {
                Console.WriteLine("Sorry That Book is Already Checked Out");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" 0 - Return");
            int returnMenu = int.Parse(Console.ReadLine());

            if (returnMenu == 0)
            {
                openMenu(library);
            }
        }

        public static void confirmCheckout(ref Book pickedBook, Book[] library)
        {
            Console.Clear();
            Console.WriteLine($"Comfirm Checking Out {pickedBook.Title} by {pickedBook.Author}?");
            Console.WriteLine();
            Console.WriteLine("1 - Yes");
            Console.WriteLine("2 - No");
            int returnMenu = int.Parse(Console.ReadLine());

            if (returnMenu == 1)
            {
                pickedBook.CheckedOut = true;
                Console.WriteLine();
                Console.WriteLine($"Book Due Date is {DateTime.Now.AddDays(14)}");
            }
            else
            {
                checkoutBook(library);
            }
        }

        public static void returnBook(Book[] library)
        {
            Console.Clear();
            const string file = "Library Books.csv";
            Console.WriteLine("Books in the Library: ");
            for (int i = 0; i < GetLineCount(file) - 1; i++)
            {
                Console.WriteLine((i + 1) + " - " + library[i]);
            }

            Console.WriteLine();
            Console.Write("Which Book Would You Like to Return? : ");
            int pickBook = int.Parse(Console.ReadLine()) - 1;
            if (library[pickBook].CheckedOut)
            {
                confirmReturn(ref library[pickBook], library);
            }
            else
            {
                Console.WriteLine("Sorry That Book is Already Returned");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" 0 - Return to Menu");
            int returnMenu = int.Parse(Console.ReadLine());

            if (returnMenu == 0)
            {
                openMenu(library);
            }
        }

        public static void donateBook(Book[] library)
        {
            Console.Clear();
            const string file = "Library Books.csv";
            Console.WriteLine("Donate a Book");
            Console.Write("Enter the author's name: ");
            string author = Console.ReadLine();
            Console.Write("Enter the book title: ");
            string title = Console.ReadLine();
            Console.Write("Enter the year published: ");
            int yearPublished = int.Parse(Console.ReadLine());
            Console.Write("Enter the genre: ");
            string genre = Console.ReadLine();
            Console.Write("Enter the amount of pages: ");
            int pageCount = int.Parse(Console.ReadLine());
            Book newBook = new Book(title, author, genre, pageCount, yearPublished, false);
            using (StreamWriter writer = new StreamWriter(file, true))
            {
                writer.WriteLine($"{newBook.Author},{newBook.Title},{newBook.YearPublished},{newBook.Genre},{newBook.PageLength},{newBook.CheckedOut}");
            }
            Book[] newLibrary = new Book[library.Length + 1];
            for (int i = 0; i < library.Length; i++)
            {
                newLibrary[i] = library[i];
            }
            newLibrary[library.Length] = newBook;
            Console.WriteLine("Thank you for donating!");
            Console.WriteLine("\n 0 - Return");
            int returnMenu = int.Parse(Console.ReadLine());
            if (returnMenu == 0)
            {
                openMenu(newLibrary);
            }
        }

        public static void confirmReturn(ref Book pickedBook, Book[] library)
        {
            Console.Clear();
            Console.WriteLine($"Comfirm Returning {pickedBook.Title} by {pickedBook.Author}?");
            Console.WriteLine();
            Console.WriteLine("1 - Yes");
            Console.WriteLine("2 - No");
            int returnMenu = int.Parse(Console.ReadLine());

            if (returnMenu == 1)
            {
                pickedBook.CheckedOut = false;
            }
            else
            {
                checkoutBook(library);
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