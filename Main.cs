static void RemoveEntry(List<object> database)
{
    Console.Clear();

    // Print all entries with their index
    for (int i = 1; i <= database.Count; i++)
    {
        Console.WriteLine($"{i}: {database[i - 1]}");
    }

    int indexToRemove = -1;
    bool validInput = false;

    // Loop until a valid index is entered
    while (!validInput)
    {
        Console.WriteLine("What would you like to remove? (Enter the number of the entry)");

        // Try to parse user input
        if (int.TryParse(Console.ReadLine(), out indexToRemove))
        {
            // Check if the index is within the valid range
            if (indexToRemove >= 1 && indexToRemove <= database.Count)
            {
                validInput = true; // Valid index, exit the loop
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

    // Remove the entry at the specified index (adjust for 0-based index)
    database.RemoveAt(indexToRemove - 1);
    Console.WriteLine("Successfully Removed!");
    
    // Update the database file
    DataBaseUpdate(database);
}
