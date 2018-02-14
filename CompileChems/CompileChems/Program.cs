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

                ConsoleKey response;
                do {
                    Console.Write("Do you wish to run a compilation again? Y/N");
                    response = Console.ReadKey(false).Key;
                    if(response != ConsoleKey.Enter) {
                        Console.WriteLine();
                    }
                } while (response != ConsoleKey.Y && response != ConsoleKey.N);
                run = response == ConsoleKey.Y;
            }
            Console.WriteLine("Loop terminated. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
