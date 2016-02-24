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
			String leido= null;
			Fichero.GuardarTexto("fichero", textoInicial);
			leido = Fichero.CargarTexto ("fichero");
			
			Assertion.AssertEquals (textoInicial, leido);
		}
		[Test]
		public void TestAcentos()
		{
			String textoInicial = "Hola mundo\n Con acéntós y ñs y cosas }[][çÇ*==)(=";
			String leido= null;
			Fichero.GuardarTexto("fichero", textoInicial);
			leido = Fichero.CargarTexto ("fichero");
			
			Assertion.AssertEquals (textoInicial, leido);
		}
		
	}
	
}
