namespace TP_TDD.Validators;

public class IsbnValidator
{
    public bool IsValid(string isbn)
    {
        return isbn.Length == 10 || isbn.Length == 13;
    }
}