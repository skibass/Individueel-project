﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VptLibrary
{
    public class Visitor
    {
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateTime SignUpDate { get; set; }
        public bool SignedOnTime { get; set; }
        public int Age { get; set; }
        public bool IsAdult { get; set; }
        public bool IsSeated { get; set; } = false;
        public bool IfEventFullIsVisitorAllowed { get; set; }

        public Visitor() 
        { 
            GetBirthDateAndAge();
            GetRandomName();
            SignUpDate = GetRandomSignUpDate();
        }

        private void GetBirthDateAndAge()
        {
            Random birthRand = new Random();

            int daysSinceBirth = birthRand.Next(365, 36500);
            BirthDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-daysSinceBirth));
            Age = DateTime.Now.Year - BirthDate.Year;

            if (Age > 12)
            {
                IsAdult = true;
            }
            else
            {
                IsAdult = false;
            }
        }

        private void GetRandomName() 
        {
            // From https://stackoverflow.com/questions/14687658/random-name-generator-in-c-sharp

            Random r = new Random();
            int lengthOfName = 10;

            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";

            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];

            int b = 2; 

            while (b < lengthOfName)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }
            this.Name = Name;
        }
        private DateTime GetRandomSignUpDate()
        {
            var random = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}
