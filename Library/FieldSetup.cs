

namespace Library
{
	public static class FieldSetup
	{
		private static char[] CharField = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
		private static int fieldsHelp = 0;
		private static int fieldsColorHelp = 1;

		private static string FieldColor() 
		{
			if (fieldsColorHelp % 2 == 0) {
				return "White";
			} else {
				return "Black";
			}
		}

		public static Field[] Setup()
		{
			Field[] tmp = new Field[64];
			for (int j = 1; j < 9; j++) {
				for (int i = 0; i < 8; i++) {
					tmp [fieldsHelp] = new Field (CharField[i], j, FieldColor());
					if (j == 1 || j == 2 || j == 7 || j == 8) {
						tmp [fieldsHelp].ChangeState ();
					}
					fieldsHelp += 1;
					fieldsColorHelp += 1;
				}
				fieldsColorHelp -= 1;
			}
			fieldsHelp = 0;
			fieldsColorHelp = 1;
			return tmp;
		}
	}
}

