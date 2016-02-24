namespace simple2.rutaDeDatos
{
	using System;
	
	/// <remarks>Esta clase representa a una microinstrucci�n del 
	/// ordenador Simple2.</remarks>
	
	public class MicroInstruccion
	{
	
		/// <summary>La microinstrucci�n en binario, como un entero
		/// largo sin signo.</summary>
		
		private ulong instruccion;

		/// <summary>Crea una instancia de la clase a partir de un valor.
		/// </summary>
		/// <param name="valor">El valor binario de la microinstrucci�n.
		/// </param>
		
		public MicroInstruccion (ulong valor)
		{
			instruccion = valor;
		}
		
		/// <summary>Obtiene la microinstrucci�n como un entero largo sin
		/// signo.</summary>
		/// <returns>la microinstrucci�n como un entero largo sin
		/// signo.</returns>
		
		public ulong ToLong ()
		{
			return instruccion;
		}
		
		/// <summary>Obtiene la operaci�n de la ALU.</summary>
		/// <returns>La operaci�n de la ALU.</returns>
		
		public int GetALU ()
		{
			return GetBits (9, 4);
		}
		
		/// <summary>Obtiene el valor del campo FIR</summary>
		/// <returns>El valor del campo FIR</returns>
		
		public int GetFIR ()
		{
			return GetBits (13, 1);
		}
		
		/// <summary>Obtiene el valor del campo ADDR.</summary>
		/// <returns>El valor del campo ADDR.</returns>
		
		public int GetADDR ()
		{
			return GetBits (14, 10);
		}
		
		/// <summary>Obtiene el valor del campo A.</summary>
		/// <returns>El valor del campo A.</returns>
		
		public int GetA ()
		{
			return GetBits (24, 4);
		}
		
		/// <summary>Obtiene el valor del campo B.</summary>
		/// <returns>El valor del campo B.</returns>
		
		public int GetB ()
		{
			return GetBits (28, 4);
		}
		
		/// <summary>Obtiene el valor del campo C.</summary>
		/// <returns>El valor del campo C.</returns>
		
		public int GetC ()
		{
			return GetBits (32, 4);
		}
		
		/// <summary>Obtiene el valor del campo ENC.</summary>
		/// <returns>El valor del campo ENC.</returns>
		
		public int GetENC ()
		{
			return GetBits (36, 1);
		}
		
		/// <summary>Obtiene el valor del campo WR.</summary>
		/// <returns>El valor del campo WR.</returns>
		
		public int GetWR ()
		{
			return GetBits (37, 1);
		}
		
		/// <summary>Obtiene el valor del campo RD.</summary>
		/// <returns>El valor del campo RD.</returns>
		
		public int GetRD ()
		{
			return GetBits (38, 1);
		}
		
		/// <summary>Obtiene el valor del campo MAR.</summary>
		/// <returns>El valor del campo MAR.</returns>
		
		public int GetMAR ()
		{
			return GetBits (39, 1);
		}
		
		/// <summary>Obtiene el valor del campo MBR.</summary>
		/// <returns>El valor del campo MBR.</returns>
		
		public int GetMBR ()
		{
			return GetBits (40, 1);
		}
		
		/// <summary>Obtiene el valor del campo SH.</summary>
		/// <returns>El valor del campo SH.</returns>
		
		public int GetSH ()
		{
			return GetBits (41, 3);
		}
		
		/// <summary>Obtiene el valor del campo COND.</summary>
		/// <returns>El valor del campo COND.</returns>
		
		public int GetCOND ()
		{
			return GetBits (44, 3);
		}
		
		/// <summary>Obtiene el valor del campo AMUX.</summary>
		/// <returns>El valor del campo AMUX.</returns>
		
		public int GetAMUX ()
		{
			return GetBits (47, 1);
		}
		
		/// <summary>Obtiene el valor de un campo en una posici�n y de
		/// un tama�o dados.</summary>
		/// <param name="b">Posici�n del primer bit del campo.</param>
		/// <param name="count">N�mero de bits que componen el campo.</param>
		/// <returns></returns>
		
		private int GetBits (int b, int count)
		{
			ulong mask = 0xFFFFFFFFFFFFFFFFL >> (64 - count);
			mask = mask << b;
			ulong resultado = (instruccion & mask) >> b;
			return (int) resultado;
		}
		
		/// <summary>Obtiene una representaci�n en hexadecimal de
		/// la microinstrucci�n como una cadena de texto.</summary>
		/// <returns>La representanci�n en hexadecimal de la 
		/// microinstrucci�n.</returns>
		
		public String ToHexString ()
		{
			String ret = instruccion.ToString ("X");
			while (ret.Length < 12)
				ret = "0" + ret;
			ret = ret.Substring (0,4) + " " + ret.Substring(4,4) +
				" " + ret.Substring (8);
			ret = ret.ToUpper();			
			return ret;
		}
	}
}
