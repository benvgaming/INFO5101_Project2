/* Authors: Manh Cuong Nguyen, Devon Tully, James Thornton, Sabrina Umeri
 * Class: Program.cs
 * Purposes: The UI for the program
 */

using System;
using System.Collections.Generic;
using System.IO;
namespace Project2_INFO5101
{
    class Program
    {
        const string PATH_CSV = ".\\..\\..\\..\\..\\Data\\Project 2_INFO_5101.csv";
        const string PATH_XML = @".\..\..\..\..\Data\Summary.xml";
        const string PATH_TXT = ".\\..\\..\\..\\..\\Data\\Project 2_INFO_5101.txt";
        static void Main(string[] args)
        {
            
            List<KeyValuePair<int, string>> Infix = new List<KeyValuePair<int, string>>();
            List<KeyValuePair<int, string>> Postfix = new List<KeyValuePair<int, string>>();
            List<KeyValuePair<int, string>> Prefix = new List<KeyValuePair<int, string>>();
            List<KeyValuePair<int, string>> PostfixEvaluated = new List<KeyValuePair<int, string>>();
            List<KeyValuePair<int, string>> PrefixEvaluated = new List<KeyValuePair<int, string>>();
            Infix = CSVFile.CSVDeserialize(PATH_CSV);
            Console.WriteLine("infix expressions from reading CSV: ");
            foreach(var e in Infix)
            {
                Console.WriteLine($"{e.Key} - {e.Value}");
            }

            Console.WriteLine();
            Console.WriteLine();
            
            Console.WriteLine("postfix expressions from converting infix expressions: ");
            Postfix = InfixToPostfix.ConvertPostfix(Infix);
            foreach(var e in Postfix)
            {
                Console.WriteLine($"{e.Key} - {e.Value}");
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("prefix expressions from converting infix expressions: ");
            Prefix = InfixToPrefix.ConvertPrefix(Infix);
            foreach (var e in Prefix)
                Console.WriteLine($"{e.Key} - {e.Value}");

            Console.WriteLine();
            Console.WriteLine();

            //Evaluation
            Console.WriteLine("Evaluating postfix expressions");
            PostfixEvaluated = ExpressEvaluation.EvaluatePostfix(Postfix);
            foreach (var e in PostfixEvaluated)
                Console.WriteLine($"{e.Key} - {e.Value}");

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Evaluating prefix expressions"); 
            PrefixEvaluated = ExpressEvaluation.EvaluatePrefix(Prefix);
            foreach (var e in PrefixEvaluated)
                Console.WriteLine($"{e.Key} - {e.Value}");

            //comparing
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Comparing results from prefix evaluation and postfix evaluation");
            CompareExpressions comparer = new CompareExpressions();
            foreach(var e in PostfixEvaluated)
                Console.WriteLine($"{e.Value} == {PrefixEvaluated[e.Key - 1].Value} :  {(comparer.Compare(e, PrefixEvaluated[e.Key - 1]) == 1 ? "true" : "false")}" );
            
            //Table result

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("======================================================================================================================");
            Console.WriteLine("*                                                  Summary report                                                    *"); 
            Console.WriteLine("======================================================================================================================");
            Console.WriteLine(
            String.Format("|{0,10}|{1,20}|{2,20}|{3,20}|{4,15}|{5,15}|{6,10}|", "sno", "infix", "postfix", "prefix","prefix res","postfix res","match")
                );
            Console.WriteLine("======================================================================================================================");
            foreach (var e in Infix)
            {
                Console.WriteLine(
                String.Format("|{0,10}|{1,20}|{2,20}|{3,20}|{4,15}|{5,15}|{6,10}|", e.Key, e.Value, Postfix[e.Key - 1].Value, Prefix[e.Key - 1].Value, PrefixEvaluated[e.Key - 1].Value, PostfixEvaluated[e.Key - 1].Value, comparer.Compare(PostfixEvaluated[e.Key-1], PrefixEvaluated[e.Key - 1]) == 1 ? "true" : "false") 
                    );
            }
            Console.WriteLine("======================================================================================================================");
            //write xml
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
            bool valid;
            string input;
            char selection = '0';
            do
            {
                valid = true;
                Console.Write("Do you want to open XML on a browser?  (Y/N) : ");
                input = Console.ReadLine().Trim();
                if (input.Length == 1)
                    selection = Char.Parse(input.ToUpper());

                if(input.Length != 1)
                {
                    Console.WriteLine("ERROR: INPUT IS NOT VALID");
                    valid = false;
                }
                else if(!"YN".Contains(selection))
                {
                    Console.WriteLine("ERROR: INPUT IS NOT VALID");
                    valid = false;
                }


            } while (!valid);
            
            if(selection == 'Y')
            {
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("msedge.exe");
                info.UseShellExecute = true;
                string path = Path.GetFullPath(PATH_XML);
                info.Arguments = "\""+path+"\"";
                System.Diagnostics.Process.Start(info);
            }
        }//End of main
    }//End of class
}//End of namespace
