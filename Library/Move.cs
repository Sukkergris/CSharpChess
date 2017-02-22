using System;

namespace Library
{
	public static class Move
	{
		public static int FindField(Field[] fields, char x, int y)
		{
			int n = 0;
			for (int i = 0; i < fields.Length; i++)
			{
				if (fields[i].X == x && fields[i].Y == y)
				{
					return i;
				}
				n++;
			}
			return -2;
		}

		public static int FindPiece(Piece[] pieces, char x, int y)
		{
			for (int i = 0; i < pieces.Length; i++)
			{
				if (pieces[i].X == x && pieces[i].Y == y)
				{
					if (pieces[i].State == Piece.StateType.Alive)
					{
						return i;
					}
					return -1;
				}
			}
			return -2;
		}

		public static int FindPieceByType(Piece[] pl, string type, string color)
		{
			for (int i = 0; i < pl.Length; i++) {
				if (pl[i].Type == type && pl[i].Color == color) {
					return i;
				}
			}
			return -1;
		}

		public static int ValidateTarget(Piece[] pieces, Piece p, char z, int u)
		{
			int target = FindPiece (pieces, z, u);
			if (target > -1) {
				if (pieces[target].Color == p.Color) {
					return -1;
				}
			}

			return target;
		}


		public static bool ValidateMove(Piece[] pl, Piece p, char z, int u)
		{
			int yDist = Math.Abs(p.Y - u);
			int xDist = Math.Abs(z - p.X);

			switch (p.Type)
			{
				case "Pawn":
					int f = FindPiece(pl, z, u);

					if (xDist == 0 && yDist == 1 && f < 0)
					{
						if (p.Color == "White" && p.Y < u)
						{
							return true;
						}

						if (p.Color == "Black" && p.Y > u)
						{
							return true;
						}
					}

					if (yDist == 2 && xDist == 0 && p.Moved == Piece.StateMoved.Unmoved)
					{
						if (p.Color == "White" && p.Y < u)
						{
							return f < 0 && FindPiece(pl, z, u - 1) < 0;
						}

						if (p.Color == "Black" && p.Y > u)
						{
							return f < 0 && FindPiece(pl, z, u + 1) < 0;
						}
					}

					return (yDist == 1 && xDist == 1 && f > -1);
					
				case "Rook":
					if (0 < yDist && yDist < 8 && xDist == 0)
					{
						return RookHelp(pl, p, z, u);
					}

					if (0 < xDist && xDist < 8 && yDist == 0)
					{
						return RookHelp(pl, p, z, u);
					}

					return false;

				case "Horse":
					if (yDist == 2 && xDist == 1)
					{
						return true;
					}

					if (xDist == 2 && yDist == 1)
					{
						return true;
					}

					return false;
					
				case "Bishop":
					if (yDist == xDist)
					{
						return DiagHelp (pl, p, z, u);
					}

					return false;

				case "Queen":
					if (yDist == xDist)
					{
						return DiagHelp (pl, p, z, u);
					}

					if (0 < yDist && yDist < 8 && xDist == 0)
					{
						return RookHelp(pl, p, z, u);
					}

					if (0 < xDist && xDist < 8 && yDist == 0)
					{
						return RookHelp(pl, p, z, u);
					}

					return false;

				case "King":
					int OtherKing = 1;
					if (p.Color == "White") {
						OtherKing = FindPieceByType (pl, "King", "Black");	
					} else {
						OtherKing = FindPieceByType (pl, "King", "White");
					}

					int KingYDist = Math.Abs (u - pl [OtherKing].Y);
					int KingXDist = Math.Abs (z - pl [OtherKing].X);
					return ((yDist == 1 && xDist == 1) || (yDist == 0 && xDist == 1) || (yDist == 1 && xDist == 0)) && (KingXDist > 1 || KingYDist > 1);

				default:
					return false;
			}
		}

		public static bool RookHelp(Piece[] pl, Piece p, char x1, int y1) 
		{
			int yDist = Math.Abs(p.Y - y1);
			int xDist = Math.Abs(p.X - x1);

			if (yDist == 0 && p.X < x1)
			{
				for (int i = p.X + 1; i < x1; i++)
				{
					if (FindPiece(pl, (char)i, y1) > -1)
					{
						return false;
					}
				}
			}

			if (yDist == 0 && p.X > x1)
			{
				for (int i = x1 + 1; i < p.X; i++)
				{
					if (FindPiece(pl, (char)i, y1) > -1)
					{
						return false;
					}
				}
			}

			if (xDist == 0 && p.Y < y1)
			{
				for (int i = p.Y + 1; i < y1; i++)
				{
					if (FindPiece(pl, x1, i) > -1)
					{
						return false;
					}
				}
			}

			if (xDist == 0 && p.Y > y1)
			{
				for (int i = y1 + 1; i < p.Y; i++)
				{
					if (FindPiece(pl, x1, i) > -1)
					{
						return false;
					}
				}
			}

			return true;
		}

		public static bool DiagHelp(Piece[] pl, Piece p, char x1, int y1)
		{
			int yDist = Math.Abs(p.Y - y1);

			if (p.X < x1 && p.Y < y1) {
				for (int i = 1; i < yDist; i++) {
					if (FindPiece(pl, (char)(p.X+i), p.Y+i) > -1) {
						return false;
					}
				}
			}

			if (p.X < x1 && p.Y > y1) {
				for (int i = 1; i < yDist; i++) {
					if (FindPiece(pl, (char)(p.X+i), p.Y-i) > -1) {
						return false;
					}
				}
			}

			if (p.X > x1 && p.Y > y1) {
				for (int i = 1; i < yDist; i++) {
					if (FindPiece(pl, (char)(p.X-i), p.Y-i) > -1) {
						return false;
					}
				}	
			}

			if (p.X > x1 && p.Y < y1) {
				for (int i = 1; i < yDist; i++) {
					if (FindPiece(pl, (char)(p.X-i), p.Y+i) > -1) {
						return false;
					}
				}
			}

			return true;
		}

		public static bool CheckHelp(Piece[] pl, Piece p)
		{
			for (int i = 0; i < pl.Length; i++)
			{
				if (pl[i] != p && pl[i].Color != p.Color)
				{
					if (ValidateMove(pl, pl[i], p.X, p.Y))
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}

