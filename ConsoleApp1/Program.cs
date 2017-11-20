using MobileNumberFormatter;
using MobileNumberFormatter.Interfaces;
using MobileNumberFormatter.Models;
using System;

namespace TestConsoleApp
{
    class Program
    {
        private static IPhoneNumberFormatter phoneNumberFormatter = new PhoneNumberFormatter();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Configure();

            Console.ReadLine();
        }
        //configure
        private static void Configure()
        {
            while (true)
            {
                Console.WriteLine("\n\nEnter a mobile number to test");

                string input = Console.ReadLine();

                TestFormat(input);
            }
        }
        //test validity
        private static void TestValidity(string number)
        {
            Console.WriteLine("Testing {0} for validity",number);

            bool result = phoneNumberFormatter.IsValid(number);

            Console.WriteLine("IsValid: {0}", result);
        }
        //test service provider
        private static void TestProvider(string number)
        {
            Console.WriteLine("Checking mobile operator for {0}", number);

            MNO result = phoneNumberFormatter.GetProvider(number);

            Console.WriteLine("Operator: {0}", result);
        }

        //test format number
        private static void TestFormat(string number)
        {
            Console.WriteLine("Formatting {0}...\n", number);

            PhoneNumber result = phoneNumberFormatter.Format(number);

            Console.WriteLine("Country Code: {0}\nPrefix: {1},{2}\nSuffix: {3}",
                   result.CountryCode,
                   result.Prefix,
                   result.MobileOperator,
                   result.Suffix);
        }
    }
}
