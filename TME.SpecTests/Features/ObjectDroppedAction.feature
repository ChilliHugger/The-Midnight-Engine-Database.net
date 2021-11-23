Feature: Object Dropped Action
    
Scenario: A non unique object is dropped by its carrier
    Given a non unique object is being carried
    When the object is dropped
    Then the object should be at the current location
    
Scenario: A unique object is dropped by its carrier
    Given a unique object is being carried
    When the object is dropped
    Then the object should be at the current location
    
Scenario: A unique object is dropped by no one
    Given an object is not being carried
    Then the object should not be able to be dropped

