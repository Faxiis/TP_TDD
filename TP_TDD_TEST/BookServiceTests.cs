using TP_TDD.Models;
using TP_TDD.Services;

namespace TP_TDD_TEST;

public class BookServiceTests
{
    private BookService _bookService;
    
    [SetUp]
    public void Setup()
    {
        _bookService = new BookService();
    }
    
    [Test]
    public void AddBook_ShouldAddBookToLibrary()
    {
        var author = new Author() { Id=1, LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "1234567890", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        _bookService.AddBook(book);

        var addedBook = _bookService.GetBookByIsbn("1234567890");
        
        Assert.NotNull(addedBook);
        Assert.That(addedBook.Title, Is.EqualTo("Test Book"));
    }
    
    [Test]
    public void AddBook_ShouldNotAddBookToLibrary()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "1234567890", Title = "Test Book", Author = author, Publisher = publisher, IsAvailable = true };
        _bookService.AddBook(book);

        var addedBook = _bookService.GetBookByIsbn("1234567890");
        
        Assert.Null(addedBook);
    }
    
    [Test]
    public void DeleteBook_ShouldDeleteBookFromLibrary()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "1234567890", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        _bookService.AddBook(book);

        _bookService.DeleteBook("1234567890");

        var deletedBook = _bookService.GetBookByIsbn("1234567890");
        
        Assert.Null(deletedBook);
    }
    
    [Test]
    public void UpdateBook_ShouldUpdateBook()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "1234567890", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        _bookService.AddBook(book);

        var updatedBook = new Book() { Isbn = "1234567890", Title = "Updated Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        _bookService.UpdateBook(updatedBook);

        var addedBook = _bookService.GetBookByIsbn("1234567890");
        
        Assert.NotNull(addedBook);
        Assert.That(addedBook.Title, Is.EqualTo("Updated Test Book"));
    }
      
    [Test]
    public void UpdateBook_ShouldNotUpdateBook()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "1234567890", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        _bookService.AddBook(book);

        var updatedBook = new Book() { Isbn = "1234567890", Title = "Updated Test Book", Author = author, Publisher = publisher, IsAvailable = true };
        _bookService.UpdateBook(updatedBook);

        var addedBook = _bookService.GetBookByIsbn("1234567890");
        
        Assert.NotNull(addedBook);
        Assert.That(addedBook.Title, Is.EqualTo("Test Book"));
    }
    
    [Test]
    public void GetBookByIsbn_ShouldReturnBook()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "1234567890", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        _bookService.AddBook(book);

        var addedBook = _bookService.GetBookByIsbn("1234567890");
        
        Assert.NotNull(addedBook);
        Assert.That(addedBook.Title, Is.EqualTo("Test Book"));
    }
    
    [Test]
    public void GetBookByIsbn_ShouldNotReturnBook()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "1234567890", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        _bookService.AddBook(book);

        var addedBook = _bookService.GetBookByIsbn("1234567891");
        
        Assert.Null(addedBook);
    }

}
