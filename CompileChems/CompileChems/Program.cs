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
            ChemCompilerFolder compilerFolder = new ChemCompilerFolder();
            ChemCompilerFile compilerFile = new ChemCompilerFile();

            bool run = true;
            while (run) {
                Console.WriteLine("Choose how to process file(s).");
                Console.WriteLine("Accepted responses: folder, file");
                string responseProcessing;
                do {
                    responseProcessing = Console.ReadLine();
                } while (responseProcessing != "folder" && responseProcessing != "file");
                if(responseProcessing == "folder") {
                    compilerFolder.CompileChems();
                } else if(responseProcessing == "file") {
                    compilerFile.CompileChems();
                }
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
