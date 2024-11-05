namespace MaximCSTasks.Sorters;

class TreeNode
{
    public char value;
    public TreeNode? left;
    public TreeNode? right;
    
    public TreeNode(char c, TreeNode? left = null, TreeNode? right = null)
    {
        value = c;
        this.left = left;
        this.right = right;
    }

    public void InsertNode(TreeNode node)
    {
        if (node.value < value)
        {
            if (left == null)
                left = node;
            else
                left.InsertNode(node);
        }
        else
        {
            if (right == null)
                right = node;
            else
                right.InsertNode(node);
        }
    }

    public List<char> ToList(List<char>? result = null)
    {
        result ??= new List<char>();
        
        left?.ToList(result);
        result.Add(value);
        right?.ToList(result);
        
        return result;
    }
}

public class StringTreeSorter: StringSorterInterface
{
    private static List<char> TreeSort(char[] text)
    {
        TreeNode root = new TreeNode(text[0]);
        for (int i = 1; i < text.Length; i++)
            root.InsertNode(new TreeNode(text[i]));
        return root.ToList();
    }
    
    public string SortString(string text)
    {
        var input = text.ToCharArray();
        List<char> result = TreeSort(input);
        return new string(result.ToArray());
    }
}