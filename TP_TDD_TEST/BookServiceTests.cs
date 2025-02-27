using Moq;
using TP_TDD.Models;
using TP_TDD.Services;

namespace TP_TDD_TEST;

public class BookServiceTests
{
    private Mock<IBookDataService> _mockDatabaseService;
    private Mock<IBookDataService> _mockWebService;

    [SetUp]
    public void Setup()
    {
        _mockDatabaseService = new Mock<IBookDataService>();
        _mockWebService = new Mock<IBookDataService>();
    }

    
    [Test]
    public void GetBookByIsbn_ShouldReturnBook()
    {
        var expectedBook = new Book() { Isbn = "2253009687", Title = "Notre Dame de Paris", Author = new Author { LastName = "Hugo", FirstName = "Victor" }, Publisher = new Publisher() { Siret = "Hugo", Name = "Victor" }, Format = "Poche"};

        _mockDatabaseService.Setup(service => service.GetBookByIsbn("2253009687")).Returns(expectedBook);

        var stockManager = new BookService(_mockDatabaseService.Object, _mockWebService.Object);

        var book = stockManager.GetBookDataByIsbn("2253009687");

        _mockWebService.VerifyNoOtherCalls();

        Assert.That(book, Is.EqualTo(expectedBook));
    }
    
    [Test]
    public void GetBookByTitle_ShouldReturnBook()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };

        _mockDatabaseService.Setup(service => service.GetBookByTitle("Test Book")).Returns(book);

        var stockManager = new BookService(_mockDatabaseService.Object, _mockWebService.Object);
        var addedBook = stockManager.GetBookDataByTitle("Test Book");

        Assert.NotNull(addedBook);
    }

    [Test]
    public void GetBookByAuthor_ShouldReturnBook()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };

        _mockDatabaseService.Setup(service => service.GetBookByAuthor("Test Author")).Returns(book);

        var stockManager = new BookService(_mockDatabaseService.Object, _mockWebService.Object);
        var addedBook = stockManager.GetBookDataByAuthor("Test Author");

        Assert.NotNull(addedBook);
    }
    
    [Test]
    public void AddBook_ShouldAddBookToLibrary()
    {
        var author = new Author() { Id = 1, LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
       
        _mockDatabaseService.Setup(service => service.GetBookByIsbn("2253009687")).Returns(book);

        var stockManager = new BookService(_mockDatabaseService.Object, _mockWebService.Object);
        stockManager.AddBook(book);

        _mockDatabaseService.Verify(service => service.AddBook(It.IsAny<Book>()), Times.Once);
        Assert.IsNotNull(stockManager.GetBookDataByIsbn("2253009687"));
        Assert.That(stockManager.GetBookDataByIsbn("2253009687"), Is.EqualTo(book));
    }
    
    [Test]
    public void AddBook_ShouldNotAddBookToLibrary_NotAllFieldsFilled()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher,IsAvailable = true };

        var stockManager = new BookService(_mockDatabaseService.Object, _mockWebService.Object);
        stockManager.AddBook(book);

        _mockDatabaseService.Verify(service => service.AddBook(It.IsAny<Book>()), Times.Never);
        Assert.IsNull(stockManager.GetBookDataByIsbn("2253009687"));
    }
    
    [Test]
    public void AddBook_ShouldNotAddBookToLibrary_Isbn9Char()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "225300578", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };

        var stockManager = new BookService(_mockDatabaseService.Object, _mockWebService.Object);
        stockManager.AddBook(book);

        _mockDatabaseService.Verify(service => service.AddBook(It.IsAny<Book>()), Times.Never);
        Assert.IsNull(stockManager.GetBookDataByIsbn("225300578"));
    }
    
    [Test]
    public void AddBook_ShouldNotAddBookToLibrary_IsbnHasNotGoodChar()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "225300a687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };

        var stockManager = new BookService(_mockDatabaseService.Object, _mockWebService.Object);
        stockManager.AddBook(book);

        _mockDatabaseService.Verify(service => service.AddBook(It.IsAny<Book>()), Times.Never);
        Assert.IsNull(stockManager.GetBookDataByIsbn("225300a687"));
    }
    
    [Test]
    public void DeleteBook_ShouldDeleteBookFromLibrary()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };

        _mockDatabaseService.Setup(service => service.GetBookByIsbn("2253009687")).Returns(book);

        var stockManager = new BookService(_mockDatabaseService.Object, _mockWebService.Object);
        stockManager.DeleteBook("2253009687");
        
        _mockDatabaseService.Setup(service => service.GetBookByIsbn("2253009687")).Returns((Book)null);

        _mockDatabaseService.Verify(service => service.DeleteBook("2253009687"), Times.Once);
        Assert.IsNull(stockManager.GetBookDataByIsbn("2253009687"));
    }

    [Test]
    public void UpdateBook_ShouldUpdateBook()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };

        _mockDatabaseService.Setup(service => service.GetBookByIsbn("2253009687")).Returns(book);

        book.Title = "Updated Test Book";
        
        var stockManager = new BookService(_mockDatabaseService.Object, _mockWebService.Object);
        stockManager.UpdateBook(book);

        _mockDatabaseService.Verify(service => service.UpdateBook(book), Times.Once);
        Assert.That(stockManager.GetBookDataByIsbn("2253009687")?.Title, Is.EqualTo("Updated Test Book"));
    }
    
    [Test]
    public void UpdateBook_ShouldNotUpdateBook_NotAllFieldsFilled()
    {
        var author = new Author() { LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };

        _mockDatabaseService.Setup(service => service.GetBookByIsbn("2253009687")).Returns(book);

        var updatedBook = new Book() { Isbn = "2253009687", Title = "Updated Test Book", Author = author, Publisher = null, Format = "Poche", IsAvailable = true };

        var stockManager = new BookService(_mockDatabaseService.Object, _mockWebService.Object);
        stockManager.UpdateBook(updatedBook);

        _mockDatabaseService.Verify(service => service.UpdateBook(It.IsAny<Book>()), Times.Never);
        Assert.That(stockManager.GetBookDataByIsbn("2253009687")?.Title, Is.EqualTo("Test Book"));
    }
}
