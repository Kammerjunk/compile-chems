using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CompileChems.RegexHandling;

namespace CompileChems.ChemCompiler {
    /// <summary>
    /// Parent class for classes which must parse strings from /vg/station chemistry logs.
    /// </summary>
    public class ChemCompilerBase {
        protected Dictionary<string, int> _ckeyDic;
        protected string _name;
        protected string _dose;
        protected List<string> _resultList;
        protected string _reagentName;
        protected StreamReader _sr;

        /// <summary>
        /// Initialises a new instance of ChemCompilerBase.
        /// Initialises the dictionary and list properties.
        /// </summary>
        protected ChemCompilerBase() {
            _ckeyDic = new Dictionary<string, int>();
            _resultList = new List<string>();
        }

        /// <summary>
        /// Gets the name of the reagent being searched for through user input, formatted to lowercase.
        /// </summary>
        /// <returns>Returns the fully lowercase name of the reagent.</returns>
        protected string GetReagentName() {
            Console.Write("Enter ID of reagent as given in Chemistry-Reagents.dm (for example dexalinp): ");
            string reagentName = Console.ReadLine();
            reagentName = reagentName.ToLower(); //make sure dexalinp isn't DEXALINP or DexalinP
            return reagentName;
        }

        /// <summary>
        /// Searches through files being fed to the StreamReader and adds them to the Dictionary if they match the pattern for reagents being created.
        /// </summary>
        /// <param name="reagentName">The reagent being searched for.</param>
        /// <param name="sr">The StreamReader going through the files.</param>
        protected void DoLines(string reagentName, StreamReader sr) {
            string line;

            while ((line = sr.ReadLine()) != null) {
                line = FileAccessing.HtmlToPlainText(line);
                if (RegexHandler.MatchFilter(line, RegexPatterns.Reagent(reagentName))) {
                    _name = RegexHandler.MatchCkey(line);
                    //_name = MatchString(line, patternName).ToLower(); //match name
                    _dose = RegexHandler.MatchDose(line);
                    //_dose = MatchString(line, patternDose); //match reagent dose

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

        /// <summary>
        /// Takes the unsorted dictionary with a string key (the ckey/handle creating the reagent) and int value (the amount of the reagent they have created).
        /// Sorts this dictionary by value in descending order and puts it into the list in a readable format.
        /// </summary>
        protected void DictionaryToOrderedList() {
            var ordered = _ckeyDic.OrderByDescending(x => x.Value); //sort dictionary by amount of reagent created
            foreach(KeyValuePair<string, int> kvp in ordered) {
                _resultList.Add($"{kvp.Key} created {kvp.Value}u {_reagentName}");
            }
        }

        /// <summary>
        /// Empties collections for multiple runs.
        /// </summary>
        protected void EmptyCollections() {
            _ckeyDic.Clear();
            _resultList.Clear();
        }

        /// <summary>
        /// Flexibly matches a pattern to a string of any length based on given parameters.
        /// </summary>
        /// <param name="line">The string being tested.</param>
        /// <param name="pattern">The regex pattern the line is being tested with.</param>
        /// <returns>Returns the matched pattern as a string.</returns>
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
