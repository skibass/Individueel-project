using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VptTests
{
    [TestClass]
    public class ChairTest
    {
        [TestMethod]
        public void PlaceVisitor()
        {
            // Arrange
            Chair chair = new("A", 1);
            Visitor visitor = new(0)
            {
                Name = "A",
                IsAdult = true,
                SignedOnTime = true,
            };

            // Act
            chair.PlaceVisitor(visitor);

            // Assert
            Console.WriteLine(chair.IsTaken == true);
        }

        [TestMethod]
        public void FailPlaceVisitorBecauseAloneChild()
        {
            // Arrange
            //Chair chair = new("A", 1);

            //// Alone child
            //Visitor visitor = new(0)
            //{
            //    Name = "A",
            //    IsAdult = false,
            //    SignedOnTime = true,
            //};

            //// Act
            //chair.PlaceVisitor(visitor);

            //// Assert
            //Console.WriteLine(chair.IsTaken);
            //Assert.IsTrue(chair.IsTaken == false);
        }
    }
}
