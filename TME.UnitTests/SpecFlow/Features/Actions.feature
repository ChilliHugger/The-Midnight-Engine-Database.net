Feature Test actions
    
Scenario: A lord drops a non unique object
    Given a lord is carrying an object
    When the object is dropped
    Then action will complete successfully

Scenario: A lord drops a unique object
    Given a lord is carrying an non unique object
    When the object is dropped
    Then the object will be at the current location

