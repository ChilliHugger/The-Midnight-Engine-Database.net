Feature: Drop Object Command
    
Background: 
	Given it is Dawn
	And a lord is carrying an object
	And the ObjectDropped action will return success
	
Scenario: A lord drops an object and the correct action should be run
	When the lord tries to drop their object
	Then the ObjectDropped action should be run
	
Scenario: A lord cannot drop an object that they are not carrying
	When the lord tries to drop an object they are not carrying
	Then they should not be able to drop the object
	
Scenario Outline: A lord can only drop an object when there are enough hours remaining
	Given <Hour> hours of the day remain
	Then the lord <ShouldShouldNot> be able to drop the object
Scenarios: 
| Hour | ShouldShouldNot |
| 1    | should          |
| 0    | should not      |

Scenario: A lord drops an object and the command should be added to the history
	When the lord tries to drop their object
	Then the 'Drop Object' command should be saved to the history
	
Scenario: After a lord drops an object then their time should not move forward
	When the lord tries to drop their object
	Then their time should not change
	
Scenario: After a lord drops an object then they should no longer be carrying the object
	When the lord tries to drop their object
	Then they should no longer be carrying the object

Scenario Outline: A lord drops an object then it should be successfull dropped
	Given the ObjectDropped action will return <SuccessOrFailure>
	When the lord tries to drop their object
	Then they <ShouldShouldNot> drop the object
Scenarios: 
| SuccessOrFailure | ShouldShouldNot |
| success          | should          |
| failure          | should not      |

	

