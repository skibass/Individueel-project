using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace VptLibrary
{

    public class Row
    {
        // Between 3 and 10 chairs
        public string EventRowName { get; set; }
        public int AmountOfChairs { get; set; }
        public bool IsRowFull { get; set; }
        private bool RowIsInFront { get; set; }
        public List<Chair> Chairs { get; set; }

        public Row(char letter, int rowNr, int amountOfChairs)
        {
            Chairs = new List<Chair>();
            AmountOfChairs = amountOfChairs;
            EventRowName = letter + "" + rowNr;
            GetChairs();
            RowIsInFront = CheckIfRowIsFront(rowNr);
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

        public void PlaceVisitors(List<Visitor> allVisitors)
        {
            foreach (var chair in Chairs)
            {
                if (!chair.IsTaken)
                {
                    foreach (var visitor in allVisitors)
                    {
                        if (CanPlaceVisitor(visitor))
                        {
                            PlaceVisitor(chair, visitor);
                            break;
                        }
                    }
                }
            }
        }

        private bool CanPlaceVisitor(Visitor visitor)
        {
            // If visitor is in a group
            if (visitor.GroupNumber != 0)
            {
                if (RowIsInFront && !visitor.IsAdult && IsVisitorAllowed(visitor))
                {
                    return true;
                }
                else if (visitor.IsAdult && IsVisitorAllowed(visitor))
                {
                    return true;
                }
                else if (!visitor.IsAdult && IsVisitorAllowed(visitor))
                {
                    return true;
                }
            }
            else
            {
                if (visitor.IsAdult && IsVisitorAllowed(visitor))
                {
                    return true;
                }
            }

            return false;
        }

        private void PlaceVisitor(Chair chair, Visitor visitor)
        {
            chair.Visitor = visitor;
            visitor.IsSeated = true;
            chair.IsTaken = true;
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
            return isAllowed;
        }
    }
}
