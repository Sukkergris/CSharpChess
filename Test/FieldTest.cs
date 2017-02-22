using NUnit.Framework;
using System;
using Library;

namespace Test
{
	[TestFixture ()]
	public class FieldTest
	{
		Field test = new Field('a', 1, "Black");
		Field test1 = new Field('a', 2, "White");
		Field test2 = new Field('a', 1, "Black");

		[Test ()]
		public void EqualOperator ()
		{
			Assert.AreEqual (test == test2, true);
		}

		[Test ()]
		public void NotEqualOperator ()
		{
			Assert.AreEqual (test != test1, true);
		}

		[Test ()]
		public void EqualsOverride ()
		{
			Assert.AreEqual (test.Equals(test2), true);
		}

		[Test ()]
		public void EqualsOverrideFalse ()
		{
			Assert.AreEqual (test.Equals(test1), false);
		}

		[Test ()]
		public void ChangeState ()
		{
			var tmp = test.State;
			test.ChangeState ();
			var tmp1 = test.State;
			Assert.AreNotEqual (tmp, tmp1);
		}

	}
}

