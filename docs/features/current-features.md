### Existing Features ✅
---

&emsp;<sup>✔️</sup> Customer Can Open Account
&emsp;<sup>✔️</sup> Customer can Deposit Funds
&emsp;<sup>✔️</sup> Customer can Withdraw Funds
&emsp;<sup>❌</sup> Customer can request statement for each account
  &emsp;&emsp;&emsp;<sup>⚠️</sup> Customer can only request a statement that has all accounts
  &emsp;&emsp;&emsp;<sup>⚠️🔻</sup> Deprioritized
  &emsp;&emsp;&emsp;&emsp;&emsp;* Workaround is available (single statement contains all accounts)
  &emsp;&emsp;&emsp;&emsp;&emsp;* Excessive time burned implementing missing unit tests
&emsp;<sup>✔️</sup> Interest is calculated based on account type
  &emsp;&emsp;&emsp;<sup>✔️</sup> Checking * flat 0.1% rate
  &emsp;&emsp;&emsp;<sup>✔️</sup> Savings
    &emsp;&emsp;&emsp;&emsp;&emsp;<sup>✔️</sup> 0.1% rate when balance is <= $1000
    &emsp;&emsp;&emsp;&emsp;&emsp;<sup>✔️</sup> 0.2% rate when balance is > $1000
  &emsp;&emsp;&emsp;<sup>✔️</sup> Maxi-Savings
    &emsp;&emsp;&emsp;&emsp;&emsp;<sup>✔️</sup> 2% rate when balance is <= $1000
    &emsp;&emsp;&emsp;&emsp;&emsp;<sup>✔️</sup> 5% rate when balance is > $1000
&emsp;<sup>✔️</sup> Bank Manager Reports
  &emsp;&emsp;&emsp;<sup>✔️</sup> List of customer & how many accounts each has 
  &emsp;&emsp;&emsp;<sup>✔️</sup> Total interest paid for all accounts