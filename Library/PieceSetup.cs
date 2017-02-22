
namespace Library
{
	public static class PieceSetup
	{

		private static char[] CharField = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
		private static string[] PieceType = { "Pawn", "Rook", "Horse", "Bishop", "Queen", "King", "Bishop", "Horse", "Rook" };
		private static char[] UnicodeWhite = { '\u2659', '\u2656', '\u2658', '\u2657', '\u2655', '\u2654', '\u2657', '\u2658', '\u2656' };
		private static char[] UnicodeBlack = { '\u265F', '\u265C', '\u265E', '\u265D', '\u265B', '\u265A', '\u265D', '\u265E', '\u265C' };

		public static Piece[] Setup()
		{
			Piece[] tmp = new Piece[32];
			// Create white pieces array
			Piece[] whiteTeam = CreateTeam ("White", UnicodeWhite);
			// Create black pieces array
			Piece[] blackTeam = CreateTeam ("Black", UnicodeBlack);
			// Concatenate the two arrays
			whiteTeam.CopyTo (tmp, 0);
			blackTeam.CopyTo (tmp, 16);
			// Return array
			return tmp;
		}

		private static Piece[] CreateTeam(string str, char[] uni)
		{
			Piece[] tmp = new Piece[16];
			int j = 0;

			for (int i = 0; i < 8; i++) {
				tmp [j] = new Piece (str, PieceType[0], uni[0], CharField[i], PawnPos(str));
				j++;
			}

			for (int i = 0; i < 8; i++) {
				tmp [j] = new Piece (str, PieceType[i+1], uni[i+1], CharField[i], BacklinePos(str));
				j++;
			}
			return tmp;
		}

		private static int PawnPos(string str)
		{
			if (str == "White") {
				return 2;
			}
			return 7;
		}

		private static int BacklinePos(string str)
		{
			if (str == "White") {
				return 1;
			}
			return 8;
		}
	}
}

