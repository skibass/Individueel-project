using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VptLibrary
{
    // min 1 max 3 rows, every row is same length
    public class Part
    {
        public char Letter { get; set; }
        public bool IsPartInUse { get; set; }
        public List<Row> Rows { get; set; }

        public Part(char letter)
        {
            this.Letter = letter;
            Rows = new List<Row>();
            GetRows();
        }

        private void GetRows()
        {
            int rowNr = 1;
            Random randRows = new Random();
            Random randChairs = new Random();
            int amountOfChairsThisRow = randChairs.Next(3, 11);

            for (int i = 0; i < randRows.Next(1, 4); i++)
            {
                Row row = new Row(Letter, rowNr++, amountOfChairsThisRow);
                Rows.Add(row);
            }
        }

        public void SetupRows(List<Visitor> grouplessVisitors, List<Group> groups, List<Visitor> allVisitors)
        {          
            if (allVisitors.Count(v => v.SignedOnTime) > 0)
            {
                foreach (var item in Rows)
                {
                    while (!RowIsReady(item, allVisitors))
                    {
                        item.PlaceVisitors(allVisitors);
                        var t = item.Chairs.Count(v => v.IsTaken == true);
                        var f = allVisitors.Any(v => v.IsVisitorAllowedInBasedOnAge);
                    }

                    if (item.Chairs.Count(v => v.IsTaken == true) == item.Chairs.Count())
                    {
                        item.IsRowFull = true;
                    }
                }
            }

        }
        private bool RowIsReady(Row item, List<Visitor> allVisitors)
        {
            var g = allVisitors.Count(v => v.IsVisitorAllowedInBasedOnAge && v.SignedOnTime && v.IsSeated);
			var t = item.Chairs.Count(v => v.IsTaken == true);
            // Added && iseventfullvisitorallowed to the condition to make the validation also validate the visitors based on if its allowed when the event is full
			var f = allVisitors.Count(v => v.IsVisitorAllowedInBasedOnAge && v.SignedOnTime && v.IfEventFullIsVisitorAllowed);

			// if the amount of available valid visitors that are seated is equal to the amount of total valid visitors
			if (g == f)
            {
                return true;
            }
            // if amaount of taken chairs is less that available chairs in row
            else if (item.Chairs.Count(v => v.IsTaken == true) < item.Chairs.Count())
            {
                return false;
            }       
            return true;
        }        
    }
}
