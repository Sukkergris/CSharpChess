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
			Console.Title = "Chess v.1.1 - No auto check for remi and no castling.";
			bool checkmate = false;

			while (!checkmate) {
				Console.Clear ();
				testBoard.DrawBoard ();
				int tmp = Move.FindPieceByType (testBoard.Pieces, "King", "White");
				int tmp1 = Move.FindPieceByType (testBoard.Pieces, "King", "Black");
				Piece wk = testBoard.Pieces [tmp];
				Piece bk = testBoard.Pieces [tmp1];
				bool cwk = Move.CheckHelp (testBoard.Pieces, wk);
				bool cbk = Move.CheckHelp (testBoard.Pieces, bk);
				bool tmp3;

 				//Prints according to player turn
				Console.WriteLine (string.Format ("\tMove: {0}", turn + 1));
				if (turn % 2 == 0) 
				{
					if (!cwk) 
					{
						tmp3 = CheckMate (testBoard, "White");
						if (tmp3) {
							checkmate = tmp3;
							continue;
						}
						Console.WriteLine ("\tCheck!\n\tWhites turn");
					} 
					else 
					{
						Console.WriteLine ("\tWhites turn\n");
					}
				} 
				else 
				{
					if (!cbk) 
					{
						tmp3 = CheckMate (testBoard, "Black");
						if (tmp3) {
							checkmate = tmp3;
							continue;
						}
						Console.WriteLine ("\tCheck!\n\tBlacks turn");
					} 
					else 
					{
						Console.WriteLine ("\tBlacks turn\n");	
					}
				}
				Console.Write ("\tPiece to move: ");
				char a = ValidateCharInput ();
				int b = ValidateIntInput ();

				//Restricts moving other teams pieces
				int check = Move.FindPiece (testBoard.Pieces, a, b);
				if (check < 0) {
					continue;
				}

				if (turn % 2 == 0) {
					if (testBoard.Pieces [check].Color != "White") 
					{
						continue;
					}
				} 
				else 
				{
					if (testBoard.Pieces [check].Color != "Black") 
					{
						continue;
					}	
				}

				Console.Write (" to ");
				char c = ValidateCharInput ();
				int d = ValidateIntInput ();
				Console.Write ("\n");

				if (a == c && b == d) {
					continue;
				}

				int cstPTI = Move.FindPiece (testBoard.Pieces, c, d);
				Piece cstP = testBoard.Pieces[check];
				Piece cstPT = new Piece();
				int xDist = Math.Abs (cstP.X - cstPT.X);
				bool checkCst;
				bool checkCst1;
				bool checkCst2;
				bool checkCst3;
				bool checkCst4;
//				bool castling = false;

				if (cstPTI > -1) {
					cstPT = testBoard.Pieces [cstPTI];
					checkCst = cstP.Moved == Piece.StateMoved.Unmoved;
					checkCst1 = cstPT.Moved == Piece.StateMoved.Unmoved;
					checkCst2 = cstP.Type == "King";
					checkCst3 = cstPT.Type == "Rook";
					checkCst4 = cstPT.Color == cstP.Color;
					bool finalCst = true;
					if (checkCst && checkCst1 && checkCst2 && checkCst3 && checkCst4) {
						xDist = Math.Abs (cstP.X - cstPT.X);
						if (xDist == 4) 
						{
							for (int i = 1; i < xDist; i++) {
								if (Move.FindPiece(testBoard.Pieces, (char)(cstPT.X+i), cstPT.Y) > -1) {
									finalCst = false;
								}
							}
						} 
						else 
						{
							for (int i = 1; i < xDist; i++) {
								if (Move.FindPiece(testBoard.Pieces, (char)(cstP.X+i), cstP.Y) > -1) {
									finalCst = false;
								}
							}
						}
					}
					castling = finalCst;
				}

				//Restrick invalid moves by coordinates
				if (testBoard.ValidateMoves (a, b, c, d) == 0) {
					continue;
				} 

				testBoard.ExecuteMoves (a, b, c, d);


				cwk = Move.CheckHelp (testBoard.Pieces, wk);
				cbk = Move.CheckHelp (testBoard.Pieces, bk);

				//Restrict moves that puts your king in chess on your own turn.
				if (turn % 2 == 0) 
				{
					if (!cwk) 
					{
						//If castling, execute different move.
						testBoard.ExecuteMoves (c, d, a, b);
						continue;
					}
				} 
				else 
				{
					if (!cbk) 
					{
						testBoard.ExecuteMoves (c, d, a, b);
						continue;
					}
				}

				turn++;
			}

			Console.WriteLine("CheckMate!");
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

		public static bool CheckMate(Board b, string checkColor)
		{
			bool retval = true;
			int tmp = Move.FindPieceByType(b.Pieces, "King", checkColor);
			Piece king = b.Pieces[tmp];
			foreach (Piece p in b.Pieces) 
			{
				if (p.Color == king.Color) 
				{
					char x = p.X;
					int y = p.Y;
					foreach (Field f in b.Fields) 
					{
						if (b.ValidateMoves(x, y, f.X, f.Y) == 1) 
						{
							int tmp1 = Move.FindPiece(b.Pieces, f.X, f.Y);
							char tmpX = 'k';
							int tmpY = -2;
							if (tmp1 > -1) {
								tmpX = b.Pieces[tmp1].X;
								tmpY = b.Pieces[tmp1].Y;
							}
							b.ExecuteMoves(x, y, f.X, f.Y);
							if (Move.CheckHelp(b.Pieces, king)) 
							{
								b.ExecuteMoves(p.X, p.Y, x, y);
								if (tmp1 > -1) {
									b.Pieces [tmp1].X = tmpX;
									b.Pieces [tmp1].Y = tmpY;
								}
								return false;
							}
							b.ExecuteMoves(p.X, p.Y, x, y);
							if (tmp1 > -1) {
								b.Pieces [tmp1].X = tmpX;
								b.Pieces [tmp1].Y = tmpY;
								b.Pieces [tmp1].StatusAlive ();
							}
						}
					}	
				}
			}
			return retval;
		}
	}
}
