namespace simple2.interfaz.gtk
{
	using Gdk;
	using System.Resources;
	using System;

	/// <remarks>Esta clase se encarga de cargar las im�genes guardadas
	/// como recursos dentro del asembly.  Las im�genes est�n guardadas
	/// como array de bytes y esta clase proporciona un m�todo para
	/// recuperarlas como Gdk.Pixbuf.</remarks>
	
	public class IconManager
	{
		/// <summary>ResourceManager encargado de cargar los recursos de
		/// im�genes.</summary>
		
		private static ResourceManager resPixmaps = null;
		
		/// <summary>Constructor privado: No se permite crear instancias
		/// de esta clase.</summary>		
		
		private IconManager()
		{}
		
		/// <summary>Obtiene un Gdk.Pixbuf a partir del nombre de la imagen.
		/// </summary>
		/// <param name="nombre">El nombre de la imagen a cargar.</param>
		/// <returns>La imagen, una por defecto si no existe una
		/// para <c>nombre</c>.</returns>
		
		public static Gdk.Pixbuf GetPixmap (String nombre)
		{
			if (resPixmaps == null)
			{
				resPixmaps = new ResourceManager 
						("Pixmaps", typeof (IconManager).Assembly);
			}
       		
			byte[]img = (byte[])resPixmaps.GetObject (nombre);
			if (img == null)
				img = (byte[])resPixmaps.GetObject ("default.png");
			PixbufLoader pixLoad = new PixbufLoader ();
			pixLoad.Write (img, (uint) img.Length);
			pixLoad.Close ();
			return (pixLoad.Pixbuf);
		}
	}

}
