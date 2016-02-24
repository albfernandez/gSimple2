namespace simple2.representacionRuta
{
	
	/// <remarks>Esta clase representa un cable de datos.</remarks>
	
	public class Cable : ElementoDibujable
	{

		/// <summary>Lista de las coordenadas de las rectas que componen
		/// el cable.</summary>
		
		protected int[] puntos = null;
		
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="dib">La superficie de dibujo sobre la que
		/// se dibujará este objeto.</param>
		/// <param name="puntos">Las coordenadas de las rectas
		/// que componen el cable.</param>
		
		public Cable (InterfaceDibujo dib, int[] puntos) : base (dib)
		{
			this.puntos = puntos;
		}
				
		/// <summary>Pinta una punta de flecha al final del cable.</summary>
		/// <param name="color">El color en el que se pintará la flecha.
		/// </param>

		protected void PintarFlechaFin (EnuColor color)
		{
			int x = puntos.Length;
			
			PintarFlecha ( color, new int[] 
				{ puntos[x-4], puntos[x-3], puntos[x-2], puntos[x-1]});
		}
		
		/// <summary>Dibuja una punta de flecha al principio del cable.
		/// </summary>
		/// <param name="color">El color en el que se pintará la flecha.
		/// </param>

		protected void PintarFlechaInicio (EnuColor color)
		{
			PintarFlecha (color, new int[] 
				{ puntos[2], puntos[3], puntos[0], puntos[1]});
		}
		
		/// <summary> Pinta una punta de flecha al final de la 
		/// línea indicada.(Sólo lineas horizontales y verticales).
		/// </summary>
		/// <param name="color">El color en el que se pintará la flecha.
		/// </param>
		/// <param name="linea">Las coordenadas de la linea al fin de la
		/// cual se pondrá la flecha.</param>
		
		private void PintarFlecha (EnuColor color, int[] linea)
		{
			if (linea.Length != 4)
				return;			
			
			int x1 = 0;
			int x2 = 0;
			int y1 = 0;
			int y2 = 0;
			
			if (linea[0] == linea[2]) // es una línea vertical
			{
				x1 = linea[0] -5;
				x2 = linea[0] +5;
				
				if (linea[1] < linea[3]) //La linea va hacia abajo
					y1 = linea[3] - 5;
				else
					y1 = linea[3] + 5;
				y2 = y1;
			}
			else if (linea[1]==linea[3]) // es una linea horizontal
			{
				y1 = linea[1] -5;
				y2 = linea[1] +5;
				
				if (linea[0] < linea[2]) // La linea va hacia la derecha
					x1 = linea[2] - 5;
				else
					x1 = linea[2] + 5;
				x2 = x1;
			}
			else
			{
				return;
			}			
			
			dibujo.DibujarRecta (color,linea[2], linea[3], x1, y1);
			dibujo.DibujarRecta (color,linea[2], linea[3], x2, y2);
				
		}
		
		
		/// <summary>Pinta la linea que compone el cable.</summary>
		/// <param name="color">El color en el que se pintará el cable.
		/// </param>
		
		private void PintarCable (EnuColor color)
		{
			int x = 0;
			while (x < (puntos.Length -2) )
			{
				int x1 = puntos[x];
				int y1 = puntos[x+1];
				int x2 = puntos[x+2];
				int y2 = puntos[x+3];
				
				x+=2;
				dibujo.DibujarRecta (color, x1, y1, x2, y2);
			}
		}
		
		/// <summary>Método para dibujar el objeto en su estado
		/// inactivo.</summary>
		
		protected override void PintarInactivo ()
		{
			this.PintarCable (COLOR_INACTIVO);
		}
		
		/// <summary>Método para dibujar el objeto en su estado
		/// activo.</summary>
		
		protected override void PintarActivo ()
		{
			this.PintarCable (COLOR_ACTIVO);
		}
	}
}
