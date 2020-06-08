using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4;
using Antlr4.Runtime;
using System.IO;

namespace Graphic
{
    public partial class lab_grafico : Form
    {
        public delegate void Accion(string m);
        public delegate string Efecto(string m);

        string path;
        Dictionary<string, Tuple<string, Accion, Efecto>> instrucciones;
        Programa_Objeto programa_objeto;

        AnalizadorSIC analizador_sic;
        AnalizadorSICXE analizador_sicxe;

        public lab_grafico()
        {
            InitializeComponent();
            InitializeInstrucciones();
        }

        private void InitializeInstrucciones()
        {
            instrucciones = new Dictionary<string, Tuple<string, Accion, Efecto>>();

            instrucciones.Add("18", new Tuple<string, Accion, Efecto>("ADD m", ADD, eADD));
            instrucciones.Add("40", new Tuple<string, Accion, Efecto>("AND m", AND, eAND));
            instrucciones.Add("28", new Tuple<string, Accion, Efecto>("COMP m", COMP, eCOMP));
            instrucciones.Add("24", new Tuple<string, Accion, Efecto>("DIV m", DIV, eDIV));
            instrucciones.Add("3C", new Tuple<string, Accion, Efecto>("J m", J, eJ));
            instrucciones.Add("30", new Tuple<string, Accion, Efecto>("JEQ m", JEQ, eJEQ));
            instrucciones.Add("34", new Tuple<string, Accion, Efecto>("JGT m", JGT, eJGT));
            instrucciones.Add("38", new Tuple<string, Accion, Efecto>("JLT m", JLT, eJLT));
            instrucciones.Add("48", new Tuple<string, Accion, Efecto>("JSUB m", JSUB, eJSUB));
            instrucciones.Add("00", new Tuple<string, Accion, Efecto>("LDA m", LDA, eLDA));
            instrucciones.Add("50", new Tuple<string, Accion, Efecto>("LDCH m", LDCH, eLDCH));
            instrucciones.Add("08", new Tuple<string, Accion, Efecto>("LDL m", LDL, eLDL));
            instrucciones.Add("04", new Tuple<string, Accion, Efecto>("LDX m", LDX, eLDX));
            instrucciones.Add("20", new Tuple<string, Accion, Efecto>("MUL m", MUL, eMUL));
            instrucciones.Add("44", new Tuple<string, Accion, Efecto>("OR m", OR, eOR));
            instrucciones.Add("D8", new Tuple<string, Accion, Efecto>("RD m", RD, eRD));
            instrucciones.Add("4C", new Tuple<string, Accion, Efecto>("RSUB", RSUB, eRSUB));
            instrucciones.Add("0C", new Tuple<string, Accion, Efecto>("STA m", STA, eSTA));
            instrucciones.Add("54", new Tuple<string, Accion, Efecto>("STCH m", STCH, eSTCH));
            instrucciones.Add("14", new Tuple<string, Accion, Efecto>("STL m", STCH, eSTCH));
            instrucciones.Add("E8", new Tuple<string, Accion, Efecto>("STSW m", STSW, eSTSW));
            instrucciones.Add("10", new Tuple<string, Accion, Efecto>("STX m", STX, eSTX));
            instrucciones.Add("1C", new Tuple<string, Accion, Efecto>("SUB m", SUB, eSUB));
            instrucciones.Add("E0", new Tuple<string, Accion, Efecto>("TD m", TD, eTD));
            instrucciones.Add("2C", new Tuple<string, Accion, Efecto>("TIX m", TIX, eTIX));
            instrucciones.Add("DC", new Tuple<string, Accion, Efecto>("WD m", WD, eWD));
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                asm_code.Lines = null;
                path = null;
                ClearAll();
                this.Text = this.Text.Substring(0, this.Text.IndexOf('-') - 1);
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != null)
                this.Text = this.Text.Substring(0, this.Text.IndexOf('-') - 1);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory + @"\Codes";
            openFileDialog.Filter = "SIC Extended (*.x)|*.x| " +
                                    "SIC Standard (*.s)|*.s| " +
                                    "All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                if (!OpenCodeFile(openFileDialog.FileName))
                    MessageBox.Show("Error al abrir archivo.", "Error");
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                File.WriteAllLines(path, asm_code.Lines);
                MessageBox.Show("Archivo guardado correctamente.", "Ensamblador SIC Estándar");
            }
            else
            {
                Stream prueba;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.CurrentDirectory + @"\Codes";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((prueba = saveFileDialog.OpenFile()) != null)
                        prueba.Close();
                    File.WriteAllLines(saveFileDialog.FileName, asm_code.Lines);
                    OpenCodeFile(saveFileDialog.FileName);
                }
            }
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                path = null;
                asm_code.Lines = null;
                ClearAll();
                this.Text = this.Text.Substring(0, this.Text.IndexOf('-') - 1);
            }
        }

        private void ensamblar_Click(object sender, EventArgs e)
        {
            if (asm_code.Lines != null && asm_code.Lines.Length > 0)
            {
                if (path != null)
                {
                    if (Path.GetExtension(path).ToLower() == ".s")
                    {
                        try
                        {
                            analizador_sicxe = null;

                            analizador_sic = new AnalizadorSIC(asm_code.Lines);
                            analizador_sic.Ensamblar();

                            Fill_Intermediary_File(analizador_sic.intermediary_code, analizador_sic.asm_code, analizador_sic.symbol_table);
                            Fill_TabSim(analizador_sic.symbol_table);
                            Fill_Registers(analizador_sic.registers);
                            Fill_Errors(analizador_sic.errors);

                            MessageBox.Show("Ensamblado se realizó con éxito");
                        }
                        catch
                        {
                            MessageBox.Show("Error en lectura de código. Programa sensible a espacios en blanco y tabulaciones", "Error");
                        }
                    }
                    else if (Path.GetExtension(path).ToLower() == ".x")
                    {
                        try
                        {
                            analizador_sic = null;

                            analizador_sicxe = new AnalizadorSICXE(asm_code.Lines);
                            analizador_sicxe.Ensamblar();
                            Fill_Intermediary_File(analizador_sicxe.intermediary_code, analizador_sicxe.asm_code, analizador_sicxe.symbol_table);
                            Fill_TabSim(analizador_sicxe.symbol_table);
                            Fill_Errors(analizador_sicxe.errors);
                        }
                        catch
                        {
                            MessageBox.Show("Error en lectura de código. Programa sensible a espacios en blanco y tabulaciones", "Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Archivo inválido. Verificar extensión.", "Error");
                    }
                }
                else
                    MessageBox.Show("Guardar archivo con respectiva extensión.", "Error");
            }
        }

        private void ClearAll()
        {
            dgv_archivo_intermedio.Rows.Clear();
            dgv_tabsim.Rows.Clear();
            errores.Text = "";
            text_programa_objeto.Text = "";
        }

        private bool OpenCodeFile(string temp_path)
        {
            if (temp_path != null)
            {
                this.path = temp_path;
                ClearAll();
                FillAsmCode();
                this.Text += " - " + Path.GetFileName(path);
                return true;
            }
            return false;
        }

        private void Fill_Intermediary_File(List<Tuple<string, string>> file, string[] code, Dictionary<string, string> tabsim)
        {

            List<string[]> res = new List<string[]>();
            int i = 0;
            foreach (var item in code)
            {

                if (item != "")
                {

                    string[] temp = item.Split(new char[] { ' ', '\t' }, 3);
                    string temp_object_code;
                    string temp_operando;

                    if (file[i].Item2 != null && file[i].Item2 != "")
                        temp_object_code = file[i].Item2;
                    else
                        temp_object_code = "Error de Sintaxis";


                    if (temp.Length > 2)
                        temp_operando = temp[2];
                    else
                        temp_operando = "";

                    try
                    {
                        res.Add(new string[] { (i + 1).ToString(), file[i++].Item1, temp[0], temp[1], temp_operando, temp_object_code });
                    }
                    catch { }
                }
            }

            dgv_archivo_intermedio.Rows.Clear();
            foreach (var item in res)
                dgv_archivo_intermedio.Rows.Add(item);
        }

        private void Fill_TabSim(Dictionary<string,string> tabsim)
        {
            dgv_tabsim.Rows.Clear();
            if(tabsim != null)
                foreach (var item in tabsim)
                    dgv_tabsim.Rows.Add(new string[] { item.Key, item.Value });
        }

        private void Fill_Registers(List<string> registers)
        {
            text_programa_objeto.Clear();
            text_programa_objeto.Lines = registers.ToArray();
        }

        private void FillAsmCode()
        {
            asm_code.Lines = File.ReadAllLines(path);
        }

        private void Fill_Errors(string [] errors)
        {
            if (errors.Length > 0)
                errores.Lines = errors;
            else
                errores.Lines = new string[] { "No hay errores" };
        }

        private void boton_cargar_Click_1(object sender, EventArgs e)
        {
            if (analizador_sic != null && path != null)
            {
                var lines = analizador_sic.registers.ToArray();
                programa_objeto = new Programa_Objeto(lines);
                if (programa_objeto.Initialize())
                {
                    crea_mapa_memoria();
                    carga_a_memoria();
                }
                else
                {
                    programa_objeto = null;
                    MessageBox.Show("Error al cargar archivo objeto.", "Error");
                    ClearCargador();
                }
            }
            else if (analizador_sicxe != null)
                MessageBox.Show("Cargador para archivos SICXE no disponible");
            else
                MessageBox.Show("Primero ensamble el programa", "Error");
        }

        private void boton_abrir_objeto_Click_1(object sender, EventArgs e)
        {
            bool error = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory + @"\Files\";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                programa_objeto = new Programa_Objeto(openFileDialog.FileName);
                if (programa_objeto.Initialize())
                {
                    try
                    {
                        crea_mapa_memoria();
                        carga_a_memoria();
                    }
                    catch {
                        error = true;
                    }
                }
                else
                {
                    error = true;
                }
            }

            if(error)
            {
                programa_objeto = null;
                MessageBox.Show("Error al abrir archivo.", "Error");
                ClearCargador();
            }
        }

        private void boton_cerrar_programa_Click_1(object sender, EventArgs e)
        {
            programa_objeto = null;
            ClearCargador();
        }

        private void boton_ejecutar_Click_1(object sender, EventArgs e)
        {
            int numero = Int32.Parse(numeric_instrucciones.Value.ToString());

            for (int i = 0; i < numero; i++)
                EjecutarSigInstruccion();
        }

        private void tab_control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 120)
            {
                EjecutarSigInstruccion();
            }
        }

        private void crea_mapa_memoria()
        {
            int size = programa_objeto.size;
            int first_direction = programa_objeto.load_direction;

            int width = (dgv_cargador.Size.Width) / 17;
            dgv_cargador.Visible = true;
            dgv_cargador.RowHeadersVisible = false;

            foreach (DataGridViewColumn item in dgv_cargador.Columns)
                item.Width = width;

            double divition = (double)(size + first_direction % 16) / 16;

            int num_rows = (int)Math.Ceiling(divition);
            int direction = first_direction - (first_direction % 16);

            dgv_cargador.Rows.Clear();

            for (int i = 0; i < num_rows; i++)
            {
                dgv_cargador.Rows.Add();
                dgv_cargador.Rows[i].Cells[0].Value = (direction + (i * 16)).ToString("X4");
                for (int j = 1; j < 17; j++)
                    dgv_cargador.Rows[i].Cells[j].Value = "FF";
            }
        }

        private void carga_a_memoria()
        {
            int dir_inicial = programa_objeto.load_direction;

            label_tam_programa.Text = programa_objeto.size.ToString("X6");
            label_direccion_carga.Text = dir_inicial.ToString("X6");

            foreach (var register in programa_objeto.registers)
            {
                int index_column = GetColumnIndexOfMemory(register.direction);
                int index_row = GetRowIndexOfMemory(register.direction);
                for (int i = 0; i < register.content.Length; i += 2)
                {
                    dgv_cargador.Rows[index_row].Cells[index_column + 1].Value =
                        register.content[i] + "" + register.content[i + 1];
                    index_column = ++index_column % 16;
                    if (index_column == 0)
                        index_row++;
                }
            }

            InicializaRegistros(programa_objeto.execution_direction);
        }

        private int GetColumnIndexOfMemory(int direction)
        {
            return direction % 16;
        }

        private int GetRowIndexOfMemory(int direction)
        {
            int dir_inicial = programa_objeto.load_direction;
            return (direction - dir_inicial + dir_inicial % 16) / 16;
        }

        private string GetBytes(int direction)
        {
            string res = "";

            int index_column = GetColumnIndexOfMemory(direction);
            int index_row = GetRowIndexOfMemory(direction);

            for (int i = 0; i < 3; i++)
            {
                res += dgv_cargador.Rows[index_row].Cells[index_column + 1].Value;
                index_column = ++index_column % 16;
                if (index_column == 0)
                    index_row++;
            }
            return res;
        }

        private string GetByte(int direction)
        {

            int index_column = GetColumnIndexOfMemory(direction);
            int index_row = GetRowIndexOfMemory(direction);

            return (string)dgv_cargador.Rows[index_row].Cells[index_column + 1].Value;
        }

        private void WriteBytes(int direction, string bytes)
        {

            int index_column = GetColumnIndexOfMemory(direction);
            int index_row = GetRowIndexOfMemory(direction);

            for (int i = 0; i < bytes.Length; i += 2)
            {
                if (i + 1 < bytes.Length)
                    dgv_cargador.Rows[index_row].Cells[index_column + 1].Value = bytes[i] + bytes[i + 1];
                else
                    Console.WriteLine("ALERTA! Error al escribir en memoria. Checar direcciones");

                index_column = ++index_column % 16;

                if (index_column == 0)
                    index_row++;
            }

        }

        private void WriteByte(int direction, string bytes)
        {

            int index_column = GetColumnIndexOfMemory(direction);
            int index_row = GetRowIndexOfMemory(direction);

            if (bytes.Length > 1)
                dgv_cargador.Rows[index_row].Cells[index_column + 1].Value = bytes;
            else
                Console.WriteLine("ALERTA! Error al escribir en memoria. Checar direcciones");
        }


        private string GetCodOp(string bytes)
        {
            return bytes.Substring(0, 2);
        }

        private string GetModoDeDireccionamiento(string bytes)
        {
            char bits = bytes[2];
            int entero = Convert.ToInt32(bits + "", 16);

            if (entero < 8)
                return "Directo";
            else
                return "Indexado";
        }

        private string GetDirectionInBytes(string bytes, string modo)
        {
            string m = bytes.Substring(2, 4);
            if (modo == "Indexado")
                return (Convert.ToInt32(m, 16)
                    - Convert.ToInt32("8000", 16)
                    + Convert.ToInt32(registro_X.Text, 16)).ToString("X6");
            else
                return m;
        }

        private string GetNextPC(string m, string codOp, int PC)
        {
            if (codOp == "3C" || codOp == "4C")
                return m;
            else if (codOp == "30" || codOp == "34" || codOp == "38")
                return "?";
            else
                return (PC + 3).ToString("X6");
        }

        private void InicializaRegistros(int CP)
        {
            registro_CP.Text = CP.ToString("X6");
            registro_A.Text = "FFFFFF";
            registro_L.Text = "FFFFFF";
            registro_X.Text = "FFFFFF";
            registro_SW.Text = "FFFFFF";
            print_info.Text = "";

            VisualizaSigInstruccion(CP.ToString("X6"));
        }

        private void ClearCargador()
        {
            label_direccion_carga.Text = "-";
            label_tam_programa.Text = "-";

            registro_CP.Text = "-";
            registro_A.Text = "-";
            registro_L.Text = "-";
            registro_X.Text = "-";
            registro_SW.Text = "-";

            info_bytes.Text = "-";
            info_codop.Text = "-";
            info_CP.Text = "-";
            info_direccion.Text = "-";
            info_nemonico.Text = "-";
            info_modo.Text = "-";
            info_sigCP.Text = "-";
            info_efecto.Text = "-";

            print_info.Text = "";

            dgv_cargador.Rows.Clear();
            dgv_cargador.Visible = false;
        }

        private void VisualizaSigInstruccion(string CP)
        {
            int entero_pc = Convert.ToInt32(CP, 16);
            string text_CP = info_CP.Text = CP;
            string text_bytes = info_bytes.Text = GetBytes(entero_pc);
            string text_codop = info_codop.Text = GetCodOp(info_bytes.Text);

            if (instrucciones.ContainsKey(text_codop))
            {
                string text_modo = info_modo.Text = GetModoDeDireccionamiento(info_bytes.Text);
                string text_direccion = info_direccion.Text = GetDirectionInBytes(info_bytes.Text, info_modo.Text);
                string text_nemonico = info_nemonico.Text = instrucciones[info_codop.Text].Item1;
                string text_efecto = info_efecto.Text = instrucciones[info_codop.Text].Item3(info_direccion.Text);
                string text_sigPC = info_sigCP.Text = GetNextPC(info_direccion.Text, info_codop.Text, entero_pc);

                string[] efectos = text_efecto.Split('\n');

                print_info.Text += "\n---------------------------------------------------------------------------------" +
                                        "---------------------------------------------------------------------------------" +
                                    "\n Siguiente Instrucción:" +
                                    "\n\t CP = " + text_CP + "\t\t\t\t CP = " + text_sigPC +
                                    "\n\t Bytes = " + text_bytes + "\t\t\t\t Efecto = " + text_nemonico +
                                    "\n\t CodOp = " + text_codop + "\t\t\t\t\t " + efectos[0] +
                                    "\n\t Modo de Direccionamiento = " + text_modo + "\t\t " + efectos[1] +
                                    "\n\t m = " + text_direccion + "\n";
            }
            else
                End();
        }

        private void End()
        {
            if (programa_objeto != null && !programa_objeto.terminado)
            {
                programa_objeto.terminado = true;
                MessageBox.Show("Fin del programa. Ya no se pueden leer más instrucciones",
                "Ensamblador SIC");
            }
        }

        private bool EjecutarSigInstruccion()
        {
            string codOp = info_codop.Text;
            string m = info_direccion.Text;
            string modo = info_modo.Text;

            try
            {
                if (!programa_objeto.terminado)
                {
                    instrucciones[codOp].Item2(m);
                    VisualizaSigInstruccion(registro_CP.Text);
                }
            }
            catch
            {
                End();
            }
            return true;
        }

        private string Get3DirectionsParentheses(string m)
        {
            int t = Convert.ToInt32(m, 16);
            return "(" + t.ToString("X4") + ", " +
                (t + 1).ToString("X4") + ", " + (t + 2).ToString("X4") + ")";
        }

        private string Get3Directions(string m)
        {
            int t = Convert.ToInt32(m, 16);
            return t.ToString("X4") + ", " +
                (t + 1).ToString("X4") + ", " + (t + 2).ToString("X4");
        }

        private void AumentaEn3PC()
        {
            registro_CP.Text = (Convert.ToInt32(registro_CP.Text, 16) + 3).ToString("X6");
        }

        /*************************************************************************
         * 
         *  DELEGADOS
         * 
        **************************************************************************/
        public void ADD(string m)
        {
            string A_value = registro_A.Text;
            string m_value = GetBytes(Convert.ToInt32(m, 16));

            int int_A_value = Convert.ToInt32(A_value, 16);
            int int_m_value = Convert.ToInt32(m_value, 16);

            registro_A.Text = (int_A_value + int_m_value).ToString("X6");

            AumentaEn3PC();
        }

        public string eADD(string m)
        {
            string A = registro_A.Text;
            return
                "A <- (A) + (m..m+2)\n" +
                "A <- " + A + Get3DirectionsParentheses(m);
        }

        // ---------------------------------------------------------------------------

        public void AND(string m)
        {
            int A = Convert.ToInt32(registro_A.Text, 16);
            string bytes = GetBytes(Convert.ToInt32(m, 16));
            int value = Convert.ToInt32(bytes, 16);

            int res = A & value;

            registro_A.Text = res.ToString("X6");
            AumentaEn3PC();
        }

        public string eAND(string m)
        {
            string A = registro_A.Text;
            return "A <- (A) & (m..m+2)\n" +
                "A <- " + A + " & " + Get3DirectionsParentheses(m);
        }

        // ---------------------------------------------------------------------------

        public void COMP(string m)
        {
            int A = Convert.ToInt32(registro_A.Text, 16);
            string bytes = GetBytes(Convert.ToInt32(m, 16));
            int value = Convert.ToInt32(bytes, 16);

            if (A < value)
                registro_SW.Text = (0).ToString("X6");
            else if (A == value)
                registro_SW.Text = (1).ToString("X6");
            else
                registro_SW.Text = (2).ToString("X6");

            AumentaEn3PC();
        }

        public string eCOMP(string m)
        {
            string A = registro_A.Text;
            return "(A) : (m..m+2)\n" +
                A + " : " + Get3DirectionsParentheses(m);
        }

        // ---------------------------------------------------------------------------

        public void DIV(string m)
        {
            int A = Convert.ToInt32(registro_A.Text, 16);
            string bytes = GetBytes(Convert.ToInt32(m, 16));
            int value = Convert.ToInt32(bytes, 16);

            int res = A / value;

            registro_A.Text = res.ToString("X6");
            AumentaEn3PC();
        }

        public string eDIV(string m)
        {
            string A = registro_A.Text;
            return "A <- (A) / (m..m+2)\n" +
                "A <- " + A + " / " + Get3DirectionsParentheses(m);
        }

        // ---------------------------------------------------------------------------

        public void J(string m)
        {
            registro_CP.Text = m;
        }

        public string eJ(string m)
        {
            return "CP <- m\n" +
                "CP <- " + m;
        }

        // ---------------------------------------------------------------------------

        public void JEQ(string m)
        {
            int CC = Convert.ToInt32(registro_SW.Text, 16);

            if (CC == 1)
                registro_CP.Text = m;
            else
                AumentaEn3PC();
        }

        public string eJEQ(string m)
        {
            int CC = Convert.ToInt32(registro_SW.Text, 16);
            char cc = '-';

            if (CC == 0)
                cc = '<';
            else if (CC == 1)
                cc = '=';
            else
                cc = '>';

            return "CP <- m si CC está en = \n" +
                "CP <- " + m + " si " + cc + " está en =";
        }

        // ---------------------------------------------------------------------------

        public void JGT(string m)
        {
            int CC = Convert.ToInt32(registro_SW.Text, 16);

            if (CC == 2)
                registro_CP.Text = m;
            else
                AumentaEn3PC();
        }

        public string eJGT(string m)
        {
            int CC = Convert.ToInt32(registro_SW.Text, 16);
            char cc = '-';

            if (CC == 0)
                cc = '<';
            else if (CC == 1)
                cc = '=';
            else
                cc = '>';

            return "CP <- m si CC está en > \n" +
                "CP <- " + m + " si " + cc + " está en >";
        }

        // ---------------------------------------------------------------------------

        public void JLT(string m)
        {
            int CC = Convert.ToInt32(registro_SW.Text, 16);

            if (CC == 0)
                registro_CP.Text = m;
            else
                AumentaEn3PC();
        }

        public string eJLT(string m)
        {
            int CC = Convert.ToInt32(registro_SW.Text, 16);
            char cc = '-';

            if (CC == 0)
                cc = '<';
            else if (CC == 1)
                cc = '=';
            else
                cc = '>';

            return "CP <- m si CC está en < \n" +
                "CP <- " + m + " si " + cc + " es <";
        }

        // ---------------------------------------------------------------------------

        public void JSUB(string m)
        {
            registro_L.Text = registro_CP.Text;
            registro_CP.Text = m;
        }

        public string eJSUB(string m)
        {
            string cp = registro_CP.Text;
            return "L <- (CP);  CP <- m\n" +
                "L <- " + cp + "; CP <- " + m;
        }

        // ---------------------------------------------------------------------------

        public void LDA(string m)
        {
            registro_A.Text = GetBytes(Convert.ToInt32(m, 16));
            AumentaEn3PC();
        }

        public string eLDA(string m)
        {
            return "A <- (m..m+2)\n" +
                "A <- " + Get3DirectionsParentheses(m);
        }

        // ---------------------------------------------------------------------------

        public void LDCH(string m)
        {
            string A = registro_A.Text;
            string temp_byte = GetByte(Convert.ToInt32(m, 16));

            A = A.Substring(0, 4) + temp_byte;

            registro_A.Text = A;
            AumentaEn3PC();
        }

        public string eLDCH(string m)
        {
            return "A [el byte de más a la derecha] <- (m)\n" +
                "A [el byte de más a la derecha] <- (" + m + ")";
        }

        // ---------------------------------------------------------------------------

        public void LDL(string m)
        {
            registro_L.Text = GetBytes(Convert.ToInt32(m, 16));
            AumentaEn3PC();
        }

        public string eLDL(string m)
        {
            return "L <- (m..m+2)\n" +
                "L <- " + Get3DirectionsParentheses(m);
        }

        // ---------------------------------------------------------------------------

        public void LDX(string m)
        {
            registro_X.Text = GetBytes(Convert.ToInt32(m, 16));
            AumentaEn3PC();
        }

        public string eLDX(string m)
        {
            return "X <- (m..m+2)\n" +
                "X <- " + Get3DirectionsParentheses(m);
        }

        // ---------------------------------------------------------------------------

        public void MUL(string m)
        {
            int A = Convert.ToInt32(registro_A.Text, 16);
            string bytes = GetBytes(Convert.ToInt32(m, 16));
            int value = Convert.ToInt32(bytes, 16);

            int res = A * value;

            registro_A.Text = res.ToString("X6");
            AumentaEn3PC();
        }

        public string eMUL(string m)
        {
            string A = registro_A.Text;
            return "A <- (A) * (m..m+2)\n" +
                "A <- " + A + " * " + Get3DirectionsParentheses(m);
        }

        // ---------------------------------------------------------------------------

        public void OR(string m)
        {
            int A = Convert.ToInt32(registro_A.Text, 16);
            string bytes = GetBytes(Convert.ToInt32(m, 16));
            int value = Convert.ToInt32(bytes, 16);

            int res = A | value;

            registro_A.Text = res.ToString("X6");
            AumentaEn3PC();
        }

        public string eOR(string m)
        {
            string A = registro_A.Text;
            return "A <- (A) | (m..m+2)\n" +
                "A <- " + A + " | " + Get3DirectionsParentheses(m);
        }

        // ---------------------------------------------------------------------------

        public void RD(string m)
        {
            AumentaEn3PC();
        }

        public string eRD(string m)
        {
            return "Instrucción RD no disponible";
        }

        // ---------------------------------------------------------------------------

        public void RSUB(string m)
        {
            registro_CP.Text = registro_L.Text;
        }

        public string eRSUB(string m)
        {
            return "PC <- (L)\n" +
                "PC <- (" + registro_L.Text + ")";
        }

        // ---------------------------------------------------------------------------

        public void STA(string m)
        {
            WriteBytes(Convert.ToInt32(m, 16), registro_A.Text);
            AumentaEn3PC();
        }

        public string eSTA(string m)
        {
            return "m..m+2 <- (A)\n" +
                Get3Directions(m) + " <- (" + registro_A.Text + ")";
        }

        // ---------------------------------------------------------------------------

        public void STCH(string m)
        {
            WriteByte(Convert.ToInt32(m, 16), registro_A.Text.Substring(4, 2));
            AumentaEn3PC();
        }

        public string eSTCH(string m)
        {
            return "m <- (A) [el byte más a la derecha]\n" +
                m + " <- " + registro_A.Text.Substring(4, 2);
        }

        // ---------------------------------------------------------------------------

        public void STL(string m)
        {
            WriteBytes(Convert.ToInt32(m, 16), registro_L.Text);
            AumentaEn3PC();
        }

        public string eSTL(string m)
        {
            return "m..m+2 <- (L)\n" +
                Get3Directions(m) + " <- (" + registro_L.Text + ")";
        }

        // ---------------------------------------------------------------------------

        public void STSW(string m)
        {
            WriteBytes(Convert.ToInt32(m, 16), registro_SW.Text);
            AumentaEn3PC();
        }

        public string eSTSW(string m)
        {
            return "m..m+2 <- (SW)\n" +
                Get3Directions(m) + " <- (" + registro_SW.Text + ")";
        }

        // ---------------------------------------------------------------------------

        public void STX(string m)
        {
            WriteBytes(Convert.ToInt32(m, 16), registro_X.Text);
            AumentaEn3PC();
        }

        public string eSTX(string m)
        {
            return "m..m+2 <- (X)\n" +
                Get3Directions(m) + " <- (" + registro_X.Text + ")";
        }

        // ---------------------------------------------------------------------------


        public void SUB(string m)
        {
            int A = Convert.ToInt32(registro_A.Text, 16);
            string bytes = GetBytes(Convert.ToInt32(m, 16));
            int value = Convert.ToInt32(bytes, 16);

            int res = A - value;

            registro_A.Text = res.ToString("X6");
            AumentaEn3PC();
        }

        public string eSUB(string m)
        {
            string A = registro_A.Text;
            return "A <- (A) | (m..m+2)\n" +
                "A <- " + A + " - " + Get3DirectionsParentheses(m);
        }

        // ---------------------------------------------------------------------------

        public void TD(string m)
        {
            AumentaEn3PC();
        }

        public string eTD(string m)
        {
            return "Instrucción TD no disponible";
        }

        // ---------------------------------------------------------------------------

        public void TIX(string m)
        {
            int X = Convert.ToInt32(registro_X.Text, 16) + 1;
            string bytes = GetBytes(Convert.ToInt32(m, 16));
            int value = Convert.ToInt32(bytes, 16);


            if (X < value)
                registro_SW.Text = (0).ToString("X6");
            else if (X == value)
                registro_SW.Text = (1).ToString("X6");
            else
                registro_SW.Text = (2).ToString("X6");

            registro_X.Text = X.ToString("X6");

            AumentaEn3PC();
        }

        public string eTIX(string m)
        {
            int X = Convert.ToInt32(registro_X.Text, 16) + 1;
            return "X <- (X) + 1; (X) : (m..m+2)\n" +
                "X <- (" + registro_X.Text + ") + 1; \n" +
                "(" + X.ToString("X6") + ") : " + Get3DirectionsParentheses(m);
        }

        // ---------------------------------------------------------------------------


        public void WD(string m)
        {
            AumentaEn3PC();
        }

        public string eWD(string m)
        {
            return "Instrucción WD no disponible";
        }
    }
} 
