
/*

Library Management System:
 * This application demonstrates core C# programming concepts including arrays,
 * control flow, and user input handling. It provides functionality for managing
 * a small library collection with operations for adding, removing, searching,
 * borrowing, and checking in books.

This code is mostly AI generated as the course says it's intended to be written using
Microsoft Copilot. 

This is a submission project for the course "Introduction to Programming With C#" on the Coursera platform. 
Course certificate link: https://coursera.org/share/d760e078ef5120a7023481e3092e9b45


*/



class LibraryManager
{
    static void Main()
    {
        string[] books = new string[5];
        bool[] isCheckedOut = new bool[5];
        int borrowedCount = 0;
        const int borrowLimit = 3;

        while (true)
        {
            Console.WriteLine("\nOptions: add / remove / search / borrow / checkin / exit");
            string action = Console.ReadLine().ToLower().Trim();

            if (action == "add")
            {
                bool added = false;
                for (int i = 0; i < books.Length; i++)
                {
                    if (string.IsNullOrEmpty(books[i]))
                    {
                        Console.WriteLine("Enter the title of the book to add:");
                        books[i] = Console.ReadLine();
                        Console.WriteLine($"'{books[i]}' has been added to the library.");
                        added = true;
                        break;
                    }
                }
                if (!added)
                    Console.WriteLine("The library is full. No more books can be added.");
            }

            else if (action == "remove")
            {
                Console.WriteLine("Enter the title of the book to remove:");
                string removeBook = Console.ReadLine();
                bool found = false;
                for (int i = 0; i < books.Length; i++)
                {
                    if (books[i] == removeBook)
                    {
                        if (isCheckedOut[i])
                        {
                            Console.WriteLine($"Cannot remove '{removeBook}' because it is currently checked out.");
                        }
                        else
                        {
                            books[i] = "";
                            Console.WriteLine($"'{removeBook}' has been removed from the library.");
                        }
                        found = true;
                        break;
                    }
                }
                if (!found)
                    Console.WriteLine("Book not found in the library.");
            }

            else if (action == "search")
            {
                Console.WriteLine("Enter the book title to search for:");
                string searchTitle = Console.ReadLine();

                bool found = false;
                for (int i = 0; i < books.Length; i++)
                {
                    if (books[i] == searchTitle)
                    {
                        found = true;
                        if (isCheckedOut[i])
                            Console.WriteLine($"'{searchTitle}' is in the collection but is currently checked out.");
                        else
                            Console.WriteLine($"'{searchTitle}' is available in the collection.");
                        break;
                    }
                }

                if (!found)
                    Console.WriteLine($"'{searchTitle}' is not in the collection.");
            }

            else if (action == "borrow")
            {
                if (borrowedCount >= borrowLimit)
                {
                    Console.WriteLine($"Borrowing limit reached. You may only borrow {borrowLimit} books at a time.");
                    Console.WriteLine("Please check in a book before borrowing another.");
                }
                else
                {
                    Console.WriteLine("Enter the title of the book you want to borrow:");
                    string borrowTitle = Console.ReadLine();

                    bool found = false;
                    for (int i = 0; i < books.Length; i++)
                    {
                        if (books[i] == borrowTitle)
                        {
                            found = true;
                            if (isCheckedOut[i])
                            {
                                Console.WriteLine($"'{borrowTitle}' is already checked out and unavailable.");
                            }
                            else
                            {
                                isCheckedOut[i] = true;
                                borrowedCount++;
                                Console.WriteLine($"You have borrowed '{borrowTitle}'.");
                                Console.WriteLine($"You currently have {borrowedCount} out of {borrowLimit} books borrowed.");
                            }
                            break;
                        }
                    }

                    if (!found)
                        Console.WriteLine("Book not found in the library.");
                }
            }

            else if (action == "checkin")
            {
                Console.WriteLine("Enter the title of the book you want to check in:");
                string checkinTitle = Console.ReadLine();

                bool found = false;
                for (int i = 0; i < books.Length; i++)
                {
                    if (books[i] == checkinTitle)
                    {
                        found = true;
                        if (isCheckedOut[i])
                        {
                            isCheckedOut[i] = false;
                            borrowedCount--;
                            Console.WriteLine($"'{checkinTitle}' has been successfully checked in.");
                            Console.WriteLine($"You now have {borrowedCount} out of {borrowLimit} books borrowed.");
                        }
                        else
                        {
                            Console.WriteLine($"'{checkinTitle}' is not currently checked out.");
                        }
                        break;
                    }
                }

                if (!found)
                    Console.WriteLine("Book not found in the library.");
            }

            else if (action == "exit")
            {
                Console.WriteLine("Goodbye!");
                break;
            }

            else
            {
                Console.WriteLine("Invalid action. Please choose: add / remove / search / borrow / checkin / exit");
            }

            Console.WriteLine("\n--- Library Status ---");
            bool anyBooks = false;
            for (int i = 0; i < books.Length; i++)
            {
                if (!string.IsNullOrEmpty(books[i]))
                {
                    string status = isCheckedOut[i] ? "[Checked Out]" : "[Available]";
                    Console.WriteLine($"  {books[i]} {status}");
                    anyBooks = true;
                }
            }
            if (!anyBooks)
                Console.WriteLine("  (library is empty)");

            Console.WriteLine($"Books currently borrowed: {borrowedCount}/{borrowLimit}");
            Console.WriteLine("----------------------");
        }
    }
}