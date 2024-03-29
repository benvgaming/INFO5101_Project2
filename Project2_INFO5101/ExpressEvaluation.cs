﻿/* Authors: Manh Cuong Nguyen, Devon Tully, James Thornton, Sabrina Umeri
 * Class: ExpressEvaluation.cs
 * Purposes: To evaluate postfix and prefix
 */

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        }//isalpha

        static bool isdigit(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return true;
            }
            return false;
        }//isdigit

        public static List<KeyValuePair<int, string>> EvaluatePostfix(List<KeyValuePair<int, string>> postfix)
        {
            List<KeyValuePair<int, string>> PostfixEvaluated = new List<KeyValuePair<int, string>>();
            foreach (KeyValuePair<int, string> exp in postfix)
            {
                Stack<double> operand = new Stack<double>();
                BinaryExpression binaryExpression;
                char[] e = exp.Value.ToCharArray();
                double result = 0;
                for (int i = 0; i < e.Length; i++)
                {
                    result = 0;
                    if (isalpha(e[i]) || isdigit(e[i]))
                    {
                        operand.Push(char.GetNumericValue(e[i]));
                    }
                    else
                    {
                        double op2 = operand.Peek();
                        operand.Pop();
                        double op1 = operand.Peek();
                        operand.Pop();
                        if (e[i] == '/')
                        {
                            binaryExpression =
                            Expression.MakeBinary(
                                ExpressionType.Divide,
                                Expression.Constant(op1),
                                Expression.Constant(op2));

                            Func<double> compiled = Expression.Lambda<Func<double>>(binaryExpression).Compile();

                            result = compiled.Invoke();
                        }
                        else if (e[i] == '*')
                        {
                            binaryExpression =
                            Expression.MakeBinary(
                                ExpressionType.Multiply,
                                Expression.Constant(op1),
                                Expression.Constant(op2));

                            Func<double> compiled = Expression.Lambda<Func<double>>(binaryExpression).Compile();

                            result = compiled.Invoke();
                        }
                        else if (e[i] == '+')
                        {
                            binaryExpression =
                            Expression.MakeBinary(
                                ExpressionType.Add,
                                Expression.Constant(op1),
                                Expression.Constant(op2));

                            Func<double> compiled = Expression.Lambda<Func<double>>(binaryExpression).Compile();
                            result = compiled.Invoke();
                        }
                        else if (e[i] == '-')
                        {

                            binaryExpression =
                            Expression.MakeBinary(
                                ExpressionType.Subtract,
                                Expression.Constant(op1),
                                Expression.Constant(op2));
                            Func<double> compiled = Expression.Lambda<Func<double>>(binaryExpression).Compile();
                            result = compiled.Invoke();
                        }
                        operand.Push(result);
                    }
                }
                PostfixEvaluated.Add(new KeyValuePair<int, string>(exp.Key, operand.Peek().ToString()));
            }
            return PostfixEvaluated;
        }//End of EvaluatePostfix
        public static List<KeyValuePair<int, string>> EvaluatePrefix(List<KeyValuePair<int, string>> prefix)
        {
            List<KeyValuePair<int, string>> PrefixEvaluated = new List<KeyValuePair<int, string>>();
            foreach (KeyValuePair<int, string> exp in prefix)
            {
                Stack<double> Stack = new Stack<double>();
                BinaryExpression binaryExpression;
                Func<double> compiled;
                string exprsn = exp.Value;
                for (int j = exprsn.Length - 1; j >= 0; j--)
                {

                    // Push operand to Stack
                    // To convert exprsn[j] to digit subtract
                    // '0' from exprsn[j].
                    if (isalpha(exprsn[j]) || isdigit(exprsn[j]))
                        Stack.Push((double)(exprsn[j] - 48));

                    else
                    {

                        // Operator encountered
                        // Pop two elements from Stack
                        double o1 = Stack.Peek();
                        var op1 = Expression.Constant(Stack.Peek());
                        Stack.Pop();
                        double o2 = Stack.Peek();
                        var op2 = Expression.Constant(Stack.Peek());
                        Stack.Pop();

                        // Use switch case to operate on o1
                        // and o2 and perform o1 O o2.
                        switch (exprsn[j])
                        {
                            case '+':
                                binaryExpression = Expression.Add(op1, op2);
                                compiled = Expression.Lambda<Func<double>>(binaryExpression).Compile();
                                Stack.Push(compiled.Invoke());
                                break;
                            case '-':
                                binaryExpression = Expression.Subtract(op1, op2);
                                compiled = Expression.Lambda<Func<double>>(binaryExpression).Compile();
                                Stack.Push(compiled.Invoke());
                                break;
                            case '*':
                                binaryExpression = Expression.Multiply(op1, op2);
                                compiled = Expression.Lambda<Func<double>>(binaryExpression).Compile();
                                Stack.Push(compiled.Invoke());
                                break;
                            case '/':

                                binaryExpression = Expression.Divide(op1, op2);
                                compiled = Expression.Lambda<Func<double>>(binaryExpression).Compile();
                                Stack.Push(compiled.Invoke());
                                break;
                        }
                    }
                }
                PrefixEvaluated.Add(new KeyValuePair<int, string>(exp.Key, Stack.Peek().ToString()));
            }
            return PrefixEvaluated;
        }//End of EvaluatePrefix
    }//End of class
}//End of namespace
