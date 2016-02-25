namespace simple2.rutaDeDatos
{

	using System;
	using System.Collections;
	
	
	/// <remarks>Esta clase representa el conjunto de registros del
	/// ordenador SIMPLE2.</remarks>
	
	
	
	public class BancoRegistros
	{
		/// <summary>Índice del registro PC (0).</summary>
		
		public const int PC = 0x00;
		
		/// <summary>Índice del registro AC (1).</summary>
		
		public const int AC = 0x01;
		
		/// <summary>Índice del registro SP (2).</summary>
		
		public const int SP = 0x02;
		
		/// <summary>Índice del registro IR (3).</summary>
		
		public const int IR = 0x03;
		
		/// <summary>Índice del registro OMASK (4).</summary>
		
		public const int OMASK = 0x04;
		
		/// <summary>Índice del registro RCON1 (5).</summary>
		
		public const int RCON1 = 0x05;
		
		/// <summary>Índice del registro RCON2 (6).</summary>
		
		public const int RCON2 = 0x06;
		
		/// <summary>Índice del registro RCON3 (7).</summary>
		
		public const int RCON3 = 0x07;
		
		/// <summary>Índice del registro AMASK (8).</summary>
		
		public const int AMASK = 0x08;
		
		/// <summary>Índice del registro SMASK (9).</summary>
		
		public const int SMASK = 0x09;
		
		/// <summary>Índice del registro A (10).</summary>
		
		public const int A = 0x0A;
		
		/// <summary>Índice del registro B (11).</summary>
		
		public const int B = 0x0B;
		
		/// <summary>Índice del registro C (12).</summary>
		
		public const int C = 0x0C;
		
		/// <summary>Índice del registro D (13).</summary>
		
		public const int D = 0x0D;
		
		/// <summary>Índice del registro E (14).</summary>
		
		public const int E = 0x0E;
		
		/// <summary>Índice del registro F (15).</summary>
		
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
			
		
		/// <summary>Lista de los listeners que serán notificados de los
		/// cambios de valores de los registros.</summary>
		
		private ArrayList listeners = new ArrayList();
		
		/// <summary>Array en el que se almacena internamente el contenido
		/// de los registros </summary>

		private short[] registros;
		
		/// <summary>Obtiene el nombre de un registro dado su índice.
		/// </summary>
		/// <param name="reg">El índice del registro.</param>
		/// <returns>El nombre del registro, la cadena vacia si el índice
		/// indicado no es válido.</returns>

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
		/// <param name="registro">El registro en el que se escribirá.</param>
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
		
		
		/// <summary>Añade un listener para las modificaciones en los 
		/// registros.</summary>
		/// <param name="l">El listener.</param>
		
		public void AddRegisterChangeListener (RegisterChangeListener l)
		{
			l.RegisterChanged(registros);
			listeners.Add(l);
		}
		
		/// <summary>Notifica a los listeners de la modificación del valor
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
