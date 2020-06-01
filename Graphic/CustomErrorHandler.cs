using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System.IO;

namespace Graphic
{
    class CustomErrorHandler : DefaultErrorStrategy
    {
        /*
        public override void Recover(Parser recognizer, RecognitionException e)
        {
            //Console.WriteLine("Error");
            BinaryWriter writer = new BinaryWriter(new FileStream(Environment.CurrentDirectory + @"\Files\errors.s", FileMode.Append));
            base.Recover(recognizer, e);
            if (recognizer.CurrentToken.Type != SICParser.Eof)
            {
                writer.Write("Error de Sintaxis en línea: " + e.OffendingToken.Line + " - " + e.OffendingToken.Text + "\n");
                //Console.WriteLine("Prueba 0 - " + recognizer.CurrentToken.Line);
                IToken temp = recognizer.Consume();
                //Console.WriteLine("Prueba 1 - " + temp.Line);
            }
            writer.Close();
        }
        */

        protected override void BeginErrorCondition([NotNull] Parser recognizer)
        {
            base.BeginErrorCondition(recognizer);

            BinaryWriter writer = new BinaryWriter(new FileStream(Environment.CurrentDirectory + @"\Files\errors.s", FileMode.Append));
            writer.Write(UTF8Encoding.Default.GetBytes("Error de Sintaxis en linea: " + recognizer.CurrentToken.Line + " - " 
                + recognizer.CurrentToken.Text + "\n"));
            writer.Close();
        }
    }
}
