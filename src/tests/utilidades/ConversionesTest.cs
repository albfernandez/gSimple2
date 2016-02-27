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
			Assert.AreEqual("0000", Conversiones.ToHexString((short) 0) );
			Assert.AreEqual("FFFF", Conversiones.ToHexString((unchecked( (short) 0xFFFF) )));
		}
		[Test]
		public void TestInt()
		{
			Assert.AreEqual("00000000", Conversiones.ToHexString ((int)0));
			Assert.AreEqual("FFFFFFFF", Conversiones.ToHexString (unchecked ((int)0xFFFFFFFF)));

		}
		[Test]
		public void TestBinary()
		{
			Assert.AreEqual ("001", Conversiones.ToBinaryString (1,3));
			Assert.AreEqual ("01", Conversiones.ToBinaryString (1, 2));
			Assert.AreEqual ("1010", Conversiones.ToBinaryString (10, 4));
			Assert.AreEqual ("010", Conversiones.ToBinaryString (10,3));
		}
		
	}
	
}
