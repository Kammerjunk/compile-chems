﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileChems.ChemCompiler {
    /// <summary>
    /// Compiles chemistry logs from a single .htm or .txt file into a single .txt file.
    /// </summary>
    class ChemCompilerFile : ChemCompilerBase, ICompileChems {
        /// <summary>
        /// Initialises a new instance of ChemCompilerFile with an empty dictionary and list.
        /// </summary>
        public ChemCompilerFile()
        : base() {
        }

        /// <summary>
        /// Compiles chemistry logs through a StreamReader into a sorted list of strings.
        /// </summary>
        public void CompileChems() {
            //get file name from user
            _sr = FileAccessing.GetFilePath();

            //get reagent name from user
            _reagentName = GetReagentName();

            //process reagents
            DoLines(_reagentName, _sr);
            _sr.Close();
            Console.WriteLine("Streams closed.");

            DictionaryToOrderedList();
            //write to file
            FileAccessing.WriteToFile(_resultList);

            //empty used collections
            EmptyCollections();

            Console.WriteLine("File written.");
        }
    }
}
