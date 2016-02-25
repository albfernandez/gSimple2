namespace simple2.interfaz.gtk
{
	
	using Gtk;
	using Gdk;
	using GLib;
	using GtkSharp;
	using System;
	using System.IO;
	
	using simple2.hilos;
	using simple2.rutaDeDatos;
	using simple2.utilidades;
	
	/// <remarks>Esta clase es el diálogo de configuración de la
	/// aplicación.</remarks>
	
	public class DialogoConfiguracion : Gtk.Dialog
	{
		
		/// <summary>Unica instancia de esta clase (patrón Singleton)
		/// </summary>
		
		private static DialogoConfiguracion instancia = null;
		
		/// <summary>Botón cancelar. Cuando se pulsa sobre él se oculta el
		/// diálogo y no se aplican los cambios.</summary>
		
		private Gtk.Button btnCancelar;
		
		/// <summary>Botón aceptar. Cuando se pulsa sobre él se comprueba
		/// la validez de las opciones, se aplican los cambios y se oculta
		/// el diálogo.</summary>
		
		private Gtk.Button btnAceptar;
		
		/// <summary>CheckButton para seleccionar si se muestran o no
		/// las advertencias generadas por el ensamblador.</summary>
		
		private Gtk.CheckButton cbAdvertencias = null;
		
		/// <summary>Campo de texto para introducir el tiempo de subciclo.
		/// </summary>
		
		private Gtk.SpinButton sbTiempo = null;
		
		/// <summary>RadioButton.  Si está seleccionado, se usará la memoria
		/// de control por defecto.</summary>
		
		private Gtk.RadioButton rbMemoriaDef = null;
		
		/// <summary>RadioButton. Si está seleccionado, se usará la memoria
		/// de control indicada por el usuario.</summary>
		
		private Gtk.RadioButton rbMemoriaUsu = null;
		
		/// <summary>Campo de texto para introducir la ruta del fichero donde
		/// se encuentra la memoria de control del usuario.</summary>
		
		private Gtk.Entry lblMemoriaAlt = null;
				
		/// <summary>Selector de ficheros para seleccionar el fichero que 
		/// contiene la memoria de control.</summary>
		
		private Gtk.FileSelection selectorFicheros = null;

		/// <summary>Botón para mostrar el selector de ficheros.</summary>
		
		private Gtk.Button btnFichero = null;

		/// <summary>Obtiene la única instancia de la clase (patrón
		/// Singleton).</summary>
		
		public static DialogoConfiguracion GetInstance()
		{
			if (instancia == null)
				instancia = new DialogoConfiguracion (Ventana.GetInstance());
			return instancia;
		}

		/// <summary>Crea una instancia de la clase.</summary>
		/// <param name="parent">La ventana padre de este diálogo.</param>
		
		private DialogoConfiguracion (Gtk.Window parent):
			base ("", parent, Gtk.DialogFlags.DestroyWithParent)
		{
			Title = Ventana.GetText ("DialogoConfiguracion_Title");
			TransientFor = parent;
			Modal = true;
			Resizable = false;
			DeleteEvent += new DeleteEventHandler (Ventana.OcultarVentana);
			Resize (300, 300);
			BorderWidth = 10;
			HasSeparator = true;
			VBox.Add (this.CrearNotebook());
			ActionArea.Add(this.CrearPanelInferior());
		}
		
		/// <summary>Crea las hojas de las opciones, la parte central
		/// del diálogo.</summary>
		/// <returns>El notebook de las opciones.</returns>
		
		private Gtk.Widget CrearNotebook ()
		{
			Gtk.Notebook notebook = new Gtk.Notebook();
			notebook.BorderWidth = 10;
			
			// Panel de configuración del ensamblador.
			
			cbAdvertencias = new Gtk.CheckButton (
				Ventana.GetText ("D_Conf_MostrarAdv"));
			
			Gtk.Frame frmEnsamblador = 
				new Gtk.Frame (Ventana.GetText("D_Conf_Ensamblador"));
			VBox panelEnsamblador = new VBox (false, 5);
			panelEnsamblador.PackStart (cbAdvertencias);
			frmEnsamblador.Add (panelEnsamblador);
			
			// Panel de Memoria de control.
			
			rbMemoriaDef = new RadioButton (null, 
				Ventana.GetText("D_Conf_MemDef"));
			rbMemoriaUsu = new RadioButton (rbMemoriaDef, 
				Ventana.GetText("D_Conf_MemUsu"));
			
			rbMemoriaDef.Toggled += new EventHandler (rbToggled1);
			rbMemoriaUsu.Toggled += new EventHandler (rbToggled2);
			lblMemoriaAlt = new Gtk.Entry("");
			lblMemoriaAlt.Sensitive = false;
			btnFichero = new Gtk.Button (Ventana.GetText("D_Conf_Explorar"));
			btnFichero.Clicked += new EventHandler (btnFicheroClicked);
			
			Gtk.Frame frmMemoria = 
				new Gtk.Frame (Ventana.GetText ("D_Conf_Memoria"));
			VBox panelMemoria = new Gtk.VBox (false, 5);
			panelMemoria.PackStart (rbMemoriaDef);
			panelMemoria.PackStart (rbMemoriaUsu);
			panelMemoria.PackStart (lblMemoriaAlt);
			panelMemoria.PackStart (btnFichero);
			frmMemoria.Add (panelMemoria);
			

			// Panel del simulador.
			
			Gtk.Frame frmSimulador = 
				new Gtk.Frame (Ventana.GetText ("D_Conf_Simulador"));
			VBox panelSimulador = new VBox (false, 5);
			panelSimulador.PackStart (
				new Gtk.Label (Ventana.GetText("D_Conf_Tiempo")));
			
			sbTiempo = new Gtk.SpinButton ( 
				new Adjustment(1000.0, 50.0, 5000.0, 10.0, 100.0, 1.0),
				1.0,
				0);
			sbTiempo.Numeric = true;
			
			panelSimulador.PackStart(sbTiempo);
			frmSimulador.Add (panelSimulador);
			
			
			//  ----
			
			notebook.AppendPage (
				frmSimulador,
				new Gtk.Label (Ventana.GetText ("D_Conf_Simulador")));
			
			notebook.AppendPage (
				frmEnsamblador,
				new Gtk.Label (Ventana.GetText ("D_Conf_Ensamblador")));
			
			notebook.AppendPage (
				frmMemoria,
				new Gtk.Label (Ventana.GetText ("D_Conf_Memoria")));		

			return notebook;
		}
		
		/// <summary>Crea el panel inferior del diálogo, el que contiene
		/// los botones aceptar y cancelar.</summary>
		/// <returns>El panel creado.</returns>
		
		private Gtk.Widget CrearPanelInferior ()
		{
			Gtk.HBox hbox = new Gtk.HBox (true, 10);
			btnCancelar = new Gtk.Button(Gtk.Stock.Cancel);
			btnAceptar  = new Gtk.Button(Gtk.Stock.Ok);
			hbox.PackStart (btnAceptar);
			hbox.PackStart (btnCancelar);
			
			btnCancelar.Clicked += new EventHandler (CancelarClick);
			btnAceptar.Clicked += new EventHandler (AceptarClick);
			return hbox;
		}
		
		/// <summary>Función que se ejecuta al pulsar sobre el botón
		/// para seleccionar fichero. Muestra el diálogo de selección
		/// de archivos.</summary>
		/// <param name="o">El objeto que llama a la función.</param>
		/// <param name="args">Los argumentos que se le pasan a la 
		/// función.</param>
		
		private void btnFicheroClicked (object o, System.EventArgs args)
		{
			if (selectorFicheros == null)
			{
				selectorFicheros = 	new Gtk.FileSelection (
						Ventana.GetText ("D_Conf_Selector"));
				selectorFicheros.Modal = true;
				selectorFicheros.OkButton.Clicked +=
					new EventHandler (SelectorFicherosOkPulsado);
				selectorFicheros.CancelButton.Clicked +=
					new EventHandler (SelectorFicherosCancelPulsado);
				selectorFicheros.DeleteEvent +=
					new DeleteEventHandler (Ventana.OcultarVentana);
				selectorFicheros.Icon = IconManager.GetPixmap ("gears.png");
			}
			selectorFicheros.ShowAll();			
		}
		
		/// <summary>Función que se ejecuta al pulsar sobre el botón aceptar
		/// del selector de ficheros.</summary>
		/// <param name="o">El objeto que llama a la función.</param>
		/// <param name="args">Los argumentos que se le pasan a la 
		/// función.</param>
		
		private void SelectorFicherosOkPulsado (object o, EventArgs args)
		{
			if (!File.Exists (selectorFicheros.Filename))
			{
				MessageDialog m =
					new MessageDialog (this,
							   Gtk.DialogFlags.Modal,
							   Gtk.MessageType.Warning,
							   Gtk.ButtonsType.Close,
							   Ventana.GetText ("Ventana_ErrorNoFichero"));
				m.Run ();
				m.Hide ();
				return;
			}
			else
			{
				lblMemoriaAlt.Text = selectorFicheros.Filename;
				selectorFicheros.Hide();
			}
			
		}
		
		/// <summary>Función que se ejecuta al pulsar sobre el botón
		/// cancelar del selector de ficheros. Oculta el selector.</summary>
		/// <param name="o">El objeto que llama a la función.</param>
		/// <param name="args">Los argumentos que se le pasan a la 
		/// función.</param>
		
		private void SelectorFicherosCancelPulsado (object o, EventArgs args)
		{
			selectorFicheros.Hide();
		}
		
		/// <summary>Función que se ejecuta al pulsar sobre el botón
		/// selector de la memoria por defecto. Desactiva el campo
		/// de entrada de texto y el botón que llama al selector de
		/// ficheros.</summary>
		/// <param name="o">El objeto que llama a la función.</param>
		/// <param name="args">Los argumentos que se le pasan a la 
		/// función.</param>
		
		private void rbToggled1 (object o, System.EventArgs args)
		{
			RadioButton b = (RadioButton) o;
			if (b.Active)
			{
				lblMemoriaAlt.Sensitive = false;
				btnFichero.Sensitive = false;
			}
		}
		
		/// <summary>Función que se ejecuta al pulsar sobre el botón
		/// selector de la memoria definida por el usuario. Activa el 
		/// campo de entrada de texto y el botón que llama al selector
		/// de ficheros.</summary>
		/// <param name="o">El objeto que llama a la función.</param>
		/// <param name="args">Los argumentos que se le pasan a la 
		/// función.</param>
		
		private void rbToggled2 (object o, System.EventArgs args)
		{
			RadioButton b = (RadioButton) o;
			if (b.Active)
			{
				lblMemoriaAlt.Sensitive = true;
				btnFichero.Sensitive = true;
			}
		}
		
		/// <summary>Función que se ejecuta al pulsar sobre el botón
		/// aceptar del diálogo. Comprueba que los datos introducidos
		/// son correctos y los aplica.</summary>
		/// <param name="o">El objeto que llama a la función.</param>
		/// <param name="args">Los argumentos que se le pasan a la 
		/// función.</param>
		
		private void AceptarClick(object o, System.EventArgs args)
		{
	
			
			if (!rbMemoriaDef.Active)
			{
				if (!File.Exists(lblMemoriaAlt.Text))
				{
					MessageDialog m =
					new MessageDialog (this,
							   Gtk.DialogFlags.Modal,
							   Gtk.MessageType.Error,
							   Gtk.ButtonsType.Close,
							   Ventana.GetText ("D_Conf_ErrorMemNoExiste"));
					m.Run ();
					m.Hide ();
					return;
				}
				// comprobar que el fichero contiene texto correcto...
				try
				{	MemoriaControl.CreateFromString (
					Fichero.CargarTexto (lblMemoriaAlt.Text));
				}
				catch (Exception e)
				{
					Hilo.Sleep(250);
					MessageDialog m2 =
						new MessageDialog (this,
							   Gtk.DialogFlags.Modal,
							   Gtk.MessageType.Error,
							   Gtk.ButtonsType.Close,
							   Ventana.GetText ("D_Conf_ErrorMem"));
					m2.Run();
					m2.Hide();
					return;
				}				
			}
			
			Opciones opc = Opciones.GetInstance();
			opc.SetMostrarAdvertencias (cbAdvertencias.Active);
			opc.SetTSubciclo (sbTiempo.ValueAsInt);
			opc.SetUsarMemoriaDefecto (rbMemoriaDef.Active);
			opc.SetMemoriaAlternativa (lblMemoriaAlt.Text);
			opc.Guardar();
			
			this.HideAll();
		}
		
		/// <summary>Función que se llama cuando se pulsa el botón cancelar
		/// del diálogo. Oculta el diálogo.</summary>
		/// <param name="o">El objeto que llama a la función.</param>
		/// <param name="args">Los argumentos que se le pasan a la 
		/// función.</param>
		
		private void CancelarClick(object o, System.EventArgs args)
		{
			this.HideAll();
		}
		
		/// <summary>Función que muestra el diálogo centrado en la ventana
		/// padre. Establece los valores de las opciones actuales en los
		/// elementos visuales que las representan.</summary>

		public new void ShowAll ()
		{
			this.WindowPosition = WindowPosition.CenterOnParent;
			//Se cargan los elementos con las opciones...
			Opciones opc = Opciones.GetInstance ();
			
			cbAdvertencias.Active = opc.GetMostrarAdvertencias();	
			sbTiempo.Value = (double) opc.GetTSubciclo();
			rbMemoriaDef.Active =  opc.GetUsarMemoriaDefecto();
			rbMemoriaUsu.Active = !opc.GetUsarMemoriaDefecto();
			lblMemoriaAlt.Text = opc.GetMemoriaAlternativa();
			
			lblMemoriaAlt.Sensitive = !opc.GetUsarMemoriaDefecto();
			btnFichero.Sensitive = !opc.GetUsarMemoriaDefecto();

			base.ShowAll ();
		}
	}
}
