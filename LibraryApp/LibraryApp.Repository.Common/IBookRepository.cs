using LibraryApp.Models.Models;

namespace LibraryApp.Repository.Common;

public interface IBookRepository
{
    Task<List<Book>> GetBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task<bool> AddBookAsync(Book book);
    Task<bool> UpdateBookAsync(int id, Book book);
    Task<bool> DeleteBookAsync(int id);
}
