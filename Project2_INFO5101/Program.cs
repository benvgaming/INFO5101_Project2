using System;
using System.Collections.Generic;

namespace Project2_INFO5101
{
    class Program
    {
        const string PATH_CSV = ".\\..\\..\\..\\..\\Data\\Project 2_INFO_5101.csv";
        const string PATH_TXT = ".\\..\\..\\..\\..\\Data\\Project 2_INFO_5101.txt";
        static void Main(string[] args)
        {
            //CSVFile reader = new CSVFile();
            //ExpressEvaluation converter = new ExpressEvaluation();
            
            List<KeyValuePair<int, string>> Infix = new List<KeyValuePair<int, string>>();
            List<KeyValuePair<int, string>> Postfix = new List<KeyValuePair<int, string>>();
            List<KeyValuePair<int, string>> Prefix = new List<KeyValuePair<int, string>>();
            List<KeyValuePair<int, string>> PostfixEvaluated = new List<KeyValuePair<int, string>>();
            List<KeyValuePair<int, string>> PrefixEvaluated = new List<KeyValuePair<int, string>>();
            Infix = CSVFile.CSVDeserialize(PATH_CSV);
            Console.WriteLine("infix: ");
            foreach(var e in Infix)
            {
                Console.WriteLine($"{e.Key} - {e.Value}");
            }
            
            Console.WriteLine("postfix: ");
            Postfix = InfixToPostfix.ConvertPostfix(Infix);
            foreach(var e in Postfix)
            {
                Console.WriteLine($"{e.Key} - {e.Value}");
            }

            Console.WriteLine("prefix: ");
            Prefix = InfixToPrefix.ConvertPrefix(Infix);
            foreach (var e in Prefix)
                Console.WriteLine($"{e.Key} - {e.Value}");

            //Testing evaluation
            PostfixEvaluated = ExpressEvaluation.EvaluatePostfix(Postfix);
            foreach (var e in PostfixEvaluated)
                Console.WriteLine($"{e.Key} - {e.Value}");

            PrefixEvaluated = ExpressEvaluation.EvaluatePrefix(Prefix);
            foreach (var e in PrefixEvaluated)
                Console.WriteLine($"{e.Key} - {e.Value}");

            //Testing comparer
            CompareExpressions comparer = new CompareExpressions();
            foreach(var e in PostfixEvaluated)
                Console.WriteLine($"{(comparer.Compare(e, PrefixEvaluated[e.Key - 1]) == 1 ? "true" : "false")}" );
            //Testing write xml
            XMLExtension xml = new XMLExtension();

            xml.WriteStartDocument();
            xml.WriteStartRootElement();
            foreach(var e in Infix)
            {
                xml.WriteStartElement("element");
                xml.WriteStartElement("sno");
                xml.WriteAttribute(e.Key.ToString());
                xml.WriteEndElement();
                xml.WriteStartElement("infix");
                xml.WriteAttribute(e.Value);
                xml.WriteEndElement();

                xml.WriteStartElement("prefix");
                xml.WriteAttribute(Prefix[e.Key -1].Value);
                xml.WriteEndElement();
                xml.WriteStartElement("postfix");
                xml.WriteAttribute(Postfix[e.Key -1].Value);
                xml.WriteEndElement();
                xml.WriteStartElement("evaluation");
                xml.WriteAttribute(PrefixEvaluated[e.Key -1].Value);
                xml.WriteEndElement();
                xml.WriteStartElement("comparison");
                xml.WriteAttribute(comparer.Compare(PrefixEvaluated[e.Key - 1], PostfixEvaluated[e.Key - 1]) == 1 ? "true" : "false");
                xml.WriteEndElement();
                xml.WriteEndElement();
            }
            xml.WriteEndRootElement();
        }
    }
}
