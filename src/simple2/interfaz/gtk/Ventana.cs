namespace simple2.interfaz.gtk
{

	using Gtk;
	using Gdk;
	using System;

	using System.IO;
	using System.Collections;

	using simple2.ensamblador;
	using simple2.rutaDeDatos;
	using simple2.hilos;
	using simple2.utilidades;
	using simple2.representacionRuta;

	/// <remarks>Esta clase es la ventana principal de la aplicación.
	/// </remarks>

	public class Ventana : Gtk.Window
	{
		///<summary>Botón para detener la simulación.</summary>
		
		private static Gtk.Button btnDetener = null;
		
		/// <summary>Botón para compilar y comenzar la simulación.</summary>
		
		private static Gtk.Button btnEjecutar = null;
		
		/// <summary>Botón para pausar la simulación.</summary>
		
		private static Gtk.ToggleButton btnPausar = null;
		
		/// <summary>Menuitem para compilar y comenzar la simulación.
		/// </summary>
		
		private static Gtk.ImageMenuItem itemEjecutar = null;
		
		/// <summary>MenuItem para detener la simulación.</summary>
		
		private static Gtk.ImageMenuItem itemDetener = null;
		
		/// <summary>MenuItem para pausar la simulación.</summary>
		
		private static Gtk.ImageMenuItem itemPausar = null;
		
		/// <summary>Hilo encargado de llamar a la simulación.</summary>
		
		private static HiloEjecucion hiloEjecucion = null;

		/// <summary>Diálogo para la selección de ficheros, en modo abrir.
		/// </summary>
		
		private static Gtk.FileSelection selectorAbrir = null;
		
		/// <summary>Diálogo para la selección de ficheros, en modo
		/// guardar.</summary>
		
		private static Gtk.FileSelection selectorGuardar = null;
		
		/// <summary>Barra de estado de la ventana.</summary>
		
		private Statusbar sb = null;

		/// <summary>Área de texto donde se introduce el código en 
		/// ensamblador.</summary>

		private Gtk.TextBuffer textoCodigo;
		
		/// <summary>Área de texto donde se muestran los errores producidos
		/// durante el ensamblado del código.</summary>
		
		private Gtk.TextBuffer textoErrores;
		
		/// <summary>Área de texto donde se muestra el resultado del 
		/// ensamblado del código.</summary>
		
		private Gtk.TextBuffer textoResultado;
		
		/// <summary>notebook</summary>
		
		private Gtk.Notebook notebook = null;

		/// <summary>Almacena el nombre del fichero que se está editando.
		/// </summary>
		
		private String _nombreFichero;
		
		/// <summary>El nombre del fichero que se está editando.</summary>
		
		private String nombreFichero
		{
			get
			{
				return _nombreFichero;
			}
			set
			{
				_nombreFichero = value;
				if (nombreFichero != null)
					Title = String.Format (GetText("Ventana_Titulo"),
							GetText	("Programa_Nombre"),
							_nombreFichero);
				else
					Title = String.
						Format (GetText("Ventana_Titulo"),
							GetText	("Programa_Nombre"),
							GetText ("Ventana_No_Archivo"));
			}
		}

		/// <summary>Panel para mostrar el contenido de la memoria.</summary>

		private PanelMemoria panelMemoria;
		
		/// <summary>Panel para mostrar el contenido de los registros.
		/// </summary>
		
		private PanelRegistros panelRegistros;
		
		/// <summary>Lienzo para dibujar la ruta de datos.</summary>
		
		private PanelDibujo dArea = null;
		
		/// <summary>Instancia única de esta clase (patrón sigleton)
		/// </summary>
		
		private static Ventana instancia = null;
		
		/// <summary>Crea una instancia de la clase.</summary>
		
		private Ventana ():base (Gtk.WindowType.Toplevel)
		{
			
			this.Icon = IconManager.GetPixmap ("gears.png");
			this.Title = String.Format (GetText ("Ventana_Titulo"),
					       GetText ("Programa_Nombre"),
					       GetText ("Ventana_No_Archivo"));

			this.SetDefaultSize (100, 100);
			this.DeleteEvent += new DeleteEventHandler (VentanaSalir);
			this.CrearComponentes ();
			GLib.Timeout.Add (
				50, 
				new GLib.TimeoutHandler(dArea.ProcesarEventosPendientes));
		}
				
		/// <summary>Obtiene la instancia única de esta clase (patrón
		/// sigleton).</summary>
		/// <returns>La única instancia de esta clase.</returns>
		
		public static Ventana GetInstance()
		{
			if (instancia == null)
				instancia = new Ventana();
			return instancia;
		}
				
		/// <summary>Obtiene el texto localizado (traducido) para 
		/// la clave dada.</summary>
		/// <param name="clave">La clave del texto.</param>
		/// <returns>El texto localizado.</returns>
		
		public static String GetText (String clave)
		{
			return TextManager.GetText (clave);

		}
		
		/// <summary>Crea los elementos de la ventana.</summary>

		private void CrearComponentes ()
		{
			Gtk.VBox vbox = new VBox (false, 2);
			Gtk.VBox menubox = new VBox (false, 0);

			// Barra de menú.
			MenuBar mb = CrearMenuBar ();
			menubox.PackStart (mb, false, false, 0);

			// Barra de herramientas         
			Gtk.Toolbar toolbar = CrearToolBar ();
			menubox.PackStart (toolbar, false, false, 0);


			vbox.PackStart (menubox, false, false, 0);

			notebook = CrearNotebook ();
			vbox.PackStart (notebook, true, true, 0);

			sb = new Statusbar ();
			vbox.PackEnd (sb, false, false, 0);

			Add (vbox);

		}

		/// <summary>Crea la barra de menú de la aplicación.</summary>
		/// <returns>La barra de menú de la aplicación.</returns>

		private MenuBar CrearMenuBar ()
		{

			Gtk.AccelGroup acel = new Gtk.AccelGroup ();

			ImageMenuItem item = null;

			//---
			// Elementos del menú Archivo.
			
			Menu menuArchivo = new Menu ();

			MenuItem menuArchivoItem =
				new
				MenuItem (GetText ("Ventana_Menu_Archivo"));
			menuArchivoItem.Submenu = menuArchivo;

			item = new ImageMenuItem (Gtk.Stock.New, acel);
			item.Activated += new EventHandler (VentanaNuevo);
			menuArchivo.Append (item);

			item = new ImageMenuItem (Gtk.Stock.Open, acel);
			item.Activated += new EventHandler (VentanaAbrir);
			menuArchivo.Append (item);

			item = new ImageMenuItem (Gtk.Stock.Save, acel);
			item.Activated += new EventHandler (VentanaGuardar);
			menuArchivo.Append (item);

			item = new ImageMenuItem (Gtk.Stock.SaveAs, acel);
			item.Activated += new EventHandler (VentanaGuardarComo);
			menuArchivo.Append (item);

			menuArchivo.Append (new Gtk.SeparatorMenuItem ());

			item = new ImageMenuItem (Gtk.Stock.Quit, acel);
			item.Activated += new EventHandler (VentanaSalir);
			menuArchivo.Append (item);


			//--
			// Elementos del menú de opciones.
			
			Menu menuOpciones = new Menu();
			MenuItem menuOpcionesItem = 
				new MenuItem (GetText("Ventana_Menu_Opciones"));
			menuOpcionesItem.Submenu = menuOpciones;
			
			item = new ImageMenuItem (Gtk.Stock.Preferences, acel);
			item.Activated += new EventHandler (VentanaOpciones);
			
			menuOpciones.Append(item);

			//--
			// Elementos del menú proyecto.
			
			Menu menuProyecto = new Menu ();

			MenuItem menuProyectoItem =
				new	MenuItem (GetText ("Ventana_Menu_Proyecto"));
			menuProyectoItem.Submenu = menuProyecto;

			itemEjecutar = 
				new ImageMenuItem (Gtk.Stock.Execute, new AccelGroup ());
			itemEjecutar.Activated += new EventHandler (VentanaEjecutar);
			itemEjecutar.Image = 
					new Gtk.Image (IconManager.GetPixmap("run16.png"));
			itemEjecutar.AddAccelerator (
				"activate", 
				acel, 
				65474, 
				Gdk.ModifierType.LockMask, 
				Gtk.AccelFlags.Visible);
			menuProyecto.Append (itemEjecutar);
			
			itemPausar = new ImageMenuItem(GetText("Ventana_Pausar"));
			itemPausar.Activated += new EventHandler (VentanaPausar);
			itemPausar.Image = 
				new Gtk.Image (IconManager.GetPixmap("pausar32.png"));
			itemPausar.Sensitive = false;
			itemPausar.AddAccelerator (
				"activate",
				acel,
				65475,
				Gdk.ModifierType.LockMask,
				Gtk.AccelFlags.Visible);
			menuProyecto.Append (itemPausar);
				
			
			itemDetener = new ImageMenuItem (Gtk.Stock.Stop, new AccelGroup());
			itemDetener.Activated += new EventHandler (VentanaDetener);
			itemDetener.Sensitive = false;
			itemDetener.AddAccelerator (
				"activate", 
				acel, 
				65476, 
				Gdk.ModifierType.LockMask, 
				Gtk.AccelFlags.Visible);
			menuProyecto.Append (itemDetener);
							
			//---	
			// Elementos del menú Ayuda			
			
			Menu menuAyuda = new Menu ();

			MenuItem menuAyudaItem =
				new MenuItem (GetText ("Ventana_Menu_Ayuda"));
			menuAyudaItem.Submenu = menuAyuda;

			item = new ImageMenuItem (Gtk.Stock.Help, acel);
			item.Activated += new EventHandler (VentanaAyuda);
			menuAyuda.Append (item);


			item = new	ImageMenuItem (GetText ("Ventana_Menu_AcercaDe"), 
						new AccelGroup ());
			item.Image = new Gtk.Image (Gtk.Stock.Refresh, IconSize.Menu);
			item.Activated += new EventHandler (VentanaAcerca);
			item.Image = new Gtk.Image (IconManager.GetPixmap("about16.png"));

			menuAyuda.Append (item);



			//--
			AddAccelGroup (acel);
			//--


			MenuBar mb = new MenuBar ();

			mb.Append (menuArchivoItem);
			mb.Append (menuOpcionesItem);
			mb.Append (menuProyectoItem);
			mb.Append (menuAyudaItem);

			return mb;

		}
		
		/// <summary>Crea la barra de herramientas de la ventana.</summary>
		/// <returns>La barra de herramientas.</returns>
		
		private Gtk.Toolbar CrearToolBar ()
		{
			Gtk.Toolbar barra = new Gtk.Toolbar ();
			barra.ToolbarStyle = ToolbarStyle.Icons;

			Gtk.Button btn = null;
			btn = (Gtk.Button) barra.InsertStock (
						Gtk.Stock.New,
						GetText("Ventana_ToolTip_Nuevo"),
						String.Empty,
						new Gtk.SignalFunc (VentanaNada),
						IntPtr.Zero,
						-1);
			btn.Clicked += new EventHandler (VentanaNuevo);

			btn = (Gtk.Button) barra.InsertStock (
						Gtk.Stock.Open,
						GetText("Ventana_ToolTip_Abrir"),
						String.Empty,
						new Gtk.SignalFunc (VentanaNada),
						IntPtr.Zero,
						-1);
			btn.Clicked += new EventHandler (VentanaAbrir);
		

			btn = (Gtk.Button) (barra.InsertStock (
						Gtk.Stock.Save,
						GetText
						("Ventana_ToolTip_Guardar"),
						String.Empty,
						new Gtk.SignalFunc (VentanaNada),
						IntPtr.Zero,
						-1));
			btn.Clicked += new EventHandler (VentanaGuardar);

			barra.AppendSpace ();

			btnEjecutar = (Gtk.Button) barra.AppendItem( 
						GetText ("Ventana_Ejecutar"), 
						GetText("Ventana_ToolTip_Ejecutar"), 
						String.Empty,
						new Gtk.Image (IconManager.GetPixmap("run32.png")), 
						new Gtk.SignalFunc (VentanaNada));
			btnEjecutar.Clicked += new EventHandler (VentanaEjecutar);
			
			btnEjecutar.Sensitive = true;


			//--
		
			btnPausar = new Gtk.ToggleButton ();
			btnPausar.Add (
				new Gtk.Image(IconManager.GetPixmap("pausar32.png")));
			btnPausar.Clicked += new EventHandler (VentanaPausar);
			btnPausar.Sensitive = false;
			barra.AppendWidget (btnPausar, 
				GetText("Ventana_ToolTip_Pausar"), 
				String.Empty);

			//--
			btnDetener = (Gtk.Button) barra.InsertStock (
							Gtk.Stock.Stop,
							GetText ("Ventana_ToolTip_Detener"),
							String.Empty,
							new Gtk.SignalFunc (VentanaNada),
							IntPtr.Zero,
							-1);
			btnDetener.Clicked += new EventHandler (VentanaDetener);
			btnDetener.Sensitive=false;
			//--

			barra.AppendSpace ();

			btn = (Gtk.Button) barra.InsertStock (
							Gtk.Stock.Quit,
							GetText("Ventana_ToolTip_Salir"),
							String.Empty,
							new Gtk.SignalFunc(VentanaNada),
							IntPtr.Zero,
							-1);
			btn.Clicked += new EventHandler (VentanaSalir);

			return barra;
		}

		/// <summary>Crea el notebook donde están los campos
		/// de edición de texto del ensamblador.</summary>
		/// <returns>El notebook.</returns>
	
		private Notebook CrearNotebook ()
		{
			Notebook not = new Notebook ();

			
			Gtk.HPaned panel1 = new Gtk.HPaned();
			panel1.Add1 (CrearEditorEnsamblador());
			panel1.Add2 (CrearResultadoEnsamblador());
			panel1.Position = 340;
			
			Gtk.VPaned panel2 = new Gtk.VPaned ();
			panel2.Add1 (panel1);
			panel2.Add2 (CrearErroresEnsamblador());
			panel2.Position = 350;
			
			not.AppendPage (
				panel2, 
				new Gtk.Label(GetText("Ventana_Notebook_Codigo"))
			);

			panelMemoria = new PanelMemoria();
			panelRegistros = new PanelRegistros();
			
			Gtk.Table memreg = new Gtk.Table (1,2, true);
			memreg.Attach(panelMemoria, 0, 1, 0, 1);
			memreg.Attach(panelRegistros, 1,2,0,1);
			not.AppendPage (
				memreg,
				new Gtk.Label (
					String.Format (
						"{0} - {1}",
						GetText ("Ventana_Notebook_Memoria"),
						GetText ("Ventana_Notebook_Registros")
					)
				)
			);


			dArea = new PanelDibujo();
			not.AppendPage (
				dArea, 
				new Gtk.Label(GetText("Ventana_Notebook_rdd"))
			);

			return not;
		}
		
		/// <summary>Crea el editor para el código en ensamblador.</summary>
		/// <returns>Un widget que contiene el campo de texto para
		/// editar código ensamblador.</returns>
		
		private Gtk.Widget CrearEditorEnsamblador ()
		{
			textoCodigo =
				new Gtk.TextBuffer ((Gtk.TextTagTable) null);
			Gtk.TextView view = new Gtk.TextView (textoCodigo);
			Gtk.ScrolledWindow sw = new Gtk.ScrolledWindow ();
			sw.Add (view);
			textoCodigo.MarkSet +=
				new Gtk.MarkSetHandler (ActualizadaPosicionCursor);
			return sw;
		}
		
		/// <summary>Crea el editor para mostrar los errores producidos
		/// al ensamblar el código en ensamblador.</summary>
		/// <returns>Un widget con el campo de texto para mostrar
		/// los errores y advertencias del ensamblador.</returns>
		
		private Gtk.Widget CrearErroresEnsamblador ()
		{
			textoErrores =
				new Gtk.TextBuffer ((Gtk.TextTagTable) null);
			Gtk.TextView view = new Gtk.TextView (textoErrores);
			view.Editable = false;

			Gtk.ScrolledWindow sw = new Gtk.ScrolledWindow ();
			sw.Add (view);
			return sw;
		}
		
		/// <summary>Crea el editor para mostrar los resultados producidos
		/// al ensamblar.</summary>
		/// <returns>Un widget con el campo de texto para mostrar los 
		/// resultados del ensamblador.</returns>
		
		private Gtk.Widget CrearResultadoEnsamblador ()
		{
			textoResultado =
				new Gtk.TextBuffer ((Gtk.TextTagTable) null);
			Gtk.TextView view = new Gtk.TextView (textoResultado);
			view.Editable = false;

			Gtk.ScrolledWindow sw = new Gtk.ScrolledWindow ();
			sw.Add (view);

			return sw;
		}
		
		/// <summary>Escribe un mensaje en la barra de estado de
		/// la ventana.</summary>
		/// <param name="mensaje">El mensaje a mostrar.</param>

		private void PonerMensajeStatusbar (String mensaje)
		{
			sb.Pop (0);
			sb.Push (0, mensaje);
		}

		/// <summary>Oculta una ventana.</summary>
		/// <param name="o">El objeto que llama a la función.</param>
		/// <param name="args">Los argumentos.</param>


		public static void OcultarVentana (object o,  DeleteEventArgs args)
		{
			Gtk.Window d = (Gtk.Window) o;
			d.Hide ();
			args.RetVal = true;
		}

		/// <summary>Función para actualizar el texto de la barra de estado
		/// cuando cambia la posición del cursor en el cuadro de edición de
		/// texto ensamblador.</summary>
		/// <param name="o">El objeto que provoca la llamada.</param>
		/// <param name="args">Los argumentos.</param>
		
		private void ActualizadaPosicionCursor 
			(object o, Gtk.MarkSetArgs args)
		{

			Gtk.TextMark mark = textoCodigo.InsertMark;
			Gtk.TextIter iter = textoCodigo.GetIterAtMark (mark);

			int lineasTotales = textoCodigo.LineCount;
			int caractTotales = textoCodigo.CharCount;
			int lineaActual = iter.Line + 1;
			int columnaActual = iter.LineOffset + 1;
			int offset = iter.Offset;
			int porc = 
				(caractTotales < 1) ? 0 : (100 * offset) / caractTotales;
			String mensaje = String.Format (
						GetText ("Ventana_Statusbar_Texto"),
						lineaActual, 
						lineasTotales,
						columnaActual, 
						offset, 
						porc);
			if (textoCodigo.Modified)
				mensaje += " *";
			PonerMensajeStatusbar (	mensaje );
		}
		
		/// <summary>Función que no hace nada.</summary>
		
		private void VentanaNada ()
		{
			return;
		}
		
		/// <summary>Función que se llama al pulsar sobre salir.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		private void VentanaSalir (object o, EventArgs args)
		{
			VentanaSalir ();
		}
		
		/// <summary>Función que se llama al pulsar sobre salir.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		private void VentanaSalir (object o, DeleteEventArgs args)
		{
			VentanaSalir ();
			args.RetVal = true;
		}
		
		/// <summary>Función que se llama al pulsar sobre salir.
		/// Si el código introducido en el campo de edición no ha sido
		/// guardado pregunta antes de cerrar la aplicación.</summary>
		
		private void VentanaSalir ()
		{
			if (textoCodigo.Modified)
			{
				MessageDialog m =
					new MessageDialog (this,
							   Gtk.DialogFlags.Modal,
							   Gtk.MessageType.Question,
							   Gtk.ButtonsType.YesNo,
							   GetText("Ventana_SalirModificado"));

				int respuesta = m.Run ();
				m.Hide ();
				m = null;
				if (!(respuesta == (int) Gtk.ResponseType.Yes))
				{
					return;
				}
			}
			if ((hiloEjecucion != null) && (hiloEjecucion.IsAlive()))
			{
				MessageDialog m = 
					new MessageDialog (this,
							Gtk.DialogFlags.Modal,
							Gtk.MessageType.Question,
							Gtk.ButtonsType.YesNo,
							GetText("Ventana_SalirEjecucion"));
				int respuesta = m.Run();
				m.Hide();
				m = null;
				if (!(respuesta == (int) Gtk.ResponseType.Yes))
				{
					return;
				}
			}
			Application.Quit ();
			System.Environment.Exit(0);
		}
		
		/// <summary>Crea un nuevo documento.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		private void VentanaNuevo (object o, EventArgs args)
		{
			VentanaNuevo ();
		}
		
		/// <summary>Crea un nuevo documento.</summary>
		
		private void VentanaNuevo ()
		{

			if (textoCodigo.Modified)
			{
				MessageDialog m = new MessageDialog (
					this,
					Gtk.DialogFlags.Modal,
					Gtk.MessageType.Question,
					Gtk.ButtonsType.YesNo,
					GetText ("Ventana_NuevoModificado"));
					

				int respuesta = m.Run ();
				m.Hide ();
				m = null;
				if (! (respuesta == (int) Gtk.ResponseType.Yes))
				{
					return;
				}
			}
			nombreFichero = null;
			textoCodigo.Text = "";
			textoCodigo.Modified = false;
			textoErrores.Text = "";
			textoResultado.Text = "";
		}

		/// <summary>Guarda el documento actual.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>

		private void VentanaGuardar (object o, EventArgs args)
		{
			VentanaGuardar ();
		}
		
		/// <summary>Guarda el documento actual.</summary>
		
		private void VentanaGuardar ()
		{
			if ((nombreFichero == null) && textoCodigo.Modified)
			{
				VentanaGuardarComo ();
				return;
			}
			if (!textoCodigo.Modified)
				return;

			Fichero.GuardarTexto (nombreFichero, textoCodigo.Text);

			textoCodigo.Modified = false;

		}

		/// <summary>Guarda el documento actual, preguntando el nombre
		/// del fichero en el que se quiere guardar.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>

		private void VentanaGuardarComo (object o, EventArgs args)
		{
			VentanaGuardarComo ();
		}
		
		/// <summary>Guarda el documento actual, preguntando el nombre del
		/// del fichero en el que se quiere guardar.</summary>
		
		private void VentanaGuardarComo ()
		{

			if (selectorGuardar == null)
			{
				selectorGuardar =
					new Gtk.FileSelection (GetText("Ventana_DialogoG"));
				selectorGuardar.Modal = true;
				selectorGuardar.OkButton.Clicked +=
					new
					EventHandler
					(SelectorGuardarOKPulsado);
				selectorGuardar.CancelButton.Clicked +=
					new
					EventHandler (SelectorCancelPulsado);
				selectorGuardar.DeleteEvent +=
					new
					DeleteEventHandler (OcultarVentana);
				selectorGuardar.Icon =
					IconManager.GetPixmap ("gears.png");

			}
			selectorGuardar.ShowAll ();
		}

		/// <summary>Carga un documento de texto.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>

		private void VentanaAbrir (object o, EventArgs args)
		{
			VentanaAbrir ();
		}

		/// <summary>Carga un documento de texto.</summary>
		
		private void VentanaAbrir ()
		{

			if (textoCodigo.Modified)
			{
				MessageDialog m = new MessageDialog (this,
					   Gtk.DialogFlags.Modal,
					   Gtk.MessageType.Question,
					   Gtk.ButtonsType.YesNo,
					   GetText ("Ventana_AbrirModificado"));

				int respuesta = m.Run ();
				m.Hide ();
				m = null;
				if (!(respuesta == (int) Gtk.ResponseType.Yes))
				{
					return;
				}
			}

			if (selectorAbrir == null)
			{
				selectorAbrir =
					new Gtk.FileSelection (GetText  ("Ventana_DialogoA"));
				selectorAbrir.Modal = true;
				selectorAbrir.OkButton.Clicked +=
					new	EventHandler (SelectorAbrirOKPulsado);
				selectorAbrir.CancelButton.Clicked +=
					new	EventHandler (SelectorCancelPulsado);
				selectorAbrir.DeleteEvent +=
					new	DeleteEventHandler (OcultarVentana);
				selectorAbrir.Icon = IconManager.GetPixmap ("gears.png");

			}
			selectorAbrir.ShowAll ();
		}

		/// <summary>Función que se ejecuta cuando se pulsa sobre el botón
		/// ok del selector de ficheros a guardar. Pregunta el nombre del
		/// archivo en el que queremos guardar el texto y lo guarda.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="args"></param>

		private void SelectorGuardarOKPulsado (object o, EventArgs args)
		{

			if (Directory.Exists (selectorGuardar.Filename))
			{
				MessageDialog m =
					new MessageDialog (this,
							   Gtk.DialogFlags.Modal,
							   Gtk.MessageType.Warning,
							   Gtk.ButtonsType.Close,
							   GetText("Ventana_SeleccionarFichero"));
				m.Run ();
				m.Hide ();
				return;
			}
			if (File.Exists (selectorGuardar.Filename))
			{
				MessageDialog m =
					new MessageDialog (this,
							   Gtk.DialogFlags.Modal,
							   Gtk.MessageType.Question,
							   Gtk.ButtonsType.YesNo,
							   GetText("Ventana_SobreescribirArchivo"));
				int respuesta = m.Run ();
				m.Hide ();
				if (!
				    (respuesta == (int) Gtk.ResponseType.Yes))
				{
					return;
				}

			}
			try
			{
				if (!File.Exists (selectorGuardar.Filename))
				{
					FileStream f =	File.Create (selectorGuardar.Filename);
					f.Close ();
				}

			}
			catch (Exception)
			{
				MessageDialog m1 =
					new MessageDialog (this,
							   Gtk.DialogFlags.Modal,
							   Gtk.MessageType.Error,
							   Gtk.ButtonsType.Close,
							   GetText  ("Ventana_ErrorCrearFichero"));
				m1.Run ();
				m1.Hide ();
				return;
			}
			try
			{
				File.SetCreationTime (selectorGuardar.Filename, DateTime.Now);
			}
			catch (Exception)
			{
				MessageDialog m2 =
					new MessageDialog (this,
							   Gtk.DialogFlags.Modal,
							   Gtk.MessageType.Error,
							   Gtk.ButtonsType.Close,
							   GetText  ("Ventana_ErrorEscritura"));
				m2.Run ();
				m2.Hide ();
				return;

			}

			//Guardar el texto donde nos digan y poner nombre fichero a eso.
			nombreFichero = selectorGuardar.Filename;

			selectorGuardar.Hide ();
			textoCodigo.Modified = true;
			this.VentanaGuardar();
		
		}
		
		/// <summary>Función que se llama al pulsar sobre el botón ok
		/// del selector de ficheros para abrir. Carga el fichero
		/// seleccionado.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		private void SelectorAbrirOKPulsado (object o, EventArgs args)
		{
			if (!File.Exists (selectorAbrir.Filename))
			{
				MessageDialog m =
					new MessageDialog (this,
							   Gtk.DialogFlags.Modal,
							   Gtk.MessageType.Warning,
							   Gtk.ButtonsType.Close,
							   GetText ("Ventana_ErrorNoFichero"));
				m.Run ();
				m.Hide ();
				return;
			}

			String texto = null;
			try
			{
				texto = Fichero.CargarTexto (selectorAbrir.Filename);
			}
			catch (Exception ex)
			{
				selectorAbrir.Hide ();

				MessageDialog m =
					new MessageDialog (this,
							   Gtk.DialogFlags.Modal,
							   Gtk.MessageType.Error,
							   Gtk.ButtonsType.Close,
							   GetText  ("Ventana_ErrorCargarFichero"));
				m.Run ();

				m.Hide ();
				m = null;

				return;

			}
			textoCodigo.Text = texto;
			textoCodigo.PlaceCursor (textoCodigo.StartIter);
			textoCodigo.Modified = false;
			textoErrores.Text = "";
			textoResultado.Text = "";
			
			nombreFichero = selectorAbrir.Filename;
			notebook.Page = 0;
			selectorAbrir.Hide ();

		}

		/// <summary>Oculta los diálogos de selección de ficheros.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		private void SelectorCancelPulsado (object o, EventArgs args)
		{
			if (selectorAbrir != null)
				selectorAbrir.Hide ();
			if (selectorGuardar != null)
				selectorGuardar.Hide ();
		}
		
		/// <summary>Muestra la ayuda.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		private void VentanaAyuda (object o, EventArgs args)
		{
			simple2.interfaz.gtk.VentanaAyuda.GetInstance().ShowAll();
		}

		/// <summary>Muestra el diálogo de configuración de la
		/// aplicación.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		private void VentanaOpciones(object o, EventArgs args)
		{
			DialogoConfiguracion.GetInstance().ShowAll();
		}

		/// <summary>Muestra el diálogo Acerca de...</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		private void VentanaAcerca (object o, EventArgs args)
		{
			DialogoAcerca.GetInstance().ShowAll();
		}

		/// <summary>Pausa la simulación.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		private void VentanaPausar (object o, EventArgs args)
		{
			
			if (hiloEjecucion != null)
			{
				if (btnPausar.Active == hiloEjecucion.GetPausado())
				{
					btnPausar.Active = !btnPausar.Active;
				}
				else
				{
					hiloEjecucion.CambiarPausado();
				}				
			}
		}
			
		/// <summary>Ensambla el código y, si no hay errores, comienza
		/// la simulación.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		private void VentanaEjecutar (object o, EventArgs args)
		{
			VentanaEjecutar ();
		}
		
		/// <summary>Ensambla el código y, si no hay errores, comienza
		/// la simulación.</summary>
		
		private void VentanaEjecutar ()
		{
			//Compilamos
			btnEjecutar.Sensitive = false;
			itemEjecutar.Sensitive = false;
			EnsambladorSimple2 e = new EnsambladorSimple2 ();
			short[] ens=null;
			try
			{
				ArrayList codigoLimpio =
					e.PrimeraPasada (textoCodigo.Text);
				ens = e.Ensamblar (codigoLimpio);
				String res = "";
				for (int i = 0; i < ens.Length; i++)
				{
					res += Conversiones.
						ToHexString (ens[i]) + "(" +
						(String) codigoLimpio[i] +
						")\n";
				}
				textoResultado.Text = res;
				textoErrores.Text = "";
				String advert = e.GetAdvertencias ();
				if ((advert != "") && 
					(Opciones.GetInstance().GetMostrarAdvertencias()))
				{
					textoErrores.Text += "\n" + 
						GetText ("Ens_Advertencias") +
						"\n\n" + e.GetAdvertencias ();
				}

			}
			catch (ErrorCodigoException ex)
			{
				textoErrores.Text = GetText ("Ens_Errores") +
									"\n" + ex.Message;
				textoResultado.Text = "";
				String advert = e.GetAdvertencias ();
				if ((advert != "") && 
					(Opciones.GetInstance().GetMostrarAdvertencias()))
				{
					textoErrores.Text +=" \n" + 
						GetText ("Ens_Advertencias") + "\n\n" + advert;
				}
				btnEjecutar.Sensitive = true;
				itemEjecutar.Sensitive = true;
				PonerMensajeStatusbar (GetText ("Ventana_Error_Ensamblar"));
				notebook.Page = 0;
				return;
			}
			
			//Si la compilación ha salido bien, entondes ejecutamos.
			
			
			MemoriaControl mc  = new MemoriaControl ();
			
			if (!Opciones.GetInstance().GetUsarMemoriaDefecto())
			{
				try
				{
					mc = MemoriaControl.CreateFromString (
						Fichero.CargarTexto (
							Opciones.GetInstance().GetMemoriaAlternativa()
						)
					);
				}
				catch (Exception)
				{
					Hilo.Sleep(50);
				}
			}
			
			SecuenciadorMicroprograma mic =	
				new SecuenciadorMicroprograma (ens, mc);
			mic.AddMemoryChangeListener (panelMemoria);
			mic.AddRegisterChangeListener (panelRegistros);
			RepresentacionRDD repRDD = new RepresentacionRDD(dArea);
			mic.SetRepresentacionRDD(repRDD);
			mic.AddRegisterChangeListener(repRDD);
			
			notebook.Page = 2;
			Hilo.Sleep (100);
			PonerMensajeStatusbar (GetText("Ventana_Simulacion_Curso"));
			
			hiloEjecucion = new HiloEjecucion(mic);
			hiloEjecucion.SetTSubciclo (Opciones.GetInstance().GetTSubciclo());
			hiloEjecucion.Start();
			Hilo hiloespera = new HiloEspera ();
			hiloespera.Start();

		}
		
		/// <summary>Detiene la simulación.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		private void VentanaDetener (object o, EventArgs args)
		{
			VentanaDetener();
		}
		
		/// <summary>Detiene la simulación.</summary>
		
		private void VentanaDetener ()
		{
			if ((hiloEjecucion != null) && (hiloEjecucion.IsAlive()))
			{
				hiloEjecucion.Abort();
			}
			btnPausar.Active = false;
		}
		
		/// <remarks>Esta clase se encarga de activar y desactivar los 
		/// botones de ejecución, pausa y parada según el estado de la
		/// simulación.</remarks>
		
		class HiloEspera : Hilo
		{
			
			/// <summary>Crea una instancia de la clase.</summary>
			
			public HiloEspera () : base()
			{
			}
			
			/// <summary>Función que se ejecuta cuando se lanza el hilo.
			/// En primera instancia deshabilita ejecutar, y habilita detener
			/// y pausar.  Espera a que termine la simulación y, cuando esto
			/// ocurre, habilita ejecutar y deshabilita detener y pausar.
			/// </summary>
			
			public override void Run ()
			{
				btnDetener.Sensitive = true;
				itemDetener.Sensitive = true;
				btnPausar.Sensitive = true;
				itemPausar.Sensitive = true;
				btnEjecutar.Sensitive = false;
				itemEjecutar.Sensitive = false;
				
				
				if (hiloEjecucion != null)
					hiloEjecucion.Join();
				btnDetener.Sensitive = false;
				itemDetener.Sensitive = false;
				btnPausar.Sensitive = false;
				itemPausar.Sensitive = false;
				btnEjecutar.Sensitive = true;
				itemEjecutar.Sensitive = true;
				
				Ventana.GetInstance().PonerMensajeStatusbar
					(GetText ("Ventana_Simulacion_Fin"));
				
			}
		}
	}
}
