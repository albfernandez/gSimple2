namespace simple2.rutaDeDatos
{
	/// <remarks>Interface que deben implementar las clases
	/// que respondan a los eventos de cambios en los contenidos
	/// de la memoria.</remarks>
	
	public interface MemoryChangeListener
	{
		
		/// <summary>Se llama cuando cambia una posici�n de la memoria.
		/// </summary>
		/// <param name="dir">La direcci�n que ha cambiado.</param>
		/// <param name="newValue">El nuevo valor de la posici�n de 
		/// memoria <c>dir</c>.</param>
		
		void MemoryChanged (int dir, short newValue);
		
		/// <summary>Se llama para inicializar el listener, pas�ndole un
		/// array con el contenido de toda la memoria.</summary>
		/// <param name="newMemoryValues">Los valores almacenados en 
		/// la memoria.</param>
		
		void MemoryChanged (short[]newMemoryValues);
		
	}
}
