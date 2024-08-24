using System;

public class Running
{
    static string Encode(string input)
    {
        string code_string = "";
        int count = 1;
        
        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == input[i - 1])
            {
                count++;
            }
            else
            {
                code_string += input[i - 1];
                code_string += Convert.ToString(count);
                count = 1;
            }
        }
        
        code_string += input[input.Length - 1];
        code_string += count;
        
        return code_string;
    }

    static string Decode(string input)
    {
        string decode_string = "";

        for (int i = 0; i < input.Length; i += 2)
        {
            char c = input[i];
            int count = 0;

            if (i + 1 < input.Length && char.IsDigit(input[i + 1]))
            {
                int nextCharIndex = i + 1;
                while (nextCharIndex < input.Length && char.IsDigit(input[nextCharIndex]))
                {
                    count = count * 10 + (input[nextCharIndex] - '0');
                    nextCharIndex++;
                }
                i = nextCharIndex - 2;
            }
            else
            {
                count = int.Parse((input[i + 1]).ToString());
            }

            for (int j = 0; j < count; j++)
            {
                decode_string += c;
            }
        }

        return decode_string;
    }
}
