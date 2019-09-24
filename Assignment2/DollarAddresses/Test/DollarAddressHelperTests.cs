using DollarAddresses;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetDollarAddresses_EmptyList_ReturnsEmptyList()
        {
            List<Address> addresses = new List<Address>();
            var result = DollarAddressHelper.GetDollarAddresses(addresses);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetDollarAddresses_OnlyDollardAddress_ReturnsAddress()
        {
            var addresses = new List<Address>();
            var address = new Address();
            address.ADDRESS_NUMBER = 2;
            address.STREETNAME = "a";
            address.SUFFIX = "a";
            addresses.Add(address);
            var result = DollarAddressHelper.GetDollarAddresses(addresses);
            CollectionAssert.AreEqual(result, addresses);
        }

        [Test]
        public void GetDollarAddresses_NoDollarAddresses_ReturnsEmptyList()
        {
            var addresses = new List<Address>();
            var address = new Address();
            address.ADDRESS_NUMBER = 5;
            address.STREETNAME = "a";
            address.SUFFIX = "a";
            addresses.Add(address);
            var result = DollarAddressHelper.GetDollarAddresses(addresses);
            CollectionAssert.IsEmpty(result);

        }

        [Test]
        public void GetDollarAddresses_Null_ReturnsEmptyList()
        {
            var result = DollarAddressHelper.GetDollarAddresses(null);
            CollectionAssert.IsEmpty(result);
        }
        
      
        [Test]
        public void GetDollarAddresses_MultipleAddresses_ReturnsOnlyDollarAddresses()
        {
            var addresses = new List<Address>();
            var address = new Address();
            address.ADDRESS_NUMBER = 2;
            address.STREETNAME = "a";
            address.SUFFIX = "a";
            var address2 = new Address();
            address2.ADDRESS_NUMBER = 1;
            address2.STREETNAME = "b";
            address2.STREETNAME = "b";
            addresses.Add(address);
            addresses.Add(address2);
            var result = DollarAddressHelper.GetDollarAddresses(addresses);

            CollectionAssert.Contains(result, address);
            CollectionAssert.DoesNotContain(result, address2);
        }

        [TestCase("a", ExpectedResult = 1)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("abc", ExpectedResult = 6)]
        [TestCase("abc123", ExpectedResult = 6)]
        [TestCase("abc123xyz", ExpectedResult = 81)]
        [TestCase("a b c d", ExpectedResult = 10)]
        [TestCase("AAaaAA", ExpectedResult = 6)]
        [TestCase(null, ExpectedResult = 0)]
        public int TestDollarAddressHelperStringToIntegerValue(string input)
        {
            return DollarAddressHelper.StringToIntegerValue(input);
        }
    }
}