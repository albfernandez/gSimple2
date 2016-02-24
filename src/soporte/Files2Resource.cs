using System;
using System.IO;
using System.Resources;

/// <remarks><para>Esta clase se encarga de leer un conjunto de archivos
/// y guardarlos como arrays de bytes en un archivo de recursos.</para>
/// <para>Útil para guardar imágenes como recursos en el assembly.
/// </para></remarks>

public class Files2Resource
{

	/// <summary>Constructor privado. No se permite crear instancias
	/// de esta clase.</summary>
	
	private Files2Resource ()
	{}

	/// <summary>Función principal del programa.</summary>
	/// <param name="args">Los argumentos pasados desde la línea de
	/// comandos. El último de ellos es el fichero de recursos a generar
	/// y los anteriores los ficheros a incluir.</param>
	
	public static void Main (String[] args)
	{

		if (args.Length < 2)
		{
			Console.WriteLine ("Parámetros incorrectos");
			Console.WriteLine ("<archivos a incluir> <archivo destino>");
			Environment.Exit (-1);
		}
		String ficheroSalida = args[args.Length - 1];
		ResourceWriter rsw = new ResourceWriter (ficheroSalida);
		for (int i = 0; i < args.Length - 1; i++)
		{
			try
			{
				byte[]img = CargarFichero (args[i]);
				rsw.AddResource (CrearNombre (args[i]), img);
			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine 
					("ERROR. Fichero no encontrado: {0}", args[i]);
				rsw.Close ();
				File.Delete (ficheroSalida);
				Environment.Exit (-1);
			}
		}
		rsw.Close ();
		Console.WriteLine 
			("Leídos {0} archivos (recursos).",  args.Length - 1);
		Console.WriteLine 
			("Escrito el archivo de recursos {0}",  ficheroSalida);
	}
	
	/// <summary>Crea un nombre para el recurso a partir del nombre
	/// del fichero de imagen de origen.</summary>
	/// <param name="fichero">El nombre del archivo.</param>
	/// <returns>El nombre para el recurso generado a partir de <c>
	/// fichero</c>.</returns>
	
	public static String CrearNombre (String fichero)
	{
		int pos = fichero.LastIndexOf ('/');
		return fichero.Substring (pos + 1);
	}
	
	/// <summary>Lee un fichero y devuelve su contenido como un array
	/// de bytes.</summary>
	/// <param name="filename">El nombre del fichero a leer.</param>
	/// <returns>El contenido del fichero en un array de bytes.</returns>
	
	public static byte[] CargarFichero (String filename)
	{
		byte[]contenido = null;
		FileStream file = File.OpenRead (filename);
		BinaryReader br = new BinaryReader (file);
		contenido = br.ReadBytes ((int) file.Length);
		return contenido;
	}
}
