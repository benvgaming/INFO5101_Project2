/* Authors: Manh Cuong Nguyen, Devon Tully, James Thornton, Sabrina Umeri
 * Class: InfixToPostfix.cs
 * Purposes: To convert Infix to Postfix
 */

using System.Collections.Generic;

namespace Project2_INFO5101
{
    public static class InfixToPostfix
    {
        public static List<KeyValuePair<int, string>> Postfix = new List<KeyValuePair<int, string>>();
        static bool isalpha(char c)
        {
            if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z')
            {
                return true;
            }
            return false;
        }//End of isalpha

        static bool isdigit(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return true;
            }
            return false;
        }//End of isdigit

        static bool isOperator(char c)
        {
            return (!isalpha(c) && !isdigit(c));
        }//end of isOperator

        static int getPriority(char C)
        {
            if (C == '-' || C == '+')
                return 1;
            else if (C == '*' || C == '/')
                return 2;
            else if (C == '^')
                return 3;

            return 0;
        }//end of getPriority

        public static List<KeyValuePair<int, string>> ConvertPostfix(List<KeyValuePair<int, string>> Infix)
        {
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
    }//End of class
}//End of namespace
