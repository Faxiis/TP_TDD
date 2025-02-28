using TP_TDD.Validators;

namespace TP_TDD_TEST;

public class IsbnValidatorTests
{
    [SetUp]
    public void Setup() { }
    
    [Test]
    public void IsValid_IsbnIs10CharValid_ShouldReturnsTrue()
    {
        var isbn = "2253009687";
        var isbnValidator = new IsbnValidator();
        
        var result = isbnValidator.IsValid(isbn);
        
        Assert.True(result);
    }
    
    [Test]
    public void IsValid_IsbnIs13CharValid_ShouldReturnsTrue()
    {
        var isbn = "9780689856662";
        var isbnValidator = new IsbnValidator();
        
        var result = isbnValidator.IsValid(isbn);
        
        Assert.True(result);
    }
    
    [Test]
    public void IsValid_IsbnIsNotValid9Char_ShouldReturnsFalse()
    {
        var isbn = "123456789";
        var isbnValidator = new IsbnValidator();
        
        var result = isbnValidator.IsValid(isbn);
        
        Assert.False(result);
    }
    
    [Test]
    public void IsValid_IsbnIsNotValid_ShouldReturnsFalse()
    {
        var isbn = "2253009684";
        var isbnValidator = new IsbnValidator();
        
        var result = isbnValidator.IsValid(isbn);
        
        Assert.False(result);
    }
    
    [Test]
    public void IsValid_IsbnHasCharacters_ShouldReturnsFalse()
    {
        var isbn = "123456789a";
        var isbnValidator = new IsbnValidator();
        
        var result = isbnValidator.IsValid(isbn);
        
        Assert.False(result);
    }
    
    public void IsValid_IsbnIsValidAndEndsWithX_ReturnsTrue()
    {
        var isbn = "123456789X";
        var isbnValidator = new IsbnValidator();
        
        var result = isbnValidator.IsValid(isbn);
        
        Assert.True(result);
    }
}

