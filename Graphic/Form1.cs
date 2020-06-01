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
        Analizador analizador;
        string path;

        public lab_grafico()
        {
            InitializeComponent();
        }

        private void ensamblar_Click(object sender, EventArgs e)
        {
            if (asm_code.Lines != null && asm_code.Lines.Length > 0)
            {
                try
                {
                    analizador = new Analizador(asm_code.Lines);
                    analizador.Ensamblar();

                    Fill_Intermediary_File(analizador.intermediary_code, analizador.asm_code);
                    Fill_TabSim(analizador.symbol_table);
                    Fill_Registers(analizador.registers);
                    Fill_Errors();

                    MessageBox.Show("Ensamblado se realizó con éxito");
                }
                catch
                {
                    MessageBox.Show("Error en lectura de código. Programa sensible a espacios en blanco y tabulaciones", "Error");
                }
            }
        }

        private void Fill_Intermediary_File(List<Tuple<string, string>> file, string[] code)
        {
            Dictionary<string, string> tabsim = analizador.symbol_table;
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
            foreach (var item in tabsim)
                dgv_tabsim.Rows.Add(new string[] { item.Key, item.Value });
        }

        private void Fill_Registers(List<string> registers)
        {
            text_programa_objeto.Clear();
            text_programa_objeto.Lines = registers.ToArray();
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
            if(path!= null)
                this.Text = this.Text.Substring(0, this.Text.IndexOf('-') - 1);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory + @"\Codes";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!OpenCodeFile(openFileDialog.FileName))
                    MessageBox.Show("Error al abrir archivo.", "Error");
            }
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

        private void ClearAll()
        {
            dgv_archivo_intermedio.Rows.Clear();
            dgv_tabsim.Rows.Clear();
            errores.Text = "";
            text_programa_objeto.Text = "";
        }

        private void FillAsmCode()
        {
            asm_code.Lines = File.ReadAllLines(path);
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

        private void Fill_Errors()
        {
            if (analizador.errors.Length > 0)
                errores.Lines = analizador.errors;
            else
                errores.Lines = new string[] { "No hay errores" };
        }

        private void boton_cargar_Click(object sender, EventArgs e)
        {

        }
    }
} 
