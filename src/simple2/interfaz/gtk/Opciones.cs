namespace simple2.interfaz.gtk
{
	using System.Runtime.Serialization.Formatters.Binary;
	using System.Runtime.Serialization;
	using System.IO;
	using System;

	/// <remarks>Esta clase representa los elementos configurables
	/// por el usuario en la aplicación.</remarks>

	[Serializable]
	public class Opciones 
	{
		/// <summary>Única instancia de la clase (patrón Singleton).
		/// </summary>
		
		private static Opciones instancia = null;
		
		/// <summary>Ruta del fichero donde se almacenarán las opciones.
		/// </summary>
		
		private static String fichero = ".gSimple2.cfg";
		
		/// <summary>Indica si se deben mostrar las advertencias generadas
		/// por el ensamblador.</summary>
		
		private bool mostrarAdvertencias = true;
		
		/// <summary>Indica el periodo de cada subciclo.</summary>
		
		private int tSubciclo = 1000;		
		
		/// <summary>Indica si debe usarse la memoria por defecto.</summary>
		
		private bool usMemoriaDefecto = true;
		
		/// <summary>Indica la ruta de la memoria indicada por el usuario.
		/// </summary>
		
		private String memAlternativa = "";

		/// <summary>Crea una instancia de la clase.</summary>
		
		private Opciones ()
		{
		}
		
		/// <summary>Obtiene la única instancia de la clase (patrón
		/// Singleton)</summary>
		/// <returns>La única instancia de la clase.</returns>
		
		public static Opciones GetInstance ()
		{
			if (instancia == null)
			{
				instancia = Opciones.CargarDeFichero ();
			}
			return instancia;
		}
		
		/// <summary>Carga las opciones de un fichero. Si se produce algún
		/// error devuelve las opciones por defecto.</summary>
		/// <returns>Las opciones almacenadas.</returns>
		
		private static Opciones CargarDeFichero ()
		{
			Opciones op = null;
			Stream file = null;
			try
			{
				file = File.Open (fichero,  FileMode.Open);
			}
			catch (Exception)
			{
				return new Opciones();
			}
			try
			{

				IFormatter formatter = new BinaryFormatter ();
				op = (Opciones) formatter.Deserialize (file);
			}
			catch (Exception e)
			{
				return new Opciones();
			}
			file.Close ();

			return op;
		}
		
		/// <summary>Guarda las opciones a disco.</summary>
		
		public void Guardar()
		{
			lock (this)
			{
				Stream file = null;
				if (this.GetMemoriaAlternativa() == null)
				{
					this.SetMemoriaAlternativa ("");
				}
				
				try
				{
					file = File.Open (fichero, FileMode.Create);
				}
				catch (Exception)
				{
				}
				try
				{
					IFormatter formatter = new BinaryFormatter ();
					formatter.Serialize (file, this);
				}
				catch (Exception e)
				{
				}
				file.Close ();			
			}
		}
		
		/// <summary>Indica si se deben mostrar las advertencias del
		/// ensamblador.</summary>
		/// <returns><c>true</c> si se deben mostrar, <c>false</c> en otro
		/// caso.</returns>
		
		public bool GetMostrarAdvertencias ()
		{
			return this.mostrarAdvertencias;
		}
		
		/// <summary>Establece si se deben mostrar las advertencias del
		/// ensamblador.</summary>
		/// <param name="valor">Indica si se deben mostrar o no.</param>
		
		public void SetMostrarAdvertencias (bool valor)
		{
			this.mostrarAdvertencias = valor;
		}
		
		/// <summary>Obtiene el periodo de cada subciclo.</summary>
		/// <returns>El periodo de un subciclo.</returns>
		
		public int GetTSubciclo ()
		{
			return this.tSubciclo;
		}
		
		/// <summary>Establece el periodo de un subciclo.</summary>
		/// <param name="valor">El nuevo valor del periodo.</param>
		
		public void SetTSubciclo (int valor)
		{
			this.tSubciclo = valor;
		}
		
		/// <summary>Indica si se debe usar la memoria por defecto.</summary>
		/// <returns><c>true</c> si se debe usar la memoria por defecto, 
		/// <c>false</c> si debe usarse la memoria indicada por el usuario.
		/// </returns>
		
		public bool GetUsarMemoriaDefecto ()
		{
			return usMemoriaDefecto;
		}
		
		/// <summary>Establece si se debe usar la memoria por defecto.
		/// </summary>
		/// <param name="valor"><c>true</c> para indicar que se debe usar
		/// la memoria por defecto, <c>false</c> para indicar que se debe
		/// usar la memoria indicada por el usuario.</param>
		
		public void SetUsarMemoriaDefecto (bool valor)
		{
			this.usMemoriaDefecto = valor;
		}
		
		/// <summary>Obtiene la ruta de la memoria indicada por el usuario.
		/// </summary>
		/// <returns>La ruta de la memoria del usuario.</returns>
		
		public String GetMemoriaAlternativa ()
		{
			if (this.memAlternativa != null)
			{
				return this.memAlternativa;
			}
			else
			{
				return "";
			}
		}
		
		/// <summary>Establece la ruta de la memoria del usuario.</summary>
		/// <param name="valor">La ruta de la memoria del usuario.</param>
		
		public void SetMemoriaAlternativa (String valor)
		{
			if (valor != null)
			{
				this.memAlternativa = valor;
			}
			else
			{
				this.memAlternativa = "";
			}
		}		
	}
}
