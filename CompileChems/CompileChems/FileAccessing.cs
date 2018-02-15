using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompileChems {
    public static class FileAccessing {
        public static StreamReader GetFilePath() {
            string filename = null;
            bool fileFound = false;
            StreamReader sr;

            Console.Write("Enter name of chemistry log file in the current directory: ");
            while (!fileFound) {
                try {
                    filename = Console.ReadLine();
                    if (!filename.EndsWith(".txt")) {
                        filename += ".txt";
                    }
                    string path = Directory.GetCurrentDirectory() + $"\\{filename}";
                    sr = new StreamReader(path);
                    Console.WriteLine("Reading from " + path);
                    fileFound = true;
                    return sr;
                } catch (FileNotFoundException) {
                    Console.Write("Unable to locate file in current directory. Please enter filename: ");
                }
            }

            return null;
        }

        public static string GetFolderPath() {
            string foldername = null;
            bool folderFound = false;
            string path = null;

            Console.Write("Enter name of folder in the current directory containing chemistry log files: ");
            while(!folderFound) {
                foldername = Console.ReadLine();
                path = Directory.GetCurrentDirectory() + $"\\{foldername}";
                Console.WriteLine("Reading from " + path);
                if(!Directory.Exists(path)) {
                    Console.Write("Unable to locate folder. Please enter folder name: ");
                } else {
                    folderFound = true;
                }
            }
            return path;
        }

        public static void WriteToFile(List<string> resultList) {
            Console.Write("Enter filename to write result to: ");
            string filename = Console.ReadLine();
            if (!filename.EndsWith(".txt")) {
                filename += ".txt";
            }
            Console.WriteLine("Writing to " + filename);
            StreamWriter sw = File.AppendText(filename);
            sw.AutoFlush = true;

            foreach(string line in resultList) {
                sw.WriteLine(line);
            }
            sw.WriteLine();
            sw.Close();
        }

        public static string HtmlToPlainText(string html) {
            string tagWhitespace = @"(>|$)(\W|\n|\r)+<";        //matches 1 or more whitespace or linebreak characters between > and <
            string stripFormatting = @"<[^>]*(>|$)";            //matches any character between < and >
            //string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";    //matches linebreak tags
            Regex rgxTagWhitespace = new Regex(tagWhitespace);
            Regex rgxStripFormatting = new Regex(stripFormatting);
            //Regex rgxLineBreak = new Regex(lineBreak);

            string plaintext = html;
            plaintext = System.Net.WebUtility.HtmlDecode(plaintext);            //decode html-specific characters
            plaintext = rgxTagWhitespace.Replace(plaintext, "><");              //remove whitespace or linebreaks between tags
            //plaintext = rgxLineBreak.Replace(plaintext, Environment.NewLine); //replaces linebreak characters with newlines
            plaintext = rgxStripFormatting.Replace(plaintext, String.Empty);    //strips formatting

            return plaintext;
        }
    }
}
