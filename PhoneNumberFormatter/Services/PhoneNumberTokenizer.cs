using MobileNumberFormatter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileNumberFormatter.Services
{
    /// <summary>
    /// Performs operations on a phone number to analyze each section
    /// as tokens
    /// </summary>
    public static class PhoneNumberTokenizer
    {
        /// <summary>
        /// Breaks down a phone number and returns all its respective
        /// sections(country code,MNO,actual number) as a dictionary
        /// </summary>
        /// <param name="phoneNumber">Raw string of the mobile number</param>
        /// <returns>A dictionary with all the sections found in
        /// the phone number supplied
        /// </returns>
        public static IDictionary<PhoneNumberSection,string> Tokenize(string phoneNumber)
        {
            IDictionary<PhoneNumberSection, string> tokens = new Dictionary<PhoneNumberSection, string>();

            //remove spaces from phone number
            phoneNumber = phoneNumber.Replace(" ", "");

            //checks if number is area coded with Kenya's country code
            if (phoneNumber.StartsWith("+254") || phoneNumber.StartsWith("254"))
            {
                tokens.Add(PhoneNumberSection.CountryCode, "+254");

                //remove country code from phone number
                if (phoneNumber.StartsWith("+254"))
                    phoneNumber = phoneNumber.Remove(0, 4);
                else if (phoneNumber.StartsWith("254"))
                    phoneNumber = phoneNumber.Remove(0, 3);
            }
            else
            {
                if (phoneNumber.StartsWith("07") || phoneNumber.StartsWith("7"))
                {
                    tokens.Add(PhoneNumberSection.CountryCode, "+254");
                }
            }

            //check and retrieve mobile operator code
            if(phoneNumber.Length == 10 || phoneNumber.Length == 9)
            {
                string operatorCode = "";

                //check if number starts with a zero
                if (phoneNumber.StartsWith("0"))
                {
                    operatorCode = phoneNumber.Substring(1, 3);
                    phoneNumber = phoneNumber.Remove(0, 4);
                }
                else if (phoneNumber.StartsWith("7"))
                {
                    operatorCode = phoneNumber.Substring(0, 3);
                    phoneNumber = phoneNumber.Remove(0, 3);
                }
                else
                {
                    throw new FormatException("Mobile Network Operator code not found in phone number supplied. e.g 072X, 073X.. Format error");
                }

                //if operator code is not null add token
                //to dictionary
                if (!string.IsNullOrWhiteSpace(operatorCode))
                {
                    tokens.Add(PhoneNumberSection.OperatorCode, operatorCode);
                    phoneNumber = phoneNumber.Replace(operatorCode, "");
                }
            }
            else
            {
                throw new ArgumentException("Mobile number does not contain enough digits. Check phone number and try again.", "phoneNumber");
            }

            //check and retreive actual mobile number
            //177 297
            if(phoneNumber.Length == 6)
            {
                tokens.Add(PhoneNumberSection.RealNumber, phoneNumber);
            }
            else if(phoneNumber.Length < 6)
            {
                throw new ArgumentOutOfRangeException("phoneNumber", "Less than 6 digits encountered for the actual phone number. After removing country code and mobile operator code, the number should contain 6 digits only.");
            }
            else if(phoneNumber.Length > 6)
            {
                throw new ArgumentOutOfRangeException("phoneNumber", "More than 6 digits encountered for the actual phone number. After removing country code and mobile operator code, the number should contain 6 digits only.");
            }

            return tokens;
        }
        /// <summary>
        /// Checks if a phone number is valid
        /// </summary>
        /// <param name="phoneNumber">Mobile number to check if is valid.</param>
        /// <returns>A boolean result with true meaning that the 
        /// number supplied is valid and false stating otherwise
        /// </returns>
        public static bool isValid(string phoneNumber)
        {
            try
            {
                IDictionary<PhoneNumberSection,string> tokens = Tokenize(phoneNumber);

                if (tokens.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
