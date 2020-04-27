using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace OverridingEquals.Test 
{
    [TestClass]
    public class PhoneNumberTests 
    {
        [TestMethod]
        public void ReferenceEquality() 
        {
            var numberA = new PhoneNumber();
            var numberB = numberA;
            Assert.IsTrue(numberA == numberB);
            Assert.IsTrue(numberA.Equals(numberB));
            Assert.IsTrue(Object.ReferenceEquals(numberA, numberB));
        }
        [TestMethod]
        public void ValueEquality() 
        {
            var numberC = new PhoneNumber {AreaCode = "123", Exchange = "456", SubscriberNumber = "7890"};
            var numberD = new PhoneNumber {AreaCode = "123", Exchange = "456", SubscriberNumber = "7890"};
            Assert.IsTrue(numberC == numberD);
            Assert.IsTrue(numberC.Equals(numberD));
        }
        [TestMethod]
        public void UseAsDictionaryKey() 
        {
            var expectedResult = new Employee {FirstName = "Gordon", LastName = "Freeman"};
            var directory = new Dictionary<PhoneNumber, Employee>
            {
                {
                    new PhoneNumber {AreaCode = "123", Exchange = "456", SubscriberNumber = "7890"},
                    expectedResult
                },
                {
                    new PhoneNumber {AreaCode = "111", Exchange = "222", SubscriberNumber = "3333"},
                    new Employee {FirstName = "Samus", LastName = "Aran"}
                }
            };
            var employee = directory[new PhoneNumber {AreaCode = "123", Exchange = "456", SubscriberNumber = "7890"}];
            Assert.IsTrue(employee == expectedResult);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PreventDictionaryKeyDuplicates() 
        {
            new Dictionary<PhoneNumber, Employee>
            {
                {
                    new PhoneNumber {AreaCode = "123", Exchange = "456", SubscriberNumber = "7890"},
                    new Employee {FirstName = "Super", LastName = "Mario"}
                },
                {
                    new PhoneNumber {AreaCode = "123", Exchange = "456", SubscriberNumber = "7890"},
                    new Employee {FirstName = "Princess", LastName = "Peach"}
                }
            };
        }
        [TestMethod]
        public void NullEquality() 
        {
            PhoneNumber phoneNumberA = null;
            PhoneNumber phoneNumberB = null;
            Assert.IsTrue(phoneNumberA == phoneNumberB);
        }
        [TestMethod]
        public void NullReferenceEquality() 
        {
            PhoneNumber phoneNumberA = null;
            var phoneNumberB = new PhoneNumber();
            Assert.IsFalse(phoneNumberA == phoneNumberB);
        }
    }
}