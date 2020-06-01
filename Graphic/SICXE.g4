grammar SICXE;

options {							
    language=CSharp2;							
}

@header{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Antlr4;
	using Antlr4.Runtime;
	using System.IO;
}

@members{
	public Dictionary<string, string> tabsim = new Dictionary<string,string>();
	public List<Tuple<string, string>> duplicated_sim = new List<Tuple<string,string>>();
	public List<Tuple<string, string>> programa_objeto = new List<Tuple<string,string>>();

	public int PC = 0;
	public int first_PC;

	public string name;
	public string last_sim;
}


/*
 * Parser Rules
 */

 programa
 	: 	inicio (proposicion SL)+ fin
 	;

 inicio
 	:	program_name 'START' numero SL	
		{
			PC = $numero.value;
			programa_objeto.Add(Tuple.Create(PC.ToString("X6"), "-------"));
			name = $program_name.value;
			first_PC = $numero.value;
		}
 	;

 fin
	 @init
		{
			programa_objeto.Add(Tuple.Create(PC.ToString("X6"), "-------"));
		}
  	:	'END' entrada EOF 
 	;

 entrada returns[string value]
	@after {last_sim = $value;}
 	:	ID {$value = $ID.text;}
		| 
		{$value = "";}
 	;

 proposicion
	 @init
	 {
		programa_objeto.Add(Tuple.Create(PC.ToString("X6"), ""));
	 }
 	:	instruccion {programa_objeto[programa_objeto.Count - 1] = Tuple.Create(PC.ToString("X6"), $instruccion.codigo_obj); PC += $instruccion.pc;}
		| 
		directiva {programa_objeto[programa_objeto.Count - 1] = Tuple.Create(PC.ToString("X6"), $directiva.codigo_ob); PC += $directiva.pc_value;}
 	;

 instruccion returns [string codigo_obj, int pc]
 	:	
		etiqueta nemonico_f1 {$codigo_obj = $nemonico_f1.codigo_op; $pc = 1;}
		|
		etiqueta nemonico_f2 opinstruccion2 {$codigo_obj = $nemonico_f2.codigo_op; $pc = 2;}
		|
		etiqueta nemonico_f3 opinstruccion3 {$codigo_obj = $nemonico_f3.codigo_op; $pc = 3;}
		| 
		etiqueta RSUB  {$codigo_obj = "4C0000"; $pc = 3;}
		|
		etiqueta nemonico_f4 opinstruccion3 {$codigo_obj = $nemonico_f4.codigo_op; $pc = 4;}
 	;

 directiva returns  [int pc_value, string codigo_ob]
 	:	etiqueta RESB  numero		{$pc_value = $numero.value; $codigo_ob = "------"; }	|
		etiqueta RESW  numero		{$pc_value = ($numero.value * 3); $codigo_ob= "------"; }	|
		etiqueta BYTE  opdirectiva	{$pc_value = $opdirectiva.value; $codigo_ob = $opdirectiva.cod;}	|
		etiqueta WORD  numero		{$pc_value = 3; $codigo_ob= $numero.value.ToString("X6");}
		etiqueta BASE ID
		;

etiqueta returns[string value]
	@after	{
				if($value != null)
					if(!tabsim.ContainsKey($value))
						tabsim[$value] = PC.ToString("X4");
					else
						duplicated_sim.Add(Tuple.Create($value,PC.ToString("X4")));
			}
	:	ID {$value = $ID.text;}  | 
	;

program_name returns[string value]
	:	ID {$value = $ID.text;} | 
	;

opinstruccion2 returns [string value]
	:
		registro ',' registro
		|
		registro ',' numero
		| 
		registro
	;

opinstruccion3 returns [string value]
	:	ID indexado {$value = $indexado.value + $ID.text;}
		|
		numero indexado {$value = $indexado.value + $numero.value.ToString();}
		|
		'@' ID {$value = '@' + $ID.text;}
		|
		'@' numero {$value = '@' + $numero.value.ToString();}
		|
		'#' numero {$value = '#' + $numero.value.ToString();}
	;

indexado returns [string value]
	:	',' 'X' {$value = "%";}| 
	;

opdirectiva returns [int value, string cod]
	:	XCONST
			{	$value = (int)Math.Ceiling((double)($XCONST.text.Replace("X", "").Replace("\'", "")).Length / 2);
				$cod = $XCONST.text.Replace("X", "").Replace("\'", "");
				if( $cod.Length % 2 != 0)
					$cod = "0" + $cod;
			} 
		|
		CCONST
			{$value = ($CCONST.text.Replace("C","").Replace("\'","")).Length;
			$cod = "&" + ($CCONST.text.Replace("C","").Replace("\'",""));}
	;

numero returns[int value]
 	:	DEC {$value = int.Parse($DEC.text);}
		|
		HEX_V{$value = Convert.ToInt32(($HEX_V.text).Replace("H", "").Replace("h", ""), 16);}
 	;

registro returns[string value]
	:
		X
	|	A		
	|	L		
	|	B		
	|	S		
	|	T		
	|	F		
	;

nemonico_f1 returns[string codigo_op]
	:
		FIX {$codigo_op = "C4";}
		| FLOAT {$codigo_op = "C0";}
		| HIO {$codigo_op = "F4";}
		| NORM {$codigo_op = "C8";}
		| SIO {$codigo_op = "F0";}
		| TIO {$codigo_op = "F8";}
	;

