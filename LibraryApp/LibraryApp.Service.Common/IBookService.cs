using LibraryApp.Models.Models;

namespace LibraryApp.Service.Common;

public interface IBookService
{
    Task<List<Book>> GetBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task<bool> AddBookAsync(Book book);
    Task<bool> UpdateBookAsync(int id, Book book);
    Task<bool> DeleteBookAsync(int id);
}
