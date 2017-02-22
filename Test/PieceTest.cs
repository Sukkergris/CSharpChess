using NUnit.Framework;
using System;
using Library;

namespace Test
{
	[TestFixture ()]
	public class PieceTest
	{
		Piece test = new Piece ("Black", "Pawn", '\u265F', 'c', 2);
		Piece test1 = new Piece ("Black", "Knight", '\u265F', 'c', 2);
		Piece test2 = new Piece ("Black", "Pawn", '\u265F', 'c', 2);
		Piece test3 = new Piece ("Black", "Pawn", '\u265F', 'd', 2);
		Piece test4 = new Piece ("Black", "Pawn", '\u265F', 'c', 3);
		Piece test5 = new Piece ("White", "Pawn", '\u2659', 'c', 2);

		[Test ()]
		public void TestAllEqualityTypes ()
		{
			bool tmp = (test != test3 && test != test4 && test != test5);
			bool tmp1 = !(test.Equals(test3) && test.Equals(test4) && test.Equals(test5));
			bool tmp2 = (test == test2);
			bool tmp3 = !(test.Equals(test1));

			Assert.AreEqual (tmp && tmp1 && tmp2 && tmp3, true);
		}

		[Test ()]
		public void ChangeState ()
		{
			var tmp = test.State;
			test.ChangeStatusState ();
			var tmp1 = test.State;
			Assert.AreNotEqual (tmp, tmp1);
		}
	}
}

