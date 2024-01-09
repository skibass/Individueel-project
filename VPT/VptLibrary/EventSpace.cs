using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VptLibrary
{
    public class EventSpace
    {
        public List<Part> Parts { get; set; }
        public DateTime LastSignUpDatePossibility { get; set; }
        // First come first serve
        public int VisitorLimit { get; set; }
        private char CurrentLetter { get; set; } = 'A';
        public EventSpace()
        {
            Parts = new List<Part>();

            Random randParts = new Random();
            for (int i = 0; i < randParts.Next(1, 11); i++)
            {
                Part part = new Part(CurrentLetter++);
                Parts.Add(part);
            }
        }


    }
}
