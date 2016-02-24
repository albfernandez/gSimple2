namespace simple2.hilos
{
	using System.Threading;
	
	
	/// <remarks>Clase utilzada para encapsular los hilos de C#
	/// al estilo de Java.</remarks>
	
	public class Hilo
	{
		/// <summary>Hilo de ejecuci�n.</summary>

		private Thread t = null;
		
		/// <summary>Crea una instancia de la clase.</summary>		
		
		public Hilo ()
		{
			// Crea un nuevo hilo de ejecuci�n con la funci�n asociada
			// a this.Run
			t = new Thread (new ThreadStart (this.Run));
		}
		
		/// <summary>Comienza la ejecuci�n del hilo.</summary>
		
		public void Start ()
		{
			t.Start ();
		}
		
		/// <summary>Detiene un hilo durante los milisegundos inidicados.
		/// </summary>
		/// <param name="millisecondsTimeout">El tiempo en milisegundos que
		/// dormir� el hilo que llame a este m�todo.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">Si el
		/// argumento es negativo y distinto de Infinite.</exception>
		
		public static void Sleep(int millisecondsTimeout)
		{
			Thread.Sleep(millisecondsTimeout);
		}	
		
		/// <summary>Nos indica si el hilo est� activo.</summary>
		/// <returns><c>true</c> si el hilo se ha iniciado y 
		/// no ha terminado, <c>false</c> en otro caso.</returns>
        
		public bool IsAlive ()
		{
			return t.IsAlive;
		}
		
		/// <summary>Bloquea al hilo que lo llama hasta que este hilo 
		/// termina.</summary>
		
		public void Join ()
		{
			t.Join ();
		}
		
		/// <summary>Bloquea al hilo que lo llama hasta que este hilo 
		/// termina o transcurre el tiempo indicado.</summary>
		/// <param name="millisecondsTimeout">El tiempo a esperar hasta que
		/// el hilo termine.</param>
		/// <returns><c>true</c>Si el hilo ha terminado, <c>false</c> en 
		/// caso de que el hilo no haya terminado transcurrido el tiempo
		/// indicado.</returns>
		
		public bool Join(int millisecondsTimeout)
		{
			return t.Join (millisecondsTimeout);
		}
		
		/// <summary>C�digo que se ejecutar� cuando arranque el hilo.
		/// Las subclases de esta deben sobreescribir este m�todo.
		/// </summary>
		
		public virtual void Run ()
		{
		}
		
		///<summary>Detiene la ejecuci�n del hilo.</summary>
		
		public virtual void Abort()
		{
			t.Abort();
		}
	}
}
