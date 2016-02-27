 namespace simple2.interfaz.gtk
{
	using simple2.rutaDeDatos;
	using simple2.hilos;
	
	/// <summary>Este hilo se encarga de llamar a la simulación de la ruta
	/// de datos paso a paso.</summary>

	public class HiloEjecucion : Hilo
	{
		/// <summary>Simulador de la ruta de datos.</summary>
		
		private SecuenciadorMicroprograma mic = null;
		
		/// <summary>Indica si se debe terminar la simulación.</summary>
		
		private bool terminar = false;
		
		/// <summary>Indica si la simulación está pausada.</summary>
		
		private bool pausado = false;
		
		/// <summary>Indica el tiempo que esperará entre subciclos.</summary>
		
		private int tSubciclo = 1000;
		
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="mic">El simulador de la ruta de datos a
		/// utilizar.</param>
		
		public HiloEjecucion (SecuenciadorMicroprograma mic):base ()
		{
			this.mic = mic;
		}
		
		/// <summary>Método de ejecución del hilo.</summary>
		
		public override void Run ()
		{
			int acumulado = 0;
			int paso = 50;
			Hilo.Sleep (paso);
			try
			{
				EjecutarSubcicloGtk();
				while (!terminar)
				{
				
					Hilo.Sleep (paso);
					if (!pausado)
					{
						acumulado += paso;
						if (acumulado >= tSubciclo)
						{
							acumulado=0;							
    						EjecutarSubcicloGtk();
						}
					}
				}
				DetenerGtk();
			}
			catch (System.Threading.ThreadAbortException)
			{
				DetenerGtk();
				return;
			}
		}
		private void DetenerGtk() 
		{
			Gtk.Application.Invoke(delegate {
				try 
				{
					mic.Detener();
				}
				catch (System.Exception)
				{
				}
			 });
		}

		private void EjecutarSubcicloGtk() 
		{
			
			Gtk.Application.Invoke(delegate {
				try {
					mic.EjecutarSubciclo();
				}
				catch (SimulacionFinalizadaException) {
					terminar = true;
				}
			});
			
		}
		
		/// <summary>Detiene la ejecución del hilo.</summary>
		
		public override void Abort ()
		{
			if( System.Environment.OSVersion.
				ToString().StartsWith("Microsoft"))
			{
				terminar = true;
			}
			else
			{
				terminar = true;
				base.Abort();
			}
		}
		
		/// <summary>Nos indica si el hilo está pausado.</summary>
		/// <returns><c>true</c> si el hilo está pausado, <c>false</c>
		/// en otro caso.</returns>
		
		public bool GetPausado()
		{
			return this.pausado;
		}
		
		/// <summary>Detiene y reanuda la ejecución del hilo.</summary>
		
		public void CambiarPausado ()
		{
			pausado = !pausado;
		}
		
		/// <summary>Establece el tiempo que durará cada subciclo.</summary>
		/// <param name="valor">El tiempo en ms que durará cada 
		/// subciclo.</param>
		
		public void SetTSubciclo (int valor)
		{
			this.tSubciclo = valor;
		}
	}
}
