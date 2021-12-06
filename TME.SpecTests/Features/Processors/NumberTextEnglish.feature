Feature: Number Text English

Scenario Outline: Display Zero
	Given the number is 0
	And the zero mode is '<Mode>'
	When the number is displayed
	Then the result should be '<Result>'
	Scenarios:
| Mode | Result |
| no   | no     |
| none | none   |
| zero | zero   |

Scenario Outline: Display a number with Zero Mode 'Zero'
	Given the number is <Number>
	And the zero mode is 'Zero'
	When the number is displayed
	Then the result should be '<Result>'
	Scenarios:
	| Number | Result                                  |
	| 1      | one                                     |
	| 11     | eleven                                  |
	| 21     | twenty one                              |
	| 31     | thirty one                              |
	| 41     | forty one                               |
	| 51     | fifty one                               |
	| 61     | sixty one                               |
	| 71     | seventy one                             |
	| 81     | eighty one                              |
	| 91     | ninety one                              |
	| 100    | one hundred                             |
	| 101    | one hundred and one                     |
	| 111    | one hundred and eleven                  |
	| 121    | one hundred and twenty one              |
	| 1000   | one thousand                            |
	| 1001   | one thousand and one                    |
	| 1011   | one thousand and eleven                 |
	| 1021   | one thousand and twenty one             |
	| 1100   | one thousand one hundred                |
	| 1101   | one thousand one hundred and one        |
	| 1111   | one thousand one hundred and eleven     |
	| 1121   | one thousand one hundred and twenty one |


