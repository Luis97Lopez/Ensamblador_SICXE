using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Graphic
{
    class Programa_Objeto
    {
        public string path;
        public string[] lines;

        public string name;
        public int load_direction;
        public int execution_direction;
        public int size;
        public List<Registro> registers;

        public bool terminado = false;

        public Programa_Objeto(string path)
        {
            this.path = path;
            lines = File.ReadAllLines(this.path);
            registers = new List<Registro>();
        }

        public Programa_Objeto(string[] lines)
        {
            this.lines = lines;
            registers = new List<Registro>();
        }

        public bool Initialize()
        {
            try
            {
                foreach (var item in lines)
                {
                    if (item.Length == 0)
                        continue;

                    char tipo = item[0];
                    if (tipo == 'H')
                    {
                        name = item.Substring(1, 6);
                        string temp_dir = item.Substring(7, 6);
                        string temp_tam = item.Substring(13, 6);

                        load_direction = Convert.ToInt32(temp_dir, 16);
                        size = Convert.ToInt32(temp_tam, 16);
                    }
                    else if (tipo == 'T')
                    {
                        Registro register = new Registro();

                        string temp_s_dir = item.Substring(1, 6);
                        string temp_tam = item.Substring(7, 2);

                        register.content = item.Substring(9, item.Length - 9);
                        register.direction = Convert.ToInt32(temp_s_dir, 16);
                        register.size = register.content.Length;

                        registers.Add(register);
                    }
                    else if (tipo == 'E')
                    {
                        string temp_dir = item.Substring(1, 6);
                        execution_direction = Convert.ToInt32(temp_dir, 16);
                    }
                    else
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public class Registro
        {
            public int direction;
            public int size;
            public string content;
        }

    }
}
