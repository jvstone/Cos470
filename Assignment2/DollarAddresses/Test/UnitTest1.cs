using DollarAddresses;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("a", ExpectedResult = 1)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("abc", ExpectedResult = 6)]
        [TestCase("abc123", ExpectedResult = 6)]
        [TestCase("abc123xyz", ExpectedResult = 81)]
        [TestCase("a b c d", ExpectedResult = 10)]
        public int TestDollarAddressHelperStringToIntegerValue(string input)
        {
            return DollarAddressHelper.StringToIntegerValue(input);
        }
    }
}