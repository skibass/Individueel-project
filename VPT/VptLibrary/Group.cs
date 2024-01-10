using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VptLibrary
{
    public class Group
    {
        public List<Visitor> groupVisitors {  get; set; }

        public Group() 
        { 
           groupVisitors = new List<Visitor>();
           CreateGroup();
        }  

        private void CreateGroup()
        {
            Random random = new Random();
            for (int i = 0; i < random.Next(2, 11); i++)
            {             
                groupVisitors.Add(new Visitor());
            }
        }
    }
}
