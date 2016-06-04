﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CommandLine;


namespace RegExpIP
{
    // Define a class to receive parsed values
    class Options
    {
        [Option('i', "input file", Required = true,
          HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option('o', "output file", Required = false,
          HelpText = "Input file to be processed or just add _result to the end of name")]
        public string OutputFile { get; set; }

    }

    class Program
    {

        static int Main(string[] args)
        {

            var options = new Options();

            CommandLine.Parser.Default.ParseArguments(args, options);
            while (args.Length == 0)
            {
                System.Console.WriteLine("No parameters for run . ");
                System.Console.WriteLine("Possible usage: RegExpIP -i 'pathToInputFile' [-o 'pathToInputFile']");
                System.Console.WriteLine("Now you may select default filename (TestData_IP4RegExp_DataSet1.txt) for current folder (just click enter) or ");
                System.Console.WriteLine("enter new filename or exit by typing EXIT. Result file will be with _result adding");
                String argStr;
                argStr = System.Console.ReadLine();
                if (argStr == "")
                {
                    options.InputFile = "TestData_IP4RegExp_DataSet1.txt";
                    options.OutputFile = "TestData_IP4RegExp_DataSet1_result.txt";
                }
                else if (argStr.ToLower() == "EXIT")
                {
                    Console.WriteLine("Bye");
                    return 1;
                }
                else
                {
                    // Instantiate the regular expression object.
                    string patFileName = @"(.+?)(\.[^.]*$|$)";
                    Regex r1 = new Regex(patFileName, RegexOptions.IgnoreCase);

                    string patFileExt = @"\.([A-Za-z0-9]+)$";
                    Regex r2 = new Regex(patFileName, RegexOptions.IgnoreCase);

                    Match filename1 = r1.Match(argStr);
                    Match filename2 = r2.Match(argStr);

                    options.InputFile = argStr;
                    options.OutputFile = String.Concat(filename1.ToString(),"_result.",filename2.ToString());
                }



            }



            return 0;
        }
    }
}
