Feature: Sorting Inventory Items
  As a user
  I want to sort inventory items
  So that I can find products more easily

  Background:
    Given I want to sort items

  Scenario: Sort items by name in ascending order
    When I select sorting option "Name (A to Z)"
    Then the items should be sorted by name in ascending order

  Scenario: Sort items by name in descending order
    When I select sorting option "Name (Z to A)"
    Then the items should be sorted by name in descending order

  Scenario: Sort items by price in ascending order
    When I select sorting option "Price (low to high)"
    Then the items should be sorted by price in ascending order

  Scenario: Sort items by price in descending order
    When I select sorting option "Price (high to low)"
    Then the items should be sorted by price in descending order
