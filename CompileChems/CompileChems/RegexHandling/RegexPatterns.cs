using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileChems.RegexHandling {
    public static class RegexPatterns {
        public static string Reagent(string reagentName) {
            return "(?<=u of )" + reagentName + " have been";
        }

        public static string Ckey() {
            return "((?<=last touched by )|(?<=carried by )).+$";
        }

        public static string Dose() {
            return "(?<=\\|\\|\\s)\\d+";
        }
    }
}
