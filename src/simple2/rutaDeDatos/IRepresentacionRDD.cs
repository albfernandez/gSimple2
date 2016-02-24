namespace simple2.rutaDeDatos
{
		
	/// <remarks>Interface que deben implementar las clases que representan
	/// la ruta de datos en pantalla.  La clase SecuenciadorMicroprograma
	/// ir� llamando a los m�todos correspondientes para que se actualizen los
	/// datos.</remarks>
	
	public interface IRepresentacionRDD
	{
		/// <summary>Vuelve a dibujar todos los elementos en la 
		/// superficie de dibujo.</summary>
		
		void ActualizarTodo();

		/// <summary>Activa los elementos activos durante el subciclo 1.
		/// </summary>
		/// <param name="mic">La microinstrucci�n que se
		/// acaba de cargar.</param>
		/// <param name="rdc>La direcci�n de la memoria de control en la que
		/// se encuentra la microinstrucci�n.</param>
		
		void DibujarCiclo1 (MicroInstruccion mic, short rdc);
		
		/// <summary>Activa los elementos activos durante el subciclo 2.
		/// </summary>
		/// <param name="mic">La microinstrucci�n actualmente en ejecuci�n.
		/// Nos indica los registros de origen.</param>
		/// <param name="regA">El contenido de BufferA.</param>
		/// <param name="regB">El contenido de BufferB.</param>
		
		void DibujarCiclo2 (MicroInstruccion mic, short regA, short regB);
		
		/// <summary>Activa los elementos activos durante el subciclo 3.
		/// </summary>
		/// <param name="mic">La microinstrucci�n en ejecuci�n.</param>
		/// <param name="vSH">El valor del registro SH.</param>
		/// <param name="vMAR">El valor del registro MAR.</param>
		/// <param name="vMBR">El valor del registro MBR.</param>
		/// <param name="valorC">El valor de la salida C de la ALU.</param>
		/// <param name="valorN">El valor de la salida N de la ALU.</param>
		/// <param name="valorZ">El valor de la salida Z de la ALU.</param>
		
		void DibujarCiclo3
			(MicroInstruccion mic, short vSH, short vMAR,
			short vMBR, int valorC, int valorN, int valorZ );
		
		/// <summary>Activa los elementos activos durante el subciclo 4.
		/// </summary>
		/// <param name="mic">La microinstrucci�n en ejecuci�n.</param>
		/// <param name="vMBR">El valor del registro MBR.</param>S
		
		void DibujarCiclo4(MicroInstruccion mic, short vMBR);
		
		/// <summary>Apaga todos los elementos.</summary>
		
		void Clean();
		
		/// <summary>Apaga todos los elementos.</summary>
		
		void Detener ();
		
	}
}
