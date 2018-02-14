using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompileChems {
    class Program {
        static void Main(string[] args) {
            Console.Write("Please enter path of chemistry file in the current directory: ");
            string filename = Console.ReadLine();
            string path = Directory.GetCurrentDirectory() + "\\chem_test.txt";
            Console.WriteLine("Reading from " + path);

            StreamReader sr = new StreamReader(path);
            Dictionary<string, int> ckeyDic = new Dictionary<string, int>();

            //ask user for reagent name
            Console.Write("Enter name of reagent as given in Chemistry-Reagents.dm (for example dexalinp): ");
            string reagentName = Console.ReadLine();
            reagentName = reagentName.ToLower(); //make sure dexalinp isn't DEXALINP or DexalinP

            string patternReagent = "(?<=u of )" + reagentName + "\\b"; // (?<=u of )reagentName\b      matches reagentName after "u of " and not reagentName as a partial word
            string patternName = "(\\b(?!by)[^\\s]+)$"; // (\b(?!by)[^\s]+)$                            matches 1 word at the end of the string, but not "by"
            string patternDose = "(?<=\\|\\|\\s)\\d+"; // (?<=\|\|\s)\d+                         matches one or more digits after "|| "
            Regex rgxReagent = new Regex(patternReagent);

            string line;
            string name;
            string dose;
            while ((line = sr.ReadLine()) != null) {
                //match reagent name and only check for the rest if reagent matches
                Match matchReagent = rgxReagent.Match(line);
                if (matchReagent.Success) {
                    //match name
                    name = MatchString(line, patternName);
                    Console.Write(name);
                    //add to dic

                    
                    Console.Write(" has created ");

                    //match reagent dose
                    dose = MatchString(line, patternDose);
                    Console.Write(dose);
                    Console.Write("u of " + reagentName);

                    Console.WriteLine();
                }
            }

            sr.Close();
            Console.ReadKey();
        }

        private static string MatchString(string line, string pattern) {
            Regex rgx = new Regex(pattern);
            string result = null;

            Match match = rgx.Match(line);
            if (match.Success) {
                result = match.Value;
            }

            return result;
        }
    }
}
