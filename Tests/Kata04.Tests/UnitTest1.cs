using System;
using Xunit;
using Shouldly;

namespace Kata04.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            true.ShouldBeTrue("Verify basic project configurations.");
        }
    }
}
