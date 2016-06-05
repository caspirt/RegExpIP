using System;
using System.Text.RegularExpressions;
using CommandLine;
using System.IO;

namespace RegExpIP
{
    // Define a class to receive parsed values
    class Options
    {
        [Option('i', "input file", Required = true,
          HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option('m', "method", Required = false,
          HelpText = "1 - ip, 2 - adress")]
        public int Method { get; set; }

    }

    class regExFilteringToFile
    {
        public string pathToTxtInputFileWithDataList; // path to input file
        public string regexPattern; // regular expression patern

        public void regex_match()
        {
            // variable for reading from file
            string line;
            // variable for Output file
            string pathToTxtOutputFileWithDataList;

            // define input file
            StreamReader fileInput = new StreamReader(pathToTxtInputFileWithDataList);

            // regex for filename
            string patFileName = @"([^\.]*)";
            Regex r1 = new Regex(patFileName, RegexOptions.IgnoreCase);

            // regex for file extension
            string patFileExt = @"\.([A-Za-z0-9]+)$";
            Regex r2 = new Regex(patFileExt, RegexOptions.IgnoreCase);

            //regexp evaluating for output file
            Match fileName = r1.Match(pathToTxtInputFileWithDataList);
            Match fileExt = r2.Match(pathToTxtInputFileWithDataList);

            //evaluating output filename
            pathToTxtOutputFileWithDataList = string.Concat(fileName.ToString(), "_result.", fileExt.ToString());

            // define output file
            StreamWriter fileOutput = new StreamWriter(pathToTxtOutputFileWithDataList);

            //define regexp and temp variable
            Regex row = new Regex(regexPattern, RegexOptions.IgnoreCase);
            string comparisonResult;


            //while EOF
            while ((line = fileInput.ReadLine()) != null)
            {
                //processing RegExp for next line from file
                Match rowResult = row.Match(line);
                //Comparing RegExp result and input string
                if (String.Compare(rowResult.ToString(), line) == 0)
                    comparisonResult = "match";
                else
                    comparisonResult = "no match";
                //write result to file
                fileOutput.WriteLine (string.Concat(line, "\t", comparisonResult));
                Console.WriteLine(string.Concat(line, "\t", comparisonResult));
            }
            //close all file
            fileInput.Close();
            fileOutput.Close();

            Console.WriteLine("All done, press any key to exit");
            Console.ReadKey();
        }
    }

    class Program
    {

        static int Main(string[] args)
        {

            var options = new Options();

            string regPatIP = @"((0?[0-3]?[0-7]{0,10}$)|((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|0+[1-3]?[0-7]{0,2}|0x[0-9A-Fa-f][0-9A-Fa-f])\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|0+[1-3]?[0-7]{0,2}|0x[0-9A-Fa-f][0-9A-Fa-f])\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|0+[1-3]?[0-7]{0,2}|0x[0-9A-Fa-f][0-9A-Fa-f])\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|0+[1-3]?[0-7]{0,2}|0x[0-9A-Fa-f][0-9A-Fa-f])$)|(429496729[0-5]|42949672[0-8]\d|4294967[01]\d\d|429496[0-6]\d{3}|42949[0-5]\d{4}|4294[0-8]\d{5}|429[0-3]\d{6}|42[0-8]\d{7}|4[01]\d{8}|[1-3]\d{0,9}|[4-9]\d{0,8}$)|(0x0*[0-9a-f]{1,8}$))";
            string regPatName = @"(http(s)?:\/\/[0-9A-Za-z]([\-\.0-9A-Za-z]*([A-Za-z]|([A-Za-z][0-9])){1,62})*(\.)([0-9A-Za-z])+(\:[0-9]{2,5})?(\/?)$)";

            Parser.Default.ParseArguments(args, options);
            while ((args.Length == 0) && (String.IsNullOrEmpty(options.InputFile)) )
            {
                Console.WriteLine("No parameters for run . ");
                Console.WriteLine("Possible usage: RegExpIP -i 'pathToInputFile' [-m 1 ]");
                string argStr;

                if ((args.Length == 0) || (String.IsNullOrEmpty(options.InputFile)))
                {
                    Console.WriteLine("After m 1 - ip, other - adress");
                    Console.WriteLine("Now you may select default filename (TestData_IP4RegExp_DataSet1.txt) for current folder (just click enter) or ");
                    Console.WriteLine("enter new filename or exit by typing EXIT. Result file will be with _result adding");

                    argStr = Console.ReadLine();
                    if (argStr == "")
                    { //if empty string - take hardcoded filename
                        options.InputFile = "TestData_IP4RegExp_DataSet1.txt";
                    }
                    else if (argStr.ToLower() == "EXIT")
                    { //EXIT
                        Console.WriteLine("Bye");
                        return 1;
                    }
                    else
                    {
                        options.InputFile = argStr;
                    }
                }
                if ((options.Method != 1) | (options.Method != 2))
                {
                    Console.WriteLine("Now you may define method  1 - ip, other - adress");
                    Console.WriteLine("enter new filename or exit by typing EXIT. Result file will be with _result adding");
                    argStr = Console.ReadLine();
                    if ((argStr != "1") || (options.Method != 1))
                    {
                        Console.WriteLine("Will be adress filtering");
                        options.Method = 2;
                    }
                    else
                    {
                        Console.WriteLine("Will be ip filtering");
                        options.Method = 1;
                    }
                }
            }

            FileStream fs = null;

            try
            {
                fs = new FileStream(options.InputFile, FileMode.Open);
            }
            catch
            {
                Console.WriteLine("Imposible to open file ");
                return 1;
            }
            fs.Close();

            if (options.Method == 1)
            {
                regExFilteringToFile IP = new regExFilteringToFile();
                IP.pathToTxtInputFileWithDataList = options.InputFile;
                IP.regexPattern = regPatIP;
                IP.regex_match();
            }
            else
            {
                regExFilteringToFile Adress = new regExFilteringToFile();
                Adress.pathToTxtInputFileWithDataList = options.InputFile;
                Adress.regexPattern = regPatName;
                Adress.regex_match();
            }
            return 0;
        }



    }
}
