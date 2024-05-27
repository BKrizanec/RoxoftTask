Feature: Cart Operations
  As a user
  I want to manage items in my cart
  So that I can purchase the desired products

  Background:
    Given I want to purchase items

  Scenario: Add all items to the cart
    When I add all items to the cart
    Then the cart should reflect the correct number of items added

  Scenario: Remove all items from the cart
    Given I have added all items to the cart
    When I remove all items from the cart
    Then the cart should be empty

  Scenario: View cart
    When I click on the cart icon
    Then I should be redirected to the cart page
