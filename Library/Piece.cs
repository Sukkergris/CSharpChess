
namespace Library
{
	public class Piece
	{
		public string Color;
		public string Type;
		public char Unicode;
		public readonly int hash;
		public char X;
		public int Y;

		public Piece ()
		{
			Color = null;
			Type = null;
			Unicode = 'x';
			hash = 1;
			X = 'z';
			Y = -1;
		}
			
		public Piece (string color, string type, char unicode, char x, int y)
		{
			Color = color;
			Type = type;
			Unicode = unicode;
			hash = x ^ y;
			X = x;
			Y = y;
		}
			
		public enum StateType { Alive, Dead };

		public StateType State {
			get;
			private set;
		}

		public void ChangeStatusState()
		{
			if (State == StateType.Alive) {
				State = StateType.Dead;
			} else {
				State = StateType.Alive;
			}
		}

		public enum StateMoved { Unmoved, Moved };

		public StateMoved Moved {
			get;
			private set;
		}

		public void ChangeMovedState()
		{
			Moved = StateMoved.Moved;
		}
			
		//Overrides
		public override string ToString()
		{
			return string.Format ("{0} {1} at ({2}, {3}) - {4}", Color, Type, X, Y, State);
		}

		public static bool operator ==(Piece p1, Piece p2)
		{
			return (p1.Type == p2.Type && p1.X == p2.X && p1.Y == p2.Y && p1.Color == p2.Color);
		}

		public static bool operator !=(Piece p1, Piece p2)
		{
			return !(p1 == p2);
		}

		public override bool Equals (object obj)
		{
			if (obj == null)
			{
				return false;
			}

			var p = obj as Piece;
			if ((object)p == null)
			{
				return false;
			}

			return (Type == p.Type && X == p.X && Y == p.Y && Color == p.Color);
		}

		public override int GetHashCode ()
		{
			return hash;
		}

	}
}

