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

            Infix = CSVFile.CSVDeserialize(PATH_CSV);
            Console.WriteLine("infix: ");
            foreach(var e in Infix)
            {
                Console.WriteLine($"{e.Key} - {e.Value}");
            }
            
            Console.WriteLine("postfix: ");
            Postfix = ExpressEvaluation.InfixToPostfix.ConvertPostfix(Infix);
            foreach(var e in Postfix)
            {
                Console.WriteLine($"{e.Key} - {e.Value}");
            }

            Console.WriteLine("prefix: ");
            Prefix = ExpressEvaluation.InfixToPrefix.ConvertPrefix(Infix);
            foreach (var e in Prefix)
                Console.WriteLine($"{e.Key} - {e.Value}");

            //Testing write xml
            XMLExtension xml = new XMLExtension();
            xml.WriteStartDocument();
            xml.WriteStartRootElement();
            xml.WriteStartElement("element1");
            xml.WriteAttribute("attribute1");
            xml.WriteEndElement();
            xml.WriteEndRootElement();
        }
    }
}
