
## Requirements

#### <u>Unit Tests</u>
* [ ] Fix Tests
  * [x] Broken Tests
  * [ ] Incorrect Tests 
  * [ ] Incomplete Tests

#### <u>Existing Features</u>
* [ ] All existing features working
  * [ ] Customer Can Open Account
  * [ ] Customer can Deposit Funds
  * [ ] Customer can Withdraw Funds
  * [ ] Customer can request statement for each account
  * [ ] Interest is calculated based on account type
    * [ ] Checking - flat 0.1% rate

    * [ ] Savings
      * [ ] 0.1% rate when balance is <= $1000
      * [ ] 0.2% rate when balance is > $1000

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
#### <u>Refactor Accounts</u>
* [x] Create Account Interface
* [x] Create CheckingAccount
  * [x] All unit tests passing for previous implementation
* [ ] Create SavingsAccount
  * [ ] All unit tests passing for previous implementation
* [ ] Create MaxiSaveAccount
  * [ ] All unit tests passing for previous implementation
* [ ] Move calculate interest to external method(?)


