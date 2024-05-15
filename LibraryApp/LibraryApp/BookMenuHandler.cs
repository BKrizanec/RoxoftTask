using LibraryApp.Models.Models;
using LibraryApp.Service.Common;
using System.Text;

namespace LibraryApp;

public static class BookMenuHandler
{
    private static IBookService _bookService;

    public static void Initialize(IBookService bookService)
    {
        _bookService = bookService;
    }

    public static async Task GetBooksAsync()
    {
        List<Book> allBooks = await _bookService.GetBooksAsync();

        foreach (Book book in allBooks)
        {
            string authorFullName = $"{book.AuthorNavigation?.FirstName} {book.AuthorNavigation?.LastName}";
            Console.WriteLine($"{book.Title}, {authorFullName}, {book.Genre}");
        }
    }

    public static async Task GetBookByIdAsync()
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
                Console.WriteLine(MainMenuHelper.ProcessFailed);
            }
        }
        else
        {
            Console.WriteLine(MainMenuHelper.InvalidInput);
        }
    }

    public static async Task AddBookAsync()
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
        while (!int.TryParse(Console.ReadLine(), out authorId));

        Book bookToAdd = new Book
        {
            Title = bookTitle,
            Genre = bookGenre,
            Author = authorId
        };

        bool bookAdded = await _bookService.AddBookAsync(bookToAdd);
        if (bookAdded)
            Console.WriteLine(MainMenuHelper.ProcessSuccess);
        else
            Console.WriteLine(MainMenuHelper.ProcessFailed);
    }

    public static async Task DeleteBookAsync()
    {
        Console.WriteLine("Enter the book ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int deleteBookId))
        {
            bool bookDeleted = await _bookService.DeleteBookAsync(deleteBookId);
            if (bookDeleted)
                Console.WriteLine(MainMenuHelper.ProcessSuccess);
            else
                Console.WriteLine(MainMenuHelper.ProcessFailed);
        }
        else
        {
            Console.WriteLine(MainMenuHelper.InvalidInput);
        }
    }

    public static async Task UpdateBookAsync()
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
                Console.WriteLine(MainMenuHelper.ProcessSuccess);
                Console.WriteLine(updateMessage.ToString());
            }
            else
            {
                Console.WriteLine(MainMenuHelper.ProcessFailed);
            }
        }
        else
        {
            Console.WriteLine(MainMenuHelper.InvalidInput);
        }
    }
}
