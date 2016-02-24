namespace simple2.interfaz.gtk
{
	using System;
	using Gtk;
	using System.Diagnostics;
	
	
	/// <summary>Esta clase se encarga de mostrar la ayuda de la aplicación.
	/// </summary>
	
	public class VentanaAyuda
	{
		/// <summary>Única instancia de la clase (patrón singleton).</summary>
		
		private static VentanaAyuda instancia = null;
		
		/// <summary>Constructor privado (patrón singleton).</summary>
		
		private VentanaAyuda()
		{
		}
		
		/// <summary>Obtiene la única instancia de la clase.
		/// (patrón singleton).</summary>
		
		public static VentanaAyuda GetInstance()
		{
			if (instancia == null)
			{
				instancia = new VentanaAyuda();
			}
			return instancia;
		}
		
		/// <summary>Muestra la ayuda.</summary>
		
		public void ShowAll ()
		{			
			bool error = false;
			String mensaje = "";
			try{
				Process p = Process.Start (GetBrowser(), "\""+GetParam()+"\"");
			}
			catch (AyudaException ae)
			{
				MessageDialog m = new MessageDialog (
						Ventana.GetInstance(),
						Gtk.DialogFlags.Modal,
						Gtk.MessageType.Error,
						Gtk.ButtonsType.Close,
						String.Format (
							Ventana.GetText ("VAyuda_Error"), 
							ae.Message)
				);
				int respuesta = m.Run ();
				m.Hide ();
			}
			catch (Exception e)
			{
			}			
		}
		
		/// <summary>Obtiene el navegador a utilizar.</summary>
		/// <returns>El navegador a utilizar.</returns>
		
		public static String GetBrowser ()
		{
			if (Environment.OSVersion.ToString().StartsWith("Microsoft"))
			{
				String[] browsers = new String[]{
				"C:\\Archivos de Programa\\Opera7\\opera.exe",
				"C:\\Archivos de Programa\\mozilla.org\\Mozilla\\mozilla.exe"
				};
				for (int i=0; (i < browsers.Length); i++)
				{
					if (System.IO.File.Exists(browsers[i]))
						return browsers[i];
				}
				return "explorer.exe";
			}
			else
			{
				String[] browsers = new String[]{ 
					"/usr/bin/opera",
					"/usr/bin/mozilla", 
					"/usr/bin/galeon", 
					"/usr/bin/konqueror"
				};
				for (int i=0; (i < browsers.Length) ; i++)
				{
					if (System.IO.File.Exists (browsers[i]))
						return browsers[i];
				}
				throw new AyudaException (Ventana.GetText("VAyuda_NoBrowser"));
			}
			
		}
		
		/// <summary>Obtiene la ruta del archivo de ayuda.</summary>
		/// <returns>La ruta del archivo de ayuda.</returns>
		
		public static String GetParam()
		{
			String retVal = "";
			String retVal1 = "";

			if (Environment.OSVersion.ToString().StartsWith("Microsoft"))
			{
				retVal1 =  "\\ayuda\\index.html";
			}
			else 
				retVal1 =  "/ayuda/index.html";
			
			retVal = System.Environment.CurrentDirectory + retVal1;
			if (!System.IO.File.Exists(retVal))
			{
				throw new AyudaException (
					String.Format (
						Ventana.GetText("VAyuda_NoDoc"),
						retVal1)
					);
			}
			return retVal;
		}		
	}
	
	/// <summary>Excepción cuando se produce un error al llamar a la ayuda.
	/// </summary>
	
	public class AyudaException : System.Exception
	{
	
		/// <summary>Constructor de la clase.</summary>
		/// <param name="mensaje">El mensaje de la excepción.</param>
		
		public AyudaException (String mensaje):base (mensaje) {}
	}
}
