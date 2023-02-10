
## Requirements

#### <u>Unit Tests</u>
* [ ] Fix Tests
  * [x] Broken Tests
  * [ ] Incorrect Tests 
    * [ ] Bank
      * [x] FirstCustomer
        * [x] Bank has no current customers added, return message (simply throwing an error is not ideal)
        * [x] Bank has customers, return name of first customer (currently, index based - shoddy...)
      * [x] CustomerSummary
        * [x] Bank has no current customers added, return message (simply throwing an error is not ideal)
        * [x] Bank has customers, return stringified customer summary
  * [ ] Incomplete Tests
  * [ ] Missing Tests
    * [ ] Bank
      * [x] FirstCustomer No-Customers exist
      * [ ] CustomerSummary
      * [ ] TotalInterestPaid
      * [ ] AddCustomer(Customer customer)
      * [ ] AddCustomer(string Name)
      * [ ] AddCustomer(string Name)
      * [ ] Format
    * [ ] Customer
    * [ ] Account(s)
    * [ ] Transaction
    * [ ] DateProvider (? doesn't appear to be in use - yet...) 

#### <u>Existing Features</u>
  * [ ] All existing features working
    * [x] Customer Can Open Account
    * [x] Customer can Deposit Funds
    * [x] Customer can Withdraw Funds
    * [ ] Customer can request statement for each account
    * [ ] Interest is calculated based on account type
      * [x] Checking - flat 0.1% rate

      * [x] Savings
        * [x] 0.1% rate when balance is <= $1000
        * [x] 0.2% rate when balance is > $1000

      * [ ] Maxi-Savings
        * [ ] 2% rate when balance is <= $1000
        * [ ] 5% rate when balance is > $1000

    * [ ] Bank Manager Reports
      * [ ] List of customer & how many accounts each has 
      * [ ] Total interest paid for all accounts

#### <u>New Features</u>
  * [ ] Customer can transfer funds between accounts

  * [ ] Update Maxi-Savings interest rate when
    * [ ] 5% rate when no withdrawls in past 10 days
    * [ ] 0.1% when a withdrawl occurs within past 10 days

  * [ ] Interest accrues daily per-annum (for each year)

## Refactoring Tasks

#### <u>Accounts</u>
  * [x] Create Account Interface
  * [x] Create CheckingAccount
    * [x] All unit tests passing for previous implementation
  * [x] Create SavingsAccount
    * [x] All unit tests passing for previous implementation    
      * [x] 0.1% rate when balance is <= $1000
      * [x] 0.2% rate when balance is > $1000
  * [x] Create MaxiSaveAccount
    * [x] All unit tests passing for previous implementation (That were originally correct & passing anyway)
  * [x] Move Common/Shared Account(s) Behaviors to AccountBase abstract class.
    * [x] AccountType Property
    * [x] CurrentBalance Property
    * [x] Transactions Property
    * [x] HasTransactions Property
    * [x] CalculateInterest Method
    * [x] Deposit Method
    * [x] Withdraw Method
    * [x] abtract InterestEarned Property

#### <u>Wishlist</u>
  * [ ] Ideally, follow up by consolidating all the repeat Account<N> tests to a single TestClass
  * [ ] Account Creation should be a factory, accepting a single parameter of Accounts.AccountType


