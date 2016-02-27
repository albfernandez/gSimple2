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
			
			Assert.AreEqual(0, alu.Operar (0,0, 0,0));
			Assert.AreEqual(0, alu.LeerResultado());
			Assert.AreEqual(0, alu.LeerC());
			Assert.AreEqual(0, alu.LeerN());
			Assert.AreEqual(1, alu.LeerZ());
			
			
			
		}
		[Test]
		public void TestAnd()
		{
			ALU alu = new ALU();
			
			Assert.AreEqual (0, alu.Operar(1,0,100,0));
			Assert.AreEqual (0, alu.LeerC());
			Assert.AreEqual (0, alu.LeerN());
			Assert.AreEqual (1, alu.LeerZ());
			
		}
		[Test]
		public void TestTransparente()
		{
			ALU alu = new ALU();
			
			Assert.AreEqual (0, alu.Operar(2,0,0,0));
			Assert.AreEqual (0, alu.LeerC());
			Assert.AreEqual (0, alu.LeerN());
			Assert.AreEqual (1, alu.LeerZ());
			
			Assert.AreEqual (-1, alu.Operar (2,0,-1,1));
			Assert.AreEqual (0, alu.LeerC());
			Assert.AreEqual (1, alu.LeerN());
			Assert.AreEqual (0, alu.LeerZ());
			
		}
		
		[Test]
		public void TestComplemento()
		{
			ALU alu = new ALU();
			
			Assert.AreEqual (unchecked((short)0xFFFF), alu.Operar(3,0,0,0));
			Assert.AreEqual (0, alu.LeerC());
			Assert.AreEqual (1, alu.LeerN());
			Assert.AreEqual (0, alu.LeerZ());

		}
		
		[Test]
		public void TestOR()
		{
			ALU alu = new ALU();
			
			Assert.AreEqual ((short)0x00FF, alu.Operar(4,0,0,0xFF));
			Assert.AreEqual (0, alu.LeerC());
			Assert.AreEqual (0, alu.LeerN());
			Assert.AreEqual (0, alu.LeerZ());
			
		}
		[Test]
		public void TestXOR()
		{
			ALU alu = new ALU();
			
			Assert.AreEqual((short) 0xF0, alu.Operar (5,0,unchecked((short)0xFF0F), unchecked((short)0xFFFF)));
			Assert.AreEqual(0, alu.LeerC());
			Assert.AreEqual(0, alu.LeerN());
			Assert.AreEqual(0, alu.LeerZ());
		}
		[Test]
		public void TestSH()
		{
			ALU alu = new ALU();
			
			Assert.AreEqual (0x007F, alu.Operar (2, 1, 0x00FF, 0x0));
			Assert.AreEqual (0, alu.LeerC());
			Assert.AreEqual (0, alu.LeerN());
			Assert.AreEqual (0, alu.LeerZ());
			Assert.AreEqual (0x01FE, alu.Operar(2,2,0x00FF, 0x0));
			Assert.AreEqual (0, alu.LeerC());
			Assert.AreEqual (0, alu.LeerN());
			Assert.AreEqual (0, alu.LeerZ());			
		}		
		[Test]
		public void TestAcarreo()
		{
			ALU alu = new ALU();
			
			Assert.AreEqual (unchecked((short) 0x8000), alu.Operar (0,0, 0x7FFF, 0x1));
			Assert.AreEqual (1, alu.LeerC());
			Assert.AreEqual (0, alu.LeerZ());
			Assert.AreEqual (1, alu.LeerN());
		
		}
	}
	
}
