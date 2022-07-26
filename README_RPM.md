RPM Responses

I wanted to add notes for what was and more importantly, was not done:

- Initial failure in TestApp due to ToDollars improperly formatting the output string. Caused inequality in the account statement assert.
- TestThreeAccount was initially ignored, but was also only setting up two accounts. Based on Test naming convention assumed that test was inccorrect, added creation of a MAXI savings acct in the test.
- Found what looked like a vestigal reference to SUPER_SAVINGS acct (commented code). Removed it. Not necessary, just style preference.
- Discovered withdraw would allow an amount greater than what was in acct. Added an Exception for that using sumTransactions in test.
- Found it odd afterwards that accounts do not have a human readable acct number, so I added one. I know it is more complex that generating a random 10 numeral string, so consider GenerateAccountNumber method a temporary "stub" 

FEATURE #1
- Simple enough to create a transfer using existing withdraw and deposit. However as is, statement would not distinguish between incoming deposit and incoming transfer. so I changed that.
	- Added a field for transaction type and added constants 0=Deposit, 1=Withdrawal, 2=Transfer
	- Created wrapper methods around adding a transaction in Account class. One for each transaction type.
	- Added TestTransfer to the tests.
	- changes in the statementForAccount method to take advantage of the new types
	- changes to tests to call specific trans methods instead of generic trans.
- (TBD) alter the transaction to keep track of the to/from acct 

FEATURE #2
- Changed calculation for the MAXI-SAVINGS. Was not certain where you wanted to use the %5/%0.1 in the current formula, so read the instructions as replacing the entire formula. (Read spec as written)
	- This required making the transactionDate public, so I could tell if a transaction was within 10 days or not.
	- changes to interest earned formula
	- modified and add a test to check with MAXI-SAVINGS with 10 day withdraw and without
- Although tests work as expected for MAXI-SAVINGS, in a real world scenario, I beieve that you would require true historical data in order to test this properly. In this case, it would require changing the Methods that add a transaction to optionally allow setting a date other than the system date. This is simple enough to do, but at this point it would be adding functionality, just to add test data. Now if a use case was to allow recording transactions in the past/future, this would make more sense.

FEATURE #3
- This would require the changes mentioned in FEATURE#2, plus would need to allow for the compounding of interest. I felt that this would exceed the alloted time and in a real world scenario, I would probably want more specifications anyway before implementing.

 
