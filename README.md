# William Washington - Example Work
I am a .NET Developer that loves to find ways to inject efficiency.
If you're interested, you can learn more about me here: https://github.com/willWashington

## The Code Challenge
In my recent job search, a company offered this programming test to me. 

Two things stood out to me in their challenge: "treat this code as if you owned this application" and "you're welcome to spend as much time as you like"

I asked myself what would I do if this indeed were my own application. My solution was to make it more extensible and refactor it to be something more. 

I decided to change it from a vague Bank app to an extensible Establishment app. Instead of just Banks, it can now start to think about Establishments in general. That means we have a foundation of preparedness to handle Rental Agencies, Loan Servicers, Loan Originators, Stock firms.. the sky is the limit.

To compound on that, I added an infrastructure based on our classic OOP notion. I've conceptualized Accounts, Customers, Establishments, and Transactions and modeled those objects in code.

I believe that the new configuration allows the application to be much more dynamic and extensible while providing a solid, modern foundation to continue to improve on.

## The Stack & Implementation
I implemented .NET 4.8.1 in the first PR I pushed for the challenge. The implementation itself leverages TDD. I built the foundation of the architecture and then started to build the first test. This test is designed to be fired in CI/CD eventually as a startup test. It should test the foundation of the entire app and be run on startup for validation at runtime. It should create an Establishment, a Customer, two Accounts for the Customer: Checking and Saving, and generate Transactions for those accounts. Once I was able to wire all this up, I engaged strictly in TDD, writing new code from the foundational test. I'll continue to engage in that behavior going forward.

In the future, we can move to different tests based on conditionals considering the various Establishments we can consume.

## Things I'd Like to Improve in this Repo

-This project did not have a front end, but if I were to implement one, I'd probably build an API infrastructure with Controllers that were accessible from the UI app. I'd build that app today in Razor, more than likely, implementing .NET 5. I choose 5 because I've ran into some issues in 6 and 7 that seem to still need time to work out, and 5 is the most stable. That said, I'd probably still use top-level statements for my entry point. I think I like that implementation on the global entry point.

-This solution needs to quickly be moved to a modern technology like .NET 5, at least. 4.8.1 is very far behind. The migration to the new technology may contain risks that require time that are beyond the scope of this task for me, so I won't implement that here, but from a business perspective, it should happen almost immediately and prior to a production launch.

-I personally am a proponent for the idea that OOP has been misunderstood in that we should not be passing objects but rather messages between objects. I like to build my objects in my more personal projects to have methods on them rather than having methods in Controllers or etc that manipulate those objects so as to implement a more message oriented flow.

##

## Programming Test

## Instructions

There are several deliberate design, code quality and test issues that should be identified and resolved. The section below details what the behavior of the application is supposed to be, which may not match up with how the code is currently functioning.

* Fix broken (non-passing), incorrect, or incomplete tests.
* Refactor and add features (from the section below) as you see fit; there is no need to add all the features in order to "complete" the exercise. Keep in mind that code quality is the critical measure and there should be an obvious focus on testing.
* Treat this code as if you owned this application, do whatever you feel is necessary to make this your own. *Clarification:* Please do not change the testing framework.
* In order to work on this take a fork into your own GitHub area; make whatever changes you feel are necessary and when you are satisfied submit back via a pull request. See details on GitHub's [Fork & Pull](https://help.github.com/articles/using-pull-requests) model.
* You're welcome to spend as much time as you like; however it's anticipated that this should take about 2 hours.

Application
-----------

A dummy application for a bank; should provide various functions of a retail bank.

Application is designed for Visual Studio 2017 or newer, MSTest as the testing framework (integrated into Visual Studio), and uses the .NET Framework v4.7.2. The solution is compatible with the community version.

### Current Features

* A customer can open an account.
* A customer can deposit / withdraw funds from an account.
* A customer can request a statement that shows transactions and totals for each of their accounts.
* Different accounts have interest calculated in different ways:
  * **Checking accounts** have a flat rate of 0.1%.
  * **Savings accounts** have a rate of 0.1% for the first $1,000 then 0.2%.
  * **Maxi-Savings accounts** have a rate of 2% for the first $1,000 then 5% for the next $1,000 then 10%.
* A bank manager can get a report showing the list of customers and how many accounts they have.
* A bank manager can get a report showing the total interest paid by the bank on all accounts.

### Additional Features

* A customer can transfer between their accounts.
* Change **Maxi-Savings accounts** to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%.
* Interest rates should accrue daily including weekends, rates above are per-annum (for each year).
