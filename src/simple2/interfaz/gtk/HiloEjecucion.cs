 namespace simple2.interfaz.gtk
{
	using simple2.rutaDeDatos;
	using simple2.hilos;
	
	/// <summary>Este hilo se encarga de llamar a la simulaci�n de la ruta
	/// de datos paso a paso.</summary>

	public class HiloEjecucion : Hilo
	{
		/// <summary>Simulador de la ruta de datos.</summary>
		
		private SecuenciadorMicroprograma mic = null;
		
		/// <summary>Indica si se debe terminar la simulaci�n.</summary>
		
		private bool terminar = false;
		
		/// <summary>Indica si la simulaci�n est� pausada.</summary>
		
		private bool pausado = false;
		
		/// <summary>Indica el tiempo que esperar� entre subciclos.</summary>
		
		private int tSubciclo = 1000;
		
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="mic">El simulador de la ruta de datos a
		/// utilizar.</param>
		
		public HiloEjecucion (SecuenciadorMicroprograma mic):base ()
		{
			this.mic = mic;
		}
		
		/// <summary>M�todo de ejecuci�n del hilo.</summary>
		
		public override void Run ()
		{
			int acumulado = 0;
			int paso = 50;
			try
			{
				mic.EjecutarSubciclo();
				while (!terminar)
				{
				
					Hilo.Sleep (paso);
					if (!pausado)
					{
						acumulado += paso;
						if (acumulado >= tSubciclo)
						{
							acumulado=0;
							mic.EjecutarSubciclo();
						}
					}
				}
				mic.Detener();
			}
			catch (SimulacionFinalizadaException)
			{
				mic.Detener();
				return;
			}
			catch (System.Threading.ThreadAbortException)
			{
				mic.Detener();
				return;
			}
		}
		
		/// <summary>Detiene la ejecuci�n del hilo.</summary>
		
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
		
		/// <summary>Nos indica si el hilo est� pausado.</summary>
		/// <returns><c>true</c> si el hilo est� pausado, <c>false</c>
		/// en otro caso.</returns>
		
		public bool GetPausado()
		{
			return this.pausado;
		}
		
		/// <summary>Detiene y reanuda la ejecuci�n del hilo.</summary>
		
		public void CambiarPausado ()
		{
			pausado = !pausado;
		}
		
		/// <summary>Establece el tiempo que durar� cada subciclo.</summary>
		/// <param name="valor">El tiempo en ms que durar� cada 
		/// subciclo.</param>
		
		public void SetTSubciclo (int valor)
		{
			this.tSubciclo = valor;
		}
	}
}
