namespace simple2.interfaz.gtk
{
	using Gdk;
	using System.Resources;
	using System;
	using System.Reflection;

	/// <remarks>Esta clase se encarga de cargar las imágenes guardadas
	/// como recursos dentro del asembly.  Las imágenes están guardadas
	/// como array de bytes y esta clase proporciona un método para
	/// recuperarlas como Gdk.Pixbuf.</remarks>
	
	public class IconManager
	{
		/// <summary>ResourceManager encargado de cargar los recursos de
		/// imágenes.</summary>
		
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
			return new Gdk.Pixbuf (Assembly.GetExecutingAssembly(), nombre);
		}
	}

}
