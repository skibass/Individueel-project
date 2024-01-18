using System.Text.RegularExpressions;
using VptLibrary;
EventSpace eventSpace = new EventSpace();

Console.WriteLine("-------------------------------");
Console.WriteLine($"|| Event date: {eventSpace.LastSignUpDatePossibility}");
Console.WriteLine($"|| Visitor limit: {eventSpace.VisitorLimit}");
Console.WriteLine($"|| Visitors who signed up: {eventSpace.AllVisitors.Count()}");
Console.WriteLine($"|| Visitors who signed up on time: {eventSpace.AllVisitors.Count(a => a.SignedOnTime)}");

// Did not add chairs based on visitor limit so this amount can sometimes be more than are seated because there isnt enough space
Console.WriteLine($"|| Visitors who signed up on time and are allowed: {eventSpace.AllVisitors.Count(a => a.SignedOnTime && a.IsVisitorAllowedInBasedOnAge)}");
Console.WriteLine($"|| Visitors who are seated: {eventSpace.AllVisitors.Count(a => a.IsSeated)}");


foreach (var part in eventSpace.Parts)
{
    if (part.IsPartInUse == true)
    {
		Console.WriteLine("-------------------------------");
		Console.WriteLine(part.Letter);

		foreach (var row in part.Rows)
		{
			Console.WriteLine("--");
			Console.WriteLine(row.EventRowName);
			Console.WriteLine("--");

			foreach (var chair in row.Chairs)
			{
				
				if (chair.Visitor != null)
				{
					if (chair.Visitor.GroupNumber != 0) 
					{
						Console.WriteLine($"{chair.EventChairName} | ID: {chair.Visitor.Id} | Group: {chair.Visitor.GroupNumber} | Age: {chair.Visitor.Age} | date: {chair.Visitor.SignUpDate}");
					}
					else
					{
						Console.WriteLine($"{chair.EventChairName} | ID: {chair.Visitor.Id} | Group: ALONE | Age: {chair.Visitor.Age} | date: {chair.Visitor.SignUpDate} ");
					}
				}
				else
				{
					Console.WriteLine($"{chair.EventChairName} | EMPTY");
				}
			}
		}
		Console.WriteLine("");
	}   
}

