namespace simple2.ensamblador
{

	using System;
	using System.Collections;

	/// <remarks>Interface para las instrucciones de la arquitectura
	/// del simple2.</remarks>
	
	public interface InstruccionSimple2
	{
	
		/// <summary>Verifica una linea de código.</summary>
		/// <param name="linea">La linea a verificar.</param>
		/// <param name="etiquetasUsadas">La lista de etiquetas usadas.
		/// </param>
		/// <param name="l_fichero">La linea en la que se encuentra la
		/// instrucción</param>
		/// <exception cref="gSimple2.Ensamblador.ErrorCodigoException">
		/// Si la instrucción no es correcta.</exception>
		
		void Verificar
			(String linea, Hashtable etiquetasUsadas, int l_fichero);
		
		/// <summary>Compila una linea de código</summary>
		/// <param name="linea">La linea a codificar</param>
		/// <param name="etiquetasDeclaradas>La lista de etiquetas declaradas
		/// con su posición.</param>
		/// <returns>El código ensamblado en binario</returns>
		
		short Compilar 
			(String linea, Hashtable etiquetasDeclaradas);
		
	}
}
