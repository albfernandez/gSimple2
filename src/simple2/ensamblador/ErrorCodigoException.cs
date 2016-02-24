namespace simple2.ensamblador
{

	using System;
	
	/// <remarks>Excepción que se lanza cuando se produce un error
	/// al verificar un texto en ensamblador.</remarks>
	
	public class ErrorCodigoException:Exception
	{
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="mensaje">El mensaje de la excepción</param>
		
		public ErrorCodigoException (String mensaje):base (mensaje)
		{
		}
	}
}
