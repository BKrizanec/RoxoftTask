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
            return _books = await _context.Books.Include(a => a.AuthorNavigation).ToListAsync();
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
            Book findBook = await _context.Books
                .Include(a => a.AuthorNavigation)
                .FirstOrDefaultAsync(b => b.BookId == id);
            return findBook;            
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
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
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
            Book deleteBook = await GetBookByIdAsync(id);

            if (deleteBook != null)
            {
                _context.Books.Remove(deleteBook);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
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

            if (updateBook == null)
                return false;
            
            if(book.Title != null)
                updateBook.Title = book.Title;
            else if(book.Genre != null)
                updateBook.Genre = book.Genre;
            else if(book.Author != null)
                updateBook.Author = book.Author;

            await _context.SaveChangesAsync();            

            return true;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            throw;
        }
    }
    
}
