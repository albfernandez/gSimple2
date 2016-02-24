namespace simple2.rutaDeDatos
{
	using System;
	
	/// <remarks>Excepci�n que se lanza cuando termina la simulaci�n.
	/// </remarks>
	
	public class SimulacionFinalizadaException : Exception
	{
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="mensaje">El mensaje de la excepci�n</param>
		
		public SimulacionFinalizadaException(String mensaje)
			:base (mensaje)
		{
		}
	}
}
