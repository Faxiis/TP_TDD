using TP_TDD.Models;

namespace TP_TDD.Services;

public interface IBookDataService
{
    void AddBook(Book book);
    void DeleteBook(string isbn);
    void UpdateBook(Book book);
    Book? GetBookByIsbn(string isbn);
    Book? GetBookByTitle(string title);
    Book? GetBookByAuthor(string authorName);
}
