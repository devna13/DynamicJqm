using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
	[TestClass]
	public class RandDTests
	{
		[TestMethod]
		public void pass_by_ref()
		{
			string test = "before passing";
			Console.WriteLine(test);
			TestI(ref test);
			Console.WriteLine(test);
		}

		public void TestI(ref string test)
		{
			test = "after passing";
		}


		[TestMethod]
		public void pass_obj()
		{
			string test = "before passing";
			Console.WriteLine(test);
			TestI(test);
			Console.WriteLine(test);
		}

		public void TestI(string test)
		{
			test = "after passing";
		}



		static DateTime ConvertFromDateTimeOffset(DateTimeOffset dateTime)
		{
			if (dateTime.Offset.Equals(TimeSpan.Zero))
				return dateTime.UtcDateTime;
			else if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
				return DateTime.SpecifyKind(dateTime.DateTime, DateTimeKind.Local);
			else
				return dateTime.DateTime;
		}

		[TestMethod]
		public void offset_to_timezone()
		{
			DateTime thisDate = new DateTime(2007, 3, 10, 0, 0, 0);
			DateTime dstDate = new DateTime(2007, 6, 10, 0, 0, 0);
			DateTimeOffset thisTime;

			thisTime = new DateTimeOffset(dstDate, new TimeSpan(-7, 0, 0));
			ShowPossibleTimeZones(thisTime);

			thisTime = new DateTimeOffset(thisDate, new TimeSpan(-6, 0, 0));
			ShowPossibleTimeZones(thisTime);

			thisTime = new DateTimeOffset(thisDate, new TimeSpan(+1, 0, 0));
			ShowPossibleTimeZones(thisTime);
		}

		private void ShowPossibleTimeZones(DateTimeOffset offsetTime)
		{
			TimeSpan offset = offsetTime.Offset;
			ReadOnlyCollection<TimeZoneInfo> timeZones;

			Console.WriteLine("{0} could belong to the following time zones:",
							  offsetTime.ToString());
			// Get all time zones defined on local system
			timeZones = TimeZoneInfo.GetSystemTimeZones();
			// Iterate time zones  
			foreach (TimeZoneInfo timeZone in timeZones)
			{
				// Compare offset with offset for that date in that time zone 
				if (timeZone.GetUtcOffset(offsetTime.DateTime).Equals(offset))
					Console.WriteLine("   {0}", timeZone.DisplayName);
			}
			Console.WriteLine();
		} 



		[TestMethod]
		public void check_daylight_saving()
		{

			DateTime thisTime = DateTime.Now;

			TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
			bool isDaylight = tst.IsDaylightSavingTime(thisTime);

			Console.WriteLine("DLS for central is " + isDaylight +" -offset "+ tst.GetUtcOffset(thisTime));

			tst = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
			isDaylight = tst.IsDaylightSavingTime(thisTime); ;

			Console.WriteLine("DLS for eastern is " + isDaylight + " -offset " + tst.GetUtcOffset(thisTime));
		}


		[TestMethod]
		public void convert_time()
		{
			DateTimeOffset localTime, otherTime, universalTime;

			// Define local time in local time zone
			localTime = new DateTimeOffset(new DateTime(2007, 6, 15, 12, 0, 0));
			Console.WriteLine("Local time: {0}", localTime);
			Console.WriteLine();

			// Convert local time to offset 0 and assign to otherTime
			otherTime = localTime.ToOffset(TimeSpan.Zero);
			Console.WriteLine("Other time: {0}", otherTime);
			Console.WriteLine("{0} = {1}: {2}",
							  localTime, otherTime,
							  localTime.Equals(otherTime));
			Console.WriteLine("{0} exactly equals {1}: {2}",
							  localTime, otherTime,
							  localTime.EqualsExact(otherTime));
			Console.WriteLine();

			// Convert other time to UTC
			universalTime = localTime.ToUniversalTime();
			Console.WriteLine("Universal time: {0}", universalTime);
			Console.WriteLine("{0} = {1}: {2}",
							  otherTime, universalTime,
							  universalTime.Equals(otherTime));
			Console.WriteLine("{0} exactly equals {1}: {2}",
							  otherTime, universalTime,
							  universalTime.EqualsExact(otherTime));
			Console.WriteLine();
			//-------------------------------------------------------------------
			DateTime timeUtc = DateTime.UtcNow;
			try
			{
				TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
				DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
				Console.WriteLine("The date and time are {0} {1}.",
								  cstTime,
								  cstZone.IsDaylightSavingTime(cstTime) ?
										  cstZone.DaylightName : cstZone.StandardName);
			}
			catch (TimeZoneNotFoundException)
			{
				Console.WriteLine("The registry does not define the Central Standard Time zone.");
			}
			catch (InvalidTimeZoneException)
			{
				Console.WriteLine("Registry data on the Central STandard Time zone has been corrupted.");
			}
			//-------------------------------------------------------------------
			DateTime timeAz = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Local);


			Console.WriteLine(TimeZoneInfo.Utc.GetUtcOffset(timeAz));
			Console.WriteLine(TimeZoneInfo.Local.GetUtcOffset(timeAz));



			DateTime timeComponent = new DateTime(2008, 6, 19, 7, 0, 0);
			DateTime returnedDate;

			// Convert UTC time
			DateTimeOffset utcTime = new DateTimeOffset(timeComponent, TimeSpan.Zero);
			returnedDate = ConvertFromDateTimeOffset(utcTime);
			Console.WriteLine("{0} converted to {1} {2}",
							  utcTime,
							  returnedDate,
							  returnedDate.Kind.ToString());

			// Convert local time
			localTime = new DateTimeOffset(timeComponent,
									   TimeZoneInfo.Local.GetUtcOffset(timeComponent));
			returnedDate = ConvertFromDateTimeOffset(localTime);
			Console.WriteLine("{0} converted to {1} {2}",
							  localTime,
							  returnedDate,
							  returnedDate.Kind.ToString());

			// Convert Central Standard Time
			DateTimeOffset cstTime2 = new DateTimeOffset(timeComponent,
						   TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time").GetUtcOffset(timeComponent));
			returnedDate = ConvertFromDateTimeOffset(cstTime2);
			Console.WriteLine("{0} converted to {1} {2}",
							  cstTime2,
							  returnedDate,
							  returnedDate.Kind.ToString());
			// The example displays the following output to the console: 
			//    6/19/2008 7:00:00 AM +00:00 converted to 6/19/2008 7:00:00 AM Utc 
			//    6/19/2008 7:00:00 AM -07:00 converted to 6/19/2008 7:00:00 AM Local 
			//    6/19/2008 7:00:00 AM -05:00 converted to 6/19/2008 7:00:00 AM Unspecified
			return;

			try
			{
				TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
				DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeAz, cstZone);
				Console.WriteLine("The date and time are {0} {1}.",
								  cstTime,
								  cstZone.IsDaylightSavingTime(cstTime) ?
										  cstZone.DaylightName : cstZone.StandardName);
			}
			catch (TimeZoneNotFoundException)
			{
				Console.WriteLine("The registry does not define the Central Standard Time zone.");
			}
			catch (InvalidTimeZoneException)
			{
				Console.WriteLine("Registry data on the Central STandard Time zone has been corrupted.");
			}
			//------------------------------------------------------------------
			DateTime easternTime = new DateTime(2007, 01, 02, 12, 16, 00);
			string easternZoneId = "Eastern Standard Time";
			try
			{
				TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById(easternZoneId);
				Console.WriteLine("The date and time are {0} UTC.",
								  TimeZoneInfo.ConvertTimeToUtc(easternTime, easternZone));
			}
			catch (TimeZoneNotFoundException)
			{
				Console.WriteLine("Unable to find the {0} zone in the registry.",
								  easternZoneId);
			}
			catch (InvalidTimeZoneException)
			{
				Console.WriteLine("Registry data on the {0} zone has been corrupted.",
								  easternZoneId);
			}
		
		}



	}
}
