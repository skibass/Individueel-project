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
        public int AmountOfChairs { get; set; }
        private bool IsRowFull { get; set; }
        private bool IsRowInFront { get; set; }
        public List<Chair> Chairs { get; set; }

        public Row(char letter, int rowNr, int amountOfChairs)
        {
            Chairs = new List<Chair>();
            AmountOfChairs = amountOfChairs;
            EventRowName = letter + "" + rowNr;
            GetChairs();
            IsRowInFront = CheckIfRowIsFront(rowNr);
        }

        public void GetChairs()
        {
            int chairNr = 1;

            for (int i = 0; i < AmountOfChairs; i++)
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
                    if (visitor.IsAdult == true && IsVisitorAllowed(visitor) == true)
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
                            // Fix this shit
                            if (IsRowInFront == true)
                            {
                                if (visitor.IsAdult == false && adultInGroup && IsVisitorAllowed(visitor) == true)
                                {
                                    chair.Visitor = visitor;
                                    visitor.IsSeated = true;
                                    chair.IsTaken = true;
                                    break;
                                }
                            }
                            else if (visitor.IsAdult == true && IsVisitorAllowed(visitor) == true)
                            {
                                chair.Visitor = visitor;
                                visitor.IsSeated = true;
                                chair.IsTaken = true;
                                adultInGroup = true;
                                break;
                            }
                            // If visitor is a child and there is already an adult in the row, its allowed
                            else if (visitor.IsAdult == false && IsVisitorAllowed(visitor) == true && adultInGroup)
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
        private bool CheckIfRowIsFront(int rowNr)
        {
            if (rowNr == 1)
            {
                return true;
            }
            return false;
        }

        private bool IsVisitorAllowed(Visitor visitor)
        {
            bool isAllowed = false;
            if (visitor.IsSeated == false && visitor.SignedOnTime == true && visitor.IfEventFullIsVisitorAllowed == true)
            {
                return isAllowed = true;
            }
            return isAllowed = false;
        }
    }
}
