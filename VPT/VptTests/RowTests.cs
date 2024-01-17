using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VptLibrary;

namespace VptTests
{
    [TestClass]
    public class RowTests
    {
        [TestMethod]
        public void CreateSeatsBasedOnDeterminedAmount()
        {
            // Arrange
            Row row = new Row('A', 1, 10);

            // Act
            Console.WriteLine($"Amount of predetermined seats is 10, amount in rowis: {row.Chairs.Count()}");

            // Assert
            Assert.AreEqual(row.Chairs.Count, 10);
        }

        [TestMethod]
        public void PlaceVisitors()
        {
            // Arrange

            //Random random = new Random();
            //Row row = new Row('A', 1, 10);
            //List<Visitor> grouplessVisitors = new List<Visitor>();

            //for (int i = 0; i < random.Next(1, 100); i++)
            //{
            //    // Only add if visitor is unique
            //    Visitor vis = new(0);
            //    if (grouplessVisitors.Count(v => v.Id == vis.Id) == 0)
            //    {
            //        grouplessVisitors.Add(new Visitor(0));
            //    }
            //}

            //foreach (var chair in row.Chairs)
            //{
            //    if (chair.IsTaken == false)
            //    {
            //        foreach (var visitor in grouplessVisitors)
            //        {
            //            if (visitor.IsAdult == true && IsVisitorAllowed(visitor) == true)
            //            {
            //                chair.Visitor = visitor;
            //                visitor.IsSeated = true;
            //                chair.IsTaken = true;
            //                break;
            //            }
            //        }
            //    }
            //}

            // Act
            //Console.WriteLine(row.Chairs.Count());

            //// Assert
            //Assert.AreEqual(row.Chairs.Count, 10);
        }
    }
}
