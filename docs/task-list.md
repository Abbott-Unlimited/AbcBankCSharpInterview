### <u>Requirements</u>

<details>
  <summary>
    <u>Unit Tests Review and Correction</u> <sup><sub>🟡</sub></sup>
  </summary>

  &emsp;<sup><sub>✅</sub></sup> Broken Tests
  &emsp;<sup><sub>🟡</sub></sup> Incorrect Tests 
    &emsp;&emsp;<sup>✔️</sup> Bank
      &emsp;&emsp;&emsp;<sup>✔️</sup> FirstCustomer
        &emsp;&emsp;&emsp;&emsp;<sup>✔️</sup> If Bank has no current customers added, return message instead of throwing error.
        &emsp;&emsp;&emsp;&emsp;<sup>✔️</sup> If Bank has customers return name of first customer
          &emsp;&emsp;&emsp;&emsp;&emsp;<sup>⚠️</sup> Ordering via List item index. _(Should be more deterministic)_<sup>🔻</sup>
      &emsp;&emsp;&emsp;<sup>✔️</sup> CustomerSummary
        &emsp;&emsp;&emsp;&emsp;<sup>✔️</sup> If Bank has no current customers added, return message instead of throwing error.
        &emsp;&emsp;&emsp;&emsp;<sup>✔️</sup> if Bank has customers, return stringified customer summary
  &emsp;<sup><sub>🟡</sub></sup> Review for Missing/wrong/incomplete Tests
    &emsp;&emsp;<sup>✔️</sup> Account(s)
    &emsp;&emsp;<sup>✔️</sup> Bank
    &emsp;&emsp;<sup><sub>🟠</sub></sup> Customer
    &emsp;&emsp;<sup><sub>🟡</sub></sup> Transaction
    &emsp;&emsp;<sup><sub>⚫🔻</sub></sup> DateProvider (looks to be cruft)
</details>


#### <u>Refactoring & Code Quality Improvements</u>

<details>
  <summary>
    <u>Accounts</u> <sup>✅</sup>
  </summary>

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
  * [x] added AccountCreator Class with GetAccount method & dictionary to instantiate new Account objects (pseudo-factory)
</details>



