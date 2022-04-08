/* Authors: Manh Cuong Nguyen, Devon Tully, James Thornton, Sabrina Umeri
 * Class: InfixToPrefix.cs
 * Purposes: To convert Infix to Prefix
 */


using System.Collections.Generic;

namespace Project2_INFO5101
{
    public static class InfixToPrefix
    {
        public static List<KeyValuePair<int, string>> Prefix = new List<KeyValuePair<int, string>>();
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

        public static List<KeyValuePair<int, string>> ConvertPrefix(List<KeyValuePair<int, string>> Infix)
        {
            foreach (KeyValuePair<int, string> exp in Infix)
            {
                // stack for operators.
                Stack<char> operators = new Stack<char>();

                // stack for operands.
                Stack<string> operands = new Stack<string>();
                string infix = exp.Value;
                for (int i = 0; i < infix.Length; i++)
                {

                    // If current character is an
                    // opening bracket, then
                    // push into the operators stack.
                    if (infix[i] == '(')
                    {
                        operators.Push(infix[i]);
                    }
                    // If current character is a
                    // closing bracket, then pop from
                    // both stacks and push result
                    // in operands stack until
                    // matching opening bracket is
                    // not found.
                    else if (infix[i] == ')')
                    {
                        while (operators.Count != 0 &&
                            operators.Peek() != '(')
                        {

                            // operand 1
                            string op1 = operands.Peek();
                            operands.Pop();

                            // operand 2
                            string op2 = operands.Peek();
                            operands.Pop();

                            // operator
                            char op = operators.Peek();
                            operators.Pop();

                            // Add operands and operator
                            // in form operator +
                            // operand1 + operand2.
                            string tmp = op + op2 + op1;
                            operands.Push(tmp);
                        }

                        // Pop opening bracket
                        // from stack.
                        operators.Pop();
                    }

                    // If current character is an
                    // operand then push it into
                    // operands stack.
                    else if (!isOperator(infix[i]))
                    {
                        operands.Push(infix[i] + "");
                    }

                    // If current character is an
                    // operator, then push it into
                    // operators stack after popping
                    // high priority operators from
                    // operators stack and pushing
                    // result in operands stack.
                    else
                    {
                        while (operators.Count != 0 &&
                            getPriority(infix[i]) <=
                                getPriority(operators.Peek()))
                        {

                            string op1 = operands.Peek();
                            operands.Pop();

                            string op2 = operands.Peek();
                            operands.Pop();

                            char op = operators.Peek();
                            operators.Pop();

                            string tmp = op + op2 + op1;
                            operands.Push(tmp);
                        }

                        operators.Push(infix[i]);
                    }
                }

                // Pop operators from operators
                // stack until it is empty and
                // operation in add result of
                // each pop operands stack.
                while (operators.Count != 0)
                {
                    string op1 = operands.Peek();
                    operands.Pop();

                    string op2 = operands.Peek();
                    operands.Pop();

                    char op = operators.Peek();
                    operators.Pop();

                    string tmp = op + op2 + op1;
                    operands.Push(tmp);
                }

                Prefix.Add(new KeyValuePair<int, string>(exp.Key, operands.Peek()));
            }

            return Prefix;
        }//End of ConvertPrefix
    }//End of class
}//end of namespace
