namespace simple2
{

	using Gtk;
	using System;
	using simple2.interfaz.gtk;

	/// <remarks>Clase que contiene el m�todo principal de la aplicaci�n.
	/// </remarks>
	
	public class Simple2
	{
	
		/// <summary>Constructor privado. No se permite crear instancias
		/// de esta clase.</summary>
		
		private Simple2 ()
		{
		}
		
		/// <summary>Funci�n principal de la aplicaci�n.</summary>
		/// <param name="args">Los par�metros pasados desde la l�nea de
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
