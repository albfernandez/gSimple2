namespace utilidades
{
	using simple2.utilidades;
	using NUnit.Framework;
	using System;
	
	[TestFixture]
	public class FicheroTest
	{
		[Test]
		public void Test()
		{
			String textoInicial = "Hola mundo";
			String fichero = System.IO.Path.GetTempPath() +"/fichero.gSimple2." + System.Guid.NewGuid() + ".test";
			Fichero.GuardarTexto(fichero, textoInicial);
			String leido = Fichero.CargarTexto (fichero);
			Assert.AreEqual (textoInicial, leido);
			System.IO.File.Delete(fichero);
		}
		[Test]
		public void TestAcentos()
		{
			String textoInicial = "Hola mundo\n Con acéntós y ñs y cosas }[][çÇ*==)(=";
			String fichero = System.IO.Path.GetTempPath() +"/fichero.gSimple2." + System.Guid.NewGuid() + ".test";
			Fichero.GuardarTexto(fichero, textoInicial);
			String leido = Fichero.CargarTexto (fichero);			
			Assert.AreEqual (textoInicial, leido);
			System.IO.File.Delete(fichero);
		}
		
	}
	
}
