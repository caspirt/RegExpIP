using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;


namespace RegExpIP
{
    // Define a class to receive parsed values
    class Options
    {
        [Option('f', "file", Required = true,
          HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option('r', "rows", DefaultValue = 10, Required = false,
          HelpText = "Number of rows")]
        public int Rows { get; set; }

    }

    class Program
    {

        string 
        static void Main(string[] args)
        {
        }
    }
}
