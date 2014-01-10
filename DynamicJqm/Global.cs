using System;
using System.Globalization;

namespace DynamicJqm
{
	public static class Global
	{
		//public static readonly string TestDataKeyStr = "testdata";

		public const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss";//2000-01-01T00:00:00

		public const string DateFormat = "yyyy-MM-dd";//2000-01-01

		public const string TimeFormat = "HH:mm:ss";//00:00:00

		public static readonly CultureInfo Usa = new CultureInfo("en-US");

		public static string RandomSafeHtmlId()
		{
			return Guid.NewGuid().ToString("N");
		}

	}
}