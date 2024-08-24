using System;

public class Huffman
{

    public static string Encode(string text)
    {
        Dictionary<char, int> charFrequency = CalculateFrequency(text);
        List<List<object>> heap = BuildHeap(charFrequency);
        List<object> tree = BuildTree(heap);
        Dictionary<char, string> codes = MakeDict(tree);

        string encodedText = "";

        foreach (char c in text)
        {
            encodedText += codes[c];
        }

        return encodedText;
    }
    public static string Encode(string text, int a = -1)
    {
        Dictionary<char, int> charFrequency = CalculateFrequency(text);
        List<List<object>> heap = BuildHeap(charFrequency);
        List<object> tree = BuildTree(heap, -1);
        Dictionary<char, string> codes = MakeDict(tree);

        string encodedText = "";

        foreach (char c in text)
        {
            encodedText += codes[c];
        }

        return encodedText;
    }


    public static Dictionary<char, int> CalculateFrequency(string myText)
    {
        Dictionary<char, int> frequency = new Dictionary<char, int>();

        foreach (char character in myText)
        {
            if (frequency.ContainsKey(character))
            {
                frequency[character]++;
            }
            else
            {
                frequency[character] = 1;
            }
        }

        return frequency;
    }


    public static List<List<object>> BuildHeap(Dictionary<char, int> freq)
    {
        List<List<object>> heap = new List<List<object>>();

        foreach (KeyValuePair<char, int> pair in freq)
        {
            heap.Add(new List<object> { pair.Value, new List<object> { pair.Key, "" } });
        }
        heap.Sort((x, y) => (int)x[0] - (int)y[0]);

        return heap;
    }


    public static List<object> BuildTree(List<List<object>> heap)
    {
        while (heap.Count > 1)
        {
            List<object> lo = heap[0];
            heap.RemoveAt(0);
            List<object> hi = heap[0];
            heap.RemoveAt(0);

            foreach (List<object> pair in lo.GetRange(1, lo.Count - 1))
            {
                pair[1] = "0" + pair[1];
            }
            foreach (List<object> pair in hi.GetRange(1, hi.Count - 1))
            {
                pair[1] = "1" + pair[1];
            }

            List<object> newNode = new List<object> { (int)lo[0] + (int)hi[0] };
            newNode.AddRange(lo.GetRange(1, lo.Count - 1));
            newNode.AddRange(hi.GetRange(1, hi.Count - 1));
            heap.Add(newNode);
            heap.Sort((x, y) => (int)x[0] - (int)y[0]);
        }

        return heap[0];
    }
    public static List<object> BuildTree(List<List<object>> heap, int a = -1)
    {
        while (heap.Count > 1)
        {
            List<object> lo = heap[0];
            heap.RemoveAt(0);
            List<object> hi = heap[0];
            heap.RemoveAt(0);

            foreach (List<object> pair in lo.GetRange(1, lo.Count - 1))
            {
                pair[1] = "1" + pair[1];
            }
            foreach (List<object> pair in hi.GetRange(1, hi.Count - 1))
            {
                pair[1] = "0" + pair[1];
            }

            List<object> newNode = new List<object> { (int)lo[0] + (int)hi[0] };
            newNode.AddRange(lo.GetRange(1, lo.Count - 1));
            newNode.AddRange(hi.GetRange(1, hi.Count - 1));
            heap.Add(newNode);
            heap.Sort((x, y) => (int)x[0] - (int)y[0]);
        }

        return heap[0];
    }


    public static Dictionary<char, string> MakeDict(List<object> tree)
    {
        Dictionary<char, string> huffCodes = new Dictionary<char, string>();

        for (int i = 1; i < tree.Count; i++)
        {
            List<object> pair = (List<object>)tree[i];
            char c = (char)pair[0];
            string code = (string)pair[1];
            huffCodes[c] = code;
        }

        return huffCodes;
    }


    public static string Decode(Dictionary<char, string> codeTable, string s)
    {
        string decoded = "";
        while (s.Length > 0)
        {
            int i = 0;
            string acc = "";
            while (i < s.Length)
            {
                char currentChar = s[i];
                acc += currentChar;
                if (codeTable.ContainsValue(acc))
                {

                    foreach (var kvp in codeTable)
                    {
                        if (kvp.Value == acc)
                        {
                            acc = kvp.Key.ToString();
                            break;
                        }
                    }
                    i += acc.Length;
                    break;
                }
                i += 1;
            }
            s = s.Substring(i);
            decoded += acc;
        }
        return decoded;
    }

}
