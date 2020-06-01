namespace Graphic
{
    partial class lab_grafico
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_archivo_intermedio = new System.Windows.Forms.DataGridView();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etiqueta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.instruccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operando = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cod_obj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asm_code = new System.Windows.Forms.RichTextBox();
            this.dgv_tabsim = new System.Windows.Forms.DataGridView();
            this.simbolo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errores = new System.Windows.Forms.TextBox();
            this.text_programa_objeto = new System.Windows.Forms.TextBox();
            this.label_code = new System.Windows.Forms.Label();
            this.label_archivo_intermedio = new System.Windows.Forms.Label();
            this.label_tabsim = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_codigo_objeto = new System.Windows.Forms.Label();
            this.ensamblar = new System.Windows.Forms.Button();
            this.tab_control = new System.Windows.Forms.TabControl();
            this.tab_ensamblador = new System.Windows.Forms.TabPage();
            this.tab_cargador = new System.Windows.Forms.TabPage();
            this.boton_cargar = new System.Windows.Forms.Button();
            this.dgv_cargador = new System.Windows.Forms.DataGridView();
            this.dir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_C = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_E = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dig_F = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_archivo_intermedio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabsim)).BeginInit();
            this.tab_control.SuspendLayout();
            this.tab_ensamblador.SuspendLayout();
            this.tab_cargador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cargador)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1234, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.cerrarToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // dgv_archivo_intermedio
            // 
            this.dgv_archivo_intermedio.AllowUserToAddRows = false;
            this.dgv_archivo_intermedio.AllowUserToDeleteRows = false;
            this.dgv_archivo_intermedio.AllowUserToResizeColumns = false;
            this.dgv_archivo_intermedio.AllowUserToResizeRows = false;
            this.dgv_archivo_intermedio.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgv_archivo_intermedio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_archivo_intermedio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.num,
            this.cp,
            this.etiqueta,
            this.instruccion,
            this.operando,
            this.cod_obj});
            this.dgv_archivo_intermedio.Location = new System.Drawing.Point(302, 57);
            this.dgv_archivo_intermedio.Name = "dgv_archivo_intermedio";
            this.dgv_archivo_intermedio.ReadOnly = true;
            this.dgv_archivo_intermedio.RowHeadersVisible = false;
            this.dgv_archivo_intermedio.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_archivo_intermedio.ShowEditingIcon = false;
            this.dgv_archivo_intermedio.Size = new System.Drawing.Size(483, 509);
            this.dgv_archivo_intermedio.TabIndex = 1;
            // 
            // num
            // 
            this.num.HeaderText = "";
            this.num.Name = "num";
            this.num.ReadOnly = true;
            this.num.Width = 30;
            // 
            // cp
            // 
            this.cp.HeaderText = "CP";
            this.cp.Name = "cp";
            this.cp.ReadOnly = true;
            this.cp.Width = 70;
            // 
            // etiqueta
            // 
            this.etiqueta.HeaderText = "Etiqueta";
            this.etiqueta.Name = "etiqueta";
            this.etiqueta.ReadOnly = true;
            this.etiqueta.Width = 90;
            // 
            // instruccion
            // 
            this.instruccion.HeaderText = "Instruccion";
            this.instruccion.Name = "instruccion";
            this.instruccion.ReadOnly = true;
            this.instruccion.Width = 90;
            // 
            // operando
            // 
            this.operando.HeaderText = "Operando";
            this.operando.Name = "operando";
            this.operando.ReadOnly = true;
            this.operando.Width = 90;
            // 
            // cod_obj
            // 
            this.cod_obj.HeaderText = "Código Objeto";
            this.cod_obj.Name = "cod_obj";
            this.cod_obj.ReadOnly = true;
            this.cod_obj.Width = 110;
            // 
            // asm_code
            // 
            this.asm_code.AcceptsTab = true;
            this.asm_code.DetectUrls = false;
            this.asm_code.Location = new System.Drawing.Point(3, 57);
            this.asm_code.Name = "asm_code";
            this.asm_code.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.asm_code.Size = new System.Drawing.Size(268, 509);
            this.asm_code.TabIndex = 2;
            this.asm_code.Text = "";
            // 
            // dgv_tabsim
            // 
            this.dgv_tabsim.AllowUserToAddRows = false;
            this.dgv_tabsim.AllowUserToDeleteRows = false;
            this.dgv_tabsim.AllowUserToResizeColumns = false;
            this.dgv_tabsim.AllowUserToResizeRows = false;
            this.dgv_tabsim.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgv_tabsim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_tabsim.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.simbolo,
            this.direccion});
            this.dgv_tabsim.Location = new System.Drawing.Point(825, 57);
            this.dgv_tabsim.Name = "dgv_tabsim";
            this.dgv_tabsim.ReadOnly = true;
            this.dgv_tabsim.RowHeadersVisible = false;
            this.dgv_tabsim.Size = new System.Drawing.Size(184, 258);
            this.dgv_tabsim.TabIndex = 3;
            // 
            // simbolo
            // 
            this.simbolo.HeaderText = "Simbolo";
            this.simbolo.Name = "simbolo";
            this.simbolo.ReadOnly = true;
            this.simbolo.Width = 90;
            // 
            // direccion
            // 
            this.direccion.HeaderText = "Dirección";
            this.direccion.Name = "direccion";
            this.direccion.ReadOnly = true;
            this.direccion.Width = 90;
            // 
            // errores
            // 
            this.errores.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.errores.Location = new System.Drawing.Point(1045, 57);
            this.errores.Multiline = true;
            this.errores.Name = "errores";
            this.errores.ReadOnly = true;
            this.errores.Size = new System.Drawing.Size(139, 258);
            this.errores.TabIndex = 4;
            // 
            // text_programa_objeto
            // 
            this.text_programa_objeto.AcceptsReturn = true;
            this.text_programa_objeto.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.text_programa_objeto.Location = new System.Drawing.Point(825, 358);
            this.text_programa_objeto.Multiline = true;
            this.text_programa_objeto.Name = "text_programa_objeto";
            this.text_programa_objeto.ReadOnly = true;
            this.text_programa_objeto.Size = new System.Drawing.Size(359, 208);
            this.text_programa_objeto.TabIndex = 5;
            // 
            // label_code
            // 
            this.label_code.AutoSize = true;
            this.label_code.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_code.Location = new System.Drawing.Point(3, 26);
            this.label_code.Name = "label_code";
            this.label_code.Size = new System.Drawing.Size(167, 20);
            this.label_code.TabIndex = 6;
            this.label_code.Text = "Código Ensamblador";
            // 
            // label_archivo_intermedio
            // 
            this.label_archivo_intermedio.AutoSize = true;
            this.label_archivo_intermedio.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_archivo_intermedio.Location = new System.Drawing.Point(298, 26);
            this.label_archivo_intermedio.Name = "label_archivo_intermedio";
            this.label_archivo_intermedio.Size = new System.Drawing.Size(153, 20);
            this.label_archivo_intermedio.TabIndex = 7;
            this.label_archivo_intermedio.Text = "Archivo intermedio";
            // 
            // label_tabsim
            // 
            this.label_tabsim.AutoSize = true;
            this.label_tabsim.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_tabsim.Location = new System.Drawing.Point(821, 26);
            this.label_tabsim.Name = "label_tabsim";
            this.label_tabsim.Size = new System.Drawing.Size(146, 20);
            this.label_tabsim.TabIndex = 8;
            this.label_tabsim.Text = "Tabla de Símbolos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1047, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Errores";
            // 
            // label_codigo_objeto
            // 
            this.label_codigo_objeto.AutoSize = true;
            this.label_codigo_objeto.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_codigo_objeto.Location = new System.Drawing.Point(821, 335);
            this.label_codigo_objeto.Name = "label_codigo_objeto";
            this.label_codigo_objeto.Size = new System.Drawing.Size(120, 20);
            this.label_codigo_objeto.TabIndex = 10;
            this.label_codigo_objeto.Text = "Código Objeto";
            // 
            // ensamblar
            // 
            this.ensamblar.Location = new System.Drawing.Point(48, 577);
            this.ensamblar.Name = "ensamblar";
            this.ensamblar.Size = new System.Drawing.Size(162, 23);
            this.ensamblar.TabIndex = 11;
            this.ensamblar.Text = "Ensamblar";
            this.ensamblar.UseVisualStyleBackColor = true;
            this.ensamblar.Click += new System.EventHandler(this.ensamblar_Click);
            // 
            // tab_control
            // 
            this.tab_control.Controls.Add(this.tab_ensamblador);
            this.tab_control.Controls.Add(this.tab_cargador);
            this.tab_control.Location = new System.Drawing.Point(0, 27);
            this.tab_control.Name = "tab_control";
            this.tab_control.SelectedIndex = 0;
            this.tab_control.Size = new System.Drawing.Size(1222, 633);
            this.tab_control.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tab_control.TabIndex = 12;
            // 
            // tab_ensamblador
            // 
            this.tab_ensamblador.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tab_ensamblador.Controls.Add(this.label_code);
            this.tab_ensamblador.Controls.Add(this.ensamblar);
            this.tab_ensamblador.Controls.Add(this.dgv_archivo_intermedio);
            this.tab_ensamblador.Controls.Add(this.label_codigo_objeto);
            this.tab_ensamblador.Controls.Add(this.asm_code);
            this.tab_ensamblador.Controls.Add(this.label2);
            this.tab_ensamblador.Controls.Add(this.dgv_tabsim);
            this.tab_ensamblador.Controls.Add(this.label_tabsim);
            this.tab_ensamblador.Controls.Add(this.errores);
            this.tab_ensamblador.Controls.Add(this.label_archivo_intermedio);
            this.tab_ensamblador.Controls.Add(this.text_programa_objeto);
            this.tab_ensamblador.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tab_ensamblador.Location = new System.Drawing.Point(4, 22);
            this.tab_ensamblador.Name = "tab_ensamblador";
            this.tab_ensamblador.Padding = new System.Windows.Forms.Padding(3);
            this.tab_ensamblador.Size = new System.Drawing.Size(1214, 607);
            this.tab_ensamblador.TabIndex = 0;
            this.tab_ensamblador.Text = "Ensamblador";
            // 
            // tab_cargador
            // 
            this.tab_cargador.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tab_cargador.Controls.Add(this.boton_cargar);
            this.tab_cargador.Controls.Add(this.dgv_cargador);
            this.tab_cargador.Location = new System.Drawing.Point(4, 22);
            this.tab_cargador.Name = "tab_cargador";
            this.tab_cargador.Padding = new System.Windows.Forms.Padding(3);
            this.tab_cargador.Size = new System.Drawing.Size(1214, 607);
            this.tab_cargador.TabIndex = 1;
            this.tab_cargador.Text = "Cargador";
            // 
            // boton_cargar
            // 
            this.boton_cargar.Location = new System.Drawing.Point(502, 566);
            this.boton_cargar.Name = "boton_cargar";
            this.boton_cargar.Size = new System.Drawing.Size(216, 23);
            this.boton_cargar.TabIndex = 1;
            this.boton_cargar.Text = "Cargar a Memoria";
            this.boton_cargar.UseVisualStyleBackColor = true;
            this.boton_cargar.Click += new System.EventHandler(this.boton_cargar_Click);
            // 
            // dgv_cargador
            // 
            this.dgv_cargador.AllowUserToDeleteRows = false;
            this.dgv_cargador.AllowUserToResizeColumns = false;
            this.dgv_cargador.AllowUserToResizeRows = false;
            this.dgv_cargador.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_cargador.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dir,
            this.dig_0,
            this.dig_1,
            this.dig_2,
            this.dig_3,
            this.dig_4,
            this.dig_5,
            this.dig_6,
            this.dig_7,
            this.dig_8,
            this.dig_9,
            this.dig_A,
            this.dig_B,
            this.dig_C,
            this.dig_D,
            this.dig_E,
            this.dig_F});
            this.dgv_cargador.Location = new System.Drawing.Point(23, 16);
            this.dgv_cargador.Name = "dgv_cargador";
            this.dgv_cargador.Size = new System.Drawing.Size(1165, 530);
            this.dgv_cargador.TabIndex = 0;
            this.dgv_cargador.Visible = false;
            // 
            // dir
            // 
            this.dir.HeaderText = "Dirección";
            this.dir.Name = "dir";
            // 
            // dig_0
            // 
            this.dig_0.HeaderText = "0";
            this.dig_0.Name = "dig_0";
            // 
            // dig_1
            // 
            this.dig_1.HeaderText = "1";
            this.dig_1.Name = "dig_1";
            // 
            // dig_2
            // 
            this.dig_2.HeaderText = "2";
            this.dig_2.Name = "dig_2";
            // 
            // dig_3
            // 
            this.dig_3.HeaderText = "3";
            this.dig_3.Name = "dig_3";
            // 
            // dig_4
            // 
            this.dig_4.HeaderText = "4";
            this.dig_4.Name = "dig_4";
            // 
            // dig_5
            // 
            this.dig_5.HeaderText = "5";
            this.dig_5.Name = "dig_5";
            // 
            // dig_6
            // 
            this.dig_6.HeaderText = "6";
            this.dig_6.Name = "dig_6";
            // 
            // dig_7
            // 
            this.dig_7.HeaderText = "7";
            this.dig_7.Name = "dig_7";
            // 
            // dig_8
            // 
            this.dig_8.HeaderText = "8";
            this.dig_8.Name = "dig_8";
            // 
            // dig_9
            // 
            this.dig_9.HeaderText = "9";
            this.dig_9.Name = "dig_9";
            // 
            // dig_A
            // 
            this.dig_A.HeaderText = "A";
            this.dig_A.Name = "dig_A";
            // 
            // dig_B
            // 
            this.dig_B.HeaderText = "B";
            this.dig_B.Name = "dig_B";
            // 
            // dig_C
            // 
            this.dig_C.HeaderText = "C";
            this.dig_C.Name = "dig_C";
            // 
            // dig_D
            // 
            this.dig_D.HeaderText = "D";
            this.dig_D.Name = "dig_D";
            // 
            // dig_E
            // 
            this.dig_E.HeaderText = "E";
            this.dig_E.Name = "dig_E";
            // 
            // dig_F
            // 
            this.dig_F.HeaderText = "F";
            this.dig_F.Name = "dig_F";
            // 
            // lab_grafico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 661);
            this.Controls.Add(this.tab_control);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "lab_grafico";
            this.Text = "Laboratorio Gráfico de Programación de Sistemas";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_archivo_intermedio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabsim)).EndInit();
            this.tab_control.ResumeLayout(false);
            this.tab_ensamblador.ResumeLayout(false);
            this.tab_ensamblador.PerformLayout();
            this.tab_cargador.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cargador)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgv_archivo_intermedio;
        private System.Windows.Forms.RichTextBox asm_code;
        private System.Windows.Forms.DataGridView dgv_tabsim;
        private System.Windows.Forms.DataGridViewTextBoxColumn simbolo;
        private System.Windows.Forms.DataGridViewTextBoxColumn direccion;
        private System.Windows.Forms.TextBox errores;
        private System.Windows.Forms.TextBox text_programa_objeto;
        private System.Windows.Forms.Label label_code;
        private System.Windows.Forms.Label label_archivo_intermedio;
        private System.Windows.Forms.Label label_tabsim;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_codigo_objeto;
        private System.Windows.Forms.Button ensamblar;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn cp;
        private System.Windows.Forms.DataGridViewTextBoxColumn etiqueta;
        private System.Windows.Forms.DataGridViewTextBoxColumn instruccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn operando;
        private System.Windows.Forms.DataGridViewTextBoxColumn cod_obj;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
        private System.Windows.Forms.TabControl tab_control;
        private System.Windows.Forms.TabPage tab_ensamblador;
        private System.Windows.Forms.TabPage tab_cargador;
        private System.Windows.Forms.Button boton_cargar;
        private System.Windows.Forms.DataGridView dgv_cargador;
        private System.Windows.Forms.DataGridViewTextBoxColumn dir;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_0;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_C;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_D;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_E;
        private System.Windows.Forms.DataGridViewTextBoxColumn dig_F;
    }
}

