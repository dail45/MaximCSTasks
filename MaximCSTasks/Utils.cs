namespace MaximCSTasks;

public class Utils
{
    public static string EvenOrOddReverseTextFunc(string text) {
        if (text.Length % 2 == 0) {
            int halfTextIndex = text.Length / 2;

            return new string(text[..halfTextIndex].Reverse().Concat(text[halfTextIndex..].Reverse()).ToArray());
        }
        return new string(text.Reverse().Concat(text).ToArray());
    }
}