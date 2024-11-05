namespace MaximCSTasks;

public class Utils
{
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
}