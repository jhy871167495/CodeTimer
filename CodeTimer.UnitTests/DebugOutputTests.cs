using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using TypeMock;
using TypeMock.ArrangeActAssert;
using System.Diagnostics;

namespace CodeTimer.UnitTests
{
    public class DebugOutputTests
    {
        [Fact]
        public void FakeNowTime_NormalBehavior_ReturnTrue()
        {
            DateTime fakeDate = new DateTime(2018, 12, 12, 0, 0, 0);
            Isolate.WhenCalled(() => DateTime.Now).WillReturn(fakeDate);

            var current = DateTime.Now;
            
            Assert.Equal(fakeDate, current);
        }
    }
}
