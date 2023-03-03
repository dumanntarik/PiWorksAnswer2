using PiWorksAnswer2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;



class Program
{
	static void Main(string[] args)
	{
		// Strictly define all rows read from the database as a list.
		var db = new exmp();
		var entries = db.NewTables.ToList();


		// Let's get the pure url information for each line
		foreach (var entry in entries)
		{
			string url = GetPureUrlFromStatsAccessLink(entry.StatsAccessLink!.ToLower());
			Console.WriteLine($"{entry.DeviceType}: {url}");
		}
	}



	static string GetPureUrlFromStatsAccessLink(string statsAccessLink)
	{

		// First remove the xml tags
		string xmlRemoved = Regex.Replace(statsAccessLink, @"<[^>]+>|</[^>]+>", "");

		// Then remove the protocol part
		string protocolRemoved = Regex.Replace(xmlRemoved, @"^\w+://", "");

		// Finally, let's find the pure url information with a regex of characters that allow underscore/period characters
		string pattern = @"^[a-z0-9_.-]+$";
		var match = Regex.Match(protocolRemoved, pattern);

		// If a match is found, let's return the value of that match
		if (match.Success)
		{
            return match.Value;
		}

		// If no match is found, let's return an empty string
		return "";
	}
}
