using System;
using System.Collections.Generic;

namespace Project2_INFO5101
{
    public static class ExpressEvaluation
    {

        public static List<KeyValuePair<int, string>> Postfix = new List<KeyValuePair<int, string>>();
        public static List<KeyValuePair<int, string>> Prefix = new List<KeyValuePair<int, string>>();
        //Helper methods
        static bool isalpha(char c)
        {
            if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z')
            {
                return true;
            }
            return false;
        }

        static bool isdigit(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return true;
            }
            return false;
        }

        static bool isOperator(char c)
        {
            return (!isalpha(c) && !isdigit(c));
        }

        static int getPriority(char C)
        {
            if (C == '-' || C == '+')
                return 1;
            else if (C == '*' || C == '/')
                return 2;
            else if (C == '^')
                return 3;

            return 0;
        }

        // Reverse the letters of the word
        static string reverse(char[] str, int start, int end)
        {
            // Temporary variable to store character
            char temp;
            while (start < end)
            {
                // Swapping the first and last character
                temp = str[start];
                str[start] = str[end];
                str[end] = temp;
                start++;
                end--;
            }
            return string.Join("", str);
        }
        static string infixToPostfix(char[] infix1)
        {
            string infix = '(' + string.Join("", infix1) + ')';

            int l = infix.Length;
            Stack<char> char_stack = new Stack<char>();
            string output = "";

            for (int i = 0; i < l; i++)
            {

                // If the scanned character is an
                // operand, add it to output.
                if (isalpha(infix[i]) || isdigit(infix[i]))
                    output += infix[i];

                // If the scanned character is an
                // ‘(‘, push it to the stack.
                else if (infix[i] == '(')
                    char_stack.Push('(');

                // If the scanned character is an
                // ‘)’, pop and output from the stack
                // until an ‘(‘ is encountered.
                else if (infix[i] == ')')
                {
                    while (char_stack.Peek() != '(')
                    {
                        output += char_stack.Peek();
                        char_stack.Pop();
                    }

                    // Remove '(' from the stack
                    char_stack.Pop();
                }

                // Operator found
                else
                {
                    if (isOperator(infix[i]))
                    {
                        while ((char_stack.Count != 0) &&
                                 (getPriority(infix[i]) <= getPriority(char_stack.Peek()))
                                   )
                        {
                            output += char_stack.Peek();
                            char_stack.Pop();
                        }

                        // Push current Operator on stack
                        char_stack.Push(infix[i]);
                    }
                }
            }
            while (char_stack.Count != 0)
            {
                output += char_stack.Pop();
            }
            return output;
        }
        public class InfixToPostfix
        {
            //public List<KeyValuePair<int, string>> Postfix = new List<KeyValuePair<int, string>>();
            public static List<KeyValuePair<int, string>> ConvertPostfix(List<KeyValuePair<int, string>> Infix)
            {
                //Postfix
                foreach (KeyValuePair<int, string> exp in Infix)
                {
                    char[] arr_infix = exp.Value.ToCharArray();
                    Stack<char> char_stack = new Stack<char>();
                    string output = "";
                    for (int i = 0; i < arr_infix.Length; i++)
                    {
                        if (isalpha(arr_infix[i]) || isdigit(arr_infix[i]))
                            output += arr_infix[i];
                        else if (arr_infix[i] == '(')
                            char_stack.Push(arr_infix[i]);
                        else if (arr_infix[i] == ')')
                        {
                            while (char_stack.Peek() != '(')
                            {
                                output += char_stack.Peek();
                                char_stack.Pop();
                            }
                            char_stack.Pop();
                        }
                        else
                        {
                            if (isOperator(arr_infix[i]))
                            {

                                while ((char_stack.Count != 0) && (getPriority(arr_infix[i]) <= getPriority(char_stack.Peek())))
                                {
                                    output += char_stack.Peek();
                                    char_stack.Pop();
                                }
                                char_stack.Push(arr_infix[i]);
                            }

                        }
                    }
                    while (char_stack.Count != 0)
                    {
                        output += char_stack.Pop();
                    }

                    Postfix.Add(new KeyValuePair<int, string>(exp.Key, output));
                }
                return Postfix;
            }//End of ConversePostfix
        }// End of InfixToPosfix


        public class InfixToPrefix
        {

            public static List<KeyValuePair<int, string>> ConvertPrefix(List<KeyValuePair<int, string>> Infix)
            {
                foreach (KeyValuePair<int, string> exp in Infix)
                {
                    char[] arr_infix = exp.Value.ToCharArray();
                    string infix1 = reverse(arr_infix, 0, arr_infix.Length - 1);
                    arr_infix = infix1.ToCharArray();

                    // Replace ( with ) and vice versa
                    for (int i = 0; i < arr_infix.Length; i++)
                    {

                        if (arr_infix[i] == '(')
                        {
                            arr_infix[i] = ')';
                            i++;
                        }
                        else if (arr_infix[i] == ')')
                        {
                            arr_infix[i] = '(';
                            i++;
                        }
                    }

                    string prefix = infixToPostfix(arr_infix);
                    prefix = reverse(prefix.ToCharArray(), 0, prefix.Length - 1);
                    Prefix.Add(new KeyValuePair<int, string>(exp.Key, prefix));
                }

                return Prefix;
            }//End of ConvertPrefix
        }

    }
}
