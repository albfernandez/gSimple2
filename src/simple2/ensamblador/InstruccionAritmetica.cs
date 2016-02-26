namespace simple2.ensamblador
{

	using System;
	using System.Collections;
	
	using simple2.utilidades;

	/// <remarks>Clase utilizada para verificar y compilar las instrucciones
	/// aritméticas de la arquitectura Simple2.</remarks>
	
	public class InstruccionAritmetica:InstruccionSimple2
	{
		/// <summary>Tabla donde se almacenan las instrucciones y su 
		/// correspondientes opcodes.</summary>
		
		private Hashtable opcodes;
		
		/// <summary>Crea una instancia de la clase.</summary>
		
		public InstruccionAritmetica ()
		{
			opcodes = new Hashtable ();

			opcodes.Add ("LODD", 0x01);
			opcodes.Add ("LODI", 0x02);
			opcodes.Add ("STOD", 0x03);
			opcodes.Add ("ADDD", 0x04);
			opcodes.Add ("ADDI", 0x05);
			opcodes.Add ("SUBD", 0x06);
			opcodes.Add ("SUBI", 0x07);
		}

		/// <summary>Verifica una linea de código.</summary>
		/// <param name="linea">La linea a verificar.</param>
		/// <param name="etiquetasUsadas">La lista de etiquetas usadas.
		/// </param>
		/// <param name="l_fichero">La linea en la que se encuentra la
		/// instrucción</param>
		/// <exception cref="gSimple2.Ensamblador.ErrorCodigoException">
		/// Si la instrucción no es correcta.</exception>
		
		public void Verificar (String linea, 
					Hashtable etiquetasUsadas,
					int l_fichero)
		{
			int numero = 0;
			  String[] op =	EnsambladorSimple2.SepararOperandos (linea);

			if (op.Length != 2)
			{
				throw new ErrorCodigoException (String.Format (
					TextManager.GetText ("Ens_err_parame1"), op[0]));
			}

			try
			{
				numero = Int32.Parse (op[1]);
			}
			catch (Exception)
			{
				throw new ErrorCodigoException
					(TextManager.GetText ("Ens_err_parm1_numero"));

			}
			if ((numero > 2047) || (numero < 0))
			{
				throw new ErrorCodigoException
						(TextManager.GetText("Ens_err_parame_fr"));
			}
		}

		/// <summary>Compila una linea de código</summary>
		/// <param name="linea">La linea a codificar</param>
		/// <param name="etiquetasDeclaradas>La lista de etiquetas declaradas
		/// con su posición.</param>
		/// <returns>El código ensamblado en binario</returns>

		public short Compilar (String linea, Hashtable etiquetasDeclaradas)
		{
			String[]inst =	EnsambladorSimple2.SepararOperandos (linea);

			int opc = (int) opcodes[inst[0]];
			int numero = Int32.Parse (inst[1]);
			int resultado = (opc << 11);
			resultado |= numero;

			return (short) resultado;
		}
	}
}
