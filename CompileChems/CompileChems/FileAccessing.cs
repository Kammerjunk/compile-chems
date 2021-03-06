﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompileChems {
    /// <summary>
    /// Contains a number of methods for interacting with files and preparing for file interaction.
    /// </summary>
    public static class FileAccessing {
        /// <summary>
        /// Gets an absolute path in the current directory from user input.
        /// </summary>
        /// <returns>Returns a StreamReader with a valid path to a .htm or .txt file.</returns>
        public static StreamReader GetFilePath() {
            string filename = null;
            bool fileFound = false;
            StreamReader sr;

            Console.Write("Enter name of chemistry log file in the current directory: ");
            while (!fileFound) {
                try {
                    filename = Console.ReadLine();
                    //add file extension if user didn't. will default to .htm
                    if (!filename.EndsWith(".txt") && !filename.EndsWith(".htm")) {
                        if (File.Exists($"{filename}.htm")) {
                            filename += ".htm";
                        } else {
                            filename += ".txt";
                        }
                    }
                    string path = Directory.GetCurrentDirectory() + $"\\{filename}";
                    sr = new StreamReader(path);
                    Console.WriteLine("Reading from " + path);
                    fileFound = true; //break
                    return sr;
                } catch (FileNotFoundException) {
                    Console.Write("Unable to locate file in current directory. Please enter filename: ");
                }
            }

            return null;
        }

        /// <summary>
        /// Gets an absolute path in a direct subdirectory from user input.
        /// </summary>
        /// <returns>Returns a string with a subdirectory inside the current directory.</returns>
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

        /// <summary>
        /// Writes a list to a file in the current directory, named from user input.
        /// </summary>
        /// <param name="resultList">A list of pre-formatted strings to print to file.</param>
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

        /// <summary>
        /// Converts single-line HTML strings to plaintext.
        /// </summary>
        /// <param name="html">A string containing HTML formatting.</param>
        /// <returns>Returns a plaintext string converted from HTML formatting.</returns>
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
