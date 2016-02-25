namespace simple2.representacionRuta
{
	using System;


	/// <remarks>Esta clase representa un dibujo de un rectángulo con
	/// un texto centrado en su interior.</remarks>

	public class CajaRegistro: ElementoDibujable
	{
		/// <summary>Posición x de la esquina superior izquierda de la caja.
		/// </summary>
		
		private int x;
		
		/// <summary>Posición y de la esquina superior izquierda de la caja.
		/// </summary>
		
		private int y;
		
		/// <summary>Ancho de la caja.</summary>
		
		private int ancho;
		
		/// <summary>Alto de la caja.</summary>
		
		private int alto;
		
		/// <summary>Texto que se muestra dentro de la caja.</summary>
		
		private string texto;
		
		/// <summary>Crea una instancia de la clase. 
		/// El ancho y el alto son valores por defecto</summary>
		/// <param name="dib">La superficie sobre la que se debe dibujar
		///  este objeto.</param>
		/// <param name="x">Posición x de la esquina superior izquierda
		/// de la caja.</param>
		/// <param name="y">Posición y de la esquina superior izquierda 
		/// de la caja.</param>
		/// <param name="texto">Texto a mostrar dentro de la caja.</param>
		
		public CajaRegistro (InterfaceDibujo dib, int x, int y, string texto): 
			this(dib, x,y,60,20, texto)
		{}
		
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="dib">La superficie sobre la que se debe dibujar 
		/// este objeto.</param>
		/// <param name="x">Posición x de la esquina superior izquierda
		/// de la caja.</param>
		/// <param name="y">Posición y de la esquina superior izquierda 
		/// de la caja.</param>
		/// <param name="texto">Texto a mostrar dentro de la caja.</param>
		/// <param name="ancho">El ancho de la caja de texto.</param>
		/// <param name="alto">El alto de la caja de texto.</param>
		
		public CajaRegistro (InterfaceDibujo dib, 
					int x, 
					int y, 
					int ancho, 
					int alto, 
					string texto):base (dib)
		{
			this.x = x;
			this.y = y;
			this.ancho = ancho;
			this.alto = alto;
			this.texto = texto;
		}
	
		/// <summary>Método para dibujar el objeto en su estado
		/// inactivo.</summary>
		
		protected override void PintarInactivo()
		{
			dibujo.Clean (x,y,ancho,alto);
			dibujo.DibujarRectangulo(COLOR_INACTIVO, x,y,ancho,alto);
			dibujo.DibujarTexto (
				COLOR_INACTIVO, 
				x + (ancho / 2), 
				y + (alto/2), 
				texto, 
				true );
		}
		
		/// <summary>Método para dibujar el objeto en su estado
		/// activo.</summary>
		
		protected override void PintarActivo ()
		{
			dibujo.Clean (x,y,ancho,alto);
			dibujo.DibujarRectangulo (COLOR_ACTIVO, x,y,ancho,alto);			
			dibujo.DibujarTexto (
				COLOR_ACTIVO, 
				x + (ancho/2), 
				y + (alto/2), 
				texto, 
				true);
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
