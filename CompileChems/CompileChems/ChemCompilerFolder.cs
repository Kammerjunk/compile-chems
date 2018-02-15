using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace CompileChems {
    public class ChemCompilerFolder : ChemCompilerBase {
        public void CompileChems() {
            //get folder name and files within from user
            string path = FileAccessing.GetFolderPath();
            string[] files = Directory.GetFiles(path);

            //ask user for reagent name
            _reagentName = GetReagentName();
            
            foreach(string file in files) {
                string filePath = path + $"{file}";
                _sr = new StreamReader(filePath);
                DoLines(_reagentName, _sr);
                _sr.Close();
            }

            FileAccessing.WriteToFile(_resultList);
            Console.WriteLine("Streams closed.");
        }
    }
}
