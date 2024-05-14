using LibraryApp.Models.Models;
using LibraryApp.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repository;

public class BookRepository : IBookRepository
{
    private readonly RoxoftLibraryContext _context;
    private List<Book> _books;

    public BookRepository(RoxoftLibraryContext context)
    {
        if(_context == null)
        {
            _context = context;
        }
        if(_books == null)
        {
            _books = new List<Book>();
        }
    }
    public async Task<List<Book>> GetBooksAsync()
    {       
        try
        {          
            return _books = await _context.Books.ToListAsync();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            throw;
        }
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        try
        {
            Book findBook = new Book();
            return findBook = await _context.Books.FindAsync(id);
            
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            throw;
        }
    }
    public async Task<bool> AddBookAsync(Book book)
    {
        if (book == null)
            return false;

        try 
        {
            Book newBook = book;
            _books.Add(newBook);
            return true;
        } 
        catch (Exception ex) 
        {
            await Console.Out.WriteLineAsync(ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        if (id == null)
            return false;

        try
        {
            Book deleteBook = new Book();
            deleteBook = await GetBookByIdAsync(id);
            _context.Books.Remove(deleteBook);
            return true;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            throw;
        }
    }

    public async Task<bool> UpdateBookAsync(int id, Book book)
    {
        if(id == null || book == null)
            return false;

        try
        {
            Book updateBook = await GetBookByIdAsync(id);
            
            if(book.Title != null)
                updateBook.Title = book.Title;
            else if(book.Genre != null)
                updateBook.Genre = book.Genre;
            else if(book.Author != null)
                updateBook.Author = book.Author;

            _context.Books.Update(updateBook);

            return true;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            throw;
        }
    }
    
}
