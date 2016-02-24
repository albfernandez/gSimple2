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
			Assertion.AssertEquals ("LODD 2047",Desensamblador.Desensamblar  (unchecked((short)0x0FFF)));
			Assertion.AssertEquals ("JUMP 25",Desensamblador.Desensamblar  (unchecked((short)0x6819)));

		}
		[Test]
		public void TestInstrucciones ()
		{
			Assertion.AssertEquals ("HALT", Desensamblador.Desensamblar  (unchecked((short)0xFFFF)));	
			Assertion.AssertEquals ("HALT", Desensamblador.Desensamblar  (unchecked((short)0xF800)));	
			Assertion.AssertEquals ("LODD 0", Desensamblador.Desensamblar  (unchecked((short)0x0800)));
			Assertion.AssertEquals ("LODI 0", Desensamblador.Desensamblar  (unchecked((short)0x1000)));
			Assertion.AssertEquals ("STOD 0", Desensamblador.Desensamblar  (unchecked((short)0x1800)));
			Assertion.AssertEquals ("ADDD 0", Desensamblador.Desensamblar  (unchecked((short)0x2000)));
			Assertion.AssertEquals ("ADDI 0", Desensamblador.Desensamblar  (unchecked((short)0x2800)));
			Assertion.AssertEquals ("SUBD 0", Desensamblador.Desensamblar  (unchecked((short)0x3000)));
			Assertion.AssertEquals ("SUBI 0", Desensamblador.Desensamblar  (unchecked((short)0x3800)));
			Assertion.AssertEquals ("PUSH", Desensamblador.Desensamblar  (unchecked((short)0x4000)));
			Assertion.AssertEquals ("POP", Desensamblador.Desensamblar  (unchecked((short)0x4800)));
			Assertion.AssertEquals ("JNEG 0", Desensamblador.Desensamblar  (unchecked((short)0x5000)));
			Assertion.AssertEquals ("JZER 0", Desensamblador.Desensamblar  (unchecked((short)0x5800)));
			Assertion.AssertEquals ("JCAR 0", Desensamblador.Desensamblar  (unchecked((short)0x6000)));
			Assertion.AssertEquals ("JUMP 0", Desensamblador.Desensamblar  (unchecked((short)0x6800)));
			Assertion.AssertEquals ("CALL 0", Desensamblador.Desensamblar  (unchecked((short)0x7000)));
			Assertion.AssertEquals ("RETN", Desensamblador.Desensamblar  (unchecked((short)0x7800)));
		}
		[Test]
		public void TestOpcodeInvalido()
		{
			Assertion.AssertEquals (";????", Desensamblador.Desensamblar (unchecked ((short)0x8000)));
		}		
	}
	
}
