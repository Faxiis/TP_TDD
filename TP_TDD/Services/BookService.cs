using TP_TDD.Models;

namespace TP_TDD.Services;

public class BookService
{

    private readonly List<Book> _books = [];
    
    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public Book? GetBookByIsbn(string isbn)
    {
        return _books.FirstOrDefault(b => b.Isbn == isbn);
    }
}
