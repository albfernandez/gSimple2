namespace simple2.interfaz.gtk
{

	using Gtk;
	using System;
	
	using simple2.rutaDeDatos;
	using simple2.utilidades;
	
	/// <remarks>Esta clase nos muestra el contenido de la memoria.
	/// </remarks>
	
	public class PanelMemoria : Gtk.VBox, MemoryChangeListener
	{
		/// <summary>Array que contiene las etiquetas que muestran la 
		/// dirección de memoria.</summary>
		
		private Gtk.Label[] direcciones =
			new Gtk.Label[MemoriaPrincipal.TAMANO];
			
		/// <summary>Array que contiene las etiquetas que muestran
		/// el contenido de la memoria.</summary>
		
		private Gtk.Label[] contenido =
			new Gtk.Label[MemoriaPrincipal.TAMANO];

		/// <summary>Crea una instancia de la clase.</summary>
		
		public PanelMemoria ():base (false, 0)
		{
		
			VBox vb = new VBox(false, 0);
			Gtk.Table tabla=new Gtk.Table (MemoriaPrincipal.TAMANO, 5, true);
			for (short i = 0; i < MemoriaPrincipal.TAMANO; i++)
			{
							
				direcciones[i] =
					new Gtk.Label (Conversiones.ToHexString (i));
				contenido[i] =
					new Gtk.Label (Conversiones.ToHexString ((short) 0));
				
				tabla.Attach (direcciones[i], 1, 2, (uint) i,(uint) i+1);
				tabla.Attach (contenido[i], 3, 4, (uint)i, (uint)i+1);
			}
			vb.PackStart(tabla);
			ScrolledWindow sw = new ScrolledWindow ();
			sw.AddWithViewport (vb);
			
			Gtk.Table t2 = new Gtk.Table (4, 1, true);
			t2.Attach (new Gtk.Label (Ventana.GetText ("PMem_Contenido")),
					0, 1, 0, 1);
			t2.Attach (sw, 0,1, 1, 3);
			t2.Attach (new Gtk.Label (""), 0, 1, 3, 4);
			
			this.PackStart (t2);			
		}
		
		/// <summary>Se llama cuando cambia una posición de la memoria.
		/// </summary>
		/// <param name="dir">La dirección que ha cambiado.</param>
		/// <param name="newValue">El nuevo valor de la posición de 
		/// memoria <c>dir</c>.</param>
		
		public void MemoryChanged (int dir, short newValue)
		{
			contenido[dir].Text =	Conversiones.ToHexString (newValue);
		}
		
		/// <summary>Se llama para inicializar el listener, pasándole un
		/// array con el contenido de toda la memoria.</summary>
		/// <param name="newMemoryValues">Los valores almacenados en 
		/// la memoria.</param>
		
		public void MemoryChanged (short[] newMemoryValues)
		{
			for (int i=0; i < newMemoryValues.Length; i++)
			{
				this.MemoryChanged (i, newMemoryValues[i]);
			}			
		}
	}
}
