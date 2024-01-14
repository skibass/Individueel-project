﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VptLibrary
{
    // Steps still needed: 7, 8, part of 9, 10, 12
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
        public EventSpace()
        {
            VisitorLimit = GetRandomVisitorLimit();
            LastSignUpDatePossibility = GetRandomEventSignDate();
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
                // If theres a child in group
                if (item.groupVisitors.Any(vis => vis.IsAdult == false) && item.groupVisitors.Count(vis => vis.IsAdult) < 1)
                {                 
                        groups.Remove(item);                                    
                }
            }
        }
        private void CheckIfVisitorSignedOnTime()
        {
            // Check groups
            foreach (var item in Groups)
            {
                foreach (var visitor in item.groupVisitors)
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

            // Check groupless visitors
            foreach (var visitor in GrouplessVisitors)
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
                    visitor.IfEventFullIsVisitorAllowed = false;                   
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
        private void GetAllVisitors()
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

        private DateTime GetRandomEventSignDate()
        {
            var random = new Random();
            DateTime start = new DateTime(2018, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
        private int GetRandomVisitorLimit()
        {
            var random = new Random();
            
            return random.Next(50, 201);
        }
    }
}
