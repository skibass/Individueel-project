using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine(row.Chairs.Count());

            // Assert
            Assert.AreEqual(row.Chairs.Count, 10);
        }
    }
}
