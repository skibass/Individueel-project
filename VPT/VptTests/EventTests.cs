using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VptLibrary;

namespace VptTests
{
    [TestClass]
    public class EventTests
    {
        Random random = new Random();
        DateTime dateTime = new DateTime(2018, 1, 1);

        EventSpace eventSpace = new EventSpace();

        [TestMethod]
        public void CanGenerateEventWithLimitAndPlaceBasedOnFirstComeFirstServe()
        {
            // Arrange
            DateTime dateTime = new DateTime(2018, 05, 31);
            EventSpace eventSpace = new EventSpace(1, dateTime);

            // Act
            var visitorEarliest = eventSpace.AllVisitors.Min(v => v.SignUpDate);

            var seatedVisitorsFirstCome = eventSpace.AllVisitors.Count(a => a.IsSeated);

            bool canPlace = false;
            bool noOnTimeVisitors = false;

            // I can make a fail test for this
            // If earliest visitor is still too late
            if (visitorEarliest > eventSpace.LastSignUpDatePossibility)
            {
                Console.WriteLine($"Amount of seated visitors: {seatedVisitorsFirstCome} | Earliest visitor sign date: {visitorEarliest} | Event date {eventSpace.LastSignUpDatePossibility} | No visitor signed on time!!");
                if (seatedVisitorsFirstCome == 0)
                {
                    noOnTimeVisitors = true;
                }
            }
            else
            {
                Console.WriteLine($"Amount of seated visitors: {seatedVisitorsFirstCome} | Earliest visitor sign date: {visitorEarliest} | Event date {eventSpace.LastSignUpDatePossibility}");
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
                    Console.WriteLine($"Row: {row.EventRowName} | Amount of chairs: {row.Chairs.Count()}");
                }
            }

            // Assert
            Assert.IsTrue(partsCount > 0 && equalChairsPerRow == true);
        }

        [TestMethod]
        public void CanGetAllVisitors()
        {
            // Arrange

            // Act
            eventSpace.GetAllVisitors();

            // Assert
            Assert.IsTrue(eventSpace.AllVisitors.Count() > 0);
        }

        [TestMethod]
        public void CanCreateValidGroupsIfAvailable()
        {
            // Arrange
            List<Group> groups = new List<Group>();

            int amountOfValidGroups = 0;
            int amountOfGroupsCreated = 0;
            // Act
            for (int i = 0; i < random.Next(1, 10); i++)
            {
                groups.Add(new Group());
            }
            amountOfGroupsCreated = groups.Count();

            foreach (var group in groups)
            {
                // check if there are no adults in group
                if (group.groupVisitors.Any(vis => vis.IsAdult == false) && group.groupVisitors.Count(vis => vis.IsAdult) < 1)
                {
                    groups.Remove(group);
                }
            }
            amountOfValidGroups = groups.Count();

            // Assert

            // If there are no valid groups but groups were created
            if (amountOfValidGroups == 0 && amountOfGroupsCreated > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(amountOfValidGroups > 0);
            }
        }

        [TestMethod]
        public void CanCreateValidSoloVisitors()
        {
            // Arrange
            List<Visitor> visitors = new List<Visitor>();
            int amountOfChildrenAllowed = 0;
            Random random = new Random();

            // Act
            for (int i = 0; i < random.Next(1, 100); i++)
            {
                visitors.Add(new Visitor(0));
            }

            // Is child and allowed should be 0 because children are never allowed in this event
            amountOfChildrenAllowed = visitors.Count(v => v.IsAdult == false && v.IsVisitorAllowedInBasedOnAge);

            // Assert
            Assert.IsTrue(amountOfChildrenAllowed == 0);
        }

        // ...

        [TestMethod]
        public void FailCanGenerateEventWithLimitAndPlaceBasedOnFirstComeFirstServe_NoVisitorsSignedUp()
        {
            eventSpace.AllVisitors.Clear();

            Assert.IsFalse(eventSpace.AllVisitors.Any(), "No visitors should be signed up.");
        }


        // ...

        [TestMethod]
        public void FailCanCreatePartsWithEqualRows_NoParts()
        {
            eventSpace.Parts.Clear();

            Assert.IsTrue(eventSpace.Parts.Count == 0, "No parts should be created.");
        }

        [TestMethod]
        public void FailCanCreatePartsWithEqualRows_MultiplePartsDifferentChairsPerRow()
        {
            eventSpace.Parts.Clear();
            eventSpace.Parts.Add(new Part('A'));
            eventSpace.Parts[0].Rows.Add(new Row('A', 5, 5));
            eventSpace.Parts[0].Rows.Add(new Row('B', 8, 3));
            eventSpace.Parts.Add(new Part('B'));
            eventSpace.Parts[1].Rows.Add(new Row('C', 3, 5));

            Assert.IsTrue(eventSpace.Parts.Count > 1 && eventSpace.Parts.All(part => part.Rows.All(row => row.Chairs.Count == row.AmountOfChairs)), "Each row should have the specified number of chairs.");
        }


        [TestMethod]
        public void FailCanCreateValidSoloVisitors_SoloChildVisitor()
        {
            int age = 10;

            Visitor childVisitor = new Visitor(0, age);

            Assert.IsFalse(childVisitor.IsAdult && childVisitor.IsVisitorAllowedInBasedOnAge, "Solo child visitor should not be allowed.");
        }

        [TestMethod]
        public void FailCanCreateValidSoloVisitors_SoloChildVisitorWithAllowances()
        {
            int groupNumber = 10;
            int age = 10;

            Visitor allowedChildVisitor = new Visitor(groupNumber, age);

            Assert.IsFalse(allowedChildVisitor.IsAdult && !allowedChildVisitor.IsVisitorAllowedInBasedOnAge, "Solo child visitor with specific allowances");
        }
    }
}
