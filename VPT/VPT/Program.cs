using System.Text.RegularExpressions;
using VptLibrary;
EventSpace eventSpace = new EventSpace();

Console.WriteLine("");

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

