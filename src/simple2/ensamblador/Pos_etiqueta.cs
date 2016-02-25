namespace simple2.ensamblador
{
	/// <remarks> Esta clase es una "estructura" que almacena el numero de 
	/// linea dentro del fichero que ocupa un objeto 
	/// (instrucción, etiqueta...) y el correspodiente nuemero
	/// de linea en el que aparece en el codigo fuente normalizado 
	/// (sin lineas en blanco, cada etiqueta va seguida de la 
	/// siguiente instrucción si existe...</remarks>

	public class Pos_etiqueta
	{

		/// <summary>Linea en la que aparece un objeto 
		/// dentro del fichero original.</summary>

		public int linea_fichero;

		/// <summary>Linea en la que aparece un objeto en el 
		/// código fuente normalizado(limpio de comentarios
		/// y lineas en blanco.</summary>
		
		public int linea_salida;


		/// <summary>Crea una instancia de la clase.</summary>
		///
		///<param name="e">Linea del fichero.</param>
		///<param name="s">Linea en el código fuente limpio.</param>

		public Pos_etiqueta (int e, int s)
		{
			linea_fichero = e;
			linea_salida = s;
		}
	}
}
