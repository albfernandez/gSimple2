namespace simple2.ensamblador
{

	using System;
	using System.IO;
	using System.Collections;
	
	using simple2.utilidades;

	/// <remarks>Se encarga de verificar la correcta sintáxis de 
	/// un programa en ensamblador y de
	/// ensamblarlo si es correcto.</remarks>

	public class EnsambladorSimple2
	{
		/// <summary> Tabla donde se almacenan las instrucciones y 
		/// los objetos InstruccionSimple2 que se encargan de 
		/// su verficación y codificación.</summary>

		private Hashtable instrucciones;

		/// <summary>Tabla donde se almacena el nombre de la etiqueta 
		/// con su correspondiente Pos_etiqueta, indicando la línea 
		/// del texto en la que se encuentra la etiqueta y 
		/// la dirección que le corresponde.</summary>

		private Hashtable etiquetasDeclaradas;

		/// <summary>Tabla que almacena el nombre de la etiqueta y 
		/// las líneas donde se utilizan.</summary>

		private Hashtable etiquetasUsadas;

		/// <summary>Lista de los caracteres permitidos como separadores 
		/// de registros, inmediato... dentro de una instruccion. 
		/// Se incluyen: el espacio, la coma y el tabulador.</summary>

		public static char[] caracteresSeparador = { ' ', ',', '\t' };

		/// <summary>Cadena en la que almacenamos las advertencias 
		/// correspondientes a etiquetas declaradas pero no usadas.
		/// </summary>

		private String Advertencias = "";

		/// <summary>Crea una instancia de la clase.</summary>
		
		public EnsambladorSimple2 ()
		{
			crearHashInstrucciones ();
		}
	
		/// <summary>Crea una tabla hash con todas las intrucciones 
		/// del repertorio de la arquitectura Simple2.</summary>		

		private void crearHashInstrucciones ()
		{
			instrucciones = new Hashtable ();
			InstruccionSimple2 sinParametros=new InstruccionSinParametros ();
			InstruccionSimple2 salto = new InstruccionSalto ();
			InstruccionSimple2 aritm =	new InstruccionAritmetica ();

			instrucciones.Add ("PUSH", sinParametros);
			instrucciones.Add ("POP" , sinParametros);
			instrucciones.Add ("RETN", sinParametros);
			instrucciones.Add ("HALT", sinParametros);

			instrucciones.Add ("JNEG", salto);
			instrucciones.Add ("JZER", salto);
			instrucciones.Add ("JCAR", salto);
			instrucciones.Add ("JUMP", salto);
			instrucciones.Add ("CALL", salto);

			instrucciones.Add ("LODD", aritm);
			instrucciones.Add ("LODI", aritm);
			instrucciones.Add ("STOD", aritm);
			instrucciones.Add ("ADDD", aritm);
			instrucciones.Add ("ADDI", aritm);
			instrucciones.Add ("SUBD", aritm);
			instrucciones.Add ("SUBI", aritm);
		}

		/// <summary>Comprueba que se utilizan sólo etiquetas declaradas.
		/// Además, produce advertencias cuando se declaran
		/// etiquetas y no se utilizan.</summary>
		///
		/// <exception cref="gSimple2.Ensamblador.ErrorCodigoException">
		/// Cuando el código introducido no es válido</exception>
		
		private void ComprobarUsoEtiquetas ()
		{

			String errores = "";

			// etiqueta usada y no declarada.

			for (IDictionaryEnumerator enu = etiquetasUsadas.GetEnumerator();
			     enu.MoveNext ();)
			{
				if (!etiquetasDeclaradas.ContainsKey (enu.Key))
				{
					ArrayList vec =	(ArrayList)	etiquetasUsadas[enu.Key];
					String lineas_ = "";
					for (int z = 0; z < vec.Count; z++)
					{
						lineas_ += " " + (int) vec[z];
					}
					errores += String.Format (
						TextManager.GetText ("Ens_err_etqnodec"),
						lineas_,
						enu.Key) + "\n";
				}
			}

			//Advertencias para las etiquetas declaradas pero no usadas.
			for (IDictionaryEnumerator enu =
				etiquetasDeclaradas.GetEnumerator ();
			    enu.MoveNext ();)
			{
				if (!etiquetasUsadas.ContainsKey (enu.Key))
				{
					int linea_ = 
						((Pos_etiqueta)etiquetasDeclaradas[enu.Key]).
						linea_fichero;
					
					Advertencias += String.Format (
						TextManager.GetText ("Ens_adv_etqnousada"),
						linea_,
						enu.Key) + "\n";						
				}
			}

			if (errores != "")
				throw new ErrorCodigoException (errores);
		}

		/// <summary>Obtiene las advertencias.</summary>
		/// <returns>Las advertencias sobre el uso de etiquetas
		/// producidas al realizar la verificación del código.</returns>

		public String GetAdvertencias ()
		{
			return Advertencias;
		}
	
		/// <summary> Comprueba que un nombre de etiqueta está compuesto 
		/// de los carácteres permitidos: comenzar con una letra, 
		/// y continuar con letras, numeros o _. </summary>
		///
		/// <exception cref="gSimple2.Ensamblador.ErrorCodigoException">
		/// Cuando el formato de una etiqueta no es correcto</exception>
		/// <param name="etiqueta">La etiqueta a comprobar.</param>

		public static void ComprobarFormatoEtiqueta (String etiqueta)
		{
			//Comprobamos el formato de la etiqueta.
			if ((etiqueta[0] < 'A') || (etiqueta[0] > 'Z'))
			{
				throw new ErrorCodigoException (
					TextManager.GetText ("Ens_Err_etqlet"));					
			}
			for (int i = 0; i < etiqueta.Length; i++)
			{
				char c = etiqueta[i];

				if (! ((c >= '0' && c <= '9')
				     || (c >= 'A' && c <= 'Z') || (c == '_')))
				{
					throw new ErrorCodigoException (String.Format(
						TextManager.GetText ("Ens_Err_etqcar"),
						etiqueta));
				}

			}
		}
		
		/// <summary>Comprueba que el formato de la definición de una 
		/// etiqueta es correcto, y que no esté previamente definida.
		/// </summary>
		///
		/// <exception cref="gSimple2.Ensamblador.ErrorCodigoException">
		/// Cuando la declaración no es válida</exception>
		/// <param name="etiqueta">La etiqueta a comprobar.</param>
		
		private void ComprobarDeclaracionEtiqueta (String etiqueta)
		{
			ComprobarFormatoEtiqueta (etiqueta);

			//Comprobar que no se utiliza palabra reservada como etiqueta.
			if (instrucciones.ContainsKey (etiqueta))
			{
				throw new ErrorCodigoException (String.Format(
					TextManager.GetText("Ens_err_etqreser"),
					etiqueta));
			}
			//Comprobar que la etiqueta no ha sido declarada anteriormente.
			if (etiquetasDeclaradas.ContainsKey (etiqueta))
			{
				int lin = ((Pos_etiqueta)
					 etiquetasDeclaradas[etiqueta]).linea_fichero;
				throw new ErrorCodigoException (String.Format (
					TextManager.GetText ("Ens_err_etqrepetida"),
					etiqueta, lin));			
				
			}
		}
	
		/// <summary>Quita los comentarios y los espacios de la instrucción
		/// para dejarla "limpia" para poderla codificar.</summary>
		///
		/// <param name="linea">La linea a procesar.</param>
		/// <returns>La linea "limpia".</returns>
		
		public static String QuitarComentariosYEspacios (String linea)
		{
			String salida = linea.ToUpper().Trim ();

			if (salida.IndexOf (';') != -1)
			{
				salida = salida.Substring (0, salida. IndexOf (';')).Trim();
			}

			// Limpiamos un poco los separadores. Un grupo de ellos pasa a 
			// ser un espacio en blanco.    
			bool separador = false;
			String tmp = "";
			for (int i = 0; i < salida.Length; i++)
			{
				if (EsSeparador (salida[i]))
				{
					if (!separador)
					{
						separador = true;
						tmp += salida[i];
					}
				}
				else
				{
					separador = false;
					tmp += salida[i];
				}
			}
			return tmp;
		}

		/// <summary>Realiza la verificación del código.</summary>
		/// <exception cref="gSimple2.Ensamblador.ErrorCodigoException">
		/// Cuando el código no es correcto.</exception>
		/// <param name="codigo">El código a verificar, en forma de lista de 
		/// lineas.</param>
		/// <returns>El código procesado listo para ensamblar.</returns>

		private ArrayList PrimeraPasada (ArrayList codigo)
		{
			String errores = "";
			String nemonico;
			ArrayList salida = new ArrayList ();
			etiquetasUsadas = new Hashtable ();
			etiquetasDeclaradas = new Hashtable ();
			Advertencias = "";

			for (int l_fichero = 1; l_fichero <= codigo.Count; l_fichero++)
			{
				String instruc = (String) codigo[l_fichero - 1];
				instruc = QuitarComentariosYEspacios (instruc);
				if (instruc == "")
					continue;

				//miramos las etiquetas
				if (instruc.IndexOf (":") == 0)
				{
					errores += String.Format (
						TextManager.GetText("Ens_err01"), l_fichero) + "\n";						
					continue;
				}
				else if (instruc.IndexOf (":") > 0)
				{
					// Si tiene ":" (pero no en el principio) 
					// analizar si la etiqueta es válida
					String etiqueta =
						instruc.Substring (0, instruc.IndexOf (":"));

					try
					{
						ComprobarDeclaracionEtiqueta (etiqueta);
						Pos_etiqueta posicion =
							new	Pos_etiqueta (l_fichero, salida.Count);
						etiquetasDeclaradas.Add (etiqueta, posicion);
					}
					catch (ErrorCodigoException ex)
					{
						errores += String.Format (
							TextManager.GetText ("Ens_err02"),
							l_fichero, ex.Message) + "\n";
							
					}
					if ((instruc.IndexOf (":") + 1) !=
					    instruc.Length)
					{
						instruc =
							instruc.Substring (
								instruc.IndexOf(":") + 1,
								instruc.Length - instruc.IndexOf (":") - 1
							).Trim ();
						if (instruc == "")
							continue;
					}
					else
					{
						continue;
					}
				}

				if (instruc.IndexOf (' ') != -1)
					nemonico =
						instruc.Substring (0, instruc.IndexOf(' '));
				else
					nemonico = instruc;

				InstruccionSimple2 x =
					(InstruccionSimple2)
					instrucciones[nemonico];

				if (x == null)
				{
					errores += String.Format (
						TextManager.GetText("Ens_err03"),
						l_fichero, nemonico) + "\n";
						
				}
				else
				{
					try
					{
						x.Verificar (instruc, etiquetasUsadas, l_fichero);
					}
					catch (ErrorCodigoException ex)
					{
						errores += String.Format (
							TextManager.GetText ("Ens_err02"),
							l_fichero, ex.Message) + "\n";
					}
				}

				salida.Add (instruc);
			}
			if ((salida.Count < 1) ||
			    ((String) salida[salida.Count - 1]) != "HALT")
			{
				salida.Add ("HALT");
			}
			try
			{
				ComprobarUsoEtiquetas ();
			}
			catch (ErrorCodigoException ex)
			{
				errores += "\n" + ex.Message;
			}

			if (errores != "")
				throw new ErrorCodigoException (errores);

			return salida;
		}

		/// <summary>Realiza la verificación del código.</summary>
		/// <exception cref="gSimple2.Ensamblador.ErrorCodigoException">
		/// Cuando el código no es correcto.</exception>
		/// <param name="codigo">El código a verificar, en forma de cadena
		/// de texto.</param>
		/// <returns>El código procesado listo para ensamblar.</returns>
		
		public ArrayList PrimeraPasada (String codigo)
		{
			ArrayList v0 = new ArrayList ();

			int first = 0;
			int last = 0;
			codigo += "\n";
			last = codigo.IndexOf ('\n', first);
			while (last != -1)
			{

				String inst = codigo.Substring (first, last - first);
				inst = inst.Trim ();
				if (inst == null)
					inst = "";

				v0.Add (inst);
				first = last + 1;
				last = codigo.IndexOf ('\n', first);
			}
			return (PrimeraPasada (v0));
		}
		
		/// <summary>Ensambla un código ya verificado.</summary>
		/// <param name="codigo">El código procesado a ensamblar.</param>
		/// <returns>El código ensamblado en binario</returns>
	
		public short[] Ensamblar (ArrayList codigo)
		{
			InstruccionSimple2 x;
			String inst;
			String nemonico;

			short[] resultado = new short[codigo.Count];

			for (int i = 0; i < codigo.Count; i++)
			{
				inst = (String) codigo[i];
				// coge el mnemonico de la instruccion 
				// (hasta el primer espacio o la intruccion entera)

				if (inst.IndexOf (' ') != -1)
					nemonico = inst.Substring (0, inst.IndexOf(' '));
				else
					nemonico = inst;

				// extrae de la tabla de instrucciones la instancia 
				// correspondiente al mnemonico 
				x = ((InstruccionSimple2) instrucciones[nemonico]);

				// codifica la instrucion 
				resultado[i] =	x.Compilar (inst, etiquetasDeclaradas);
			}
			return resultado;

		}
	
		/// <summary>Nos dice si consideramos un determinado 
		/// caraceter como separador o no.</summary>
		/// <param name="letra">El caracter a comprobar.</param>
		/// <returns>True si es separador, False en otro caso.
		/// </returns>

		public static bool EsSeparador (char letra)
		{
			for (int i = 0; i < caracteresSeparador.Length; i++)
			{
				if (caracteresSeparador[i] == letra)
				{
					return true;
				}
			}
			return false;
		}
	
		/// <summary>Separa una instrucción en sus operandos.</summary>
		/// <param name="linea">La instrucción a descomponer</param>
		/// <returns>Los componentes de la instrucción.</returns>
		
		public static String[] SepararOperandos (String linea)
		{
			ArrayList ar = new ArrayList ();
			String tmp = "";
			for (int i = 0; i < linea.Length; i++)
			{
				if (EsSeparador (linea[i]) && (tmp != ""))
				{
					ar.Add (tmp);
					tmp = "";
				}
				else
				{
					tmp += linea[i];
				}
			}
			if (tmp != "")
			{
				ar.Add (tmp);
			}

			String[]d = new String[ar.Count];
			for (int i = 0; i < ar.Count; i++)
			{
				d[i] = (String) ar[i];
			}
			return d;
		}
	}
}
