# Mobile Number Formatter

[![Scrutinizer Build](https://img.shields.io/scrutinizer/build/g/filp/whoops.svg)]()
[![license](https://img.shields.io/github/license/mashape/apistatus.svg)]()
[![build/tests](https://img.shields.io/badge/tests-5%20passing-brightgreen.svg)]()

This library works to help developers with the fear of using faulty mobile numbers from users in processing transactions and communications. Its currently built on .NET Standard and thus targets; Android, UWP, net framework and iOS. The library currently targets phone numbers in Kenya and is looking forward to scale out to other countries soon.

### Usage

The easiest way to use this library in your project is via nuget. To install phoneNumberFormatter, run the following command in Package Manager Console

```
PM> Install-Package MobileNumberFormatter
```

OR

If you want to have a look at the source code and maybe modify some things, cloning this github repository to your dev machine might work for you.

```
git clone https://github.com/devTimmy/phoneNumberFormatter.git
```
## Quickstart
Reference the main interface to use plus its implementation to get started.

```cs
using MobileNumberFormatter;
using MobileNumberFormatter.Interfaces;
```
Create variables to hold our interface

```cs
private readonly IPhoneNumberFormatter _phoneNumberFormatter = new PhoneNumberFormatter();
```

The interface provides 3 methods that you can use to process phone numbers with namely

* Format
* IsValid
* GetProvider

### 1. Format
Breaks down a phone number from a string and builds a PhoneNumber object with all the separate sections of a phone number. Call this method using the following syntax

```cs
PhoneNumber phoneNumber = _phoneNumberFormatter.Format("+254716 123 456");
```

### 2. IsValid
Checks if a phone number is valid or not. Call this method using the following syntax.

```cs
bool result = _phoneNumberFormatter.IsValid("0716123456");
```

### 3. GetProvider
Analyzes a phone number and gets the [Mobile Network Operator(MNO)](https://en.wikipedia.org/wiki/Telephone_numbers_in_Kenya)
e.g Safaricom, Airtel, Telkom or Equitel. How to call this method

```cs
MNO mobileOperator = _phoneNumberFormatter.GetProvider("0765 123 456");
```

### About me

FullStack developer based in Nairobi, Kenya

Welcome to contact me

Email: [timothy.macharia@outlook.com](mailto:timothy.macharia@outlook.com)
