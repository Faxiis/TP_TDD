using TP_TDD.Models;
using TP_TDD.Validators;

namespace TP_TDD.Services;

public class BookService(IBookDataService databaseService, IBookDataService webService)
{
    private readonly IsbnValidator _isbnValidator = new IsbnValidator();

    public void AddBook(Book book)
    {
        var existingBook = databaseService.GetBookByIsbn(book.Isbn);
        if (!_isbnValidator.IsValid(book.Isbn))
            throw new ArgumentException("L'ISBN n'est pas valide.");
        if(!AreAllFieldsFilled(book))
            throw new ArgumentException("Les champs ne sont pas tous remplis.");
        if(existingBook != null)
            throw new ArgumentException("Le livre existe déjà.");

        databaseService.AddBook(book);
    }

    public void UpdateBook(Book book)
    {
        if (!_isbnValidator.IsValid(book.Isbn))
            throw new ArgumentException("L'ISBN n'est pas valide.");
        if(!AreAllFieldsFilled(book))
            throw new ArgumentException("Les champs ne sont pas tous remplis.");
        
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
