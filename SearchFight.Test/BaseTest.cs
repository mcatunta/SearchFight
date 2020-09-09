using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SearchFight.Test
{
    public class BaseTest
    {
        public static void Throws<T>(Action action, string expectedMessageContent = null)
            where T : Exception
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Assert.IsTrue(expectedMessageContent == null
                    || e.Message.ToLower().Contains(expectedMessageContent.ToLower())
                    , $"Expected message: {expectedMessageContent}{Environment.NewLine}Actual message:{e.Message}");
                return;
            }

            Assert.Fail("No exception was thrown");
        }
    }
}
