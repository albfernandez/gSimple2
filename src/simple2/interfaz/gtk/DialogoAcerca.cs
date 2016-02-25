namespace simple2.interfaz.gtk
{
	using Gtk;
	using Gdk;
	using System;
	using System.Reflection;

	/// <remarks>Esta clase es un diálogo en el que se muestra información
	/// acerca del programa y sus autores.</remarks>

	public class DialogoAcerca : Gtk.Dialog
	{
		/// <summary>Almacen la instancia de esta clase (patrón
		/// singleton).</summary>
		
		private static DialogoAcerca instancia = null;
		
		/// <summary>Obtiene la instancia de esta clase (patrón
		/// singleton).</summary>
		/// <returns>La instancia de esta clase.</returns>
		
		public static DialogoAcerca GetInstance()
		{
			if (instancia == null)
				instancia = new DialogoAcerca (Ventana.GetInstance());
			return instancia;
		
		}
		
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="parent">La ventana padre de este diálogo.
		/// </param>

		private DialogoAcerca (Gtk.Window parent):
			base ("", parent, Gtk.DialogFlags.DestroyWithParent)
		{
			Title = Ventana.GetText ("DialogoAcerca_Title");
			TransientFor = parent;
			Modal = true;
			Resizable = false;
			DeleteEvent += new DeleteEventHandler (Ventana.OcultarVentana);
			Resize (300, 300);
			BorderWidth = 20;
			HasSeparator = false;


			VBox vbox = new VBox (false, 5);

			vbox.PackStart (
				new Gtk.Label (Ventana.GetText ("Programa_Nombre") + " " +
					String.Format (
						Ventana.GetText("DialogoAcerca_Version"), 
						Assembly.GetExecutingAssembly().GetName().Version))
			);
			vbox.PackStart (
				new Gtk.Label (Ventana.GetText("Programa_descripcion")));
			vbox.PackStart (new Gtk.Label (" "));
			vbox.PackStart (new Gtk.Label  (
					Ventana.GetText ("DialogoAcerca_Autores")));
			vbox.PackStart (new Gtk.Label ("Montserrat Sotomayor González"));
			vbox.PackStart (new Gtk.Label ("Alberto Fernández Martínez"));
					
			vbox.PackStart (
				new Gtk.Image (IconManager.GetPixmap ("acerca.png")));

			Gtk.EventBox eventBox = new Gtk.EventBox ();

			eventBox.Add (vbox);
			eventBox.ButtonPressEvent +=
				new Gtk.ButtonPressEventHandler (CerrarClick);
			VBox.Add (eventBox);

		}

		/// <summary>Función que se ejecuta al pulsar sobre el diálogo, 
		/// ocultándolo.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>

		private void CerrarClick(object o, Gtk.ButtonPressEventArgs args)
		{
			Hide ();
		}

		/// <summary>Función que muestra el diálogo centrado en la ventana
		/// padre.</summary>

		public new void ShowAll ()
		{
			WindowPosition = WindowPosition.CenterOnParent;
			base.ShowAll ();
		}
	}

}
