Feature: Home Page Navigation
  As a user
  I want to navigate through the home page
  So that I can access different parts of the application

  Background:
    Given I am logged in

  Scenario: Verify all inventory items are visible and enabled
    When I navigate to the home page
    Then all inventory items should be visible and enabled

  Scenario: Click on "All Items" menu item
    When I click on the "All Items" menu item
    Then I should be redirected to the inventory page

  Scenario: Click on "About" menu item
    When I click on the "About" menu item
    Then I should be redirected to the about page

  Scenario: Click on "Logout" menu item
    When I click on the "Logout" menu item
    Then I should be logged out and redirected to the login page

  Scenario: Click on "Reset App State" menu item
    When I click on the "Reset App State" menu item
    Then the cart should be empty and I should remain on the inventory page
