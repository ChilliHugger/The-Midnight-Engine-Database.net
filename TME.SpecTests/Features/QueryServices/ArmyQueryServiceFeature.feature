Feature: Army Query Service Feature

Background: 
    Given there are lords with the following friend or foe status
      | status | warriors | riders |
      | foe    | yes      | yes    |
      | foe    | yes      | no     |
      | foe    | no       | yes    |
      | foe    | no       | no     |
      | friend | yes      | yes    |
      | friend | yes      | no     |
    And there are regiments with the following friend or foe status
      | status | type     | total |
      | foe    | warriors | 100   |
      | foe    | warriors | 100   |
      | foe    | warriors | 0     |
      | foe    | riders   | 100   |
      | foe    | riders   | 100   |
      | foe    | riders   | 0     |
      | friend | warriors | 100   |
      | friend | riders   | 100   |
      | friend | riders   | 0     |
    And there are strongholds with the following friend or foe status
      | status | type     | total |
      | foe    | warriors | 100   |
      | foe    | warriors | 100   |
      | foe    | warriors | 0     |
      | foe    | riders   | 100   |
      | foe    | riders   | 100   |
      | friend | warriors | 100   |
      | friend | riders   | 100   |
      | friend | riders   | 0     |
Scenario Outline: Query Service should return the correct character army count
    When the request to count the <type> of the character is made
    Then there should be <count> armies
    Scenarios: 
        | count | type    |
        | 3     | friends |
        | 4     | foes    |
    
Scenario Outline: Query Service should return the correct character armies
    When the request for <unit> that are <type> of the character
    Then there should only be <count> armies of the correct type
    Scenarios: 
        | unit     | count | type    |
        | warriors | 2     | friends |
        | warriors | 2     | foes    |
        | riders   | 1     | friend  |
        | riders   | 2     | foes    |
  
Scenario Outline: Query Service should return the correct regiment count
    When the request to count regiments that are <type> of the character
    Then there should be <count> armies
    Scenarios: 
        | count | type    |
        | 2     | friends |
        | 4     | foes    |
      
Scenario Outline: Query Service should return the correct regiment armies
    When the request for <unit> regiments that are <type> of the character
    Then there should be <count> armies of the correct type
    Scenarios: 
        | unit    | count | type    |
        | warrior | 1     | friends |
        | warrior | 2     | foes    |
        | rider   | 1     | friend  |
        | rider   | 2     | foes    |
        
Scenario Outline: Query Service should return the correct stronghold count
    When the request to count strongholds that are <type> of the character
    Then there should be <count> armies
    Scenarios: 
      | count | type    |
      | 2     | friends |
      | 4     | foes    |
  
Scenario Outline: Query Service should return the correct stronghold armies
    When the request for <unit> strongholds that are <type> of the character
    Then there should be <count> armies of the correct type
    Scenarios: 
      | unit    | count | type    |
      | warrior | 1     | friends |
      | warrior | 2     | foes    |
      | rider   | 1     | friend  |
      | rider   | 2     | foes    |