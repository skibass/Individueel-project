using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VptLibrary
{
    // Steps still needed: 7, 8, part of 9, 10, 12
    // TODO: Amount of chairs equal to visitor limit
    public class EventSpace
    {
        public List<Part> Parts { get; set; }
        public DateTime LastSignUpDatePossibility { get; set; }
        public int VisitorLimit { get; set; }
        public int VisitorSignUpCount { get; set; }
        public bool EventFull { get; set; }
        private char CurrentLetter { get; set; } = 'A';
        public List<Group> Groups { get; set; }
        public List<Visitor> GrouplessVisitors { get; set; }
        public List<Visitor> AllVisitors { get; set; }
        private Random random = new Random();
        public EventSpace()
        {
            VisitorLimit = GetRandomVisitorLimit();
            LastSignUpDatePossibility = GetRandomEventSignUpDate();
            Parts = new List<Part>();
            Groups = new List<Group>();
            GrouplessVisitors = new List<Visitor>();
            AllVisitors = new List<Visitor>();
            GetParts();
            GetRandomAmountOfGroups();
            GetRandomAmountOfGrouplessVisitors();
            GetAllVisitors();
            CheckIfVisitorSignedOnTime();
            CheckSpotsAvailable();
            EventFull = IsEventOverSigned(); 
            PlaceVisitors();
        }
        public EventSpace(int visitorLimit, DateTime lastSignDate)
        {
            VisitorLimit = visitorLimit;
            LastSignUpDatePossibility = lastSignDate;
            Parts = new List<Part>();
            Groups = new List<Group>();
            GrouplessVisitors = new List<Visitor>();
            AllVisitors = new List<Visitor>();
            GetParts();
            GetRandomAmountOfGroups();
            GetRandomAmountOfGrouplessVisitors();
            GetAllVisitors();
            CheckIfVisitorSignedOnTime();
            CheckSpotsAvailable();
            EventFull = IsEventOverSigned();
            PlaceVisitors();
        }
        private int GetRandomVisitorLimit()
        {
            return random.Next(50, 201);
        }
        private DateTime GetRandomEventSignUpDate()
        {
            DateTime start = new DateTime(2018, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
        private void GetParts()
        {
            for (int i = 0; i < random.Next(1, 11); i++)
            {
                Part part = new Part(CurrentLetter++);
                Parts.Add(part);
            }
        }
        private void GetRandomAmountOfGroups()
        {
            for (int i = 0; i < random.Next(1, 10); i++)
            {
                Groups.Add(new Group());
            }
            CheckIfGroupIsAllowed(Groups);
        }
        private void CheckIfGroupIsAllowed(List<Group> groups)
        {
            foreach (var item in groups)
            {
                // If theres a child in group
                if (item.groupVisitors.Any(vis => vis.IsAdult == false) && item.groupVisitors.Count(vis => vis.IsAdult) < 1)
                {
                    groups.Remove(item);
                }
            }
        }

        private void GetRandomAmountOfGrouplessVisitors()
        {
            for (int i = 0; i < random.Next(1, 100); i++)
            {
                // Only add if visitor is unique
                Visitor vis = new(0);
                if (AllVisitors.Count(v => v.Id == vis.Id) == 0)
                {
                    GrouplessVisitors.Add(new Visitor(0));
                }
            }
        }
        public void GetAllVisitors()
        {
            foreach (var visitor in Groups)
            {
                foreach (var item in visitor.groupVisitors)
                {
                    AllVisitors.Add(item);
                }
            }
            foreach (var item in GrouplessVisitors)
            {
                AllVisitors.Add(item);
            }
        }
        private void CheckIfVisitorSignedOnTime()
        {           
            foreach (var visitor in AllVisitors)
            {
                if (visitor.SignUpDate > LastSignUpDatePossibility)
                {
                    visitor.SignedOnTime = false;
                }
                else
                {
                    visitor.SignedOnTime = true;
                }

            }
        }
        private void CheckSpotsAvailable()
        {
            if (IsEventOverSigned())
            {
                FirstComeFirstServe();
            }
        }
        private void FirstComeFirstServe()
        {
            int count = 1;
            var allOrdenedVisitors = AllVisitors.OrderBy(v => v.SignUpDate).Where(v => v.SignedOnTime == true).ToList();

            foreach (var visitor in allOrdenedVisitors)
            {
                // If count exceeds limit
                if (count > VisitorLimit)
                {
                    visitor.FirstComeFirstServe = false;
                }
                count++;
            }
        }
        private bool IsEventOverSigned()
        {
            if (AllVisitors.Count >= VisitorLimit)
            {
                return true;
            }
            return false;
        }
        public void PlaceVisitors()
        {
            foreach (var part in Parts)
            {
                part.SetupRows(GrouplessVisitors, Groups, AllVisitors);
                part.IsPartInUse = CheckIfPartIsInUse(part);
            }
        } 
        private bool CheckIfPartIsInUse(Part part)
        {
            bool isPartInUse = false;
            foreach (var item in part.Rows)
            {
                // are there seated chairs in part
                if (item.Chairs.Count(v => v.IsTaken) > 0)
                {
                    isPartInUse = true;
                }
            }
            return isPartInUse;
        }
    }
}
