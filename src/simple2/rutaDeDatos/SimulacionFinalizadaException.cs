namespace simple2.rutaDeDatos
{
	using System;
	
	/// <remarks>Excepción que se lanza cuando termina la simulación.
	/// </remarks>
	
	public class SimulacionFinalizadaException : Exception
	{
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="mensaje">El mensaje de la excepción</param>
		
		public SimulacionFinalizadaException(String mensaje)
			:base (mensaje)
		{
		}
	}
}
