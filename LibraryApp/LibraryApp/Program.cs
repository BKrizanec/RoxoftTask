using LibraryApp.Models.Models;
using LibraryApp.Repository;
using LibraryApp.Repository.Common;
using LibraryApp.Service;
using LibraryApp.Service.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryApp;

public class Program
{    
    private static IBookService _bookService;
    static async Task Main(string[] args)
    {
        #region DI Container Setup
        var serviceProvider = new ServiceCollection()
            .AddDbContext<RoxoftLibraryContext>(
            options => options.UseSqlServer())
            .AddSingleton<IBookService, BookService>()
            .AddSingleton<IBookRepository, BookRepository>()
            .BuildServiceProvider();

        _bookService = serviceProvider.GetService<IBookService>();
        #endregion

        Dictionary<string, Func<Task>> bookMenu = new Dictionary<string, Func<Task>>()
        {
            {"1", GetBooksAsync},
            {"2", GetBookByIdAsync},
            {"3", AddBookAsync},
            {"4", DeleteBookAsync},
            {"5", UpdateBookAsync}
        };

        Roleplay();

        string userSelection;
        do
        {
            MainMenu();

            userSelection = Console.ReadLine();

            if (bookMenu.TryGetValue(userSelection, out var action))
            {
                await action();
            }
            else if (userSelection != "9")
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }
        }
        while (userSelection != "9");

        Console.ReadKey();
    }

    static void Roleplay()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("**********************************");
        Console.WriteLine("* Welcome to the Roxoft Library! *");
        Console.WriteLine("**********************************");
        Console.ForegroundColor = ConsoleColor.Cyan;

        Thread.Sleep(1000);
        Console.WriteLine("...");
        Thread.Sleep(1000);
        Console.WriteLine("Loading...");
        Thread.Sleep(1000);
        Console.WriteLine("...");
    }

    static void MainMenu()
    {        

        Console.WriteLine("**********************************");
        Console.WriteLine("* Select a book related activity *");
        Console.WriteLine("**********************************");
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine("1: List all the books in the library");
        Console.WriteLine("2: Find a book using the book's id");
        Console.WriteLine("3: Add a book to the library");
        Console.WriteLine("4: Delete a book using the book's id");
        Console.WriteLine("5: Edit a book using the book's id");
        Console.WriteLine("9: You don't actually like books, you just want to leave the application");
        Console.WriteLine();
    }

    static async Task GetBooksAsync()
    {
        List<Book> allBooks = await _bookService.GetBooksAsync();

        foreach(Book book in allBooks) 
        {
            string authorFullName = $"{book.AuthorNavigation?.FirstName} {book.AuthorNavigation?.LastName}";
            Console.WriteLine($"{book.Title}, {authorFullName}, {book.Genre}");
        }
    }

    static async Task GetBookByIdAsync()
    {
        Console.WriteLine("Enter the book ID:");
        if (int.TryParse(Console.ReadLine(), out int bookId))
        {
            Book findBook = await _bookService.GetBookByIdAsync(bookId);

            if (findBook != null)
            {                
                string authorFullName = $"{findBook.AuthorNavigation?.FirstName} {findBook.AuthorNavigation?.LastName}";
                Console.WriteLine($"Book found: {findBook.Title}, Author: {authorFullName}, Genre: {findBook.Genre}");
            }
            else
            {
                Console.WriteLine("Book not found");        
            }
        }
        else
        {
            Console.WriteLine("Your input is invalid. Please enter a valid book ID.");
        }
    }

    static async Task AddBookAsync()
    {
        Console.WriteLine("Enter the details of the book you'd like to add: ");

        Console.Write("Title: ");
        string bookTitle = Console.ReadLine();
        bookTitle = string.IsNullOrEmpty(bookTitle) ? null : bookTitle;

        Console.Write("Genre: ");
        string bookGenre = Console.ReadLine();
        bookGenre = string.IsNullOrEmpty(bookGenre) ? null : bookGenre;

        int authorId;
        do 
        {
            Console.Write("Author (enter the author ID): ");
        }
        while(!int.TryParse(Console.ReadLine(),out authorId));

        Book bookToAdd = new Book
        {
            Title = bookTitle, 
            Genre = bookGenre, 
            Author = authorId
        };

        bool bookAdded = await _bookService.AddBookAsync(bookToAdd);
        if (bookAdded)
            Console.WriteLine("Book added successfully!");
        else 
            Console.WriteLine("Failed to add book.");
    }

    static async Task DeleteBookAsync()
    {
        Console.WriteLine("Enter the book ID to delete: ");
        if(int.TryParse(Console.ReadLine(),out int deleteBookId))
        {
            bool bookDeleted = await _bookService.DeleteBookAsync(deleteBookId);
            if (bookDeleted)
                Console.WriteLine("Book deleted sccuessfully!");
            else
                Console.WriteLine("Failed to delete book.");
        }
        else
        {
            Console.WriteLine("Your input is invalid. Please enter a valid book ID.");
        }
    }

    static async Task UpdateBookAsync()
    {
        Console.WriteLine("Enter the book ID:");
        if (int.TryParse(Console.ReadLine(), out int bookId))
        {
            Book existingBook = await _bookService.GetBookByIdAsync(bookId);
            if (existingBook == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.WriteLine($"Updating book: {existingBook.Title}");

            Console.WriteLine("Enter the updated details of the book:");

            Console.Write("Title: ");
            string updatedBookTitle = Console.ReadLine();
            updatedBookTitle = string.IsNullOrEmpty(updatedBookTitle) ? null : updatedBookTitle;

            Console.Write("Genre: ");
            string updatedBookGenre = Console.ReadLine();
            updatedBookGenre = string.IsNullOrEmpty(updatedBookGenre) ? null : updatedBookGenre;

            int updatedAuthorId;
            do
            {
                Console.WriteLine("Author (enter author ID):");
            }
            while (!int.TryParse(Console.ReadLine(), out updatedAuthorId));

            StringBuilder updateMessage = new StringBuilder("Updated fields: ");

            Book updatedBook = new Book
            {
                BookId = bookId, 
                Title = updatedBookTitle ?? existingBook.Title,
                Genre = updatedBookGenre ?? existingBook.Genre,
                Author = updatedAuthorId <= 0 ? updatedAuthorId : existingBook.Author
            };

            if (updatedBook.Title != existingBook.Title)
                updateMessage.Append("Title, ");

            if (updatedBook.Genre != existingBook.Genre)
                updateMessage.Append("Genre, ");

            if (updatedBook.Author != existingBook.Author)
                updateMessage.Append("Author, ");

            if (updateMessage.Length > 0)
                updateMessage.Length -= 2;
            else
                Console.WriteLine("No fields were updated");

            bool bookUpdated = await _bookService.UpdateBookAsync(bookId, updatedBook);
            if (bookUpdated)
            {
                Console.WriteLine("Book updated successfully.");
                Console.WriteLine(updateMessage.ToString());
            }
            else
            {
                Console.WriteLine("Failed to update book.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid book ID.");
        }
    }
}
