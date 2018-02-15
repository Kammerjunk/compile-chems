using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace CompileChems.ChemCompiler {
    /// <summary>
    /// Compiles chemistry logs from an entire folder of .htm or .txt files into a single .txt file.
    /// </summary>
    public class ChemCompilerFolder : ChemCompilerBase, ICompileChems {
        /// <summary>
        /// Initialises a new instance of ChemCompilerFolder with an empty dictionary and list.
        /// </summary>
        public ChemCompilerFolder()
        : base() {
        }

        /// <summary>
        /// Compiles chemistry logs through a StreamReader into a sorted list of strings.
        /// </summary>
        public void CompileChems() {
            //get folder name and files within from user
            string path = FileAccessing.GetFolderPath();
            string[] files = Directory.GetFiles(path);

            //ask user for reagent name
            _reagentName = GetReagentName();
            
            foreach(string file in files) {
                _sr = new StreamReader(file);
                DoLines(_reagentName, _sr);
                _sr.Close();
            }
            DictionaryToOrderedList();

            //empty used collections
            EmptyCollections();

            FileAccessing.WriteToFile(_resultList);
            Console.WriteLine("Streams closed.");
        }
    }
}
