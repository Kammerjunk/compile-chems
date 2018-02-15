using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompileChems {
    public class ChemCompilerBase {
        protected Dictionary<string, int> _ckeyDic;
        protected string _name;
        protected string _dose;
        protected List<string> _resultList;
        protected string _reagentName;
        protected StreamReader _sr;

        protected ChemCompilerBase() {
            _ckeyDic = new Dictionary<string, int>();
            _resultList = new List<string>();
        }

        protected string GetReagentName() {
            Console.Write("Enter ID of reagent as given in Chemistry-Reagents.dm (for example dexalinp): ");
            string reagentName = Console.ReadLine();
            reagentName = reagentName.ToLower(); //make sure dexalinp isn't DEXALINP or DexalinP
            return reagentName;
        }

        protected void DoLines(string reagentName, StreamReader sr) {
            string line;
            string patternReagent = "(?<=u of )" + reagentName + " have been"; // (?<=u of )reagentName have been               matches to "u of " and "reagentname" and "have been" in order
            string patternName = "((?<=last touched by )|(?<=carried by )).+$"; // ((?<=last touched by )|(?<=carried by )).+$  matches 1 or more chars at the end of the string after "last touched by " OR "carried by "
            string patternDose = "(?<=\\|\\|\\s)\\d+"; // (?<=\|\|\s)\d+                                                        matches one or more digits after "|| "
            Regex rgxReagent = new Regex(patternReagent);

            while ((line = sr.ReadLine()) != null) {
                Match matchReagent = rgxReagent.Match(line);
                if (matchReagent.Success) {
                    _name = MatchString(line, patternName); //match name
                    _dose = MatchString(line, patternDose); //match reagent dose

                    //add to dic
                    if (Int32.TryParse(_dose, out int doseInt)) {
                        if (!_ckeyDic.ContainsKey(_name)) {
                            _ckeyDic.Add(_name, doseInt);
                        } else {
                            _ckeyDic[_name] = _ckeyDic[_name] + doseInt;
                        }
                    }
                }
            }
        }

        protected void DictionaryToOrderedList(Dictionary<string, int> dictionary) {
            var ordered = _ckeyDic.OrderByDescending(x => x.Value); //sort dictionary by amount of reagent created
            foreach(KeyValuePair<string, int> kvp in ordered) {
                _resultList.Add($"{kvp.Key} created {kvp.Value}u {_reagentName}");
            }
        }

        protected string MatchString(string line, string pattern) {
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
