grammar SIC;

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
 	:	instruccion {programa_objeto[programa_objeto.Count - 1] = Tuple.Create(PC.ToString("X6"), $instruccion.codigo_ob); PC += 3;}
		| 
		directiva {programa_objeto[programa_objeto.Count - 1] = Tuple.Create(PC.ToString("X6"), $directiva.codigo_ob); PC += $directiva.pc_value;}
 	;

 instruccion returns [string codigo_ob]
 	:	etiqueta nemonico opinstruccion {$codigo_ob = $nemonico.codigo_op + "$" + $opinstruccion.value;}
		| 
		etiqueta RSUB  {$codigo_ob = "4C0000";}
 	;

 directiva returns  [int pc_value, string codigo_ob]
 	:	etiqueta RESB  numero		{$pc_value = $numero.value; $codigo_ob = "------"; }	|
		etiqueta RESW  numero		{$pc_value = ($numero.value * 3); $codigo_ob= "------"; }	|
		etiqueta BYTE  opdirectiva	{$pc_value = $opdirectiva.value; $codigo_ob = $opdirectiva.cod;}	|
		etiqueta WORD  numero		{$pc_value = 3; $codigo_ob= $numero.value.ToString("X6");}
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

opinstruccion returns [string value]
	:	ID indexado {$value = $indexado.value + $ID.text;}
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

nemonico returns[string codigo_op]
	:
		  ADD {$codigo_op = "18";}
		| AND	{$codigo_op = "40";}
		| COMP {$codigo_op = "28";}
		| DIV {$codigo_op = "24";}
		| J  {$codigo_op = "3C";}
		| JEQ {$codigo_op = "30";}
		| JGT {$codigo_op = "34";}
		| JLT {$codigo_op = "38";}
		| JSUB  {$codigo_op = "48";}
		| LDA  {$codigo_op = "00";}
		| LDCH {$codigo_op = "50";}
		| LDL  {$codigo_op = "08";}
		| LDX  {$codigo_op = "04";}
		| MUL  {$codigo_op = "20";}
		| OR  {$codigo_op = "44";}
		| RD  {$codigo_op = "D8";}
		| STA  {$codigo_op = "0C";}
		| STCH {$codigo_op = "54";}
		| STL  {$codigo_op = "14";}
		| STSW  {$codigo_op = "E8";}
		| STX  {$codigo_op = "10";}
		| SUB  {$codigo_op = "1C";}
		| TD {$codigo_op = "E0";}
		| TIX  {$codigo_op = "2C";}
		| WD  {$codigo_op = "DC";}
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

BYTE	:	'BYTE';
WORD	:	'WORD';
RESB	:   'RESB';
RESW	:	'RESW';

ADD		:	'ADD';
AND		:	'AND';
COMP	:	'COMP';
DIV		:	'DIV';
J 		:	'J';
JEQ		:	'JEQ';
JGT		:	'JGT';
JLT		:	'JLT';
JSUB 	:	'JSUB';
LDA		:	'LDA';
LDCH	:	'LDCH';
LDL 	:	'LDL';
LDX 	:	'LDX';
MUL 	:	'MUL';
OR 		:	'OR';
RD 		:	'RD';
RSUB	:	'RSUB';
STA 	:	'STA';
STCH	:	'STCH';
STL 	:	'STL';
STSW	:	'STSW';
STX		:	'STX';
SUB		:	'SUB';
TD		:	'TD';
TIX		:	'TIX';
WD		:	'WD';

CCONST	: C COMILLA ID COMILLA;
XCONST	: X COMILLA HEX COMILLA;

DEC		:	('0'..'9')+;

HEX_V	:	('0'..'9'|'a'..'f'|'A'..'F')+ H;

ID		: 	('a'..'z'|'A'..'Z' | '0'..'9')+;
HEX		:	('0'..'9'|'a'..'f'|'A'..'F')+;
