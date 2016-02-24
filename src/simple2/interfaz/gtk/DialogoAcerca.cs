namespace simple2.interfaz.gtk
{
	using Gtk;
	using Gdk;
	using System;
	using System.Reflection;

	/// <remarks>Esta clase es un di�logo en el que se muestra informaci�n
	/// acerca del programa y sus autores.</remarks>

	public class DialogoAcerca : Gtk.Dialog
	{
		/// <summary>Almacen la instancia de esta clase (patr�n
		/// singleton).</summary>
		
		private static DialogoAcerca instancia = null;
		
		/// <summary>Obtiene la instancia de esta clase (patr�n
		/// singleton).</summary>
		/// <returns>La instancia de esta clase.</returns>
		
		public static DialogoAcerca GetInstance()
		{
			if (instancia == null)
				instancia = new DialogoAcerca (Ventana.GetInstance());
			return instancia;
		
		}
		
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="parent">La ventana padre de este di�logo.
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
			vbox.PackStart (new Gtk.Label ("Montserrat Sotomayor Gonz�lez " +
					"<msotomayorgonzalez@yahoo.es>"));
			vbox.PackStart (new Gtk.Label ("Alberto Fern�ndez Mart�nez " +
					"<infjaf@yahoo.es>"));
					
			vbox.PackStart (
				new Gtk.Image (IconManager.GetPixmap ("acerca.png")));

			Gtk.EventBox eventBox = new Gtk.EventBox ();

			eventBox.Add (vbox);
			eventBox.ButtonPressEvent +=
				new Gtk.ButtonPressEventHandler (CerrarClick);
			VBox.Add (eventBox);

		}

		/// <summary>Funci�n que se ejecuta al pulsar sobre el di�logo, 
		/// ocult�ndolo.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>

		private void CerrarClick(object o, Gtk.ButtonPressEventArgs args)
		{
			Hide ();
		}

		/// <summary>Funci�n que muestra el di�logo centrado en la ventana
		/// padre.</summary>

		public new void ShowAll ()
		{
			WindowPosition = WindowPosition.CenterOnParent;
			base.ShowAll ();
		}
	}

}
