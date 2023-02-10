
## Requirements

#### <u>Unit Tests</u>
  * [ ] Fix Tests
    * [x] Broken Tests
    * [ ] Incorrect Tests 
      * [x] Bank
        * [x] FirstCustomer
          * [x] Bank has no current customers added, return message (simply throwing an error is not ideal)
          * [x] Bank has customers, return name of first customer (currently, index based - shoddy...)
        * [x] CustomerSummary
          * [x] Bank has no current customers added, return message (simply throwing an error is not ideal)
          * [x] Bank has customers, return stringified customer summary
    * [ ] Incomplete Tests
    * [ ] Review for Missing/wrong/incomplete Tests
      * [x] Account(s)
      * [x] Bank
      * [ ] Customer
      * [ ] Transaction
      * [ ] DateProvider

#### <u>Existing Features</u>
  * [x] All existing features working
    * [x] Customer Can Open Account
    * [x] Customer can Deposit Funds
    * [x] Customer can Withdraw Funds
    * [ ] Customer can request statement for each account (But moving on without...)
      * [x] Currently, Customer can request 'a' statement that has all accounts
    * [x] Interest is calculated based on account type
      * [x] Checking - flat 0.1% rate
      * [x] Savings
        * [x] 0.1% rate when balance is <= $1000
        * [x] 0.2% rate when balance is > $1000
      * [x] Maxi-Savings
        * [x] 2% rate when balance is <= $1000
        * [x] 5% rate when balance is > $1000
    * [x] Bank Manager Reports
      * [x] List of customer & how many accounts each has 
      * [x] Total interest paid for all accounts

#### <u>New Features</u>
  * [ ] Customer can transfer funds between accounts
  * [ ] Update Maxi-Savings interest rate when
    * [ ] 5% rate when no withdrawls in past 10 days
    * [ ] 0.1% when a withdrawl occurs within past 10 days
  * [ ] Interest accrues daily per-annum (for each year)
  - [per-annum](https://www.accountingcoach.com/blog/what-does-per-annum-mean#:~:text=Per%20annum%20means%20yearly%20or,pays%20interest%20of%206%25.%22)

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
  * [x] added (double)initialDeposit arg to constructor(s)
  * [x] added AccountCreator Class with GetAccount method & dictionary to instantiate new Account objects



