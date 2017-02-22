using NUnit.Framework;
using System;
using Library;

namespace Test
{
	[TestFixture ()]
	public class BoardInitializeTest
	{
		private char[] CharField = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
		private Board boardTest = new Board ();

		[Test ()]
		public void InitialiezBoardFieldsLength()
		{
			Assert.AreEqual (boardTest.Fields.Length, 64);
		}

		[Test ()]
		public void BoardInitializationAllFieldsCreated ()
		{
			Assert.That (boardTest.Fields, Is.All.Not.Null);
		}

		[Test ()]
		public void NoFieldIsEqualToAnother() 
		{
			for (int i = 0; i < boardTest.Fields.Length; i++) {
				for (int j = 0; j < boardTest.Fields.Length; j++) {
					if (i != j) {
						Assert.AreNotEqual (boardTest.Fields[i], boardTest.Fields[j]);
					}
				}
			}
		}

		[Test ()]
		public void NewlyInitializedBoardsAreEqual()
		{
			for (int i = 0; i < 100; i++) 
			{
				var tmp = new Board ();
				var tmp1 = new Board ();
				Assert.AreEqual (tmp1, tmp);	
			}
		}

		[Test ()]
		public void AllFieldsAreWithinActualBoardRangeY()
		{
			for (int i = 0; i < boardTest.Fields.Length; i++) 
			{
				bool tmp = boardTest.Fields[i].Y < 9;
				bool tmp1 = boardTest.Fields[i].Y > 0;

				Assert.AreEqual (tmp && tmp1, true);	
			}
		}

		[Test ()]
		public void AllFieldsAreWithinActualBoardRangeX ()
		{
			for (int i = 0; i < boardTest.Fields.Length; i++) {
				Assert.AreEqual (Array.IndexOf(CharField, boardTest.Fields[i].X) != -1, true);
			}
		}

		[Test ()]
		public void InitialiezBoardPiecesLength()
		{
			Assert.AreEqual (boardTest.Pieces.Length, 32);
		}

		[Test ()]
		public void BoardInitializationAllPiecesCreated ()
		{
			Assert.That (boardTest.Pieces, Is.All.Not.Null);
		}
			
		[Test ()]
		public void NoPieceIsEqualToAnother() 
		{
			for (int i = 0; i < boardTest.Pieces.Length; i++) {
				for (int j = 0; j < boardTest.Pieces.Length; j++) {
					if (i != j) {
						Assert.AreNotEqual (boardTest.Pieces[i], boardTest.Pieces[j]);
					}
				}
			}
		}

		[Test ()]
		public void AmountOfBlackAndWhitePiecesAreEqual ()
		{
			int whites = 0;
			int blacks = 0;

			for (int k = 0; k < boardTest.Pieces.Length; k++) {
				if (boardTest.Pieces[k].Color == "White") {
					whites++;
				} else {
					blacks++;
				}
			}

			Assert.AreEqual (whites, blacks);
		}
	}
}

