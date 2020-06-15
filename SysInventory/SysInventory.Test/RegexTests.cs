using NUnit.Framework;
using SysInventory.LogMessages;

namespace SysInventory.Test
{
    [TestFixture]
    public class RegexTests
    {
        [Test]
        public void TestCustomerNumberValidation()
        {
            var validator = new MyRegExValidations();
            var result = validator.ValidateCustomerNo("CU12345");
            Assert.IsTrue(result);
            result = validator.ValidateCustomerNo("CU123456");
            Assert.IsFalse(result);
            result = validator.ValidateCustomerNo("KU12345");
            Assert.IsFalse(result);
            result = validator.ValidateCustomerNo("");
            Assert.IsFalse(result);
            result = validator.ValidateCustomerNo(null);
            Assert.IsFalse(result);
        }
        [Test]
        public void TestEMailValidation()
        {
            var validator = new MyRegExValidations();
            var result = validator.ValidateEMail("gabriel.weibel@hotmail.de");
            Assert.IsTrue(result);
            result = validator.ValidateEMail("gabriel.weibel@rey-automation.ch");
            Assert.IsTrue(result);
            result = validator.ValidateEMail("gabriel-weibel@rey-automation.ch");
            Assert.IsTrue(result);
            result = validator.ValidateEMail("gabriel-weibel_123@gmail1.com");
            Assert.IsTrue(result);
            result = validator.ValidateEMail("gabriel-weibel_123@gmail1.co.uk");
            Assert.IsTrue(result);
            result = validator.ValidateEMail("gabriel-weibel_123@gmail1..");
            Assert.IsFalse(result);
        }
        [Test]
        public void TestUrlValidation()
        {
            var validator = new MyRegExValidations();
            var result = validator.ValidateUrl("www.google.com");
            Assert.IsTrue(result);
            result = validator.ValidateUrl("http://www.google.com");
            Assert.IsTrue(result);
            result = validator.ValidateUrl("https://www.google.com");
            Assert.IsTrue(result);
            result = validator.ValidateUrl("google.com");
            Assert.IsTrue(result);
            result = validator.ValidateUrl("https://policies.google.com");
            Assert.IsTrue(result);
            result = validator.ValidateUrl("https://policies.google.com/technologies/voice?hl=de&gl=ch");
            Assert.IsTrue(result);
            result = validator.ValidateUrl("https://www.policies.google.com/technologies/voice?hl=de&gl=ch");
            Assert.IsTrue(result);
            result = validator.ValidateUrl("ftp://www.policies.google.com/technologies/voice?hl=de&gl=ch");
            Assert.IsFalse(result);
            result = validator.ValidateUrl("asdf");
            Assert.IsFalse(result);
        }
        [Test]
        public void TestPasswordValidation()
        {
            var validator = new MyRegExValidations();
            var result = validator.ValidatePassword("Test1234");
            Assert.IsTrue(result);
            result = validator.ValidatePassword("TeSt123467862");
            Assert.IsTrue(result);
            result = validator.ValidatePassword("TeSt123467862.");
            Assert.IsTrue(result);
            result = validator.ValidatePassword("Ta1");
            Assert.IsFalse(result);
            result = validator.ValidatePassword("asdfASDF");
            Assert.IsFalse(result);
            result = validator.ValidatePassword("123456789a");
            Assert.IsFalse(result);
        }
    }
}
