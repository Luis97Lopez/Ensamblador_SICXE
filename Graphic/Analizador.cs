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
    class Analizador
    {
        public string[] asm_code;
        public List<Tuple<string, string>> intermediary_code;
        public Dictionary<string, string> symbol_table;
        public string[] errors;
        public List<string> registers;

        public Analizador(string[] asm_code)
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
                SICLexer lex = new SICLexer(new AntlrInputStream(temp_code));
                CommonTokenStream tokens = new CommonTokenStream(lex);
                SICParser parser = new SICParser(tokens);

                parser.ErrorHandler = new CustomErrorHandler();

                parser.programa();

                WriteTabSim(parser.tabsim, tabsim_file, parser.PC);
                symbol_table = parser.tabsim;

                List<Tuple<string, string>> programa_objeto =
                    WriteObjectProgram(intermediary_file, asm_code, parser.programa_objeto, parser.tabsim);
                intermediary_code = programa_objeto;

                registers = new List<string>();
                registers.Add(WriteHeaderRegister(object_program, parser.name, parser.first_PC, parser.PC));

                List<string> temp_registers = WriteTextRegisters(object_program, programa_objeto, parser.tabsim);
                registers.AddRange(temp_registers);

                registers.Add(WriteEndRegister(object_program, parser.tabsim, parser.last_sim, parser.first_PC));

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

        static private List<Tuple<string, string>> WriteObjectProgram(FileStream code_file, string[] lines,
            List<Tuple<string, string>> code, Dictionary<string, string> tabsim)
        {
            List<Tuple<string, string>> new_code = new List<Tuple<string, string>>();
            BinaryWriter writer = new BinaryWriter(code_file);
            int i = 1;
            foreach (var item in lines)
                if (item != "")
                {
                    string temp_pc = code[i - 1].Item1;
                    string temp_obj = code[i - 1].Item2;
                    if (temp_obj != null && temp_obj != "")
                    {
                        if (temp_obj.Contains("$"))
                        {

                            int indexado = 0;
                            if (temp_obj.Contains("%"))
                            {
                                indexado = 8;
                                Console.WriteLine("Hola");
                                temp_obj = temp_obj.Replace("%", "");
                            }
                            Console.WriteLine("Prueba lois: " + temp_obj);
                            int index = temp_obj.IndexOf("$");
                            string simbolo = temp_obj.Substring(index + 1, temp_obj.Length - 3);

                            string operando = "";

                            if (tabsim.ContainsKey(simbolo))
                                operando += tabsim[simbolo];
                            else
                                operando += "7FFF";

                            operando = (Convert.ToInt32(operando[0].ToString(), 16) + indexado).ToString("X1") +
                                operando.Substring(1, 3);
                            temp_obj = temp_obj.Replace("$" + simbolo, operando);
                        }
                        else if (temp_obj.Contains("&"))
                        {
                            string temp2_obj = "";
                            foreach (char c in temp_obj)
                            {
                                if (c != '&')
                                    temp2_obj += ((int)c).ToString("X2");
                            }
                            temp_obj = temp2_obj;
                        }
                    }
                    writer.Write(i++ + "\t" + temp_pc + "\t" + item + "\t\t" + temp_obj + "\n");
                    new_code.Add(Tuple.Create(temp_pc, temp_obj));
                }
            writer.Close();
            return new_code;
        }

        static private string WriteHeaderRegister(FileStream file, string name, int pc_inicial, int pc_final)
        {
            BinaryWriter bn = new BinaryWriter(file);
            string temp = 'H' + name.PadRight(6, ' ').Substring(0, 6) + pc_inicial.ToString("X6") + (pc_final - pc_inicial).ToString("X6");
            bn.Write(UTF8Encoding.Default.GetBytes(temp));
            return temp;

        }

        static private List<string> WriteTextRegisters(FileStream file, List<Tuple<string, string>> object_code,
            Dictionary<string, string> tabsim)
        {
            List<string> res = new List<string>();

            BinaryWriter bn = new BinaryWriter(file);
            int contador = 0;
            int pre_contador = 0;
            bool primero = true;
            string registers = "";
            string temp_num;

            foreach (var item in object_code)
            {
                if (item.Item2 != null)
                {
                    if (!item.Item2.Contains("-"))
                    {
                        int item2_tam = (item.Item2.Length) / 2;
                        if (primero)
                        {
                            registers += "\nT" + item.Item1 + "__";
                            primero = false;
                        }
                        else if (contador == 0 || contador + item2_tam > 30)
                        {
                            if (contador == 0)
                                temp_num = pre_contador.ToString("X2").PadLeft(2, '0');
                            else
                                temp_num = contador.ToString("X2").PadLeft(2, '0');

                            registers = WriteSizeOfTextRegister(registers, temp_num);
                            registers += "\nT" + item.Item1 + "__";

                            contador = 0;
                        }
                        registers += item.Item2;
                        contador += item2_tam;
                    }
                    else
                    {
                        if (contador != 0)
                        {
                            pre_contador = contador;
                            contador = 0;
                        }
                    }
                }
            }

            temp_num = pre_contador.ToString().PadLeft(2, '0');
            registers = WriteSizeOfTextRegister(registers, temp_num);

            foreach (var item in registers.Split('\n'))
                res.Add(item);

            bn.Write(UTF8Encoding.Default.GetBytes(registers));
            return res;
        }

        static private string WriteSizeOfTextRegister(string registers, string temp_num)
        {
            StringBuilder temp_builder;
            int index = registers.IndexOf('_');
            temp_builder = new StringBuilder(registers);
            temp_builder[index] = temp_num[0];
            temp_builder[index + 1] = temp_num[1];
            return temp_builder.ToString();
        }

        static private string WriteEndRegister(FileStream file, Dictionary<string, string> tabsim, string sim, int first_pc)
        {
            BinaryWriter bn = new BinaryWriter(file);
            string dir, res;

            if (sim == "")
                dir = first_pc.ToString("X6");
            else if (tabsim.ContainsKey(sim))
                dir = "00" + tabsim[sim];
            else
                dir = "FFFFFF";

            res = "\nE" + dir;

            bn.Write(UTF8Encoding.Default.GetBytes(res));
            return res;

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
