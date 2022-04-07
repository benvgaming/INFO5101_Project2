﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_INFO5101
{
    public static class CSVFile
    {
        public static List<KeyValuePair<int,string>> expressions = new List<KeyValuePair<int,string>>();
        public static List<KeyValuePair<int,string>> CSVDeserialize(string path)
        {
            StreamReader reader = new StreamReader(path);
            reader.ReadLine();
            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(",");
                int.TryParse(values[0], out int sno);
                expressions.Add(new KeyValuePair<int, string>(sno, values[1]));
            }
            return expressions;
        }//End of CSVDeserialize
    }//End of Class
}//End of Namespace

