namespace TP_TDD.Validators;

public class IsbnValidator
{
    public bool IsValid(string isbn)
    {
        if (isbn.Any(c => !char.IsDigit(c)) || isbn.Length != 10 && isbn.Length != 13)
        {
            return false;
        }
        
        // if length 10 apply 165 % 11 = 0 : la clé est valide
        if(isbn.Length == 10)
        {
            if (!IsIsbnLength10Ok(isbn)) return false;
        }else if (isbn.Length == 13)
        {
            if (!IsIsbnLength13Ok(isbn)) return false;
        }
        
        return true;
    }

    private bool IsIsbnLength10Ok(string isbn)
    {
        var sum = 0;
        for (var i = 0; i < 9; i++)
        {
            sum += (i + 1) * int.Parse(isbn[i].ToString());
        }

        var key = isbn[9] == 'X' ? 10 : int.Parse(isbn[9].ToString());
        sum += 10 * key;
        return sum % 11 == 0;
    }
    
    private bool IsIsbnLength13Ok(string isbn)
    {
        var sum = 0;
        for (var i = 0; i < 12; i++)
        {
            sum += (i % 2 == 0 ? 1 : 3) * int.Parse(isbn[i].ToString());
        }

        var key = int.Parse(isbn[12].ToString());
        sum += key;
        return sum % 10 == 0;
    }
}