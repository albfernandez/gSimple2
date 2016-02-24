namespace simple2.utilidades
{
	using System;
	using System.IO;

	/// <remarks>Esta clase contiene métodos para leer y escribir ficheros.
	/// </remarks>
	
	public class Fichero
	{

		/// <summary>Constructor privado.  No se permiten crear instancias 
		/// de esta clase.</summary>
		
		private Fichero ()
		{
		}
		
		/// <summary>Carga el texto contenido en un archivo.</summary>
		/// <param name="archivo">La ruta del archivo a leer.
		/// </param>
		/// <returns>El texto contenido en <c>archivo</c>.</returns>
		
		public static String CargarTexto (String archivo)
		{

			try
			{
				TextReader file = File.OpenText (archivo);
				String texto = file.ReadToEnd ();
				file.Close ();
				return texto;

			}
			catch (ArgumentException ex)
			{

				TextReader file = new StreamReader (
									archivo, System.Text.Encoding.Default);
				String texto = file.ReadToEnd ();
				file.Close ();
				return texto;
			}
		}
		
		/// <summary>Guarda un texto en el archivo indicado.</summary>
		/// <param name="nombreFichero">La ruta del fichero en el que
		/// se guardará el texto. Si no existe se creará.</param>
		/// <param name="texto">El texto a guardar.</param>
		
		public static void GuardarTexto (String nombreFichero, String texto)
		{
			TextWriter file = new StreamWriter (nombreFichero, false,
						  System.Text.Encoding.Default);
			file.Write (texto);
			file.Close ();
		}
	}
}
