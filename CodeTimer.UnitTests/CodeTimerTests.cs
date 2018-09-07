using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CodeTimer.UnitTests
{
    public class CodeTimerTests
    {
        [Fact]
        public void Monitor_CallNone_ReturnTrue()
        {
            ITestJob job = Substitute.For<ITestJob>();

            Action action = job.DoWork;
            CodeTimer.Evaluate("a", action, 0, true);

            job.DidNotReceive().DoWork();
        }

        [Fact]
        public void Monitor_CallOnce_ReturnTrue()
        {
            ITestJob job = Substitute.For<ITestJob>();

            Action action = job.DoWork;
            CodeTimer.Evaluate("a", action, 1, true);

            job.Received(1).DoWork();
        }

        [Fact]
        public void Monitor_CallTwice_ReturnTrue()
        {
            ITestJob job = Substitute.For<ITestJob>();

            Action action = job.DoWork;
            CodeTimer.Evaluate("a", action, 2, true);

            job.Received(2).DoWork();
        }

        [Fact]
        public void Monitor_CallException_ThrowException()
        {
            ITestJob job = new ThrowExceptionJob();
            
            Action action = job.DoWork;
            Assert.Throws<NotImplementedException>(() => CodeTimer.Evaluate("a", action, 1, true));
        }

        public interface ITestJob
        {
            void DoWork();
        }

        public class TestJob : ITestJob
        {
            public void DoWork()
            {

            }
        }


        public class ThrowExceptionJob : ITestJob
        {
            public void DoWork()
            {
                throw new NotImplementedException();
            }
        }

        public static void DoJob(ITestJob testJob)
        {
            testJob.DoWork();
        }

        [Fact]
        public void GetOutputByType_Normal_ReturnTrue()
        {
            var output1 = CodeTimer.GetOutputByType(true);
            var output2 = CodeTimer.GetOutputByType(false);


            Assert.IsType(typeof(ConsoleOutput), output1);
            Assert.IsType(typeof(DebugOutput), output2);
        }
    }
}
