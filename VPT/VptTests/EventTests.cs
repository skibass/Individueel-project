using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VptTests
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void CanGenerateEventWithLimitAndPlaceBasedOnFirstComeFirstServe()
        {
            DateTime dateTime = new DateTime(2018, 05, 31); ;
            // Arrange
            EventSpace eventSpace = new EventSpace(1, dateTime);

            // Act
            var visitorEarliest = eventSpace.AllVisitors.Min(v => v.SignUpDate);

            var seatedVisitorsFirstCome = eventSpace.AllVisitors.Count(a => a.IsSeated );

            bool canPlace = false;
            bool noOnTimeVisitors = false;           

            // I can make a fail test for this
            // If earliest visitor is still too late
            if (visitorEarliest > eventSpace.LastSignUpDatePossibility)
            {
                Console.WriteLine($"Amount of seated visitors: {seatedVisitorsFirstCome} Earliest visitor sign date: {visitorEarliest} Event date {eventSpace.LastSignUpDatePossibility} No visitor signed on time!!");
                if (seatedVisitorsFirstCome == 0)
                {
                    noOnTimeVisitors = true;
                }
            }
            else
            {
                Console.WriteLine($"Amount of seated visitors: {seatedVisitorsFirstCome} Earliest visitor sign date: {visitorEarliest} Event date {eventSpace.LastSignUpDatePossibility}");
                if (seatedVisitorsFirstCome == eventSpace.VisitorLimit)
                {
                    canPlace = true;
                }
            }

            // Assert
            Assert.IsTrue(canPlace || noOnTimeVisitors, "Visitors cannot be placed based on first come, first serve criteria.");
        }

        [TestMethod]
        public void CanCreatePartsWithEqualRows()
        {
            // Arrange
            EventSpace eventSpace = new EventSpace();

            // Act
            int partsCount = eventSpace.Parts.Count;
            bool equalChairsPerRow = false;

            foreach (var item in eventSpace.Parts)
            {
                foreach (var row in item.Rows)
                {
                    if (row.Chairs.Count == row.AmountOfChairs)
                    {
                        equalChairsPerRow = true;
                    }
                    else
                    {
                        equalChairsPerRow = false;
                    }
                }
            }
            
            // Assert
            Assert.IsTrue(partsCount > 0 && equalChairsPerRow == true);
        }
    }
}
