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
                Console.WriteLine("Accepted responses: folder, file, help");
                string responseProcessing;
                do {
                    responseProcessing = Console.ReadLine();
                    responseProcessing = responseProcessing.ToLower();
                } while (responseProcessing != "folder" && responseProcessing != "file" && responseProcessing != "help");
                if(responseProcessing == "folder") {
                    compilerFolder.CompileChems();
                } else if(responseProcessing == "file") {
                    compilerFile.CompileChems();
                } else if(responseProcessing == "help") {
                    PrintHelp();
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

        private static void PrintHelp() {
            string text = null;
            text += "-----HELP-----\n";
            text += "This is a tool for quickly and easily compiling chemistry\n";
            text += "log files together to get an overview of the reagents made.\n";
            text += "For both the \"folder\" and the \"file\" options, you must\n";
            text += "have the application and the folder or file placed in the same\n";
            text += "directory. The folder and file names are both case insensitive.\n";
            text += "The files must be in a readable format - either plaintext .txt\n";
            text += "or the .htm the chemistry logs come in by default. You don't\n";
            text += "have to provide a file extension for the \"file\" option.\n";
            text += "The program will first try to find an .htm file and, failing\n";
            text += "that, a .txt file with the provided name.\n";
            Console.WriteLine(text);
        }
    }
}
