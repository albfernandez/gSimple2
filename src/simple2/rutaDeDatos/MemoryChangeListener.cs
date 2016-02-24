namespace simple2.rutaDeDatos
{
	/// <remarks>Interface que deben implementar las clases
	/// que respondan a los eventos de cambios en los contenidos
	/// de la memoria.</remarks>
	
	public interface MemoryChangeListener
	{
		
		/// <summary>Se llama cuando cambia una posición de la memoria.
		/// </summary>
		/// <param name="dir">La dirección que ha cambiado.</param>
		/// <param name="newValue">El nuevo valor de la posición de 
		/// memoria <c>dir</c>.</param>
		
		void MemoryChanged (int dir, short newValue);
		
		/// <summary>Se llama para inicializar el listener, pasándole un
		/// array con el contenido de toda la memoria.</summary>
		/// <param name="newMemoryValues">Los valores almacenados en 
		/// la memoria.</param>
		
		void MemoryChanged (short[]newMemoryValues);
		
	}
}
