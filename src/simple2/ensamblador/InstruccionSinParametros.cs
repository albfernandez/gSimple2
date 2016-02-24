namespace simple2.ensamblador
{

	using System;
	using System.Collections;
	
	using simple2.utilidades;
	
	/// <remarks>Clase utilizada para verificar y compilar las instrucciones
	/// sin par�metros de la arquitectura Simple2.</remarks>	
	
	public class InstruccionSinParametros:InstruccionSimple2
	{
	
		/// <summary>Tabla donde se almacenan las instrucciones y su 
		/// correspondientes opcodes.</summary>
		
		private Hashtable opcodes;
		
		/// <summary>Crea una instancia de la clase.</summary>
		
		public InstruccionSinParametros ()
		{
			opcodes = new Hashtable ();

			opcodes.Add ("PUSH", 0x08);
			opcodes.Add ("POP" , 0x09);
			opcodes.Add ("RETN", 0x0F);
			opcodes.Add ("HALT", 0x1F);
		}
		
		/// <summary>Verifica una linea de c�digo.</summary>
		/// <param name="linea">La linea a verificar.</param>
		/// <param name="etiquetasUsadas">La lista de etiquetas usadas.
		/// </param>
		/// <param name="l_fichero">La linea en la que se encuentra la
		/// instrucci�n</param>
		/// <exception cref="gSimple2.Ensamblador.ErrorCodigoException">
		/// Si la instrucci�n no es correcta.</exception>
		
		public void Verificar (String linea,
				       Hashtable etiquetasUsadas,
				       int l_fichero)
		{

			if (linea.IndexOf (' ') != -1)
			{
				throw new ErrorCodigoException	(String.Format (
					TextManager.GetText ("Ens_err_parm0"), 
					linea.Substring (0, linea.IndexOf (' '))));
			}
		}

		/// <summary>Compila una linea de c�digo</summary>
		/// <param name="linea">La linea a codificar</param>
		/// <param name="etiquetasDeclaradas>La lista de etiquetas declaradas
		/// con su posici�n.</param>
		/// <returns>El c�digo ensamblado en binario</returns>

		public short Compilar (String linea,  Hashtable etiquetasDeclaradas)
		{
			int opc = (int) opcodes[linea];
			return ((short) (opc << 11));
		}
	}
}
