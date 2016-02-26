namespace simple2.rutaDeDatos
{

	using System;
	using System.Collections;
	using System.Globalization;
	
	using simple2.utilidades;
	
	/// <remarks>Esta clase representa la memoria de control del ordenador
	/// Simple2.</remarks>
	
	public class MemoriaControl
	{
		/// <summary>Número de palabras de 48 bits que almacena la memoria.
		/// </summary>

		public const int TAMANO = 1024;

		/// <summary>Array donde se guardan los datos que hay en la memoria.
		/// </summary>

		private ulong[] memoria = null;

		/// <summary>Obtiene la memoria de control por defecto.</summary>
		/// <returns>La memoria de control por defecto.</returns>
		
		private static ulong[] GetMemoriaDefecto ()
		{
			ulong[]mem = new ulong[TAMANO];
			
			//Salto al microprograma de búsqueda y decodificación
			mem[0] = 0x400000FF4000L;	
			//;
			//; Tabla de saltos
			mem[0x001] = 0x40000008C000L; //Salto al microprograma 1 (LODD n)
			mem[0x002] = 0x4000000DC000L; //Salto al microprograma 2 (LODI n)
			mem[0x003] = 0x40000012C000L; //Salto al microprograma 3 (STOD n)
			mem[0x004] = 0x40000017C000L; //Salto al microprograma 4 (ADDD n)
			mem[0x005] = 0x400000200000L; //Salto al microprograma 5 (ADDI n)
			mem[0x006] = 0x400000240000L; //Salto al microprograma 6 (SUBD n)
			mem[0x007] = 0x400000280000L; //Salto al microprograma 7 (SUBI n)
			mem[0x008] = 0x4000002C0000L; //Salto al microprograma 8 (PUSH)
			mem[0x009] = 0x400000300000L; //Salto al microprograma 9 (POP)
			mem[0x00A] = 0x400000340000L; //Salto al microprograma 10 (JNEG n)
			mem[0x00B] = 0x400000380000L; //Salto al microprograma 11 (JZER n)
			mem[0x00C] = 0x4000003C0000L; //Salto al microprograma 12 (JCAR n)
			mem[0x00D] = 0x400000400000L; //Salto al microprograma 13 (JUMP n)
			mem[0x00E] = 0x400000440000L; //Salto al microprograma 14 (CALL n)
			mem[0x00F] = 0x400000480000L; //Salto al microprograma 15 (RETN)
			//;
			//; Microprograma 1 (LODD n)
			mem[0x23] = 0x001A43000200L;
			mem[0x24] = 0x00C0A0000000L;
			mem[0x25] = 0xC05100FF0400L;
			//;
			//; Microprograma 2 (LODI n)
			mem[0x37] = 0x401143FF0200L;
			//;
			//; Microprograma 3 (STOD n)
			mem[0x4B] = 0x001A43000200L;
			mem[0x4C] = 0x0080A0000000L;
			mem[0x4D] = 0x010001000400L;
			mem[0x4E] = 0x002000000000L;
			mem[0x4F] = 0x402000FF0000L;
			//;
			//; Microprograma 4 (ADDD n)
			mem[0x5F] = 0x001A43000200L;
			mem[0x60] = 0x00C0A0000000L;
			mem[0x61] = 0x004000000000L;
			mem[0x62] = 0xC01110FF0000L;
			//;
			//; Microprograma 5 (ADDI n)
			mem[0x080] = 0x001A43000200L;
			mem[0x081] = 0x4011A1FF0000L;
			//;
			//; Microprograma 6 (SUBD n)
			mem[0x090] = 0x001A43000200L;
			mem[0x091] = 0x00C0A0000000L;
			mem[0x092] = 0x805A00000400L;
			mem[0x093] = 0x001A0A000600L;
			mem[0x094] = 0x001A6A000000L;
			mem[0x095] = 0x4011A1FF0000L;
			//;
			//; Microprograma 7 (SUBI n)
			mem[0x0A0] = 0x001A43000200L;
			mem[0x0A1] = 0x001A0A000600L;
			mem[0x0A2] = 0x001A6A000000L;
			mem[0x0A3] = 0x4011A1FF0000L;
			//;
			//; Microprograma 8 (PUSH)
			mem[0x0B0] = 0x001272000000L;
			mem[0x0B1] = 0x008020000000L;
			mem[0x0B2] = 0x010001000400L;
			mem[0x0B3] = 0x002000000000L;
			mem[0x0B4] = 0x402000FF0000L;
			//;
			//; Microprograma 9 (POP)
			mem[0x0C0] = 0x00C020000000L;
			mem[0x0C1] = 0x805100000400L;
			mem[0x0C2] = 0x401262FF0000L;
			//;
			//; Microprograma 10 (JNEG n)
			mem[0x0D0] = 0x100051400000L;
			mem[0x0D1] = 0x400000FF0000L;
			//;
			//; Microprograma 11 (JZER n)
			mem[0x0E0] = 0x200051400000L;
			mem[0x0E1] = 0x400000FF0000L;
			//;
			//; Microprograma 12 (JCAR n)
			mem[0x0F0] = 0x400000FF0000L;
			//; 
			//; Microprograma 13 (JUMP n)
			mem[0x100] = 0x401043FF4200L;
			//;
			//; Microprograma 14 (CALL n)
			mem[0x110] = 0x001272000000L;
			mem[0x111] = 0x008020000000L;
			mem[0x112] = 0x010000000400L;
			mem[0x113] = 0x003043000200L;
			mem[0x114] = 0x402000FF4000L;
			//; 
			//; Microprograma 15 (RETN)
			mem[0x120] = 0x00C020000000L;
			mem[0x121] = 0x805000000400L;
			mem[0x122] = 0x401262FF0000L;
			//;
			//; Microprograma de búsqueda y decodificación
			mem[0x3FC] = 0x001060000000L;
			mem[0x3FD] = 0x00C000000000L;
			mem[0x3FE] = 0x805300000400L;
			mem[0x3FF] = 0x000000002000L;

			return mem;

		}
		
		/// <summary>Carga la memoria de control que se le pasa como texto.
		/// </summary>
		/// <param name="texto">El texto que contiene definida la memoria
		/// de control.</param>
		/// <returns>La memoria de control.</returns>
		/// <exception cref="MemParseException">Si se produce algún error
		/// al procesar el texto.</exception>
		
		public static MemoriaControl CreateFromString (String texto)
		{
			
			// Pasamos de un texto a un array de lineas
			
			ArrayList v0 = new ArrayList ();

			int first = 0;
			int last = 0;
			texto += "\n";
			last = texto.IndexOf ('\n', first);
			while (last != -1)
			{

				String inst = texto.Substring (first, last - first);
				inst = inst.Trim ();
				if (inst == null)
					inst = "";
				inst = simple2.ensamblador.EnsambladorSimple2.
					QuitarComentariosYEspacios (inst);
				v0.Add (inst);
				first = last + 1;
				last = texto.IndexOf ('\n', first);
			}
			
			// Interpretamos el texto...
			
			ulong[] m = new ulong[TAMANO];
			
			for (int i=0; i < v0.Count; i++)
			{
				try
				{
	
					String linea = (String) v0[i];
					
					int pos = linea.IndexOf(' ');
					
					if (pos == -1)
					{
						//Error, no separamos la dirección de los datos.
						throw new MemParseException (
							TextManager.GetText("MemControl_Err01"));
					}
					String direc = linea.Substring (0, pos).Trim();
					String dato = linea.Substring(pos).Trim();
					dato = dato.Replace (" ", "");
					
					int idirec = Int32.Parse (direc, NumberStyles.HexNumber);
					if ((idirec > 0x3FF) || (idirec < 0))
					{
						throw new MemParseException (
							TextManager.GetText("MemControl_Err02"));
					}
					ulong ldato = UInt64.Parse(dato, NumberStyles.HexNumber);
					if ( (ldato >0xFFFFFFFFFFFF ) || (ldato < 0))
					{
						throw new MemParseException (
							TextManager.GetText("MemControl_Err03"));
					}	
					if (m[idirec] != 0)
					{
						throw new MemParseException (String.Format
							(TextManager.GetText("MemControl_Err04"), 
							idirec));
					}
					else
						m[idirec] = ldato;
				}
				catch (FormatException)
				{
					throw new MemParseException (String.Format (
						TextManager.GetText ("MemControl_Err05"),
						i+1));
				}
				catch (OverflowException)
				{
					throw new MemParseException (String.Format (
						TextManager.GetText ("MemControl_Err06"),
						i+1));
				}
				catch (MemParseException e)
				{
					throw new MemParseException (String.Format (
						TextManager.GetText ("MemControl_Err07"),
						i+1,
						e.Message));
				}
				catch (Exception)
				{
					throw new MemParseException (String.Format (
						TextManager.GetText ("MemControl_Err08"),
						i+1));
				}
			}			
			return new MemoriaControl (m);
		}
		
		/// <summary>Crea una instancia de la clase con la memoria por
		/// defecto.</summary>
		
		public MemoriaControl ()
		{
			this.memoria = MemoriaControl.GetMemoriaDefecto();
		}
		
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="memoriaInicial">El contenido inicial de la memoria
		/// de control. Si es <c>null</c>, entonces se cargará una por
		/// defecto.</param>

		public MemoriaControl (ulong[] memoriaInicial)
		{
			if ((memoriaInicial == null)
			    || (memoriaInicial.Length != TAMANO))
			{
				this.memoria =
					MemoriaControl.GetMemoriaDefecto ();
			}
			else
			{
				this.memoria = memoriaInicial;
			}
		}

		/// <summary>Lee una microinstrucción.</summary>
		/// <param name="direccion">La dirección en la que se leerá
		/// la microinstrucción.</param>
		/// <returns>La microinstrución solicitada.</returns>

		public MicroInstruccion LeerMicroInstruccion (short direccion)
		{
			return new MicroInstruccion (memoria[direccion]);
		}

	}
	
	/// <remarks>Excepcion que se lanza al producirse un error al
	/// procesar un archivo de texto con la memoria de control.</remarks>
	
	public class MemParseException : Exception
	{
	
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="mensaje">El mensaje de la excepción.</param>
		
		public MemParseException (String mensaje):base(mensaje){}
	}
}
