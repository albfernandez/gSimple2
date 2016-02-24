namespace simple2.rutaDeDatos
{
	using System;
	using System.Collections;

	/// <remarks>Esta clase representa la memoria principal del ordenador
	/// Simple2.</remarks>

	public class MemoriaPrincipal
	{

		/// <summary>Número de palabras de 16 bits que almacena la memoria.
		/// </summary>
		
		public const int TAMANO = 2048;
		
		/// <summary>Número de bits utilizados para las direcciones.
		/// </summary>
		
		public const int BITSDIRECCION = 11;

		/// <summary>Lista con los listeners que deben ser notificados
		/// cuando cambia alguno de los datos almacenados en la memoria.
		/// </summary>

		private ArrayList listeners = new ArrayList ();

		/// <summary>Array donde se guardan los datos que hay en la memoria.
		/// </summary>
		
		private short[] memoria = null;
		
		/// <summary>Máscara de bits a aplicar para direccionar únicamente
		/// <c>TAMANO</c> direcciones.</summary>
		
		private short mask = 0;
		
		/// <summary>Crea una instancia de la clase con todos los datos
		/// inicializado a 0. </summary>
		
		public MemoriaPrincipal ()
		{
			memoria = new short[TAMANO];
			mask = (0xFFFF >> (16 - BITSDIRECCION));
		}
		
		/// <summary>Crea una instancia de la clase con los datos iniciales
		/// que se le indican como parámetros.</summary>
		/// <param name="codigoInicial">Los datos iniciales de la memoria, 
		/// se ponen en la memoria a partir de la posición 0.
		/// </param>
		
		public MemoriaPrincipal (short[] codigoInicial)
		{
			memoria = new short[TAMANO];
			mask = (0xFFFF >> (16 - BITSDIRECCION));
			
			for (int i = 0; 
				(i < memoria.Length) && (i < codigoInicial.Length); 
				i++)
			{
				memoria[i] = codigoInicial[i];
			}
		}
		
		/// <summary>Escribe un dato en la memoria.</summary>
		/// <param name="direccion">La dirección en la que se 
		/// escribirá el dato.</param>
		/// <param name="dato">El dato a escribir.</param>
		
		public void EscribirDato (short direccion, short dato)
		{
			int dir = (direccion & mask);
			memoria[dir] = dato;

			ActualizarListeners (dir, dato);
		}
		
		/// <summary>Lee un dato de la memoria.</summary>
		/// <param name="direccion">La dirección en la que leeremos el dato.
		/// </param>
		/// <returns>El dato solicitado</returns>
		
		public short LeerDato (short direccion)
		{
			int dir = (direccion & mask);
			return memoria[dir];
		}

		/// <summary>Añade un listener para los cambios en la memoria.
		/// </summary>
		/// <param name="l">El listener.</param>
		
		public void AddMemoryChangeListener (MemoryChangeListener l)
		{
			listeners.Add (l);
			l.MemoryChanged (memoria);
		}
		
		/// <summary>Notifica a los listeners que se ha producido una 
		/// una modificación en la memoria.</summary>
		/// <param name="dir">La dirección de la memoria modificada.</param>
		/// <param name="newValue">El nuevo valor almacenado en
		/// <c>dir</c></param>
		
		private void ActualizarListeners (int dir, short newValue)
		{
			foreach (MemoryChangeListener list in listeners)
			{
				list.MemoryChanged(dir,newValue);
			}
		}
	}
}
