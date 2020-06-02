using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Antlr4;
using Antlr4.Runtime;

namespace Graphic
{
    class AnalizadorSICXE
    {
        public string[] asm_code;
        public List<Tuple<string, string>> intermediary_code;
        public Dictionary<string, string> symbol_table;
        public string[] errors;

        public AnalizadorSICXE(string[] asm_code)
        {
            this.asm_code = asm_code;
        }

        public bool Ensamblar()
        {
            if (asm_code != null)
            {
                string errors_path = Environment.CurrentDirectory + @"\Files\errors.s";
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Files\");
                FileStream intermediary_file = new FileStream(Environment.CurrentDirectory + @"\Files\intermediary.s", FileMode.Create);
                FileStream tabsim_file = new FileStream(Environment.CurrentDirectory + @"\Files\tabsim.s", FileMode.Create);
                FileStream object_program = new FileStream(Environment.CurrentDirectory + @"\Files\object.o", FileMode.Create);
                FileStream error_program = new FileStream(errors_path, FileMode.Create);
                error_program.Close();

                string temp_code = string.Join("\n", asm_code);
                SICXELexer lex = new SICXELexer(new AntlrInputStream(temp_code));
                CommonTokenStream tokens = new CommonTokenStream(lex);
                SICXEParser parser = new SICXEParser(tokens);

                parser.ErrorHandler = new CustomErrorHandler();

                parser.programa();

                intermediary_code = parser.programa_objeto;

                WriteTabSim(parser.tabsim, tabsim_file, parser.PC);
                symbol_table = parser.tabsim;

                errors = GetErrors(errors_path, parser.duplicated_sim);

                tabsim_file.Close();
                intermediary_file.Close();
                object_program.Close();

                return true;
            }
            else
                return false;
        }

        static private void WriteTabSim(Dictionary<string, string> tabsim, FileStream tabsim_file, int PC)
        {
            BinaryWriter writer = new BinaryWriter(tabsim_file);
            writer.Write("\n\nTABSIM\n");
            writer.Write("Etiqueta\tDireccion\n");
            foreach (var item in tabsim)
                writer.Write(item.Key + "\t\t" + item.Value + "\n");
            writer.Write("Size: " + PC.ToString("X6"));
            writer.Close();
        }

        static private string[] GetErrors(string file, List<Tuple<string, string>> errors)
        {
            List<string> ret = new List<string>();
            string[] temp = File.ReadAllLines(file);

            foreach (var item in temp)
                ret.Add(item);

            foreach (var error in errors)
                ret.Add("Error simbolo duplicado - " + error.Item1);

            return ret.ToArray();
        }
    }
}
