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
    }
}
