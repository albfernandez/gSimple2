namespace simple2.representacionRuta
{
	using System;


	/// <remarks>Interface que deben implementar las superficies
	/// para dibujar la ruta de datos.</remarks>

	public interface InterfaceDibujo
	{
		/// <summary>Redibuja la superficie de dibujo.</summary>

		void Refresh ();

		/// <summary>Limpia la superficie de dibujo.</summary>

		void Clean ();

		/// <summary>Limpia un cuadrado indicado en la superficie de dibujo
		/// </summary>
		/// <param name="x">La coordenada x de la esquina superior izquierda
		/// del cuadrado a limpiar.</param>
		/// <param name="y">La coordenada y de la esquina superior izquierda
		/// del cuadrado a limpiar.</param>
		/// <param name="ancho">El ancho del cuadrado a limpiar.</param>
		/// <param name="alto">El alto del cuadrado a limpiar.</param>

		void Clean (int x, int y, int ancho, int alto);

		/// <summary>Dibuja una recta en la superficie de dibujo.</summary>
		/// <param name="c">El color de la recta.</param>
		/// <param name="x1">La coordenada x del punto inicial de la recta.
		/// </param>
		/// <param name="y1">La coordenada y del punto inicial de la recta.
		/// </param>
		/// <param name="x2">La coordenada x del punto final de la recta.
		/// </param>
		/// <param name="y2">La coordenada y del punto final de la recta.
		/// </param>

		void DibujarRecta (EnuColor c, int x1, int y1, int x2, int y2);

		/// <summary>Dibuja un texto en la superficie de dibujo.</summary>
		/// <param name="c">El color del texto.</param>
		/// <param name="x">La posici�n x del texto.</param>
		/// <param name="y">La posici�n y del texto.</param>
		/// <param name="texto">El texto a dibujar en la 
		/// superficie de dibujo.</param>
		/// <param name="cent">Indica si el texto est� centrado (x e y se
		/// refieren al centro del texto) o no (x e y se refieren a la 
		/// esquina superior del texto)</param>

		void DibujarTexto 
			(EnuColor c, int x, int y, string texto, bool cent);

		/// <summary>Dibuja los bordes de un rect�ngulo en la superficie
		/// de dibujo.</summary>
		/// <param name="c">El color de las l�neas del rect�ngulo.</param>
		/// <param name="x1">La posici�n x de la esquina superior
		/// izquierda del rect�ngulo.</param>
		/// <param name="y1">La posici�n y de la esquina superior
		/// izquierda del rect�ngulo.</param>
		/// <param name="ancho">El ancho del rect�ngulo.</param>
		/// <param name="alto">El alto del rect�ngulo.</param>

		void DibujarRectangulo
			(EnuColor c, int x1, int y1, int ancho,int alto);
		
		/// <summary>Establece que elemento <c>RepresentacionRDD</c> se 
		/// encargar� de mostrar la evoluci�n de la simulaci�n.</summary>
		/// <param name="r">El elemento de representaci�n.</param>
		
		void SetRepresentacionRDD (RepresentacionRDD r);
	}
}
