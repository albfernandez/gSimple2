namespace simple2.rutaDeDatos
{
	/// <remarks>Interface que deben implementar las clases
	/// que respondan a los eventos de cambios en los contenidos
	/// de los registros.</remarks>
	
	public interface RegisterChangeListener
	{
		/// <summary>Se llama cuando cambia el contenido de un registro.
		/// </summary>
		/// <param name="registro">El registro que ha cambiado.</param>
		/// <param name="newValue">El nuevo valor almacenado en
		/// <c>registro</c>.</param>
		
		void RegisterChanged (int registro, short newValue);
		
		/// <summary>Se llama para inicializar el listener, pasándole un
		/// array con el contenido de todos los registros.</summary>
		/// <param name="newValues">Los valores almacenados 
		/// en los registros.</param>
		
		void RegisterChanged (short[] newValues);
	}
}
