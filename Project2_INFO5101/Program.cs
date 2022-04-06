using System;

namespace Project2_INFO5101
{
    class Program
    {
        const string PATH_CSV = ".\\..\\..\\..\\..\\Data\\Project 2_INFO_5101.csv";
        const string PATH_TXT = ".\\..\\..\\..\\..\\Data\\Project 2_INFO_5101.txt";
        static void Main(string[] args)
        {
            CSVFile reader = new CSVFile();

            reader.CSVDeserialize(PATH_CSV);
            foreach(var e in reader.expressions)
            {
                Console.WriteLine($"{e.Key} - {e.Value}");
            }
        }
    }
}
