namespace simple2.interfaz.gtk
{
	using System;
	using Gtk;
	
	using simple2.rutaDeDatos;
	using simple2.utilidades;
	
	/// <remarks>Esta clase muestra el contenido de los registros
	/// en un panel.</remarks>
	
	public class PanelRegistros: Gtk.VBox, RegisterChangeListener
	{
		/// <summary>Array de etiquetas con los nombres de los registros.
		/// </summary>
		
		private Gtk.Label[] direcciones = new Gtk.Label[16];
		
		/// <summary>Array con las etiquetas que muestran el contenido de
		/// los registros.</summary>
		
		private Gtk.Label[] contenido = new Gtk.Label[16];
		
		/// <summary>Crea una instancia de la clase.</summary>
		
		public PanelRegistros():base (false, 0)
		{
			Gtk.Table tabla = new Gtk.Table (16, 5, true);
			VBox vb = new VBox(false, 0);
			
			for (int i=0; i < 16; i++)
			{
				direcciones[i] =
					new Gtk.Label(BancoRegistros.GetNombreRegistro(i));
				contenido[i] =
					new Gtk.Label (Conversiones.ToHexString ((short)  0));
				tabla.Attach (direcciones[i], 1, 2, (uint) i,(uint) i+1);
				tabla.Attach (contenido[i], 3, 4, (uint)i, (uint)i+1);			
			}
			vb.PackStart (new Gtk.Label 
				(Ventana.GetText("PReg_Contenido")));
			vb.PackStart (tabla);
			vb.PackStart (new Gtk.Label(""));
			this.PackStart (vb);

		}
		
		/// <summary>Se llama cuando cambia el contenido de un registro.
		/// </summary>
		/// <param name="registro">El registro que ha cambiado.</param>
		/// <param name="newValue">El nuevo valor almacenado en
		/// <c>registro</c>.</param>
		
		public void RegisterChanged (int registro, short newValue)
		{
			if (registro != 3)
			{
				contenido[registro].Text =
					Conversiones.ToHexString (newValue);
			}
			else
			{
				contenido[registro].Text = 
					Desensamblador.Desensamblar(newValue);				
			}
		}
		
		/// <summary>Se llama para inicializar el listener, pasándole un
		/// array con el contenido de todos los registros.</summary>
		/// <param name="newValues">Los valores almacenados en los registros.
		/// </param>
		
		public void RegisterChanged (short[] newValues)
		{
			for (int i = 0; (i < 16) && (i < newValues.Length); i++)
			{
				RegisterChanged (i, newValues[i]);
			}
		}
	}
}
