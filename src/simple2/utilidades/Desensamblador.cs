namespace simple2.utilidades
{
	using System;
	using System.Collections;
	
	
	/// <remarks>Clase que se encarga de desensamblar instrucciones
	/// del computador SIMPLE2.</remarks>
	
	public class Desensamblador
	{
	
		/// <summary>Tabla donde se guardan los nemónicos correspondientes a
		/// cada opcode.</summary>
		
		private static Hashtable hash = null;
		
		/// <summary>Constructor privado, no se permite crear instancias de
		/// esta clase.</summary>
		
		private Desensamblador(){}
		
		/// <summary>Obtiene la la cadena de la instrucción codificada.
		/// </summary>
		/// <param name="inst">La instrucción codificada a desensamblar.
		/// </param>
		/// <returns>La instrucción desensamblada.</returns>
		
		public static String Desensamblar (short inst)
		{
			if (hash == null)
				hash = CrearHash();
			
			int opcode = (inst >> 11) & 0x1F;
			
			String txt = (String) hash[opcode];
			
			switch (txt)
			{
				case null:
					return ";????";
				case "POP":
				case "RETN":
				case "PUSH":
				case "HALT":
					return txt;
				default:
					return (txt + " " + (inst & 0x07FF));
			}
		}
		
		/// <summary>Crea la tabla con los opcodes y nemónicos de 
		/// las instrucciones. </summary>
		
		private static Hashtable CrearHash()
		{
			Hashtable h = new Hashtable();
			
			h.Add (0x01, "LODD");
			h.Add (0x02, "LODI");
			h.Add (0x03, "STOD");
			h.Add (0x04, "ADDD");
			h.Add (0x05, "ADDI");
			h.Add (0x06, "SUBD");
			h.Add (0x07, "SUBI");
			h.Add (0x08, "PUSH");
			h.Add (0x09, "POP");
			h.Add (0x0A, "JNEG");
			h.Add (0x0B, "JZER");
			h.Add (0x0C, "JCAR");
			h.Add (0x0D, "JUMP");
			h.Add (0x0E, "CALL");
			h.Add (0x0F, "RETN");
			h.Add (0x1F, "HALT");			
			
			return h;			
		}
	}
}
