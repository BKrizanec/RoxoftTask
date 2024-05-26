Feature: Login Tests with Invalid Users
  As a user
  I want to verify that login with invalid credentials displays the correct error messages
  So that I can understand what went wrong

  @LoginPageInvalidTests
  Scenario Outline: Login with invalid credentials
    Given I navigated to the login page
    When I attempt to login as user <username> <password> and expect message <expectedErrorMessage>

    Examples:
    | username               | password       | expectedErrorMessage                 |
    | standard_user          |                | Epic sadface: Password is required                 |
    |                        | secret_sauce   | Epic sadface: Username is required                 |
    | random_nonexistant_user| secret_sauce   | Epic sadface: Username and password do not match any user in this service |
    | standard_user          | random_wrong_password | Epic sadface: Username and password do not match any user in this service|