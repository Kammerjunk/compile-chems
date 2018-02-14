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
            bool run = true;
            while (run) {
                ChemCompiler.CompileChems();
                Console.WriteLine();
                
                Console.Write("Do you wish to run a compilation again? Y/N");
                ConsoleKey response;
                do {
                    response = Console.ReadKey(true).Key;
                } while (response != ConsoleKey.Y && response != ConsoleKey.N);
                run = response == ConsoleKey.Y;
                Console.WriteLine();
            }
            Console.WriteLine("Loop terminated. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
