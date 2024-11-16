using MaximCSTasks.Services;

namespace MaximCSTasks;

public class Utils
{
    public static string EvenOrOddReverseTextFunc(string text)
    {
        
        if (text.Length % 2 == 0)
        {
            var halfTextIndex = text.Length / 2;

            return new string(text[..halfTextIndex].Reverse().Concat(text[halfTextIndex..].Reverse()).ToArray());
        }

        return new string(text.Reverse().Concat(text).ToArray());
    }

    public static List<char> CheckOnlyEnglishChars(string text)
    {
        var unexpectedChars = new List<char>();
        foreach (var ch in text)
        {
            if ('a' > ch || ch > 'z')
                unexpectedChars.Add(ch);
        }

        return unexpectedChars;
    }
    
    public static Dictionary<char, int> CalcCountChars(string text)
    {
        var result = new Dictionary<char, int>();
        foreach (var ch in text)
        {
            if (result.ContainsKey(ch))
                result[ch]++;
            else
                result[ch] = 1;
        }

        return result;
    }

    public static string FindMaxLengthSubStringOfVowelChars(string text)
    {
        var vowelChars = "aeiouy";
        for (var i = 0; i < text.Length; i++)
        {
            if (vowelChars.Contains(text[i]))
            {
                for (var j = text.Length - 1; j >= i; j--)
                {
                    if (vowelChars.Contains(text[j]))
                    {
                        return text.Substring(i, j - i + 1);
                    }
                }
            }
        }

        return string.Empty;
    }
    
    public static string RemoveRandomCharInString(string text)
    {
        var rngService = RandomNumberGeneratorService.Instance;
        var randomNumber = rngService.GetRandomNumber(0, text.Length - 1);
        return text.Remove(randomNumber, 1);
    }
}