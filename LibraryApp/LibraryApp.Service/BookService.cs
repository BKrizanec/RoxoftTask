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

    public async Task<List<Book>> GetBooksAsync()
    {
        return await _bookRepository.GetBooksAsync();
    }
    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await _bookRepository.GetBookByIdAsync(id);
    }

    public async Task<bool> AddBookAsync(Book book)
    {
        return await _bookRepository.AddBookAsync(book);
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        return await _bookRepository.DeleteBookAsync(id);
    }

    public async Task<bool> UpdateBookAsync(int id, Book book)
    {
        return await _bookRepository.UpdateBookAsync(id, book);
    }        

}
