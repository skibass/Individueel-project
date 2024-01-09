using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VptLibrary
{
    public class Chair
    {
        public string ChairCode { get; set; }
        public Visitor Visitor { get; set; }

        public Chair(string eventRowName, int chairNr) 
        { 
          Visitor = new Visitor();
          
          
        }
    }
}
