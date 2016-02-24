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
			Assertion.AssertEquals("001A 0A00 0600", mic.ToHexString());
			mic = new MicroInstruccion (0);
			Assertion.AssertEquals("0000 0000 0000", mic.ToHexString());
			mic = new MicroInstruccion (0x805300000400);
			Assertion.AssertEquals("8053 0000 0400", mic.ToHexString());
			mic = new MicroInstruccion (0x000000002000);
			Assertion.AssertEquals("0000 0000 2000", mic.ToHexString());
			mic = new MicroInstruccion (0x001060000000);
			Assertion.AssertEquals("0010 6000 0000", mic.ToHexString());
			
		}
		[Test]
		public void TestGets()
		{
			MicroInstruccion mic = new MicroInstruccion (0x805300000400);
			
			Assertion.AssertEquals ("mic01", 1, mic.GetAMUX ());
			Assertion.AssertEquals ("mic02", 0, mic.GetCOND ());
			Assertion.AssertEquals ("mic03", 0, mic.GetSH ());
			Assertion.AssertEquals ("mic04", 0, mic.GetMBR ());
			Assertion.AssertEquals ("mic05", 0, mic.GetMAR ());
			Assertion.AssertEquals ("mic06", 1, mic.GetRD ());
			Assertion.AssertEquals ("mic07", 0, mic.GetWR ());
			Assertion.AssertEquals ("mic08", 1, mic.GetENC ());
			Assertion.AssertEquals ("mic09", 3, mic.GetC ());
			Assertion.AssertEquals ("mic10", 0, mic.GetB ());
			Assertion.AssertEquals ("mic11", 0, mic.GetA ());
			Assertion.AssertEquals ("mic12", 0, mic.GetADDR ());
			Assertion.AssertEquals ("mic13", 0, mic.GetFIR ());
			Assertion.AssertEquals ("mic14", 2, mic.GetALU ());
		}		
	}	
}
