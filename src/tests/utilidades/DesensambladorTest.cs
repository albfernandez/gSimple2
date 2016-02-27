namespace utilidades
{
	using simple2.utilidades;
	using NUnit.Framework;
	using System;
	
	[TestFixture]
	public class DesensambladorTest
	{
		[Test]
		public void TestParametros()
		{
			Assert.AreEqual ("LODD 2047",Desensamblador.Desensamblar  (unchecked((short)0x0FFF)));
			Assert.AreEqual ("JUMP 25",Desensamblador.Desensamblar  (unchecked((short)0x6819)));

		}
		[Test]
		public void TestInstrucciones ()
		{
			Assert.AreEqual ("HALT", Desensamblador.Desensamblar  (unchecked((short)0xFFFF)));	
			Assert.AreEqual ("HALT", Desensamblador.Desensamblar  (unchecked((short)0xF800)));	
			Assert.AreEqual ("LODD 0", Desensamblador.Desensamblar  (unchecked((short)0x0800)));
			Assert.AreEqual ("LODI 0", Desensamblador.Desensamblar  (unchecked((short)0x1000)));
			Assert.AreEqual ("STOD 0", Desensamblador.Desensamblar  (unchecked((short)0x1800)));
			Assert.AreEqual ("ADDD 0", Desensamblador.Desensamblar  (unchecked((short)0x2000)));
			Assert.AreEqual ("ADDI 0", Desensamblador.Desensamblar  (unchecked((short)0x2800)));
			Assert.AreEqual ("SUBD 0", Desensamblador.Desensamblar  (unchecked((short)0x3000)));
			Assert.AreEqual ("SUBI 0", Desensamblador.Desensamblar  (unchecked((short)0x3800)));
			Assert.AreEqual ("PUSH", Desensamblador.Desensamblar  (unchecked((short)0x4000)));
			Assert.AreEqual ("POP", Desensamblador.Desensamblar  (unchecked((short)0x4800)));
			Assert.AreEqual ("JNEG 0", Desensamblador.Desensamblar  (unchecked((short)0x5000)));
			Assert.AreEqual ("JZER 0", Desensamblador.Desensamblar  (unchecked((short)0x5800)));
			Assert.AreEqual ("JCAR 0", Desensamblador.Desensamblar  (unchecked((short)0x6000)));
			Assert.AreEqual ("JUMP 0", Desensamblador.Desensamblar  (unchecked((short)0x6800)));
			Assert.AreEqual ("CALL 0", Desensamblador.Desensamblar  (unchecked((short)0x7000)));
			Assert.AreEqual ("RETN", Desensamblador.Desensamblar  (unchecked((short)0x7800)));
		}
		[Test]
		public void TestOpcodeInvalido()
		{
			Assert.AreEqual (";????", Desensamblador.Desensamblar (unchecked ((short)0x8000)));
		}		
	}
	
}
