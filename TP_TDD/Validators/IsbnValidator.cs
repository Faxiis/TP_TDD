namespace TP_TDD.Validators;

public class IsbnValidator
{
    public bool IsValid(string isbn)
    {
        if (isbn.Any(c => !char.IsDigit(c)))
        {
            return false;
        }
        
        return isbn.Length == 10 || isbn.Length == 13;
    }
}