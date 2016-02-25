namespace simple2.rutaDeDatos
{

	using System;
	
	/// <remarks>Esta clase es la encargada de simular el funcionamiento
	/// del ordenador Simple2.</remarks>

	public class SecuenciadorMicroprograma
	{
		/// <summary>Almacena el contenido de bufferA.</summary>
		
		private short regA = 0;
		
		/// <summary>Almacena el contenido de bufferB.</summary>
		
		private short regB = 0;

		/// <summary>Almacena el contenido del registro RDC.</summary>
		
		private short RDC = 0;
		
		/// <summary>Almacena el contenido de MAR, el registro de 
		///   direccionamiento de memoria.</summary>
		
		private short _mar = 0;
		
		/// <summary>Almacena el contenido de MBR, el registro que contiene
		///  los datos leídos/ a escribir de memoria.</summary>
		
		private short _mbr = 0;
		
		/// <summary>Indica las peticiones de lectura consecutivas.
		/// </summary>
		
		private int _petLecturaMemoria = 0;
		
		/// <summary>Indica las peticiones de escritura consecutivas.
		/// </summary>
		
		private int _petEscrituraMemoria = 0;
		
		/// <summary>Lleva la cuenta de los subciclos ejecutados.</summary>
		
		private int subciclos = 0;
		
		/// <summary>Almacena el contenido de RMC, es decir, la 
		/// microinstrucción a ejecutar.</summary>
		
		private MicroInstruccion rmc = null;
		
		/// <summary>Almacena la memoria de control.</summary>
		
		private MemoriaControl memoriaControl = null;
		
		/// <summary>Almacena los registros.</summary>
		
		private BancoRegistros registros = new BancoRegistros ();
		
		/// <summary>ALU del ordenador Simple2.</summary>
		
		private ALU alu = new ALU ();
		
		/// <summary>Almacena la memoria principal.</summary>
		
		private MemoriaPrincipal mp = null;
		
		/// <summary>Se encarga de dibujar la ruta de datos.</summary>
		
		private IRepresentacionRDD repRdd; 
		
		/// <summary>Crea una instancia de la clase</summary>
		/// <param name="memoriaPrincipal">La memoria principal con su 
		/// contenido inicial</param>
		/// <param name="memControl">La memoria de control con su contenido
		/// inicial.</param>
				
		public SecuenciadorMicroprograma (short[] memoriaPrincipal,
			MemoriaControl memControl)
		{
			mp = new MemoriaPrincipal (memoriaPrincipal);
			memoriaControl = memControl;
		}
		
		/// <summary>Actualiza el contenido del registro MBR y de
		/// la memoria según la secuencia de mInstrucciones.</summary>
		/// <param name="inst"></param>
		
		private void ActualizarPeticionesMemoria (MicroInstruccion  inst)
		{

			if ((inst.GetMAR () == 1) || (inst.GetMBR () == 1))
			{
				_petLecturaMemoria = 0;
				_petEscrituraMemoria = 0;
			}

			if ((inst.GetWR () == 1) && (inst.GetRD () == 1))
			{
				_petEscrituraMemoria = 0;
				_petLecturaMemoria = 0;
			}
			else if (inst.GetWR () == 1)
			{
				_petEscrituraMemoria++;
				_petLecturaMemoria = 0;
			}
			else if (inst.GetRD () == 1)
			{
				_petLecturaMemoria++;
				_petEscrituraMemoria = 0;
			}
			else
			{
				_petLecturaMemoria = 0;
				_petEscrituraMemoria = 0;
			}
			if (_petLecturaMemoria > 1)
				_mbr = mp.LeerDato (_mar);
			if (_petEscrituraMemoria > 1)
				mp.EscribirDato (_mar, _mbr);
		}
		
		/// <summary>Ejecuta el siguiente subciclo en la ruta de datos.
		/// </summary>
		/// <exception cref="SimulacionFinalizadaException> cuando la 
		/// simulación ha finalizado (se ha cargado la instrucción 
		/// HALT = 0xF800 en el registro IR).</exception>

		public void EjecutarSubciclo ()
		{

			switch (subciclos % 4)
			{
				case 0: EjecutarSubciclo1();
						break;
				case 1: EjecutarSubciclo2();
						break;
				case 2: EjecutarSubciclo3();
						break;
				case 3: EjecutarSubciclo4();
						break;
			}
			subciclos++;
		}
		
		/// <summary><para>Ejecuta el primer subciclo de la microinstrucción.
		/// </para>
		/// <para>En él, lo que hace es cargar en RMC la microinstrucción
		/// situada en la dirección indicada por RDC en la memoria de control.
		/// </para></summary>
		/// <exception cref="SimulacionFinalizadaException> cuando la 
		/// simulación ha finalizado (se ha cargado la instrucción 
		/// HALT = 0xF800 en el registro IR).</exception>
		
		private void EjecutarSubciclo1()
		{
			if ((registros.LeerRegistro (BancoRegistros.IR) & 0xF800)==0xF800)
			{
				throw new SimulacionFinalizadaException ("Fin normal");
			}
			
			//Leemos la siguiente microinstrucción de la memoria de control
			rmc = memoriaControl.LeerMicroInstruccion (RDC);

			repRdd.DibujarCiclo1(rmc, RDC);
		}
		
		/// <summary><para>Ejecuta el segundo subciclo de la microinstrucción.
		/// </para>
		/// <para>En él, se cargan el bufferA y bufferB los contenidos de 
		/// los registros indicados en los campos A y B de la 
		/// microinstrucción.</para></summary>
		
		private void EjecutarSubciclo2()
		{
				ActualizarPeticionesMemoria (rmc);
				regA =registros.LeerRegistro (rmc.GetA ());
				regB =	registros.LeerRegistro (rmc.GetB ());

				repRdd.DibujarCiclo2(rmc, regA, regB);
		}
		
		/// <summary><para>Ejecuta el tercer subciclo de la microinstrucción.
		/// </para>
		/// <para>En él:
		///	   <list type="bullet">
		///	   <item><description>
		///    se ejecuta la operación de la ALU 
		///    (indicada en el campo ALU de la microinstrucción)
		///    </description></item>
		///    <item><description>
		///    Desplaza el resultado obtenido en la ALU en el registro SH.
		///    </description></item>
		///    <item><description>
		///    Si es necesario se carga el registro MAR con el 
		///    contenido de bufferB.
		///    </description></item>
		///    </list>
		/// </para>
		/// </summary>
		
		private void EjecutarSubciclo3()
		{

			if (rmc.GetAMUX () == 1)
			{
				alu.Operar (rmc.GetALU (),  rmc.GetSH (), _mbr, regB);
			}
			else
			{
				alu.Operar (rmc.GetALU (),  rmc.GetSH (), regA, regB);
			}


			if (rmc.GetMAR () == 1)
				_mar = regB;
			repRdd.DibujarCiclo3(rmc, alu.LeerResultado(), _mar, _mbr, 
				alu.LeerC(), alu.LeerN(), alu.LeerZ());
		}
		
		/// <summary><para>Ejecuta el cuarto subciclo de la microinstrucción.
		/// </para>
		/// <para>En él:
		///    <list type="bullet">
		///    <item><description>
		///    Almacenamos el contenido de SH en el banco de registros.
		///    </description></item>
		///    <item><description>
		///    Si es necesario copiamos SH a MBR.
		///    </description></item>
		///    <item><description>
		///    Obtenemos en RDC la dirección de la siguiente microinstrucción
		///    a ejecutar.
		///    </description></item>
		///    </list>
		/// </para></summary>
		
		
		
		private void EjecutarSubciclo4()
		{

			if (rmc.GetMBR () == 1)
			{
				_mbr = alu.LeerResultado ();
			}
			if (rmc.GetENC () == 1)
			{
				registros.EscribirRegistro (rmc.GetC (),  alu.LeerResultado());
			}
			//Opciones de salto.
			if (rmc.GetFIR () == 1)
			{
				RDC = (short) ((registros.LeerRegistro
						(BancoRegistros.IR) >> 11) & (0x1F));
			}
			else if (Bifurca (rmc.GetCOND()))
			{
				RDC = (short) rmc.GetADDR ();
			}
			else
			{
				RDC++;
			}

			repRdd.DibujarCiclo4(rmc, _mbr);
		}
		
		/// <summary>Detiene la simulación de la ruta de datos</summary>
		
		public void Detener ()
		{
			repRdd.Detener();
		}

		/// <summary>Indica si se produce un salto (se bifurca) o no.
		/// </summary>
		/// <returns><c>true</c> si se bifurca, <c>false</c> en otro caso.
		/// </returns>
		
		private bool Bifurca (int condicion)
		{
			switch (condicion)
			{
				case 0:
					return false;
				case 1:
					return (alu.LeerN () == 1);
				case 2:
					return (alu.LeerZ () == 1);
				case 3:
					return (alu.LeerC () == 1);
				case 4:
					return true;
			}
			return true;
		}
		
		/// <summary>Añade un listener para los cambios de memoria.</summary>
		/// <param name="l">El listener.</param>
		
		public void AddMemoryChangeListener (MemoryChangeListener l)
		{
			mp.AddMemoryChangeListener (l);
		}
		
		/// <summary>Añade un listener para los cambios de los registros.
		/// </summary>
		/// <param name="l">El listener.</param>
		
		public void AddRegisterChangeListener (RegisterChangeListener l)
		{
			registros.AddRegisterChangeListener(l);
		}
		
		/// <summary>Establece la representación de ruta de datos a utilizar
		/// para dibujar.</summary>
		/// <param name="r">La representación.</param>
		
		public void SetRepresentacionRDD(IRepresentacionRDD r)
		{
			repRdd = r;
			repRdd.Clean();
		}
	}
}
