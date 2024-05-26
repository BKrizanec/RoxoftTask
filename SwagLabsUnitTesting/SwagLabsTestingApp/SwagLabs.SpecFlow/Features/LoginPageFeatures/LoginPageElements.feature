Feature: Login Page Basic Functionality
  As a user
  I want to ensure that the login page elements and functionality work correctly
  So that I can attempt to log in successfully

  @LoginPageElements
  Scenario Outline: Verify login page elements and functionality

    Given I open the login page
    Then the login form elements should be displayed
    And the placeholder text for the <expectedPlaceholder> field should be <elementName>

    Examples:
      | expectedPlaceholder | elementName |
      | Username            | user-name  |
      | Password            | password   |
