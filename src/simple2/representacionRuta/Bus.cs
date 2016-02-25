namespace simple2.representacionRuta
{

	/// <remarks>Esta clase representa un bus de cables. Sólo se
	/// permite que un cable esté activo en un momento dado.</remarks>
	
	public class Bus
	{
		/// <summary>Array con los cables que componen el bus.</summary>
		
		private Cable[] cables = null;
		
		/// <summary>Cable activo en el bus (un número negativo es ninguno).
		/// </summary>
		
		private int cableActivo = -1;
		
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="cables">Los cables que componen el bus.</param>
		
		public Bus (Cable[] cables)
		{
			this.cables = cables;
		}
		
		/// <summary>Enciende un cable del bus, apagando todos los 
		/// demás.</summary>
		/// <param name="linea">El número de cable a encender.</param>
		
		public void Encender (int linea)
		{
			if ((linea >= cables.Length) || (linea < 0))
				return;			
			Apagar();
			cables[linea].Encender();
			cableActivo = linea;
		}
		
		/// <summary>Redibuja todos los cables.</summary>
		
		public void Repintar ()
		{
			for (int i=0; i < cables.Length; i++)
				cables[i].Repintar();
				
			if (cableActivo > -1)
				cables[cableActivo].Repintar();
		}
		
		/// <summary>Pone todos los cables a su estado inactivo.</summary>
		
		public void Apagar()
		{
			for (int i=0; i < cables.Length; i++)
				cables[i].Apagar();
			cableActivo = -1;
		}
	}
}
