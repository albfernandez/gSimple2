namespace simple2
{

	using Gtk;
	using System;
	using simple2.interfaz.gtk;

	/// <remarks>Clase que contiene el método principal de la aplicación.
	/// </remarks>
	
	public class Simple2
	{
	
		/// <summary>Constructor privado. No se permite crear instancias
		/// de esta clase.</summary>
		
		private Simple2 ()
		{
		}
		
		/// <summary>Función principal de la aplicación.</summary>
		/// <param name="args">Los parámetros pasados desde la línea de
		/// comandos.</param>
		
		public static void Main (String[]args)
		{
			Application.Init ();
			Ventana v = Ventana.GetInstance();		
			v.SetSizeRequest (700, 600);
			v.ShowAll ();
			Application.Run ();
		}
	}	
}
