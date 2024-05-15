using LibraryApp.Models.Models;
using LibraryApp.Repository.Common;
using LibraryApp.Service.Common;

namespace LibraryApp.Service;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task<List<Book>> GetBooksAsync() => _bookRepository.GetBooksAsync();

    public Task<Book> GetBookByIdAsync(int id) => _bookRepository.GetBookByIdAsync(id);

    public Task<bool> AddBookAsync(Book book) => _bookRepository.AddBookAsync(book);

    public Task<bool> DeleteBookAsync(int id) => _bookRepository.DeleteBookAsync(id);

    public Task<bool> UpdateBookAsync(int id, Book book) => _bookRepository.UpdateBookAsync(id, book);
}
