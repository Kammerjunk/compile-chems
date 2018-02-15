using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileChems {
    class ChemCompilerFile : ChemCompilerBase {
        public ChemCompilerFile()
        : base() {

        }

        public void CompileChems() {
            //get file name from user
            _sr = FileAccessing.GetFilePath();

            //get reagent name from user
            _reagentName = GetReagentName();

            //process reagents
            DoLines(_reagentName, _sr);
            _sr.Close();

            //write to file
            FileAccessing.WriteToFile(_resultList);
            Console.WriteLine("Streams closed.");
        }
    }
}
