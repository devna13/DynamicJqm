using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
	[TestClass]
	public class GlobalTests
	{
		public const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss";//2000-01-01T00:00:00

		public const string DateFormat = "yyyy-MM-dd";//2000-01-01

		public const string TimeFormat = "HH:mm:ss";//00:00:00

		public static readonly CultureInfo Usa = new CultureInfo("en-US");

		[TestMethod]
		public void test_formats()
		{
			Assert.IsTrue(Global.DateFormat == DateFormat);

			Assert.IsTrue(Global.DateTimeFormat == DateTimeFormat);
			Assert.IsTrue(Global.TimeFormat == TimeFormat);
			Assert.IsTrue(Global.Usa.Name == Usa.Name);

		}

	}
}
