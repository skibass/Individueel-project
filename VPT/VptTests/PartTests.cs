using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VptTests
{
    [TestClass]
    public class PartTests
    {
        [TestMethod]
        public void CanCreateRows()
        {
            // Arrange
            Part part = new Part('A');

            // Act
            int rowCount = part.Rows.Count();

            Console.WriteLine(rowCount);

            // Assert
            Assert.IsTrue(rowCount >= 1 && rowCount <= 3);
        }


        [TestMethod]
        public void PlaceVisitorInPart()
        {
            // Arrange
            Part part = new Part('A');
            List<Group> groups = new();
            List<Visitor> grouplessVis = new List<Visitor>();
            List<Visitor> allVisitors = new List<Visitor>();

            for (int i = 0; i < 2; i++)
            {
                groups.Add(new Group());
            }
            foreach (Group group in groups)
            {
                foreach (Visitor visitor in group.groupVisitors)
                {
                    allVisitors.Add(visitor);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                Visitor vis = new Visitor(0);
                grouplessVis.Add(vis);
                allVisitors.Add(vis);
            }

            // Act
            part.SetupRows(grouplessVis, groups, allVisitors);

            int adultsInGroup = 0;
            foreach (Group grou in groups)
            {
                // Atleast one adult in group
                adultsInGroup = grou.groupVisitors.Count(a => a.IsAdult);
            }

            Console.WriteLine($"Total visitors should be atleast 10: {allVisitors.Count()}");

            // Kids should never be valid being alone
            Console.WriteLine($"Valid kids in groupless should always be 0: {grouplessVis.Count(a => a.IsAdult == false && a.IsVisitorAllowed == true)}");


            //Assert
            Assert.IsTrue(grouplessVis.Count(a => a.IsAdult == false && a.IsVisitorAllowed == true) == 0 && adultsInGroup > 0);
        }
    }
}
