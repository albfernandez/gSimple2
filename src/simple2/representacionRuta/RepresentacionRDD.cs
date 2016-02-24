namespace simple2.representacionRuta
{
	using System;
	
	using simple2.utilidades;
	using simple2.rutaDeDatos;

	
	/// <remarks>Esta clase se encarga de agrupar los elementos
	/// gráficos que componen el simulador visual de la ruta de datos.
	/// Se encarga de ir activando y desactivando los elementos según
	/// las señales que recibe del simulador de la ruta de datos.</remarks>
	
	public class RepresentacionRDD : 
		RegisterChangeListener, IRepresentacionRDD
	{
		/// <summary>Superficie de dibujo, sobre la que se pintarán
		/// todos los objetos.</summary>
		
		private InterfaceDibujo dibujo;
		
		/// <summary>El ciclo en curso.</summary>
		
		private int instruccion;
		
		/// <summary>Bus de datos A.</summary>
		
		private Bus busA = null;
		
		/// <summary>Bus de datos B.</summary>
		
		private Bus busB = null;
		
		/// <summary>Bus de datos C.</summary>
		
		private Bus busC = null;
		
		/// <summary>Registro bufferA.</summary>
		
		private IElementoDibujable bufferA = null;
		
		/// <summary>Registro bufferB.</summary>
		
		private IElementoDibujable bufferB = null;
		
		/// <summary>Registro MAR.</summary>
		
		private IElementoDibujable mar = null;
		
		/// <summary>Registro MBR.</summary>
		
		private IElementoDibujable mbr = null;
		
		/// <summary>Registro de desplazamiento SH.</summary>
		
		private IElementoDibujable sh = null;
		
		/// <summary>Alu.</summary>		
		
		private IElementoDibujable alu = null;
		
		/// <summary>Memoria principal.</summary>
		
		private IElementoDibujable memoriappal = null;
		
		/// <summary>Cable RD de la memoria principal.</summary>
		
		private IElementoDibujable cable_rd = null;
		
		/// <summary>Cable WR de la memoria principal.</summary>
		
		private IElementoDibujable cable_wr = null;
		
		/// <summary>Cable M0 del registro MAR.</summary>
		
		private IElementoDibujable cable_m0 = null;
		
		/// <summary>Cable M1 del registro MBR.</summary>
		
		private IElementoDibujable cable_m1 = null;
		
		/// <summary>Registros del Simple2.</summary>
		
		private IElementoDibujable[] regs = new CajaRegistro[16];
		
		/// <summary>Cable L0 del registro BufferA.</summary>

		private IElementoDibujable cable_l0 = null;
		
		/// <summary>Cable L1 del registro BufferB.</summary>
		
		private IElementoDibujable cable_l1 = null;
		
		/// <summary>Cable A0 de MUX para selección de entrada 
		/// de la ALU.</summary>		
		
		private IElementoDibujable cable_a0 = null;
		
		/// <summary>Entradas de control F3-F0 de operación de la ALU.
		/// </summary>
		
		private IElementoDibujable cable_f3_f0 = null;
		
		/// <summary>Cable C de salida de la ALU.</summary>
		
		private IElementoDibujable cable_c = null;
		
		/// <summary>Cable N de salida de la ALU.</summary>
		
		private IElementoDibujable cable_n = null;
		
		/// <summary>Cable Z de salida de la ALU.</summary>
		
		private IElementoDibujable cable_z = null;
		
		/// <summary>Cable S2-S0 para indicar la operación del registro
		/// SH.</summary>
		
		private IElementoDibujable cable_s2_s0 = null;
		
		/// <summary>Cable que va de MBR a MUX.</summary>
		
		private IElementoDibujable cable_mbr_mux = null;
		
		/// <summary>Cable que va de MAR a la memoria principal.</summary>
		
		private IElementoDibujable cable_mar_ppal = null;
		
		/// <summary>Cable que va de MBR a la memoria principal.</summary>
		
		private IElementoDibujable cable_mbr_ppal = null;
		
		/// <summary>Cable que va de BufferA a MUX.</summary>
		
		private IElementoDibujable cable_bufferA_mux = null;
		
		/// <summary>Cable que va de MUX a la ALU.</summary>
		
		private IElementoDibujable cable_mux_alu = null;
		
		/// <summary>Cable que va de la ALU al registro SH.</summary>
		
		private IElementoDibujable cable_alu_sh = null;
		
		/// <summary>Cable que va del BufferB a la entrada de la ALU.
		/// </summary>
		
		private IElementoDibujable cable_bufferB_alu = null;
		
		/// <summary>Cable que va del BufferB al registro MAR.</summary>
		
		private IElementoDibujable cable_bufferB_mar = null;
		
		/// <summary>Cable que va del registro SH a MBR.</summary>
		
		private IElementoDibujable cable_sh_mbr = null;
		
		/// <summary>Cable que va del registroSH a la intersección
		/// del cable con BusC.</summary>
		
		private IElementoDibujable cable_sh = null;
		
		/// <summary>El multiplexor MUX.</summary>
		
		private IElementoDibujable mux = null;
		
		/// <summary>Etiqueta WR de la memoria principal.</summary>
		
		private IElementoDibujable et_wr = null;
		
		/// <summary>Etiqueta RD de la memoria principal.</summary>
		
		private IElementoDibujable et_rd = null;
		
		/// <summary>Etiqueta M0 del registro MAR.</summary>
		
		private IElementoDibujable et_m0 = null;
		
		/// <summary>Etiqueta M1 del registro MBR.</summary>
		
		private IElementoDibujable et_m1 = null;
		
		/// <summary>Etiqueta L0 del registro BufferA.</summary>
		
		private IElementoDibujable et_l0 = null;
		
		/// <summary>Etiqueta L1 del registro BufferB.</summary>
		
		private IElementoDibujable et_l1 = null;
		
		/// <summary>Etiqueta S2-S0 del registro SH.</summary>
		
		private IElementoDibujable et_s0= null;		
		
		/// <summary>Etiqueta A0 del MUX.</summary>
		
		private IElementoDibujable et_a0 = null;	

		/// <summary>Etiqueta F3-F0 de selección de operación de la ALU.
		/// </summary>

		private IElementoDibujable et_f0 = null;
		
		/// <summary>Etiqueta de la salida C de la ALU.</summary>
		
		private IElementoDibujable et_c = null;
		
		/// <summary>Etiqueta de la salida N de la ALU.</summary>
		
		private IElementoDibujable et_n = null;
		
		/// <summary>Etiqueta de la salida Z de la ALU.</summary>
		
		private IElementoDibujable et_z = null;
		
		/// <summary>Etiqueta del bus A.</summary>
		
		private IElementoDibujable et_busa = null;
		
		/// <summary>Etiqueta del bus B.</summary>
		
		private IElementoDibujable et_busb = null;
		
		/// <summary>Etiqueta del bus C.</summary>
		
		private IElementoDibujable et_busc = null;
		
		/// <summary>Etiqueta para mostrar el ciclo y subciclo actual.
		/// </summary>
		
		private IElementoDibujable etiqueta_ciclos = null;
		
		/// <summary>Etiqueta para mostrar la microinstrucción que hay
		/// actualmente en rmc.</summary>
		
		private IElementoDibujable etiqueta_rmc = null;
		
		/// <summary>Etiqueta para mostrar la instrucción en ejecución.
		/// </summary>
		
		private IElementoDibujable etiqueta_inst = null;
		
		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="dibujo">Superficie sobre la que se dibujarán los
		/// objetos del simulador.</param>
		
		public RepresentacionRDD (InterfaceDibujo dibujo)
		{
			this.dibujo = dibujo;
			dibujo.SetRepresentacionRDD(this);
	
			etiqueta_ciclos = 
				new Etiqueta (dibujo, 10, 10, "", EnuColor.Rojo, false);
			etiqueta_rmc = 
				new Etiqueta (dibujo, 10, 50, "", EnuColor.Azul, false);
			etiqueta_inst = 
				new Etiqueta (dibujo, 10, 30, "", EnuColor.Rojo, false);
			
			int x_deC1 = 350;
			int x_deC2 = 400;
			int y_deC = 436;
			int anchoRegistro = 110;
			int altoRegistro = 15;
			int separacionRegistros = 3;
			int yreg1 = 15;
			int y_deB = 310;
			int sepregA = 40;
			int sepAB = 30;
			int x_deA2 = x_deC2+anchoRegistro+sepregA;
			int x_deB2 = x_deC2+anchoRegistro+sepregA+sepAB;
			int y_deAlu = 390;
			int x_deAlu = 530;
			int anchoAlu = 80;
			int y_deMUX = 360;
			int y_deMAR = 330;
			int x_deMAR = 160;
			int ancho_MAR = 90;
			int y_deMBR = y_deMUX;

			
			et_busa = new Etiqueta (dibujo, x_deA2-5 , yreg1 - 5, "Bus A");
			et_busb = new Etiqueta (dibujo, x_deB2+10, yreg1, "Bus B");
			et_busc = new Etiqueta (dibujo, x_deC1, yreg1 - 5, "Bus C");

			Cable[] cablesDeC = new Cable[16];

			for (int i=0; i < 16; i++)
			{
				cablesDeC[i] = new CableUnidireccional (
					dibujo, new int[] {
						x_deC1,
						y_deC, 
						x_deC1, 
						yreg1 + 5 + (altoRegistro + separacionRegistros) * i,
						x_deC2, 
						yreg1 + 5 + (altoRegistro + separacionRegistros) * i
					}
				);
			}
			busC = new Bus (cablesDeC);
			
			Cable[] cablesDeA = new Cable[16];
			
			for (int i = 0; i < 16; i++)
			{
				cablesDeA[i] = new CableUnidireccional (
					dibujo, new int[]{
						x_deC2 + anchoRegistro, 
						yreg1 + 5 + (altoRegistro + separacionRegistros) * i, 
						x_deA2, 
						yreg1 + 5 + (altoRegistro + separacionRegistros) * i, 
						x_deA2, 
						y_deB
					}
				);
			}
			busA = new Bus (cablesDeA);
			
			Cable[] cablesDeB = new Cable[16];
			
			for (int i=0; i < 16; i++)
			{
				cablesDeB[i] = new CableUnidireccional (
					dibujo, new int[]{
						x_deC2 + anchoRegistro,
						yreg1 + 10 + (altoRegistro + separacionRegistros) * i,
						x_deB2,
						yreg1 + 10 + (altoRegistro + separacionRegistros) * i,
						x_deB2,
						y_deB
					}
				);
			}
			busB = new Bus (cablesDeB);
			
			for (int i=0; i < 16; i++)
			{
				regs[i] = new CajaRegistro (
					dibujo, 
					x_deC2, 
					yreg1 + (altoRegistro+ separacionRegistros) * i,
					anchoRegistro,
					altoRegistro,
					BancoRegistros.GetNombreRegistro(i)
				);
			}					
			
			memoriappal = new CajaRegistro
				(dibujo, 31, 330, 90, 100," Memoria\nPrincipal\n2048x16");
			cable_rd = 
				new CableUnidireccional(dibujo, new int[] {65, 320, 65, 330});
			cable_wr = 
				new CableUnidireccional (dibujo, new int[] {100,320,100,330});
			et_rd = new Etiqueta (dibujo, 65,310, "RD");
			et_wr = new Etiqueta (dibujo, 100,310, "WR");			
			
			bufferA = 
				new CajaRegistro (dibujo, 450, y_deB, 110, 16, "BufferA 0000");
			bufferB = 
				new CajaRegistro(dibujo, 570, y_deB, 110, 16, "BufferB 0000");
			cable_l0 = new CableUnidireccional 
				(dibujo, new int[] {440, y_deB + 8, 450, y_deB+8});
			cable_l1 = new CableUnidireccional 
				(dibujo, new int[]{690, y_deB + 8, 680, y_deB+8});
			
			et_l0 = new Etiqueta (dibujo, 420, y_deB + 7 , "L0");
			et_l1 = new Etiqueta (dibujo, 700, y_deB + 7 , "L1");
			
			
			cable_bufferB_alu = new CableUnidireccional 
				(dibujo, new int[]{580, y_deB+16, 580, y_deAlu} );
			cable_bufferB_mar  = new CableUnidireccional 
				(dibujo, new int[]{
					580, 
					y_deB + 16,
					580, 
					y_deMAR + 8,
					x_deMAR + ancho_MAR,
					y_deMAR + 8
				});
			cable_bufferA_mux = new CableUnidireccional 
				(dibujo, new int[]{ 550, y_deB + 16, 550, y_deMUX} );
			
			mux = new CajaRegistro(dibujo, 500, y_deMUX, 60, 16, "MUX");
			cable_a0 = new CableUnidireccional 
				(dibujo, new int[]{510, y_deMUX + 26, 510, y_deMUX + 16} );
			cable_mux_alu = new CableUnidireccional 
				(dibujo, new int[]{ 550, y_deMUX + 16 , 550, 390} );
			cable_alu_sh = new CableUnidireccional 
				(dibujo, new int[]{ 570, 422, 570, 430} );
			
			et_a0 = new Etiqueta (dibujo, 510, y_deMUX + 33, "A0 0");
			
			mar = new CajaRegistro
				(dibujo, x_deMAR, y_deMAR, ancho_MAR, 16, "MAR 0000");
			mbr = new CajaRegistro 
				(dibujo, x_deMAR, y_deMUX, ancho_MAR, 16, "MBR 0000");
			
			cable_m0 = new CableUnidireccional 
				(dibujo, new int[]{190, y_deMAR - 10, 190, y_deMAR} );
			cable_m1 = new CableUnidireccional 
				(dibujo, new int[]{170, y_deMBR + 26, 170, y_deMBR + 16} );
			
			et_m0 = new Etiqueta (dibujo, 190, y_deMAR - 17, "M0");
			et_m1 = new Etiqueta (dibujo, 170, y_deMBR + 32, "M1");
			
			cable_mbr_mux = new CableUnidireccional 
				(dibujo, new int[]{
					x_deMAR + ancho_MAR,y_deMUX + 8, 500, y_deMUX + 8} );
			cable_mar_ppal = new CableUnidireccional 
				(dibujo, new int[]{ x_deMAR, y_deMAR + 8, 121, y_deMAR + 8});
			cable_mbr_ppal = new CableBidireccional 
				(dibujo, new int[]{121, y_deMUX + 8, x_deMAR, y_deMUX + 8});		
		
			sh = new CajaRegistro (dibujo,x_deAlu,430,anchoAlu,16,"SH 0000");
			cable_s2_s0 = new CableUnidireccional 
				(dibujo, new int[]
					{x_deAlu+anchoAlu + 10, 438, x_deAlu + anchoAlu, 438});
			et_s0 = 
				new Etiqueta (dibujo, x_deAlu+anchoAlu + 50, 438, "S2-S0 000");
			
			cable_sh_mbr  = new CableUnidireccional 
				(dibujo, new int[]{x_deC1, y_deC, 190, y_deC, 190,y_deMUX+16});
			cable_sh = new Cable 
				(dibujo, new int[] {x_deAlu, y_deC, x_deC1, y_deC});
			
			
			alu = new CajaRegistro(dibujo, x_deAlu, y_deAlu,anchoAlu,32,"ALU");
			
			
			cable_c = new CableUnidireccional 
				(dibujo, new int[]
					{x_deAlu + anchoAlu, 395, x_deAlu + anchoAlu + 10, 395});
			cable_n = new CableUnidireccional 
				(dibujo, new int[]
					{x_deAlu + anchoAlu, 405, x_deAlu + anchoAlu + 10, 405});
			cable_z = new CableUnidireccional 
				(dibujo, new int[]
					{x_deAlu + anchoAlu, 415, x_deAlu + anchoAlu + 10,415});
			cable_f3_f0 = new CableUnidireccional 
				(dibujo, new int[]{x_deAlu - 10, 415, x_deAlu, 415} );
			et_f0 = new Etiqueta (dibujo, x_deAlu - 50, 415, "F3-F0 0000");
			et_c = new Etiqueta (dibujo, x_deAlu+anchoAlu+25, 395, "C 0");
			et_n = new Etiqueta (dibujo, x_deAlu+anchoAlu+25, 405, "N 0");
			et_z = new Etiqueta (dibujo, x_deAlu+anchoAlu+25, 415, "Z 0");
			
			ActualizarTodo();
		}

		/// <summary>Vuelve a dibujar todos los elementos en la 
		/// superficie de dibujo.</summary>
		
		public void ActualizarTodo()
		{
			dibujo.Clean();
			etiqueta_ciclos.Repintar();
			etiqueta_rmc.Repintar();
			etiqueta_inst.Repintar();
			
			et_busa.Repintar();
			et_busb.Repintar();
			et_busc.Repintar();
			
			et_rd.Repintar();
			et_wr.Repintar();
			et_m0.Repintar();
			et_m1.Repintar();
			et_l0.Repintar();
			et_l1.Repintar();
			
			et_s0.Repintar();
			et_a0.Repintar();
			et_f0.Repintar();
			et_c.Repintar();
			et_n.Repintar();
			et_z.Repintar();			
			
			for (int i=0; i < 16; i++)
				regs[i].Repintar();
			busC.Repintar();
			busA.Repintar();
			busB.Repintar();
			bufferA.Repintar();
			bufferB.Repintar();
			mar.Repintar();
			mbr.Repintar();
			sh.Repintar();

			cable_m0.Repintar();
			cable_m1.Repintar();
			cable_l0.Repintar();
			cable_l1.Repintar();
			cable_a0.Repintar();
			cable_f3_f0.Repintar();
			cable_c.Repintar();
			cable_n.Repintar();
			cable_z.Repintar();
			cable_s2_s0.Repintar();
			cable_mbr_mux.Repintar();
			cable_mar_ppal.Repintar();
			cable_mbr_ppal.Repintar();
			cable_bufferA_mux.Repintar();
			cable_mux_alu.Repintar();
			cable_alu_sh.Repintar();
			cable_bufferB_mar.Repintar();
			cable_bufferB_alu.Repintar();
			cable_sh_mbr.Repintar();
			cable_sh.Repintar();
			memoriappal.Repintar();
			cable_rd.Repintar();
			cable_wr.Repintar();			
			mux.Repintar();			
			alu.Repintar();
			
			dibujo.Refresh();
		}
		
		/// <summary>Activa los elementos activos durante el subciclo 1.
		/// </summary>
		/// <param name="mic">La microinstrucción que se
		/// acaba de cargar.</param>
		/// <param name="rdc>La dirección de la memoria de control en la que
		/// se encuentra la microinstrucción.</param>
		
		public void DibujarCiclo1 (MicroInstruccion mic, short rdc)
		{
			instruccion++;
			PintarMicro (mic);
			etiqueta_ciclos.SetText ("Ciclo:" + instruccion + " Subciclo 1");
			cable_sh.Apagar();
			cable_sh_mbr.Apagar();
			mbr.Apagar();
			sh.Apagar();
			busC.Apagar();
			for (int i=0; i < 16; i++)
				regs[i].Apagar();
			
			if ((rdc == 0) || ((rdc >=1020) && (rdc < 1023)))
			{
				etiqueta_inst.SetText ("Cargando sig. instrucción...");
			}
			ActualizarTodo();
		}
		/// <summary>Activa los elementos activos durante el subciclo 2.
		/// </summary>
		/// <param name="mic">La microinstrucción actualmente en ejecución.
		/// Nos indica los registros de origen.</param>
		/// <param name="regA">El contenido de BufferA.</param>
		/// <param name="regB">El contenido de BufferB.</param>
		
		public void DibujarCiclo2 
			(MicroInstruccion mic, short regA, short regB)
		{
			PintarMicro (mic);
			etiqueta_ciclos.SetText ("Ciclo:" + instruccion + " Subciclo 2");
			busA.Encender (mic.GetA());
			busB.Encender (mic.GetB());
			
			regs[mic.GetA()].Encender();
			regs[mic.GetB()].Encender();
			
			bufferA.SetText ("BufferA " + Conversiones.ToHexString(regA));
			bufferB.SetText ("BufferB " + Conversiones.ToHexString(regB));			
			
			bufferA.Encender();
			bufferB.Encender();
			
			ActualizarTodo();

		}
		
		/// <summary>Activa los elementos activos durante el subciclo 3.
		/// </summary>
		/// <param name="mic">La microinstrucción en ejecución.</param>
		/// <param name="vSH">El valor del registro SH.</param>
		/// <param name="vMAR">El valor del registro MAR.</param>
		/// <param name="vMBR">El valor del registro MBR.</param>
		/// <param name="valorC">El valor de la salida C de la ALU.</param>
		/// <param name="valorN">El valor de la salida N de la ALU.</param>
		/// <param name="valorZ">El valor de la salida Z de la ALU.</param>
		
		public void DibujarCiclo3
			(MicroInstruccion mic, short vSH, short vMAR,
			short vMBR, int valorC, int valorN, int valorZ )
		{
			PintarMicro (mic);
			etiqueta_ciclos.SetText("Ciclo:" + instruccion + " Subciclo 3");
			busA.Apagar();
			busB.Apagar();			
			regs[mic.GetA()].Apagar();
			regs[mic.GetB()].Apagar();
			sh.Encender();
			
			mbr.SetText ("MBR " + Conversiones.ToHexString (vMBR));
			mar.SetText ("MAR " + Conversiones.ToHexString (vMAR));
			sh.SetText ("SH " + Conversiones.ToHexString (vSH));
			et_s0.SetText ("S2-S0 " + 
				Conversiones.ToBinaryString(mic.GetSH(), 3));
			et_a0.SetText ("A0 " +
				Conversiones.ToBinaryString(mic.GetAMUX(), 1));
			et_f0.SetText ("F3-F0 " +
				Conversiones.ToBinaryString(mic.GetALU(), 4));
			et_c.SetText ("C " + valorC);
			et_n.SetText ("N " + valorN);
			et_z.SetText ("Z " + valorZ);
			
			
			if (mic.GetMAR() == 1)
			{
				//Activamos el cable que va de bufferB a MAR
				mar.Encender();
				cable_bufferB_mar.Encender();
			}
			if (mic.GetAMUX() == 1)
			{
				cable_mbr_mux.Encender();
				mbr.Encender();
			}
			else
			{
				cable_bufferA_mux.Encender();
			}
			cable_bufferB_alu.Encender();
			cable_alu_sh.Encender();
			alu.Encender();
			mux.Encender();
			cable_mux_alu.Encender();

			ActualizarTodo();
		}
		
		/// <summary>Activa los elementos activos durante el subciclo 4.
		/// </summary>
		/// <param name="mic">La microinstrucción en ejecución.</param>
		/// <param name="vMBR">El valor del registro MBR.</param>S
		
		public void DibujarCiclo4(MicroInstruccion mic, short vMBR)
		{
			PintarMicro (mic);
			etiqueta_ciclos.SetText ("Ciclo:" + instruccion + " Subciclo 4");
			bufferA.Apagar();
			bufferB.Apagar();
			mar.Apagar();
			mbr.Apagar();
			mbr.SetText ("MBR " + Conversiones.ToHexString (vMBR));
			cable_bufferB_mar.Apagar();
			cable_bufferB_alu.Apagar();
			alu.Apagar();
			mux.Apagar();
			cable_bufferA_mux.Apagar();
			cable_alu_sh.Apagar();
			cable_mux_alu.Apagar();
			cable_mbr_mux.Apagar();
			//Escribir el dato de salida en la ALU.
			//Activar los cables de salida necesarios.
			if (mic.GetENC() == 1)
			{
				busC.Encender(mic.GetC());
				regs[mic.GetC()].Encender();
			}
			if (mic.GetMBR() == 1)
			{
				//Activamos el cable que va de SH a MBR
				cable_sh_mbr.Encender();
				mbr.Encender();
			}
			if ( (!(mic.GetENC()==1)) && (!(mic.GetMBR()==1) ) )
			{
				sh.Apagar();
			}
			else
			{
				cable_sh.Encender();
			}
			ActualizarTodo();
		}
		
		/// <summary>Apaga todos los elementos.</summary>
		
		public void Clean()
		{			
			
			busC.Apagar();
			busA.Apagar();
			busB.Apagar();
			bufferA.Apagar();
			bufferB.Apagar();
			mar.Apagar();
			mbr.Apagar();
			sh.Apagar();

			cable_m0.Apagar();
			cable_m1.Apagar();
			cable_l0.Apagar();
			cable_l1.Apagar();
			cable_a0.Apagar();
			cable_f3_f0.Apagar();
			cable_c.Apagar();
			cable_n.Apagar();
			cable_z.Apagar();
			cable_s2_s0.Apagar();
			cable_mbr_mux.Apagar();
			cable_mar_ppal.Apagar();
			cable_mbr_ppal.Apagar();
			cable_bufferA_mux.Apagar();
			cable_mux_alu.Apagar();
			cable_alu_sh.Apagar();
			cable_bufferB_alu.Apagar();
			cable_bufferB_mar.Apagar();
			cable_sh_mbr.Apagar();
			cable_sh.Apagar();
			memoriappal.Apagar();
			cable_rd.Apagar();
			cable_wr.Apagar();			
			mux.Apagar();	
			alu.Apagar();			
			
			for (int i=0; i < 16; i++)
				regs[i].Apagar();			
			ActualizarTodo();

		}
		
		/// <summary>Apaga todos los elementos.</summary>
		
		public void Detener ()
		{
			this.Clean();
		}
		
		/// <summary>Se llama cuando cambia el contenido de un registro.
		/// </summary>
		/// <param name="registro">El registro que ha cambiado.</param>
		/// <param name="newValue">El nuevo valor almacenado en
		/// <c>registro</c>.</param>
		
		public void RegisterChanged (int registro, short newValue)
		{
			regs[registro].SetText (
				BancoRegistros.GetNombreRegistro(registro) + " " + 
				Conversiones.ToHexString (newValue));
				
			if (registro==BancoRegistros.IR)
			{
				regs[registro].SetText(
					BancoRegistros.GetNombreRegistro(registro) + " " +
					Desensamblador.Desensamblar(newValue));
				etiqueta_inst.SetText (
					"Ejecutando:"+Desensamblador.Desensamblar(newValue));
			}
		}
		
		/// <summary>Se llama para inicializar el listener, pasándole un
		/// array con el contenido de todos los registros.</summary>
		/// <param name="newValues">Los valores almacenados en los registros.
		/// </param>
		
		public void RegisterChanged (short[] newValues)
		{
			for (int i=0; i < newValues.Length; i++)
			{
				RegisterChanged (i, newValues[i]);
			}
		}	
		
		/// <summary>Establece el texto de etiqueta_rmc con el contenido
		/// de la microinstrucción que se le pasa como parámetro.
		/// </summary>
		/// <param name="mic">La microinstruccion a escribir.</param>

		private void PintarMicro (MicroInstruccion mic)
		{
			etiqueta_rmc.SetText (
				"mInst = " + mic.ToHexString() +
				"\nAMUX="+ Conversiones.ToBinaryString (mic.GetAMUX(), 1) +
				"\nCOND="+ Conversiones.ToBinaryString (mic.GetCOND(), 3) +
				"\nSH="  + Conversiones.ToBinaryString (mic.GetSH()  , 3) +
				"\nMBR=" + Conversiones.ToBinaryString (mic.GetMBR() , 1) +
				"\nMAR=" + Conversiones.ToBinaryString (mic.GetMAR() , 1) +
				"\nRD="  + Conversiones.ToBinaryString (mic.GetRD()  , 1) +
				"\nWR="  + Conversiones.ToBinaryString (mic.GetWR()  , 1) +
				"\nENC=" + Conversiones.ToBinaryString (mic.GetENC() , 1) +
				"\nC="   + Conversiones.ToBinaryString (mic.GetC(),    4) +
				"\nB="   + Conversiones.ToBinaryString (mic.GetB(),    4) +
				"\nA="   + Conversiones.ToBinaryString (mic.GetA(),    4) +
				"\nADDR="+ Conversiones.ToBinaryString (mic.GetADDR(),10) +
				"\nFIR=" + Conversiones.ToBinaryString (mic.GetFIR() , 1) +
				"\nALU=" + Conversiones.ToBinaryString (mic.GetALU() , 4));					
		}
	}
}
