namespace simple2.utilidades
{
	using System;
	using System.Resources;
	
	/// <remarks>Clase que se encarga de gestionar los recursos 
	/// de internacionalizaci�n de texto de la aplicaci�n.</remarks>
	
	public class TextManager
	{
		/// <summary>Recursos del idioma activo.</summary>
		
		private ResourceManager resMan = null;
		
		/// <summary>Recursos del idioma por defecto.</summary>
		
		private ResourceManager resManDefecto = null;
		
		/// <summary>Idioma por defecto.</summary>
		
		public const String lengDefecto = "es";
		
		/// <summary>�nica instancia de la clase. Patr�n singleton.
		/// </summary>
		
		private static TextManager instancia = null;
		
		/// <summary>Constructor privado. No se permite crear instancias
		/// de esta clase exteriormente. Patr�n singleton.</summary>
		
		private TextManager ()
		{
			try
			{
				String lang = TextManager.GetLanguage();
				
				String recurso = "Mensajes." + lang;

				try
				{
					resManDefecto =
						new	ResourceManager ("Mensajes." + lengDefecto + ".txt",
								 typeof(TextManager).Assembly);
				}
				catch (Exception ex)
				{
					//No tenemos el idioma por defecto.
				}

				try
				{
					resMan = new ResourceManager (recurso,
								      typeof(TextManager).Assembly);
					resMan.GetString ("Programa_Lenguaje");
				}
				catch (Exception ex1)
				{

					try
					{
						if (lang.Length > 2)
							recurso = "Mensajes." +	lang.Substring (0, 2) + ".txt";
						resMan = new ResourceManager (recurso,
							 typeof(TextManager).Assembly);
						resMan.GetString ("Programa_Lenguaje");

					}
					catch (Exception ex2)
					{
						resMan = resManDefecto;
					}
				}
			}
			catch (Exception ex)
			{
			}
		}
		
		/// <summary>Obtiene la �nica instancia de la clase (patr�n singleton)
		/// </summary>
		/// <returns>La �nica instancia de la clase.</returns>
		
		public static TextManager GetInstance()
		{
			if (instancia == null)
				instancia = new TextManager();
			return instancia;
		}
		
		/// <summary>Obtiene el texto para la clave indicada en el idioma
		/// activo. Primero intenta con el idioma del sistema. Si
		/// no est� disponible, prueba ocn el idioma por defecto. Si tambi�n
		/// falla, devuelve la clave de b�squeda.</summary>
		/// <param name="clave">Cadena de texto con la clave de b�squeda
		/// </param>
		/// <returns>El texto correspondiente en el idioma del sistema.
		/// </returns>
		
		public static String GetText (String clave)
		{
			return GetInstance().GetMessage(clave);
		}
		
		/// <summary>Obtiene el texto para la clave indicada en el idioma
		/// activo. Primero intenta con el idioma del sistema. Si
		/// no est� disponible, prueba ocn el idioma por defecto. Si tambi�n
		/// falla, devuelve la clave de b�squeda.</summary>
		/// <param name="clave">Cadena de texto con la clave de b�squeda
		/// </param>
		/// <returns>El texto correspondiente en el idioma del sistema.
		/// </returns>
		
		public String GetMessage (String clave)
		{
			try
			{
				return (resMan.GetString (clave));
			}
			catch (Exception ex)
			{
			}
			try
			{
				return (resManDefecto.GetString (clave));
			}
			catch (Exception ex)
			{
			}

			return (clave);
		}
		
		/// <summary>Obtiene el lenguaje del sistema.</summary>
		/// <returns>El lenguaje del sistema.</returns>
		
		private static String GetLanguage ()
		{
			String lang =
				System.Environment.GetEnvironmentVariable ("LANG");
			String language =
				System.Environment.GetEnvironmentVariable ("LANGUAGE");

			if (language == null)
				language = lang;
			if ( (language == null) || (language.Length < 2) )
				language = lengDefecto;

			return language;
		}		
	}
}
