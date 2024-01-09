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
            int chairNr = 0;

            Random randChairs = new Random();
            for (int i = 0; i < randChairs.Next(3, 11); i++)
            {
                Chair chair = new Chair(EventRowName, chairNr++);
                Chairs.Add(chair);
            }
        }

    }
}
