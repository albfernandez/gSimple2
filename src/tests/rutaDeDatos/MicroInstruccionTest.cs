 namespace rutaDeDatos
{
	using simple2.rutaDeDatos;
	using NUnit.Framework;
	using System;
	
	[TestFixture]
	public class MicroInstruccionTest
	{
		[Test]
		public void TestToHexString ()
		{
			MicroInstruccion mic = new MicroInstruccion (0x001A0A000600);
			Assert.AreEqual("001A 0A00 0600", mic.ToHexString());
			mic = new MicroInstruccion (0);
			Assert.AreEqual("0000 0000 0000", mic.ToHexString());
			mic = new MicroInstruccion (0x805300000400);
			Assert.AreEqual("8053 0000 0400", mic.ToHexString());
			mic = new MicroInstruccion (0x000000002000);
			Assert.AreEqual("0000 0000 2000", mic.ToHexString());
			mic = new MicroInstruccion (0x001060000000);
			Assert.AreEqual("0010 6000 0000", mic.ToHexString());
			
		}
		[Test]
		public void TestGets()
		{
			MicroInstruccion mic = new MicroInstruccion (0x805300000400);
			
			Assert.AreEqual ( 1, mic.GetAMUX ());
			Assert.AreEqual ( 0, mic.GetCOND ());
			Assert.AreEqual ( 0, mic.GetSH ());
			Assert.AreEqual ( 0, mic.GetMBR ());
			Assert.AreEqual ( 0, mic.GetMAR ());
			Assert.AreEqual ( 1, mic.GetRD ());
			Assert.AreEqual ( 0, mic.GetWR ());
			Assert.AreEqual ( 1, mic.GetENC ());
			Assert.AreEqual ( 3, mic.GetC ());
			Assert.AreEqual ( 0, mic.GetB ());
			Assert.AreEqual ( 0, mic.GetA ());
			Assert.AreEqual ( 0, mic.GetADDR ());
			Assert.AreEqual ( 0, mic.GetFIR ());
			Assert.AreEqual ( 2, mic.GetALU ());
		}		
	}	
}
