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

        [TestMethod]
        public void CanPlaceVisitorsBasedOnFirstComeFirstServe()
        {
            EventSpace eventSpace = new EventSpace();
            int limit = eventSpace.VisitorLimit = 1;
            var visitorEarliest = eventSpace.AllVisitors.Min(v => v.SignUpDate);

            var seatedVisitorsFirstCome = eventSpace.AllVisitors.Count(a => a.IsSeated);

            bool canPlace = false;
            foreach (var item in eventSpace.AllVisitors)
            {
                if (item.SignUpDate == visitorEarliest)
                {
                    if (item.IsSeated)
                    {
                        canPlace = true;
                    }
                    else
                    {
                        canPlace = false;
                    }
                }
            }

            // I can make a fail test for this
            if (visitorEarliest > eventSpace.LastSignUpDatePossibility)
            {
                Console.Write("Earliest visitor sign date: " + visitorEarliest + " Event date " + eventSpace.LastSignUpDatePossibility + " No visitor signed on time!!");
                canPlace = true;
            }
            else
            {
                Console.Write("Earliest visitor sign date: " + visitorEarliest + " Event date " + eventSpace.LastSignUpDatePossibility);
            }

            Assert.IsTrue(canPlace == true);
        }
    }
}
