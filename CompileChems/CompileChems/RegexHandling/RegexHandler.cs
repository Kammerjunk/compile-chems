using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompileChems.RegexHandling {
    /// <summary>
    /// Handles 
    /// </summary>
    public static class RegexHandler {
        public static string MatchReagent(string line, string reagentName) {
            // (?<=u of )reagentName have been
            // matches to "u of " and "reagentName" and "have been" in order
            string pattern = RegexPatterns.Reagent(reagentName);
            string result = MatchString(line, pattern);
            return result;
        }

        /// <summary>
        /// Finds and formats a ckey in a chemistry log line following the "last touched by " and the "carried by " formats.
        /// </summary>
        /// <param name="line">The line being tested.</param>
        /// <returns>Returns the formatted ckey in lowercase.</returns>
        public static string MatchCkey(string line) {
            // (?<=carried by ).+$
            // matches 1 or more chars at the end of the string after "carried by "
            string patternCarried = "(?<=carried by ).+$";
            // ((?<=last touched by )|(?<=carried by )).+$
            // matches 1 or more chars at the end of the string after "last touched by " OR "carried by "
            string patternDefault = "(?<=last touched by ).+$";
            string result;

            //try first to match it to the special "carried by " log format
            result = MatchString(line, patternCarried);
            //if above succeeded, cut down string to get only the ckey
            if (!String.IsNullOrWhiteSpace(result)) {
                result = result.Substring(result.IndexOf("(") + 1);
                result = result.Substring(0, result.Length - 1);
            } else {
                result = MatchString(line, patternDefault);
            }

            result = result.ToLower();
            return result;
        }

        /// <summary>
        /// Finds a dose in a chemistry log line following 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string MatchDose(string line) {
            string pattern = "(?<=\\|\\|\\s)\\d+";
            string result = MatchString(line, pattern);
            return result;
        }


        /// <summary>
        /// Flexibly matches a pattern to a string of any length based on given parameters.
        /// </summary>
        /// <param name="line">The string being tested.</param>
        /// <param name="pattern">The regex pattern the line is being tested with.</param>
        /// <returns>Returns the matched pattern as a string.</returns>
        private static string MatchString(string line, string pattern) {
            Regex rgx = new Regex(pattern);
            string result = null;

            Match match = rgx.Match(line);
            if (match.Success) {
                result = match.Value;
            }

            return result;
        }

        public static bool MatchFilter(string line, string pattern) {
            Regex rgx = new Regex(pattern);
            Match match = rgx.Match(line);
            return match.Success;
        }
    }
}
