using System;

public class LZW
{

    static List<int> Compress(string uncompressed, int bitSize)
    {
        
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        int maxDictionarySize = (int)Math.Pow(2, bitSize);
        for (int i = 0; i < maxDictionarySize; i++)
            dictionary.Add(((char)i).ToString(), i);
        string w = string.Empty;
        List<int> compressed = new List<int>();

        foreach (char c in uncompressed)
        {
            string wc = w + c;
            if (dictionary.ContainsKey(wc))
            {
                w = wc;
            }
            else
            {
                compressed.Add(dictionary[w]);
                dictionary.Add(wc, dictionary.Count);
                w = c.ToString();
            }
        }

        if (!string.IsNullOrEmpty(w))
            compressed.Add(dictionary[w]);
        Console.WriteLine("Coding: ");

        foreach (char c in uncompressed)
        {
            string g = c.ToString();
            char l = Convert.ToChar(dictionary[g]);
            Console.Write(l + " ");
        }

        Console.Write("\nKey: ");
        for (int j = 0; j < compressed.Count; j++)
        {
            string k = dictionary.Where(x => x.Value == compressed[j]).FirstOrDefault().Key;
            Console.Write(" " + k);
        }
        
        Console.Write("\nEqual: ");
        for (int j = 0; j < compressed.Count; j++)
        {
            Console.Write(" " + compressed[j]);
        }

        return compressed;
    }


    static string Decompress(List<int> compressed, int bitSize)
    {
        
        Dictionary<int, string> dictionary = new Dictionary<int, string>();
        int maxDictionarySize = (int)Math.Pow(2, bitSize);
        for (int i = 0; i < maxDictionarySize; i++)
            dictionary.Add(i, ((char)i).ToString());
        string w = dictionary[compressed[0]];
        compressed.RemoveAt(0);
        StringBuilder decompressed = new StringBuilder(w);

        foreach (int k in compressed)
        {
            string entry = null;
            if (dictionary.ContainsKey(k))
                entry = dictionary[k];
            else if (k == dictionary.Count)
                entry = w + w[0];

            decompressed.Append(entry);

            // new sequence; add it to the dictionary
            dictionary.Add(dictionary.Count, w + entry[0]);

            w = entry;
        }

        return decompressed.ToString();
    }

}
