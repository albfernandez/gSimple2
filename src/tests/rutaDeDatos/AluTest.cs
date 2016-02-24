namespace rutaDeDatos
{
	using simple2.rutaDeDatos;
	using NUnit.Framework;
	using System;
	
	[TestFixture]
	public class AluTest
	{
		[Test]
		public void TestSuma()
		{
			//public short Operar(int operacion, int operacionSH, short operandoA, short operandoB);
			ALU alu = new ALU();
			
			Assertion.AssertEquals("ALU-SUMA01", 0, alu.Operar (0,0, 0,0));
			Assertion.AssertEquals("ALU-SUMA02", 0, alu.LeerResultado());
			Assertion.AssertEquals("ALU-SUMA03", 0, alu.LeerC());
			Assertion.AssertEquals("ALU-SUMA02", 0, alu.LeerN());
			Assertion.AssertEquals("ALU-SUMA02", 1, alu.LeerZ());
			
			//Assertion.AssertEquals ("SUMA02",  
			
			
		}
		[Test]
		public void TestAnd()
		{
			ALU alu = new ALU();
			
			Assertion.AssertEquals ("ALU-AND01", 0, alu.Operar(1,0,100,0));
			Assertion.AssertEquals ("ALU-AND02", 0, alu.LeerC());
			Assertion.AssertEquals ("ALU-AND03", 0, alu.LeerN());
			Assertion.AssertEquals ("ALU-AND04", 1, alu.LeerZ());
			
		}
		[Test]
		public void TestTransparente()
		{
			ALU alu = new ALU();
			
			Assertion.AssertEquals ("ALU-TR01", 0, alu.Operar(2,0,0,0));
			Assertion.AssertEquals ("ALU-TR02", 0, alu.LeerC());
			Assertion.AssertEquals ("ALU-TR03", 0, alu.LeerN());
			Assertion.AssertEquals ("ALU-TR04", 1, alu.LeerZ());
			
			Assertion.AssertEquals ("ALU-TR05", -1, alu.Operar (2,0,-1,1));
			Assertion.AssertEquals ("ALU-TR06", 0, alu.LeerC());
			Assertion.AssertEquals ("ALU-TR07", 1, alu.LeerN());
			Assertion.AssertEquals ("ALU-TR08", 0, alu.LeerZ());
			
		}
		
		[Test]
		public void TestComplemento()
		{
			ALU alu = new ALU();
			
			Assertion.AssertEquals ("ALU-COM01", unchecked((short)0xFFFF), alu.Operar(3,0,0,0));
			Assertion.AssertEquals ("ALU-COM02", 0, alu.LeerC());
			Assertion.AssertEquals ("ALU-COM03", 1, alu.LeerN());
			Assertion.AssertEquals ("ALU-COM04", 0, alu.LeerZ());

		}
		
		[Test]
		public void TestOR()
		{
			ALU alu = new ALU();
			
			Assertion.AssertEquals ("ALU-OR01", (short)0x00FF, alu.Operar(4,0,0,0xFF));
			Assertion.AssertEquals ("ALU-OR02", 0, alu.LeerC());
			Assertion.AssertEquals ("ALU-OR03", 0, alu.LeerN());
			Assertion.AssertEquals ("ALU-OR04", 0, alu.LeerZ());
			
		}
		[Test]
		public void TestXOR()
		{
			ALU alu = new ALU();
			
			Assertion.AssertEquals("ALU-XOR01", (short) 0xF0, alu.Operar (5,0,unchecked((short)0xFF0F), unchecked((short)0xFFFF)));
			Assertion.AssertEquals("ALU-XOR02", 0, alu.LeerC());
			Assertion.AssertEquals("ALU-XOR03", 0, alu.LeerN());
			Assertion.AssertEquals("ALU-XOR04", 0, alu.LeerZ());
		}
		[Test]
		public void TestSH()
		{
			ALU alu = new ALU();
			
			Assertion.AssertEquals ("ALU-SH01", 0x007F, alu.Operar (2, 1, 0x00FF, 0x0));
			Assertion.AssertEquals ("ALU-SH02", 0, alu.LeerC());
			Assertion.AssertEquals ("ALU-SH03", 0, alu.LeerN());
			Assertion.AssertEquals ("ALU-SH04", 0, alu.LeerZ());
			Assertion.AssertEquals ("ALU-SH05", 0x01FE, alu.Operar(2,2,0x00FF, 0x0));
			Assertion.AssertEquals ("ALU-SH06", 0, alu.LeerC());
			Assertion.AssertEquals ("ALU-SH07", 0, alu.LeerN());
			Assertion.AssertEquals ("ALU-SH08", 0, alu.LeerZ());			
		}		
		[Test]
		public void TestAcarreo()
		{
			ALU alu = new ALU();
			
			Assertion.AssertEquals ("ALU-AC01", unchecked((short) 0x8000), alu.Operar (0,0, 0x7FFF, 0x1));
			Assertion.AssertEquals ("ALU-AC02", 1, alu.LeerC());
			Assertion.AssertEquals ("ALU-AC03", 0, alu.LeerZ());
			Assertion.AssertEquals ("ALU-AC04", 1, alu.LeerN());
		
		}
	}
	
}
