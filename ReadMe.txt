I took the directions to mean that I should not change the framework from .net to core so I refactored in such 
a way as to facilitate the use of dependencey injection down the road and the code could be converted to CORE. 
Also, the static helper classes can be made to be non-static and injected as services down the road should they become 
a bottleneck.

Breaking out the helper logic seemed to make sense given the smallest operations to test. The code could've remained
in the original format but the breakout also lent itself to ease of maintenance by even the newest resources to the code.

I hope what I did makes sense and I thank you for the opportunity to take this evaluation.

Joe Angott