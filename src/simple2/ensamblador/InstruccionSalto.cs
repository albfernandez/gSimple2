namespace simple2.ensamblador
{

	using System;
	using System.Collections;
	
	using simple2.utilidades;

	/// <remarks>Clase utilizada para verificar y compilar las instrucciones
	/// de salto de la arquitectura Simple2.</remarks>
	
	public class InstruccionSalto:InstruccionSimple2
	{
	
		/// <summary>Tabla donde se almacenan las instrucciones y su 
		/// correspondientes opcodes.</summary>
		
		private Hashtable opcodes;
		
		/// <summary>Crea una instancia de la clase.</summary>
		
		public InstruccionSalto ()
		{
			opcodes = new Hashtable ();

			opcodes.Add ("JNEG", 0x0A);
			opcodes.Add ("JZER", 0x0B);
			opcodes.Add ("JCAR", 0x0C);
			opcodes.Add ("JUMP", 0x0D);
			opcodes.Add ("CALL", 0x0E);

		}
		
		/// <summary>Verifica una linea de código.</summary>
		/// <param name="linea">La linea a verificar.</param>
		/// <param name="etiquetasUsadas">La lista de etiquetas usadas.
		/// </param>
		/// <param name="l_fichero">La linea en la que se encuentra la
		/// instrucción</param>
		/// <exception cref="gSimple2.Ensamblador.ErrorCodigoException">
		/// Si la instrucción no es correcta.</exception>
		
		public void Verificar (String linea, 
					Hashtable etiquetasUsadas,
					int l_fichero)
		{
			String[]op = EnsambladorSimple2.SepararOperandos (linea);
			if (op.Length != 2)
			{
				throw new ErrorCodigoException (String.Format (
					TextManager.GetText ("Ens_err_parame1"), op[0]));
			}
			int direccion = 0;
			try
			{
				direccion = Int32.Parse (op[1]);
				if ((direccion > 2047) || (direccion < 0))
				{
					throw new ErrorCodigoException
						(TextManager.GetText("Ens_err_parame_fr"));
				}
			}
			catch (ErrorCodigoException ex)
			{
				throw;
			}
			catch (Exception ex)
			{
				//Entonces es que usamos una etiqueta
				//Comprobamos que la etiqueta sea correcta.
				EnsambladorSimple2.ComprobarFormatoEtiqueta (op[1]);
				ArrayList l = (ArrayList) etiquetasUsadas[op[1]];
				if (l == null)
				{
					l = new ArrayList ();
					l.Add (l_fichero);
					etiquetasUsadas.Add (op[1], l);
				}
				else
					l.Add (l_fichero);
			}
		}
		
		/// <summary>Compila una linea de código</summary>
		/// <param name="linea">La linea a codificar</param>
		/// <param name="etiquetasDeclaradas>La lista de etiquetas declaradas
		/// con su posición.</param>
		/// <returns>El código ensamblado en binario</returns>
		
		public short Compilar (String linea,
				       Hashtable etiquetasDeclaradas)
		{
			String[]inst =	EnsambladorSimple2.SepararOperandos (linea);
			int opc = (int) opcodes[inst[0]];
			int direccion = 0;
			try
			{
				direccion = Int32.Parse (inst[1]);
			}
			catch (Exception ex)
			{
				direccion =	((Pos_etiqueta)
					 etiquetasDeclaradas[inst[1]]).linea_salida;
			}
			int instruccion = opc << 11;
			instruccion |= (direccion & 0x01FF);
			return ((short) instruccion);
		}
	}
}
