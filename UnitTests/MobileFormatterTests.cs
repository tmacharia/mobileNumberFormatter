using MobileNumberFormatter;
using MobileNumberFormatter.Interfaces;
using MobileNumberFormatter.Models;
using NUnit.Framework;
using System;
using System.Collections;

namespace TestCases
{
    [TestFixture]
    public class MobileFormatterTests
    {
        #region Local Variables
        private readonly IPhoneNumberFormatter _phoneNumberFormatter = new PhoneNumberFormatter();
        #endregion

        [Test, TestCaseSource(typeof(DataClass), "TestCases")]
        public void CheckNumberValidity(string saf, string airtel, string telkom, string equitel)
        {
            bool saf_result = _phoneNumberFormatter.IsValid(saf);
            bool airtel_result = _phoneNumberFormatter.IsValid(airtel);
            bool telkom_result = _phoneNumberFormatter.IsValid(telkom);
            bool equitel_result = _phoneNumberFormatter.IsValid(equitel);

            Assert.IsTrue(saf_result);
            Assert.IsTrue(airtel_result);
            Assert.IsTrue(telkom_result);
            Assert.IsTrue(equitel_result);
        }



        [Test]
        public void WrongNumberCheckValidity()
        {
            string numberWithCountryCode = "+25471612345";
            //wrong number without country code
            string number = "071612345";

            bool firstResult = _phoneNumberFormatter.IsValid(numberWithCountryCode);

            Assert.IsFalse(firstResult);

            bool secondResult = _phoneNumberFormatter.IsValid(number);

            Assert.IsFalse(secondResult);
        }



        [Test]
        public void FormatWrongNumber()
        {
            PhoneNumber phone = _phoneNumberFormatter.Format("+25471612345");
            PhoneNumber phone2 = _phoneNumberFormatter.Format("0738 123 4569");

            Assert.IsNull(phone);
            Assert.IsNull(phone2);
        }



        [Test, TestCaseSource(typeof(DataClass), "TestCases")]
        public void FormatNumber(string saf, string airtel, string telkom, string equitel)
        {
            PhoneNumber saf_no = _phoneNumberFormatter.Format(saf);
            PhoneNumber airtel_no = _phoneNumberFormatter.Format(airtel);
            PhoneNumber telkom_no = _phoneNumberFormatter.Format(telkom);
            PhoneNumber equitel_no = _phoneNumberFormatter.Format(equitel);

            Assert.AreEqual(MNO.Safaricom, saf_no.MobileOperator);
            Assert.AreEqual(MNO.Airtel, airtel_no.MobileOperator);
            Assert.AreEqual(MNO.Telkom, telkom_no.MobileOperator);
            Assert.AreEqual(MNO.Equitel, equitel_no.MobileOperator);
        }




        [Test, TestCaseSource(typeof(DataClass), "TestCases")]
        public void VerifyMobileOperator(string saf, string airtel, string telkom, string equitel)
        {
            MNO safaricom_result, airtel_result, telkom_result, equitel_test;

            safaricom_result = _phoneNumberFormatter.GetProvider(saf);
            airtel_result = _phoneNumberFormatter.GetProvider(airtel);
            telkom_result = _phoneNumberFormatter.GetProvider(telkom);
            equitel_test = _phoneNumberFormatter.GetProvider(equitel);

            Assert.AreEqual(MNO.Safaricom, safaricom_result);
            Assert.AreEqual(MNO.Airtel, airtel_result);
            Assert.AreEqual(MNO.Telkom, telkom_result);
            Assert.AreEqual(MNO.Equitel, equitel_test);
        }
    }

    public class DataClass
    {
        public static IEnumerable TestCases
        {
            get
            {
                //numbers starting with "+254.."
                yield return new TestCaseData("+254716123456", "+254738123456", "+254775123456", "+254765123456");

                //numbers starting with "..254.." only
                yield return new TestCaseData("254716123456", "254738123456", "254775123456", "254765123456");

                //numbers starting with "..7.." with no "0"
                yield return new TestCaseData("716123456", "738123456", "775123456", "765123456");

                //numbers starting with "07.."
                yield return new TestCaseData("0716123456", "0738123456", "0775123456", "0765123456");
            }
        }
    }
}
