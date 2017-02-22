
namespace Library
{
	public class Field
	{
		public readonly char X;
		public readonly int Y;
		public readonly string Color;

		public Field (char c, int i, string color)
		{
			X = c;
			Y = i;
			Color = color;
		}

		public override string ToString()
		{
			return string.Format ("({0}, {1}, {2}, {3})", X, Y, Color, State);
		}

		// Overload operators
		public static bool operator ==(Field f1, Field f2)
		{
			return f1.X == f2.X && f1.Y == f2.Y && f1.Color == f2.Color;
		}

		public static bool operator !=(Field f1, Field f2)
		{
			return !(f1 == f2);
		}

		// Overide Equals
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			var p = obj as Field;
			if ((object)p == null)
			{
				return false;
			}

			return (X == p.X) && (Y == p.Y) && (Color == p.Color);
		}

		//override GetHashCode
		public override int GetHashCode ()
		{
			return X ^ Y;
		}

		// Manage field state
		public enum StateType { Free, Occupied };

		public StateType State {
			get;
			private set;
		}

		public void ChangeState()
		{
			if (State == StateType.Occupied) {
				State = StateType.Free;	
			} else {
				State = StateType.Occupied;
			}
		}
	}
}

