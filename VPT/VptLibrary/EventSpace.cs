using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VptLibrary
{
    public class EventSpace
    {
        public List<Part> Parts { get; set; }
        public DateTime LastSignUpDatePossibility { get; set; }
        // First come first serve
        public int VisitorLimit { get; set; } = 100;
        private char CurrentLetter { get; set; } = 'A';
        public List<Group> Groups { get; set; }
        public List<Visitor> GrouplessVisitors { get; set; }
        public EventSpace()
        {
            Groups = new List<Group>();
            Parts = new List<Part>();
            GrouplessVisitors = new List<Visitor>();
            GetParts();
            GetRandomAmountOfGroups();
            //GetRandomSignupdate();
            GetRandomAmountOfGrouplessVisitors();
            PlaceVisitors();
        }
        private void GetParts()
        {
            Random randParts = new Random();
            for (int i = 0; i < randParts.Next(1, 11); i++)
            {
                Part part = new Part(CurrentLetter++);
                Parts.Add(part);
            }
        }
        private void GetRandomAmountOfGroups()
        {
            Random random = new Random();
            for (int i = 0; i < random.Next(1, 10); i++)
            {
                Groups.Add(new Group());
            }
            CheckIfGroupIsAllowed(Groups);
        }

        public void GetRandomAmountOfGrouplessVisitors()
        {
            Random random = new Random();
            for (int i = 0; i < random.Next(1, 100); i++)
            {
                GrouplessVisitors.Add(new Visitor());
            }
        }

        public void PlaceVisitors()
        {
            foreach (var part in Parts)
            {
                part.SetupRows(GrouplessVisitors, Groups);
            }
        }

        private void CheckIfGroupIsAllowed(List<Group> groups)
        {
            foreach (var item in groups)
            {
                if (item.groupVisitors.Any(vis => vis.IsAdult == false))
                {
                    groups.Remove(item);
                    break;
                }
            }
        }

    }
}
