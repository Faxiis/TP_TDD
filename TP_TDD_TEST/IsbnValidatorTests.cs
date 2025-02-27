using TP_TDD.Validators;

namespace TP_TDD_TEST;

public class IsbnValidatorTests
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void IsValid_IsbnIsValid_ReturnsTrue()
    {
        var isbn = "1234567890";
        var isbnValidator = new IsbnValidator();
        
        var result = isbnValidator.IsValid(isbn);
        
        Assert.True(result);
    }
    
    [Test]
    public void IsValid_IsbnIsNotValid_ReturnsFalse()
    {
        var isbn = "123456789";
        var isbnValidator = new IsbnValidator();
        
        var result = isbnValidator.IsValid(isbn);
        
        Assert.False(result);
    }
}

