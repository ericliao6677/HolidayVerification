using System.Globalization;
using System.Text.RegularExpressions;

namespace Holiday.API.Common.Extensions;

public static class StringExtension
{
    public static string ToCamelCase(this string str)
    {
        // Remove any non-letter or non-digit characters and normalize the case.
        string normalizedString = Regex.Replace(str, "[^a-zA-Z0-9]", " ").ToLowerInvariant();

        // Split the string into words.
        string[] words = normalizedString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Process each word.
        for (int i = 0; i < words.Length; i++)
        {
            if (i == 0)
            {
                // Make the first word lowercase.
                words[i] = words[i].ToLowerInvariant();
            }
            else
            {
                // Capitalize the first letter of each subsequent word.
                words[i] = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(words[i]);
            }
        }

        // Join the words to form the camelCase result.
        return string.Concat(words);
    }
}
