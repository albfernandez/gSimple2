namespace simple2.interfaz.gtk
{
	using Gtk;
	using Gdk;
	using Pango;
	using System;
	using System.Timers;
	using GLib;
	
	using simple2.representacionRuta;


	/// <summary>Clase utilizada para representar la ruta de datos
	/// del ordenador Simple2 en la pantalla.</summary>
	/// <remarks>La clase representa un espacio virtual de dibujo.
	/// Las operaciones se realizan sobre un lienzo virtual de tamaño 
	/// fijo. La clase se encarga de dibujarlos utilizando el tamaño
	/// real disponible, manteniendo el aspecto (salvo para el texto).
	/// </remarks>

	public class PanelDibujo: Gtk.DrawingArea, InterfaceDibujo
	{
		/// <summary>Pixmap sobre el que se dibujará para realizar
		/// el DoubleBuffering.</summary>
		
		private Gdk.Pixmap pixmap;
		
		/// <summary>El espacio de pantalla sobre el que se dibujará.
		/// </summary>
		
		private Gdk.Window window;
		
		/// <summary>Almacena el ancho del lienzo.</summary>
				
		private int _ancho;
		
		/// <summary>Almacena el alto del lienzo.</summary>
		
		private int _alto;		
		
		/// <summary>Ancho virtual del lienzo.</summary>
		
		public const double ANCHO = 720;
		
		/// <summary>Alto virtual del lienzo.</summary>
		
		public const double ALTO = 470;
		
		/// <summary>Objeto que pintará sobre el lienzo.</summary>
		
		private RepresentacionRDD rdd = null;
		
		/// <summary>Layout para dibujar texto en el lienzo.</summary>
		
		private Pango.Layout layout = null;
		
		/// <summary>El ancho del lienzo.</summary>
		
		public int Ancho
		{
			get
			{
				return _ancho;
			}
		}
		
		/// <summary>El alto del lienzo.</summary>
		
		public int Alto
		{
			get
			{
				return _alto;
			}
			
		}
		
		/// <summary>El color de fondo del lienzo.</summary>
		
		public const EnuColor COLOR_FONDO = EnuColor.Blanco;
		
		
		
		private static Gdk.Color[] tabla_colores = null;
		
		private Gdk.GC[] tabla_gc = null;
		
		/// <summary>Crea una nueva instancia de la clase.</summary>
		
		public PanelDibujo ()
		{
			rdd = new RepresentacionRDD (this);
			ExposeEvent += new ExposeEventHandler (OnExposeEvent);
			ConfigureEvent += new ConfigureEventHandler (OnConfigureEvent);
			this.layout = new Pango.Layout(this.PangoContext);			
		}
		
		/// <summary>Establece el objeto RepresentacionRDD que dibujará
		/// sobre este lienzo. Es utilizado para notificarle de los cambios
		/// de tamaño del lienzo, de modo que se redibuje entero de nuevo.
		/// </summary>
		/// <param name="r"></param>
		
		public void SetRepresentacionRDD (RepresentacionRDD r)
		{
			this.rdd = r;
		}
		
		/// <summary>Función que se llama cuando el objeto es "expuesto" (se
		/// muestra después de estar oculto) para que se redibuje.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		
		public void OnExposeEvent (object o,  Gtk.ExposeEventArgs args)
		{
			lock (this)
			{
				Refresh();			
				SignalArgs sa = (SignalArgs) args;
				sa.RetVal = false;
			}

		}
		
		/// <summary>Procesa los eventos pendientes de la interfaz (redibuja
		/// los elementos que han cambiado.</summary>
		
		public bool ProcesarEventosPendientes ()
		{
			lock(this)
			{
				simple2.hilos.Hilo.Sleep(10);
				while (Application.EventsPending()) {
					Application.RunIteration();
				}
				return true;
			}
		}
		
		/// <summary>Función encargada de responder a los cambios de 
		/// tamaño del lienzo de dibujo.</summary>
		/// <param name="o"></param>
		/// <param name="args"></param> 
		
		private void OnConfigureEvent (object o, ConfigureEventArgs args)
		{
			lock (this)
			{
				// TODO ver de donde sacamos window ahora
				Gdk.EventConfigure ev = args.Event;


				pixmap = new Pixmap (window, ev.Width, ev.Height, -1);
				_ancho = ev.Width;
				_alto = ev.Height;
				this.Clean();
				if (rdd != null)
				{
					rdd.ActualizarTodo();
				}
				SignalArgs sa = (SignalArgs) args;
				sa.RetVal = true;
			}
		}
		
		/// <summary>Calcula la posición X en la que se debe dibujar
		/// un elemento. Para ello multiplica el valor pasado
		/// por el ancho real del área de dibujo, y lo divide
		/// entre el ancho virtual.</summary>
		/// <param name="x">La coordenada virtual x.</param>
		/// <returns>La coordenada real correspondiente a la coordenada
		/// virtual x.</returns>
		
		private int CoordX (int x)
		{
			return (int) (x *   Ancho / ANCHO);
		}
		
		/// <summary>Calcula la posición Y en la que se debe dibujar
		/// un elemento. Para ello multiplica el valor pasado por el
		/// alto real del área de dibujo, y lo divide entre el alto
		/// virtual.</summary>
		/// <param name="y">La coordenada virtual y.</param>
		/// <returns>La coordenada real correspondiente a la coordenada 
		/// virtual y.</returns>
		
		private int CoordY (int y)
		{
			return (int)(y * Alto / ALTO);
		}
		
		/// <summary>Nos obtiene el entorno gráfico (Gdk.GC) para el color c.
		/// </summary>
		/// <param name="c">El color.</param>
		/// <returns>El entorno gráfico para ese color.</returns>
		
		private Gdk.GC GetGC (EnuColor c)		
		{
			if (tabla_gc == null)
			{
				tabla_gc = new Gdk.GC[Enum.GetValues(typeof(EnuColor)).Length];
				
				tabla_gc[(int) EnuColor.Blanco] = new Gdk.GC(pixmap);
				tabla_gc[(int) EnuColor.Negro] = new Gdk.GC(pixmap);
				tabla_gc[(int) EnuColor.Rojo] = new Gdk.GC(pixmap);
				tabla_gc[(int) EnuColor.Verde] = new Gdk.GC(pixmap);
				tabla_gc[(int) EnuColor.Azul] = new Gdk.GC(pixmap);
				tabla_gc[(int) EnuColor.Amarillo]= new Gdk.GC(pixmap);
				
				tabla_gc[(int) EnuColor.Blanco].
					RgbBgColor = GetColor(COLOR_FONDO);
				tabla_gc[(int) EnuColor.Negro].
					RgbBgColor = GetColor(COLOR_FONDO);
				tabla_gc[(int) EnuColor.Rojo].
					RgbBgColor = GetColor(COLOR_FONDO);
				tabla_gc[(int) EnuColor.Verde].
					RgbBgColor = GetColor(COLOR_FONDO);
				tabla_gc[(int) EnuColor.Azul].
					RgbBgColor = GetColor(COLOR_FONDO);
				tabla_gc[(int) EnuColor.Amarillo].
					RgbBgColor = GetColor(COLOR_FONDO);
				
				tabla_gc[(int) EnuColor.Blanco].
					RgbFgColor = GetColor(EnuColor.Blanco);
				tabla_gc[(int) EnuColor.Negro].
					RgbFgColor = GetColor(EnuColor.Negro);
				tabla_gc[(int) EnuColor.Rojo].
					RgbFgColor = GetColor(EnuColor.Rojo);
				tabla_gc[(int) EnuColor.Verde].
					RgbFgColor = GetColor(EnuColor.Verde);
				tabla_gc[(int) EnuColor.Azul].
					RgbFgColor = GetColor(EnuColor.Azul);
				tabla_gc[(int) EnuColor.Amarillo].
					RgbFgColor = GetColor(EnuColor.Amarillo);
			}
			return (tabla_gc[(int) c]);
		}
		
		/// <summary>Transforma un color de EnuColor en un color de
		/// Gdk.Color.</summary>
		/// <param name="c">El color.</param>
		/// <returns>El color en tipo Gdk.Color.</returns>
		
		private static Gdk.Color GetColor (EnuColor c)
		{
			if (tabla_colores == null)
			{
				tabla_colores = 
					new Gdk.Color[Enum.GetValues(typeof(EnuColor)).Length];
					
				tabla_colores[(int) EnuColor.Blanco] = 
					new Gdk.Color (255,255,255);
				tabla_colores[(int) EnuColor.Negro] = 
					new Gdk.Color (0,0,0);
				tabla_colores[(int) EnuColor.Rojo] = 
					new Gdk.Color (255,0,0);
				tabla_colores[(int) EnuColor.Verde] = 
					new Gdk.Color(0, 255,0);
				tabla_colores[(int) EnuColor.Azul] = 
					new Gdk.Color (0,0,255);
				tabla_colores[(int) EnuColor.Amarillo] = 
					new Gdk.Color(255,255,0);
			}
			return tabla_colores[(int) c];
		}
		
		/// <summary>Limpia la superficie de dibujo.</summary>
		
		public void Clean ()
		{
			lock(this)
			{
				if (pixmap != null)
				{
					pixmap.DrawRectangle (
						GetGC (COLOR_FONDO), true, 0, 0, Ancho, Alto);
				}				
			}
		}
				
		/// <summary>Limpia un cuadrado indicado en la superficie de dibujo
		/// </summary>
		/// <param name="x">La coordenada x de la esquina superior izquierda
		/// del cuadrado a limpiar.</param>
		/// <param name="y">La coordenada y de la esquina superior izquierda
		/// del cuadrado a limpiar.</param>
		/// <param name="ancho">El ancho del cuadrado a limpiar.</param>
		/// <param name="alto">El alto del cuadrado a limpiar.</param>
		
		public void Clean (int x, int y, int ancho, int alto)
		{
			lock(this)
			{
				if (pixmap != null)
				{
					pixmap.DrawRectangle (
						GetGC(COLOR_FONDO), 
						true, 
						CoordX(x), 
						CoordY(y), 
						CoordX(ancho), 
						CoordY(alto)
					);
				}				
			}
		}
		
		/// <summary>Redibuja la superficie de dibujo.</summary>
		
		public void Refresh ()
		{
			lock(this)
			{
				if (pixmap != null)
				{
					simple2.hilos.Hilo.Sleep(10);
					window.DrawDrawable (new Gdk.GC (pixmap),
							 pixmap, 0, 0, 0, 0, Ancho, Alto);
				}	
				System.GC.WaitForPendingFinalizers();			
			}
		}
		
		/// <summary>Dibuja una recta en la superficie de dibujo.</summary>
		/// <param name="c">El color de la recta.</param>
		/// <param name="x1">La coordenada x del punto inicial de la recta.
		/// </param>
		/// <param name="y1">La coordenada y del punto inicial de la recta.
		/// </param>
		/// <param name="x2">La coordenada x del punto final de la recta.
		/// </param>
		/// <param name="y2">La coordenada y del punto final de la recta.
		/// </param>		
			
		public void DibujarRecta (EnuColor c, int x1, int y1, int x2, int y2)
		{
			lock(this)
			{
				if (pixmap != null)
				{
					pixmap.DrawLine(GetGC(c), CoordX(x1),CoordY(y1),
					CoordX(x2),CoordY(y2));
				}				
			}
		}
		
		/// <summary>Dibuja un texto en la superficie de dibujo.</summary>
		/// <param name="c">El color del texto.</param>
		/// <param name="x">La posición x del texto.</param>
		/// <param name="y">La posición y del texto.</param>
		/// <param name="texto">El texto a dibujar en la superficie de dibujo.
		/// </param>
		/// <param name="cent">Indica si el texto está centrado (x e y se
		/// refieren al centro del texto) o no (x e y se refieren a la 
		/// esquina superior del texto)</param>
		
		public void DibujarTexto 
			(EnuColor c, int x, int y, String texto, bool cent)
		{
			int anc;
			int alt;
			lock (this)
			{
				if (pixmap != null)
				{
					if (texto != null)
					{
						layout.SetText(texto);
					}
					else
					{
						layout.SetText (" ");
					}
					
					x = CoordX (x);
					y = CoordY (y);
					
					if (cent)
					{
						
						try
						{
							layout.GetSize (out anc, out alt);
						}
						catch (System.NullReferenceException )
						{
							this.layout = new Pango.Layout(this.PangoContext);
							return;
						}
						
						x = x - (anc / 2200);
						y = y - (alt / 2200);		
						
					}
					pixmap.DrawLayout (GetGC(c), x, y, layout);
				}				
			}
		}
		
		/// <summary>Dibuja los bordes de un rectángulo en la superficie
		/// de dibujo.</summary>
		/// <param name="c">El color de las líneas del rectángulo.</param>
		/// <param name="x1">La posición x de la esquina superior
		/// izquierda del rectángulo.</param>
		/// <param name="y1">La posición y de la esquina superior
		/// izquierda del rectángulo.</param>
		/// <param name="ancho">El ancho del rectángulo.</param>
		/// <param name="alto">El alto del rectángulo.</param>
		
		public void DibujarRectangulo 
			(EnuColor c,int x1, int y1, int ancho, int alto)
		{
			lock (this)
			{
				if (pixmap != null)
				{
					pixmap.DrawRectangle(
						GetGC(c), 
						false, 
						CoordX(x1), 
						CoordY(y1), 
						CoordX(ancho), 
						CoordY(alto));
				}			
			}
		}		
	}
}
