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
        var book = new Book { Isbn = "1234567890", Title = "Test Book", Author = "Test Author", Publisher = "Test Publisher", Format = "Poche", IsAvailable = true };
        _bookService.AddBook(book);

        var addedBook = _bookService.GetBookByIsbn("1234567890");
        
        Assert.NotNull(addedBook);
        Assert.AreEqual("Test Book", addedBook.Title);
    }
}
