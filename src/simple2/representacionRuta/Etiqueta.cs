namespace simple2.representacionRuta
{
	using System;

	/// <remarks>Esta clase representa una etiqueta de texto.</remarks>

	public class Etiqueta : ElementoDibujable
	{
	
		/// <summary>Indica si el texto está centrado a las coordenadas.
		/// </summary>
		
		private bool centrado;
		
		/// <summary>Color del texto.</summary>
		
		private EnuColor color;
		
		/// <summary>Posición x de la esquina superior izquierda de la caja.
		/// </summary>
		
		private int x;
		
		/// <summary>Posición y de la esquina superior izquierda de la caja.
		/// </summary>
		
		private int y;
		
		/// <summary>Texto que se muestra dentro de la caja.</summary>
		
		private string texto;
		
		
		/// <summary>Crea una instancia de la clase. Toma el color
		/// por defecto y el texto centrado en las coordenadas.</summary>
		/// <param name="dib">La superfice sobre la que se dibujará
		/// la etiqueta.</param>
		/// <param name="x">La coordenada x de la etiqueta.</param>
		/// <param name="y">La coordenada y de la etiqueta.</param>
		/// <param name="texto">El texto de la etiqueta.</param>
		
		public Etiqueta (InterfaceDibujo dib, int x, int y, String texto):
			this(dib, x, y, texto, COLOR_INACTIVO, true)
		{}
		
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="dib">La superfice sobre la que se dibujará
		/// la etiqueta.</param>
		/// <param name="x">La coordenada x de la etiqueta.</param>
		/// <param name="y">La coordenada y de la etiqueta.</param>
		/// <param name="texto">El texto de la etiqueta.</param>
		/// <param name="color">El color en el que se escribirá el texto.
		/// </param>
		/// <param name="centrado>Indica si el texto se mostrará centrado.
		/// </param>
		
		public Etiqueta 
			(InterfaceDibujo dib, int x, int y, String texto,
			EnuColor color, bool centrado) : base (dib)
		{
			this.x = x;
			this.y = y;
			this.color = color;
			this.centrado = centrado;
			this.texto = texto;
		}
	
		/// <summary>Método para dibujar el objeto en su estado
		/// inactivo.</summary>
		
		protected override void PintarInactivo()
		{
			dibujo.DibujarTexto (this.color, x, y, this.texto, centrado);
		}
		
		/// <summary>Método para dibujar el objeto en su estado
		/// activo.</summary>
		
		protected override void PintarActivo ()
		{
			dibujo.DibujarTexto (this.color, x,y,this.texto, centrado);
		}
				
		/// <summary>Obtiene el texto del objeto.</summary>
		/// <returns>El texto del objeto.</returns>
		
		public override String GetText ()
		{
			return texto;
		}
		
		/// <summary>Establece el texto del objeto.</summary>
		/// <param name="texto">El nuevo texto del objeto.</param>
		
		public override void SetText (String texto)
		{
			this.texto = texto;
		}		
	}
}
