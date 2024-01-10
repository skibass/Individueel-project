using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VptLibrary
{

    public class Row
    {
        // Between 3 and 10 chairs
        public string EventRowName { get; set; }
        public List<Chair> Chairs { get; set; }

        public Row(char letter, int rowNr)
        {
            Chairs = new List<Chair>();
            EventRowName = letter + "" + rowNr;
            GetChairs();
        }

        public void GetChairs()
        {
            int chairNr = 1;

            Random randChairs = new Random();
            for (int i = 0; i < randChairs.Next(3, 11); i++)
            {
                Chair chair = new Chair(EventRowName, chairNr++);
                Chairs.Add(chair);
            }
        }
        public void PlaceGrouplessVisitors(List<Visitor> grouplessVisitors)
        {
            foreach (var chair in Chairs)
            {
                foreach (var visitor in grouplessVisitors)
                {
                    if (visitor.IsAdult == true && visitor.IsSeated == false)
                    {
                        chair.Visitor = visitor;
                        visitor.IsSeated = true;
                        chair.IsTaken = true;
                        break;
                    }
                }
            }
        }

        public void PlaceGroups(List<Group> groups)
        {
            foreach (var group in groups)
            {
                bool adultInGroup = false;

                if (group.groupVisitors.Any(a => a.IsAdult == true))
                {
                    adultInGroup = true;
                }
                foreach (var chair in Chairs)
                {
                    if (chair.IsTaken == false)
                    {
                        foreach (var visitor in group.groupVisitors)
                        {
                            if (visitor.IsAdult == true && visitor.IsSeated == false)
                            {
                                chair.Visitor = visitor;
                                visitor.IsSeated = true;
                                chair.IsTaken = true;
                                adultInGroup = true;
                                break;
                            }
                            // If visitor is a child and there is already an adult in the row, its allowed
                            else if (visitor.IsAdult == false && visitor.IsSeated == false && adultInGroup)
                            {
                                chair.Visitor = visitor;
                                visitor.IsSeated = true;
                                chair.IsTaken = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }



    }
}
