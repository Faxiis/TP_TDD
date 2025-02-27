using TP_TDD.Models;

namespace TP_TDD.Services;

public class BookService
{

    private readonly List<Book> _books = [];
    
    public void AddBook(Book book)
    {
        if (string.IsNullOrEmpty(book.Isbn) || string.IsNullOrEmpty(book.Title) || book.Author == null || book.Publisher == null || string.IsNullOrEmpty(book.Format))
        {
            return;
        }
        
        _books.Add(book);
    }
    
    public void DeleteBook(string isbn)
    {
        var book = GetBookByIsbn(isbn);
        if (book != null)
        {
            _books.Remove(book);
        }
    }
    
    public void UpdateBook(Book book)
    {
        var existingBook = GetBookByIsbn(book.Isbn);
        if (existingBook != null)
        {
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Publisher = book.Publisher;
            existingBook.Format = book.Format;
            existingBook.IsAvailable = book.IsAvailable;
        }
    }

    public Book? GetBookByIsbn(string isbn)
    {
        return _books.FirstOrDefault(b => b.Isbn == isbn);
    }
    
    
}
