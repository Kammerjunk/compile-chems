using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompileChems.RegexHandling {
    public static class RegexPatterns {
        public static string Reagent(string reagentName) {
            // (?<=u of )reagentName have been
            // matches to "u of " and "reagentName" and "have been" in order
            return "(?<=u of )" + reagentName + " have been";
        }

        /// <summary>
        /// Finds and formats a ckey in a chemistry log line following the "last touched by " and the "carried by " formats.
        /// </summary>
        /// <param name="line">The line being tested.</param>
        /// <returns>Returns the formatted ckey in lowercase.</returns>
        public static string CkeyCarried() {
            // (?<=carried by ).+$
            // matches 1 or more chars at the end of the string after "carried by "
            return "(?<=carried by ).+$";
        }

        public static string CkeyTouched() {
            // (?<=last touched by ).+$
            // matches 1 or more chars at the end of the string after "last touched by "
            return "(?<=last touched by ).+$";
        }

        /// <summary>
        /// Finds a dose in a chemistry log line following 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string Dose() {
            return "(?<=\\|\\|\\s)\\d+";
        }
    }
}
