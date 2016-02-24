namespace utilidades
{
	using simple2.utilidades;
	using NUnit.Framework;
	using System;
	
	[TestFixture]
	public class ConversionesTest
	{
		[Test]
		public void TestShort()
		{
			Assertion.AssertEquals("0000", Conversiones.ToHexString((short) 0) );
			Assertion.AssertEquals("FFFF", Conversiones.ToHexString((unchecked( (short) 0xFFFF) )));
		}
		[Test]
		public void TestInt()
		{
			Assertion.AssertEquals("00000000", Conversiones.ToHexString ((int)0));
			Assertion.AssertEquals("FFFFFFFF", Conversiones.ToHexString (unchecked ((int)0xFFFFFFFF)));

		}
		[Test]
		public void TestBinary()
		{
			Assertion.AssertEquals ("001", Conversiones.ToBinaryString (1,3));
			Assertion.AssertEquals ("01", Conversiones.ToBinaryString (1, 2));
			Assertion.AssertEquals ("1010", Conversiones.ToBinaryString (10, 4));
			Assertion.AssertEquals ("010", Conversiones.ToBinaryString (10,3));
		}
		
	}
	
}
