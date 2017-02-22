using System;
using Library;

namespace TUI
{
	class TUI
	{
		public static void Main(string[] args)
		{
			var testBoard = new Board();
			int turn = 0;
			Console.Title = "Chess v.1.0 - No autocheck for check mate";
			bool checkmate = false;

			// foreach p in pl 
			// 		if p.Color == CheckColor
			// 			foreach f in fl
			// 				if testBoard.ValidateMoves(p.X, p.Y, f.X, f.Y)
			// 					execute move(a, b, c, d)
			// 					if Move.CheckHelp
			// 						execute move(c, d, a, b)
			// 						checkmate = true
			// 						return
			//					execute move(c, d, a, b)


			while (!checkmate) {
				Console.Clear ();
				testBoard.DrawBoard ();
				int tmp = Move.FindPieceByType (testBoard.Pieces, "King", "White");
				int tmp1 = Move.FindPieceByType (testBoard.Pieces, "King", "Black");
				Piece wk = testBoard.Pieces [tmp];
				Piece bk = testBoard.Pieces [tmp1];
				bool cwk = Move.CheckHelp (testBoard.Pieces, wk);
				bool cbk = Move.CheckHelp (testBoard.Pieces, bk);

				//Prints according to player turn
				Console.WriteLine (string.Format ("\tMove: {0}", turn + 1));
				if (turn % 2 == 0) {
					if (!cwk) {
						Console.WriteLine ("\tCheck!\n\tWhites turn");
					} else {
						Console.WriteLine ("\tWhites turn\n");
					}
				} else {
					if (!cbk) {
						Console.WriteLine ("\tCheck!\n\tBlacks turn");
					} else {
						Console.WriteLine ("\tBlacks turn\n");	
					}
				}
				Console.Write ("\tPiece to move: ");
				char a = ValidateCharInput ();
				int b = ValidateIntInput ();

				//Restricts moving other teams pieces
				int check = Move.FindPiece (testBoard.Pieces, a, b);
				if (check > -1) {
					if (turn % 2 == 0) {
						if (testBoard.Pieces [check].Color != "White") {
							continue;
						}
					} else {
						if (testBoard.Pieces [check].Color != "Black") {
							continue;
						}	
					}
				}

				Console.Write (" to ");
				char c = ValidateCharInput ();
				int d = ValidateIntInput ();
				Console.Write ("\n");

				//Restrick invalid moves by coordinates
				if (testBoard.ValidateMoves (a, b, c, d) == 0) {
					continue;
				}
					
				testBoard.ExecuteMoves (a, b, c, d);

				cwk = Move.CheckHelp (testBoard.Pieces, wk);
				cbk = Move.CheckHelp (testBoard.Pieces, bk);

				//Restrict moves that puts your king in chess on your own turn.
				if (turn % 2 == 0) {
					if (!cwk) {
						testBoard.ExecuteMoves (c, d, a, b);
						continue;
					}
				} else {
					if (!cbk) {
						testBoard.ExecuteMoves (c, d, a, b);
						continue;
					}
				}
				turn++;
			}

			//Console.WriteLine("CheckMate!");
		}

		public static char ValidateCharInput()
		{
			char tmp = 'a';
			while (true)
			{
				tmp = Console.ReadKey().KeyChar;
				if (64 < tmp && tmp < 73 || 96 < tmp && tmp < 105)
				{
					break;
				}
				Console.Write("\b \b");
			}
			return tmp;
		}

		public static int ValidateIntInput()
		{
			int retVal = 0;
			while (true)
			{
				char tmp = Console.ReadKey().KeyChar;
				if (char.IsDigit(tmp))
				{
					retVal = int.Parse(tmp.ToString());
					if (retVal < 1 || retVal > 8)
					{
						Console.Write("\b \b");
						continue;
					}
					break;
				}
				Console.Write("\b \b");
			}
			return retVal;
		}
	}
}
