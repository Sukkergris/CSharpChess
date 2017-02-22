using System;

namespace Library
{
	public class Board
	{
		public readonly Field[] Fields = new Field[64];
		public readonly Piece[] Pieces = new Piece[32];

		public Board()
		{
			Fields = FieldSetup.Setup();
			Pieces = PieceSetup.Setup();
		}

		public int ValidateMoves(char x, int y, char z, int u)
		{
			int p = Move.FindPiece(Pieces, x, y);
			if (p > -1 && Move.ValidateMove(Pieces, Pieces[p], z, u))
			{
				var ft = Move.ValidateTarget(Pieces, Pieces[p], z, u);
				if (ft != -1)
				{
					return 1;
				}
			}
			return 0;
		}

		public void ExecuteMoves(char x, int y, char z, int u) 
		{
			int p = Move.FindPiece(Pieces, x, y);
			int kp = Move.FindPiece(Pieces, z, u);
			if (kp > -1)
			{
				Pieces[kp].X = 'z';
				Pieces[kp].Y = -1;
				Pieces[kp].ChangeStatusState();
			}

			Pieces[p].X = z;
			Pieces[p].Y = u;
			Pieces[p].ChangeMovedState();
		}

		public void cprintf(string c, ConsoleColor b)
		{
			Console.ForegroundColor = b;
			Console.Write(c);
			Console.ResetColor();
		}

		public void whitefield(string c, string p, string b)
		{
			ConsoleColor pCol = ConsoleColor.Magenta;
			ConsoleColor bCol = ConsoleColor.Cyan;
			if (p == "White")
			{
				pCol = ConsoleColor.Blue;
			}
			else
			{
				pCol = ConsoleColor.Red;
			}
			if (b == "White")
			{
				bCol = ConsoleColor.White;
			}
			else
			{
				bCol = ConsoleColor.Black;
			}
			Console.ForegroundColor = pCol;
			Console.BackgroundColor = bCol;
			Console.Write(c);
			Console.ResetColor();
		}

		public void DrawBoard()
		{
			Console.WriteLine ();
			Console.WriteLine ();
			Console.WriteLine("\t   a  b  c  d  e  f  g  h ");
			for (int q = 1; q < 9; q++)
			{
				Console.Write("\t{0} ", q);
				for (int p = 'a'; p < 'i'; p++)
				{
					int fi = Move.FindField(Fields, (char)p, q);
					if (fi > -1)
					{
						int pi = Move.FindPiece(Pieces, (char)p, q);
						if (pi > -1)
						{
							whitefield(" " + Pieces[pi].Unicode + " ", Pieces[pi].Color, Fields[fi].Color);
						}
						else
						{
							whitefield("   ", Fields[fi].Color, Fields[fi].Color);
						}
					}
				}
				Console.WriteLine();
			}
		}

		// Overrides
		public static bool operator ==(Board b1, Board b2)
		{
			bool fieldsHelp = true;
			bool piecesHelp = true;

			for (int i = 0; i < b1.Fields.Length - 1; i++) {
				if (b1.Fields[i] != b2.Fields[i]) {
					fieldsHelp = false;
				}
			}

			for (int i = 0; i < b1.Pieces.Length - 1; i++) {
				if (b1.Pieces[i] != b2.Pieces[i]) {
					piecesHelp = false;
				}
			}

			return (fieldsHelp && piecesHelp);
		}

		public static bool operator !=(Board b1, Board b2)
		{
			return !(b1 == b2);
		}

		private bool Comparer (Board p)
		{
			bool fieldsHelp = true;
			bool piecesHelp = true;

			for (int i = 0; i < Fields.Length - 1; i++) {
				if (Fields[i] != p.Fields[i]) {
					fieldsHelp = false;
				}
			}

			for (int i = 0; i < Pieces.Length - 1; i++) {
				if (Pieces[i] != p.Pieces[i]) {
					piecesHelp = false;
				}
			}

			return (fieldsHelp && piecesHelp);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var p = obj as Board;
			if ((object)p == null)
			{
				return false;
			}

			return (Comparer(p));
		}

		//override GetHashCode
		public override int GetHashCode ()
		{
			int PieceYpos = 0;

			for (int i = 0; i < Pieces.Length - 1; i++) {
				PieceYpos += Pieces[i].GetHashCode();
			}

			return PieceYpos;
		}
	}
}

