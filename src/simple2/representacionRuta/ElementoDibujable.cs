namespace simple2.representacionRuta
{
	using System;	
	
	
	/// <summary>Esta clase implementa las funciones por defecto para
	/// la interfaz IElementodibujable. Todos los elementos que se 
	/// pueden dibujar heredan de esta clase abstracta.</summary>
	
	public abstract class ElementoDibujable : IElementoDibujable
	{
	
		/// <summary>Superficie de dibujo sobre la que se dibujará este
		/// objeto.</summary>
		
		protected InterfaceDibujo dibujo = null;
		
		/// <summary>Color de los elementos activos.</summary>
		
		public const EnuColor COLOR_ACTIVO = EnuColor.Rojo;
		
		/// <summary>Color de los elementos inactivos.</summary>
		
		public const EnuColor COLOR_INACTIVO = EnuColor.Negro;

		/// <summary>Indica si el elemento está activo o no.</summary>

		protected bool activo = false;
				
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="d">La superficie de dibujo sobre la que se 
		/// dibujará este objeto.</param>
		
		protected ElementoDibujable(InterfaceDibujo d)
		{
			this.dibujo=d;
		}

		/// <summary>Establece el estado del objeto en inactivo.</summary>
		
		public void Apagar()
		{
			activo = false;
			PintarInactivo();
		}
		
		/// <summary>Establece el estado del objeto en activo.</summary>
		
		public void Encender ()
		{
			activo = true;
			PintarActivo();
		}
		
		/// <summary>Redibuja el objeto en su estado actual.</summary>
		
		public void Repintar()
		{
			if (activo==true)
				this.PintarActivo();
			else
				this.PintarInactivo();
			
		}
		
		/// <summary>Obtiene el texto del objeto. En esta implementación
		/// por defecto devuelve la cadena vacia.</summary>
		/// <returns>El texto del objeto.</returns>

		public virtual String GetText ()
		{
			return "";
		}
		
		/// <summary>Establece el texto del objeto. En esta implementación
		/// por defecto no hace nada.</summary>
		/// <param name="texto">El nuevo texto del objeto.</param>
		
		public virtual void SetText (String texto)
		{
			return;
		}
		
		/// <summary>Método para dibujar el objeto en su estado
		/// inactivo.</summary>
		
		protected abstract void PintarInactivo();
		
		/// <summary>Método para dibujar el objeto en su estado
		/// activo.</summary>
		
		protected abstract void PintarActivo ();
		
	}	
}
