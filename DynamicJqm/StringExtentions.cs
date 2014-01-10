using System;

namespace DynamicJqm
{
	public static class StringExtentions
	{
		public static string F(this string format, params object[] args)
		{
			if (format == null)
				throw new ArgumentNullException("format");

			return string.Format(format, args);
		}

		public static bool IsNullOrEmpty(this string str)
		{
			return string.IsNullOrEmpty(str);
		}

		public static bool IsNotNullOrEmpty(this string str)
		{
			return !string.IsNullOrEmpty(str);
		}


		public static bool CompareIgnoreCase(this string a, string b)
		{
			return System.String.Compare(a, b, System.StringComparison.OrdinalIgnoreCase) == 0;
		}

		public static string Str(this Enum eff)
		{
			return Enum.GetName(eff.GetType(), eff);
		}


		public static int CountStringOccurrences(this string text, string pattern)
		{
			// Loop through all instances of the string 'text'.
			int count = 0;
			int i = 0;
			while ((i = text.IndexOf(pattern, i)) != -1)
			{
				i += pattern.Length;
				count++;
			}
			return count;
		}
	}
}