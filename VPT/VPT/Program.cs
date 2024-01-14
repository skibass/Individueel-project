using System.Text.RegularExpressions;
using VptLibrary;
EventSpace eventSpace = new EventSpace();

Console.WriteLine("");

foreach (var part in eventSpace.Parts)
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
                Console.WriteLine(chair.EventChairName + " | Age:" + chair.Visitor.Age);
            }
            else
            {
                Console.WriteLine(chair.EventChairName + " | EMPTY");
            }
        }
    }
    Console.WriteLine("");
}

