Feature: Login Page Placeholder Text
  As a user
  I want to ensure the login form placeholder text is correct
  So that I can understand what to input in each field

  Background:
    Given The user enters the login page

 Scenario Outline: Verify placeholder text
    Then the placeholder text for the <expectedPlaceholder> field should be <elementName>

    Examples:
      | expectedPlaceholder | elementName |
      | Username            | user-name  |
      | Password            | password   |