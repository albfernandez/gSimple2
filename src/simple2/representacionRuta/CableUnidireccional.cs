namespace simple2.representacionRuta
{
	/// <remarks>Esta clase representa a un cable unidireccional, con
	/// punta de flecha en uno de sus extremos.</remarks>

	public class CableUnidireccional:Cable
	{
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="dib">La superficie sobre la que se dibujará
		/// este objeto.</param>
		/// <param name="puntos">Las coordenadas de las rectas que
		/// componen este cable.</param>
		
		public CableUnidireccional (InterfaceDibujo dib, int[]puntos):
			base (dib, puntos)
		{
		}
		
		/// <summary>Método para dibujar el objeto en su estado
		/// inactivo.</summary>
		
		protected override void PintarInactivo ()
		{
			base.PintarInactivo ();
			PintarFlechaFin (COLOR_INACTIVO);
		}
		
		/// <summary>Método para dibujar el objeto en su estado
		/// activo.</summary>
		
		protected override void PintarActivo ()
		{
			base.PintarActivo ();
			PintarFlechaFin (COLOR_ACTIVO);
		}
	}
}
