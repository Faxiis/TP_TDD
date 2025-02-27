using TP_TDD.Models;
using TP_TDD.Validators;

namespace TP_TDD.Services;

public class BookService(IBookDataService databaseService, IBookDataService webService)
{
    private readonly IsbnValidator _isbnValidator = new IsbnValidator();

    public void AddBook(Book book)
    {
        // TODO : vérifier l'existance du livre
        
        if (!AreAllFieldsFilled(book) || !_isbnValidator.IsValid(book.Isbn))
        {
            return;
        }

        databaseService.AddBook(book);
    }

    public void UpdateBook(Book book)
    {
        if (!AreAllFieldsFilled(book) || !_isbnValidator.IsValid(book.Isbn))
        {
            return;
        }

        databaseService.UpdateBook(book);
    }

    public void DeleteBook(string isbn)
    {
        databaseService.DeleteBook(isbn);
    }

    public Book? GetBookDataByIsbn(string isbn)
    {
        return databaseService.GetBookByIsbn(isbn) ?? webService.GetBookByIsbn(isbn);
    }

    public Book? GetBookDataByTitle(string title)
    {
        return databaseService.GetBookByTitle(title) ?? webService.GetBookByTitle(title);
    }

    public Book? GetBookDataByAuthor(string name)
    {
        return databaseService.GetBookByAuthor(name) ?? webService.GetBookByAuthor(name);
    }
    
    private bool AreAllFieldsFilled(Book book)
    {
        return !string.IsNullOrEmpty(book.Isbn)
               && !string.IsNullOrEmpty(book.Title)
               && book.Author != null
               && book.Publisher != null
               && !string.IsNullOrEmpty(book.Format);
    }
}
