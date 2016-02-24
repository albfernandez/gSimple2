namespace simple2.rutaDeDatos
{

	using System;
	using System.Collections;
	
	
	/// <remarks>Esta clase representa el conjunto de registros del
	/// ordenador SIMPLE2.</remarks>
	
	
	
	public class BancoRegistros
	{
		/// <summary>�ndice del registro PC (0).</summary>
		
		public const int PC = 0x00;
		
		/// <summary>�ndice del registro AC (1).</summary>
		
		public const int AC = 0x01;
		
		/// <summary>�ndice del registro SP (2).</summary>
		
		public const int SP = 0x02;
		
		/// <summary>�ndice del registro IR (3).</summary>
		
		public const int IR = 0x03;
		
		/// <summary>�ndice del registro OMASK (4).</summary>
		
		public const int OMASK = 0x04;
		
		/// <summary>�ndice del registro RCON1 (5).</summary>
		
		public const int RCON1 = 0x05;
		
		/// <summary>�ndice del registro RCON2 (6).</summary>
		
		public const int RCON2 = 0x06;
		
		/// <summary>�ndice del registro RCON3 (7).</summary>
		
		public const int RCON3 = 0x07;
		
		/// <summary>�ndice del registro AMASK (8).</summary>
		
		public const int AMASK = 0x08;
		
		/// <summary>�ndice del registro SMASK (9).</summary>
		
		public const int SMASK = 0x09;
		
		/// <summary>�ndice del registro A (10).</summary>
		
		public const int A = 0x0A;
		
		/// <summary>�ndice del registro B (11).</summary>
		
		public const int B = 0x0B;
		
		/// <summary>�ndice del registro C (12).</summary>
		
		public const int C = 0x0C;
		
		/// <summary>�ndice del registro D (13).</summary>
		
		public const int D = 0x0D;
		
		/// <summary>�ndice del registro E (14).</summary>
		
		public const int E = 0x0E;
		
		/// <summary>�ndice del registro F (15).</summary>
		
		public const int F = 0x0F;
		
		/// <summary>Lista con los nombres de los registros.</summary>
		
		private static String[] nombres = new String[] {
			"PC",
			"AC", 
			"SP", 
			"IR", 
			"OMASK", 
			"RCON1", 
			"RCON2", 
			"RCON3", 
			"AMASK", 
			"SMASK", 
			"A",
			"B",
			"C", 
			"D", 
			"E", 
			"F"};
			
		
		/// <summary>Lista de los listeners que ser�n notificados de los
		/// cambios de valores de los registros.</summary>
		
		private ArrayList listeners = new ArrayList();
		
		/// <summary>Array en el que se almacena internamente el contenido
		/// de los registros </summary>

		private short[] registros;
		
		/// <summary>Obtiene el nombre de un registro dado su �ndice.
		/// </summary>
		/// <param name="reg">El �ndice del registro.</param>
		/// <returns>El nombre del registro, la cadena vacia si el �ndice
		/// indicado no es v�lido.</returns>

		public static String GetNombreRegistro(int reg)
		{
			if ((reg>=0) && (reg < nombres.Length))
				return nombres[reg];
			else
				return "";
		}
		
		/// <summary>Crea una instancia de la clase.</summary>
		
		public BancoRegistros ()
		{
			registros = new short[16];
			Reset ();

		}
		
		/// <summary>Inicializa el contenido de todos los registros.
		/// </summary>
		
		public void Reset ()
		{
			for (int i = 0; i < registros.Length; i++)
				registros[i] = 0;

			registros[4] = (short) 0x07FF;
			registros[5] = (short) 0x0000;
			registros[6] = (short) 0x0001;
			registros[7] = (short) -1;
			registros[8] = (short) 0x0FFF;
			registros[9] = (short) 0x00FF;
		}

		/// <summary>Escribe el valor de un registro.</summary>
		/// <param name="registro">El registro en el que se escribir�.</param>
		/// <param name="valor">El valor a escribir.</param>
		
		public void EscribirRegistro (int registro, short valor)
		{
			if ((registro > 15) || (registro < 0))
				return;
			if ((registro < 4) || (registro > 9))
			{
				registros[registro] = valor;
				ActualizarListeners(registro, valor);
			}
		}

		/// <summary>Obtiene el valor almacenado en un registro.</summary>
		/// <param name="registro">El registro a leer.</param>
		/// <returns>El dato almacenado en el registro.</returns>
		
		public short LeerRegistro (int registro)
		{
			if ((registro < 0) || (registro > 15))
				return 0;
			return registros[registro];
		}
		
		
		/// <summary>A�ade un listener para las modificaciones en los 
		/// registros.</summary>
		/// <param name="l">El listener.</param>
		
		public void AddRegisterChangeListener (RegisterChangeListener l)
		{
			l.RegisterChanged(registros);
			listeners.Add(l);
		}
		
		/// <summary>Notifica a los listeners de la modificaci�n del valor
		/// de un registro.</summary>
		/// <param name="dir">El registro modificado.</param>
		/// <param name="newValue">El nuevo valor del registro.</param>
		
		private void ActualizarListeners (int dir, short newValue)
		{
			foreach (RegisterChangeListener lis in listeners)
			{
				lis.RegisterChanged (dir, newValue);
			}
		}
	}
}
