using System.Security.Cryptography;
using System.Text;

namespace Application.Extersion;

public static class StringExtentions
{
    public static string ComputeHash(this string input)
    {
        using SHA256 sha256Hash = SHA256.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = sha256Hash.ComputeHash(inputBytes);

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            builder.Append(hashBytes[i].ToString("x2"));
        }
        return builder.ToString();
    }
}