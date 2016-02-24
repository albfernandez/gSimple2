namespace simple2.rutaDeDatos
{

	/// <remarks>Esta clase se encarga de simular el funcionamiento de
	/// la ALU del ordenador SIMPLE 2.</remarks>
	
	public class ALU
	{
		/// <summary>Campo para almacenar internamente el resultado de 
		/// la ALU C (activo cuando hay acarreo)</summary>
		
		private int c;
		
		/// <summary>Campo para almacenar internamente el resultado de 
		/// la ALU N (activo cuando el resultado es negativo)</summary>
		
		private int n;

		/// <summary>Campo para almacenar internamente el resultado 
		/// de la ALU Z (activo cuando el resultado es cero)</summary>

		private int z;
		
		/// <summary>Campo para almacenar internamente el resultado 
		/// de la ALU.</summary>

		private short resultado;

		/// <summary>Crea una instancia de la clase.</summary>
		
		public ALU ()
		{
		}
		
		/// <summary>Comprueba si se produce un arrastre.</summary>
		/// <param name="op">La operación a realizar.</param>
		/// <param name="operandoA">El operando A.</param>
		/// <param name="operandoB">El operando B.</param>
		/// <returns><c>True</c> si se produce arrastre, <c>False</c> en
		/// otro caso.</returns>
		
		private static bool Arrastre 
			(int op, short operandoA, short operandoB)
		{
			if (op == 0)
			{
				return (((operandoA + operandoB) > 0x7FFF) ||
					((operandoA + operandoB) < -32767));
			}
			else
			{
				return false;
			}
		}
		
		/// <summary>Realiza una operación de la ALU. También
		/// actualiza las salidas N, Z, C.</summary>
		/// <param name="operacion">El entero con la operación
		/// a realizar.</param>
		/// <param name="operacionSH">El entero indicando el desplazamiento
		/// a realizar en SH.</param>
		/// <param name="operandoA">El operando A.</param>
		/// <param name="operandoB">El operando B.</param>
		/// <returns>El resultado tras pasar por SH de la operación.
		/// </returns>
		
		public short Operar (int operacion, int operacionSH,
				     short operandoA, short operandoB)
		{
			short retval = 0;
			
			c = (Arrastre(operacion, operandoA, operandoB)) ? 1 : 0;
			
			switch (operacion)
			{
			case 0:
				retval = (short) (operandoA + operandoB);
				break;
			case 1:
				retval = (short) (operandoA & operandoB);
				break;
			case 2:
				retval = operandoA;
				break;
			case 3:
				retval = (short) (~operandoA);
				break;
			case 4:
				retval = (short) (operandoA | operandoB);
				break;
			case 5:
				retval = (short) (operandoA ^ operandoB);
				break;
			}

			n = (retval < 0) ? 1 : 0;
			z = (retval == 0) ? 1 : 0;

			resultado = OperarSH (operacionSH, retval);
			return resultado;
		}
		
		/// <summary>Realiza el desplazamiento de SH</summary>
		/// <param name="op">El desplazamiento a realizar</param>
		/// <param name="valor">El valor a desplazar</param>
		/// <returns><c>valor</c> desplazado según <c>op</c>.</returns>
		
		private short OperarSH (int op, short valor)
		{
			switch (op)
			{
			case 1:
				valor = (short) (valor >> 1);
				break;
			case 2:
				valor = (short) (valor << 1);
				break;
			}
			return valor;
		}
		
		/// <summary>Obtiene el resultado de la última operación de la ALU.
		/// </summary>
		/// <returns>El resultado de la última operación de la ALU.
		/// </returns>
		
		public short LeerResultado ()
		{
			return resultado;
		}
		
		/// <summary>Obtiene el valor de C tras la última 
		/// operación de la ALU.</summary>
		/// <returns>El valor de C tras la última operación de la ALU.
		/// </returns>
		
		public int LeerC ()
		{
			return c;
		}
		
		/// <summary>Obtiene el valor de N tras la última op
		/// eración de la ALU.</summary>
		/// <returns>El valor de N tras la última operación de la ALU.
		/// </returns>
		
		public int LeerN ()
		{
			return n;
		}
		
		/// <summary>Obtiene el valor de Z tras la última 
		/// operación de la ALU.</summary>
		/// <returns>El valor de Z tras la última operación de la ALU.
		/// </returns>
		
		public int LeerZ ()
		{
			return z;
		}
	}
}