nemonico_f2 returns[string codigo_op]
	:
		ADDR {$codigo_op = "90";}
		| CLEAR {$codigo_op = "B4";}
		| COMPR {$codigo_op = "A0";}
		| DIVR {$codigo_op = "9C";}
		| MULR {$codigo_op = "98";}
		| RMO {$codigo_op = "AC";}
		| SHIFTL {$codigo_op = "AC";}
		| SHIFTR {$codigo_op = "A8";}
		| SUBR {$codigo_op = "94";}
		| SVC {$codigo_op = "B0";}
		| TIXR {$codigo_op = "B8";}
	;

nemonico_f3 returns[string codigo_op]
	:
		  ADD {$codigo_op = "18";}
		| ADDF {$codigo_op = "58";}
		| AND	{$codigo_op = "40";}
		| COMP {$codigo_op = "28";}
		| COMPF {$codigo_op = "88";}
		| DIV {$codigo_op = "24";}
		| DIVF {$codigo_op = "64";}
		| J  {$codigo_op = "3C";}
		| JEQ {$codigo_op = "30";}
		| JGT {$codigo_op = "34";}
		| JLT {$codigo_op = "38";}
		| JSUB  {$codigo_op = "48";}
		| LDA  {$codigo_op = "00";}
		| LDB  {$codigo_op = "68";}
		| LDCH {$codigo_op = "50";}
		| LDF {$codigo_op = "70";}
		| LDL  {$codigo_op = "08";}
		| LDS  {$codigo_op = "6C";}
		| LDT  {$codigo_op = "74";}
		| LDX  {$codigo_op = "04";}
		| LPS  {$codigo_op = "D0";}
		| MUL  {$codigo_op = "20";}
		| MULF  {$codigo_op = "60";}
		| OR  {$codigo_op = "44";}
		| RD  {$codigo_op = "D8";}
		| STA  {$codigo_op = "0C";}
		| STB  {$codigo_op = "78";}
		| STCH {$codigo_op = "54";}
		| STF  {$codigo_op = "80";}
		| STL  {$codigo_op = "14";}
		| STS  {$codigo_op = "7C";}
		| STSW  {$codigo_op = "E8";}
		| STT  {$codigo_op = "84";}
		| STX  {$codigo_op = "10";}
		| SUB  {$codigo_op = "1C";}
		| SUBF  {$codigo_op = "5C";}
		| TD {$codigo_op = "E0";}
		| TIX  {$codigo_op = "2C";}
		| WD  {$codigo_op = "DC";}
	;
nemonico_f4 returns[string codigo_op]
	:
		'+' nemonico_f3 {$codigo_op = $nemonico_f3.codigo_op;}
	;


/*
 * Lexer Rules
 */
SL		:	('\n')+;
COMILLA :	'\'';
WS		: 	(' '|'\r'|'\t')+ {Skip();};
C		:	'C';
X		:	'X';
H		:	[Hh];

//Registros
A		:	'A';
L		:	'L';
B		:	'B';
S		:	'S';
T		:	'T';
F		:	'F';

//	Directivas
BYTE	:	'BYTE';
WORD	:	'WORD';
RESB	:   'RESB';
RESW	:	'RESW';
BASE	:	'BASE';

//	Formato 1
FIX		:	'FIX';
FLOAT	:	'FLOAT';
HIO		:	'HIO';
NORM	:	'NORM';
SIO		:	'SIO';
TIO		:	'TIO';

//	Formato 2
ADDR	:	'ADDR';
CLEAR	:	'CLEAR';
COMPR	:	'COMPR';
DIVR	:	'DIVR';
MULR	:	'MULR';
RMO		:	'RMO';
SHIFTL	:	'SHIFTL';
SHIFTR	:	'SHIFTR';
SUBR	:	'SUBR';
SVC		:	'SVC';
TIXR	:	'TIXR';

// Formato 3 
ADD		:	'ADD';
ADDF	:	'ADDF';
AND		:	'AND';
COMP	:	'COMP';
COMPF	:	'COMPF';
DIV		:	'DIV';
DIVF	:	'DIVF';
J 		:	'J';
JEQ		:	'JEQ';
JGT		:	'JGT';
JLT		:	'JLT';
JSUB 	:	'JSUB';
LDA		:	'LDA';
LDB		:	'LDB';
LDCH	:	'LDCH';
LDF 	:	'LDF';
LDL 	:	'LDL';
LDS 	:	'LDS';
LDT 	:	'LDT';
LDX 	:	'LDX';
LPS 	:	'LPS';
MUL 	:	'MUL';
MULF 	:	'MULF';
OR 		:	'OR';
RD 		:	'RD';
RSUB	:	'RSUB';
SSK 	:	'SSK';
STA 	:	'STA';
STB 	:	'STB';
STCH	:	'STCH';
STF 	:	'STF';
STI 	:	'STI';
STL 	:	'STL';
STS 	:	'STS';
STSW	:	'STSW';
STT		:	'STT';
STX		:	'STX';
SUB		:	'SUB';
SUBF	:	'SUBF';
TD		:	'TD';
TIX		:	'TIX';
WD		:	'WD';

CCONST	: C COMILLA ID COMILLA;
XCONST	: X COMILLA HEX COMILLA;

DEC		:	('0'..'9')+;

HEX_V	:	('0'..'9'|'a'..'f'|'A'..'F')+ H;

ID		: 	('a'..'z'|'A'..'Z' | '0'..'9')+;
HEX		:	('0'..'9'|'a'..'f'|'A'..'F')+;
