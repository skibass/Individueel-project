using VptLibrary;
Visitor visitor = new Visitor();
EventSpace eventSpace = new EventSpace();

Console.WriteLine("");

foreach (var part in eventSpace.Parts)
{
    Console.WriteLine("-------------------------------");
    Console.WriteLine(part.Letter);

	foreach (var row in part.Rows)
	{
        Console.WriteLine("");
        Console.WriteLine(row.EventRowName);
        Console.WriteLine("");

        foreach (var chair in row.Chairs)
		{
            Console.WriteLine(chair.EventChairName);
        }
    }
    Console.WriteLine("");

}

