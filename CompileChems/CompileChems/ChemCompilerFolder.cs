using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace CompileChems {
    static class ChemCompilerFolder {
        private static Dictionary<string, int> _ckeyDic = new Dictionary<string, int>();
        private static string _name;
        private static string _dose;

        public static void CompileChems() {
            StreamReader sr;
            List<string> resultList = new List<string>();
            //get folder name from user
            string path = FileAccessing.GetFolderPath();
            string[] files = Directory.GetFiles(path);

            //ask user for reagent name
            Console.Write("Enter ID of reagent as given in Chemistry-Reagents.dm (for example dexalinp): ");
            string reagentName = Console.ReadLine();
            reagentName = reagentName.ToLower(); //make sure dexalinp isn't DEXALINP or DexalinP




            foreach(string file in files) {
                string filePath = path + $"{file}";
                sr = new StreamReader(filePath);

                DoLines(reagentName, sr);

                sr.Close();
            }
            var ordered = _ckeyDic.OrderByDescending(x => x.Value); //sort dictionary by amount of reagent created
            //add gathered dictionary of ckeys vs created amount to list for writing
            foreach(KeyValuePair<string, int> kvp in ordered) {
                resultList.Add($"{kvp.Key} created {kvp.Value}u {reagentName}");
            }

            FileAccessing.WriteToFile(resultList);
            Console.WriteLine("Streams closed.");
        }

        private static void DoLines(string reagentName, StreamReader sr) {
            string line;
            string patternReagent = "(?<=u of )" + reagentName + " have been"; // (?<=u of )reagentName have been               matches to "u of " and "reagentname" and "have been" in order
            string patternName = "((?<=last touched by )|(?<=carried by )).+$"; // ((?<=last touched by )|(?<=carried by )).+$  matches 1 or more chars at the end of the string after "last touched by " OR "carried by "
            string patternDose = "(?<=\\|\\|\\s)\\d+"; // (?<=\|\|\s)\d+                                                        matches one or more digits after "|| "
            Regex rgxReagent = new Regex(patternReagent);

            while ((line = sr.ReadLine()) != null) {
                Match matchReagent = rgxReagent.Match(line);
                if(matchReagent.Success) {
                    _name = MatchString(line, patternName); //match name
                    _dose = MatchString(line, patternDose); //match reagent dose

                    //add to dic
                    if(Int32.TryParse(_dose, out int doseInt)) {
                        if(!_ckeyDic.ContainsKey(_name)) {
                            _ckeyDic.Add(_name, doseInt);
                        } else {
                            _ckeyDic[_name] = _ckeyDic[_name] + doseInt;
                        }
                    }
                }
            }
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
