using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Isbn.Providers.Helpers;

public static class IsbnCalculator
{
    public static string AsIsbn13(this string isbn10)
    {
        var twelveDigit = "978" + isbn10[..9];
        var sum = 0;
        for (var i = 0; i < twelveDigit.Length; i++)
        {
            sum += twelveDigit[i] * (i % 2 == 0 ? 1 : 3);
        }
        var checkDigit = sum % 10;
        return "978" + isbn10 + checkDigit;
    }

    public static string GenerateIsbn10(string nineDigits)
    {
        Console.WriteLine($"digits: {nineDigits}");
        var sumFirst9 = 0;
        for (var i = 0; i < nineDigits.Length; i++)
        {
            sumFirst9 += int.Parse(nineDigits[i].ToString()) * (10 - i);
        }
        Console.WriteLine($"9digitSum: {sumFirst9}");


        var checkDigit = 0;
        while (true)
        {
            if (!((sumFirst9 + (1 * checkDigit)) % 11 == 0))
            {
                checkDigit++;
                continue;
            }
            break;
        }
        return nineDigits + (checkDigit == 10 ? "X" : checkDigit);
    }
}
