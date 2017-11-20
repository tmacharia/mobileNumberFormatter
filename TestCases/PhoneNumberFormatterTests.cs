using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileNumberFormatter;
using MobileNumberFormatter.Interfaces;
using MobileNumberFormatter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCases
{
    [TestClass]
    public class PhoneNumberFormatterTests
    {
        private readonly IPhoneNumberFormatter _phoneNumberFormatter = new PhoneNumberFormatter();
        private string numberWithCountryCode = "+254716123456";
        private string number = "0716123456";

        [TestMethod]
        public void CheckNumberValidity()
        {
            bool firstResult = _phoneNumberFormatter.IsValid(numberWithCountryCode);

            Assert.IsTrue(firstResult);

            bool secondResult = _phoneNumberFormatter.IsValid(number);

            Assert.IsTrue(secondResult);
        }

        [TestMethod]
        public void WrongNumberCheckValidity()
        {
            numberWithCountryCode = "+25471612345";
            //wrong number without country code
            number = "071612345";

            bool firstResult = _phoneNumberFormatter.IsValid(numberWithCountryCode);

            Assert.IsFalse(firstResult);

            bool secondResult = _phoneNumberFormatter.IsValid(number);

            Assert.IsFalse(secondResult);
        }

        [TestMethod]
        public void FormatWrongNumber()
        {
            PhoneNumber phone = _phoneNumberFormatter.Format("+25471612345");
            PhoneNumber phone2 = _phoneNumberFormatter.Format("0738 123 4569");

            Assert.IsNull(phone);
            Assert.IsNull(phone2);
        }

        [TestMethod]
        public void FormatNumber()
        {
            PhoneNumber phone = _phoneNumberFormatter.Format(numberWithCountryCode);
            PhoneNumber phone2 = _phoneNumberFormatter.Format("0738 123 456");

            Assert.AreEqual(MNO.Safaricom, phone.MobileOperator);
            Assert.AreEqual(MNO.Airtel, phone2.MobileOperator);
            Assert.AreEqual(phone2.CountryCode, "+254");
        }

        [TestMethod]
        public void CheckServiceProvider()
        {
            //safaricom number test
            MNO saf_result = _phoneNumberFormatter.GetProvider(numberWithCountryCode);

            Assert.AreEqual(MNO.Safaricom,saf_result);

            MNO airtel_result, telkom_result, equitel_test;

            airtel_result = _phoneNumberFormatter.GetProvider("0738789654");
            telkom_result = _phoneNumberFormatter.GetProvider("0771678123");
            equitel_test = _phoneNumberFormatter.GetProvider("0765890234");

            Assert.AreEqual(MNO.Airtel, airtel_result);
            Assert.AreEqual(MNO.Telkom, telkom_result);
            Assert.AreEqual(MNO.Equitel, equitel_test);
        }
    }
}
