namespace MaximCSTasks.Sorters;

public class StringQuickSorter
{
    private static int partition(char[] text, int start, int end)
    {
        int pivot = text[end];
        int pivotIndex = start;
        for (int i = start; i < end; i++)
        {
            if (text[i] < pivot)
            {
                (text[pivotIndex], text[i]) = (text[i], text[pivotIndex]);
                pivotIndex++;
            }
        }

        (text[pivotIndex], text[end]) = (text[end], text[pivotIndex]);
        return pivotIndex;
    }

    private static void QuickSort(char[] text, int start, int end)
    {
        if (start < end)
        {
            var pivotIndex = partition(text, start, end);
            QuickSort(text, start, pivotIndex - 1);
            QuickSort(text, pivotIndex + 1, end);
        }
    }

    public static string SortString(string text)
    {
        var result = text.ToCharArray();
        QuickSort(result, 0, result.Length - 1);
        return new string(result);
    }
}