namespace simple2.utilidades
{

	using System;

	/// <remarks>Clase que contiene varios métodos estáticos útiles para
	/// realizar conversiones.</remarks>
	
	public class Conversiones
	{
		
		/// <summary>Constructor privado, no se permite crear instancias de
		/// esta clase.</summary>
		
		private Conversiones(){}
	
		/// <summary>Obtiene la cadena con la representación en hexadecimal
		/// de <c>valor</c></summary>
		/// <param name="valor">El número.</param>
		/// <returns>La cadena con la representación en hexadecimal de 
		///	<c>valor</c>.</returns>

		public static String ToHexString (int valor)
		{
			String ret = valor.ToString ("X");
			while (ret.Length < 8)
				  ret = "0" + ret;
			  return ret;
		}
		
		/// <summary>Obtiene la cadena con la representación en hexadecimal
		/// de <c>valor</c>.</summary>
		/// <param name="valor">El número.</param>
		/// <returns>La cadena con la representación en hexadecimal de 
		///	<c>valor</c>.</returns>
		
		public static String ToHexString (short valor)
		{
			String ret = valor.ToString ("X");
			while (ret.Length < 4)
				  ret = "0" + ret;
			return ret;

		}
		
		/// <summary>Obtiene la cadena con la representación en binario
		/// de <c>valor</c>.</summary>
		/// <param name="valor">El número.</param>
		/// <param name="digitos">El número de digítos que tendrá el 
		/// resultado.</param>
		/// <returns>Una cadena con la representación binaria de 
		/// <c>valor</c>.</returns>
		
		public static String ToBinaryString (int valor, int digitos)
		{
			int aux;
			aux = valor;
			String ret = "";
			for (int i=0; (i < digitos) && (i < 32); i++)
			{
				ret = (aux & 1) + ret;
				aux = aux >> 1;
			}
			return ret;
		}
	}	
}
