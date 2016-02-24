namespace simple2.representacionRuta
{
	using System;

	/// <remarks>Interface que deben implementar todos los objetos
	/// que se puedan dibujar.</remarks>
	
	public interface IElementoDibujable
	{

		/// <summary>Establece el estado del objeto en inactivo.</summary>

		void Apagar ();

		/// <summary>Establece el estado del objeto en activo.</summary>

		void Encender ();

		/// <summary>Redibuja el objeto en su estado actual.</summary>

		void Repintar ();
		
		/// <summary>Obtiene el texto del objeto.</summary>
		/// <returns>El texto del objeto.</returns>
		
		String GetText ();
		
		/// <summary>Establece el texto del objeto.</summary>
		/// <param name="texto">El nuevo texto del objeto.</param>
		
		void SetText (String texto);		
		
	}
}
