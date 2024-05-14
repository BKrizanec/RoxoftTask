using LibraryApp.Models.Models;
using LibraryApp.Service.Common;

namespace LibraryApp.Service
{
    public class BookServiceUI
    {
        private readonly IBookService _bookService;

        public BookServiceUI(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task RunMainMenuAsync()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("*************************************");
            Console.WriteLine("* Welcome to the Roxoft Library app *");
            Console.WriteLine("*************************************");
            Console.ForegroundColor = ConsoleColor.White;

            string userSelection;

            do
            {
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("**********************************");
                Console.WriteLine("* Select a book related activity *");
                Console.WriteLine("**********************************");
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("1: Find a book using the book's id");
                Console.WriteLine("2: List all the books in the library");
                Console.WriteLine("3: Add a book to the library");
                Console.WriteLine("4: Edit a book using the book's id");
                Console.WriteLine("5: Delete a book using the book's id");
                Console.WriteLine("9: You don't actually like books, you just want to leave the application");

                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        Console.WriteLine("Enter the book ID:");
                        if (int.TryParse(Console.ReadLine(), out int bookId))
                        {
                            var book = await _bookService.GetBookByIdAsync(bookId);
                            if (book != null)
                            {
                                Console.WriteLine($"Book found: {book.Title}, {book.Author}, {book.Genre}");
                            }
                            else
                            {
                                Console.WriteLine("Book not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid book ID.");
                        }
                        break;
                    case "2":
                        var allBooks = await _bookService.GetBooksAsync();
                        foreach (var book in allBooks)
                        {
                            Console.WriteLine($"{book.Title}, {book.Author}, {book.Genre}");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Enter the details of the book to add:");
                        break;
                    case "4":
                        Console.WriteLine("Enter the book ID:");
                        if (int.TryParse(Console.ReadLine(), out int editBookId))
                        {
                            Console.WriteLine("Enter the updated details of the book:");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid book ID.");
                        }
                        break;
                    case "5":
                        Console.WriteLine("Enter the book ID to delete:");
                        if (int.TryParse(Console.ReadLine(), out int deleteBookId))
                        {
                            var deleteResult = await _bookService.DeleteBookAsync(deleteBookId);
                            if (deleteResult)
                            {
                                Console.WriteLine("Book deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to delete book.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid book ID.");
                        }
                        break;
                    case "9": break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
            while (userSelection != "9");

            Console.WriteLine("Thank you for using the application");
            Console.Read();
        }
    }
}
