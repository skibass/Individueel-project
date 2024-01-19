using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VptLibrary
{
    public class Chair
    {
        public string EventChairName { get; set; }
        public Visitor? Visitor { get; set; }
        public bool IsTaken { get; set; } = false;

        public Chair(string eventRowName, int chairNr) 
        { 
            EventChairName = eventRowName + "-" + chairNr;
        }
        public void PlaceVisitor(Visitor visitor)
        {
            this.Visitor = visitor;
            visitor.IsSeated = true;
            this.IsTaken = true;
        }
    }
}
