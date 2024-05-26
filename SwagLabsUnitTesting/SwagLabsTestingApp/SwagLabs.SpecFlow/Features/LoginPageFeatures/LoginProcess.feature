Feature: Login Process
  As a user
  I want to verify the login functionality with valid and invalid credentials
  So that I can ensure users can log in successfully and see appropriate error messages

  @LoginPage
  Scenario Outline: Login with valid credentials
    Given I navigate to the login page
    When I attempt to login with valid credentials <username> and <password>
    Then the login should be successful if the user is not <isLocked>
    Examples:
      | username                | password     | isLocked |
      | standard_user           | secret_sauce | false    |
      | problem_user            | secret_sauce | false    |
      | performance_glitch_user | secret_sauce | false    |
      | error_user              | secret_sauce | false    |
      | visual_user             | secret_sauce | false    |
      | locked_out_user         | secret_sauce | true     |

  @LoginPage
  Scenario Outline: Login with invalid credentials
    Given I navigate to the login page
    When I attempt to login with invalid credentials <invalidUsername> and <invalidPassword>
    Then I should see the error message <expectedErrorMessage>
    Examples:
      | invalidUsername                | invalidPassword              | expectedErrorMessage                                                      |
      | standard_user           |                       | Epic sadface: Password is required                                        |
      |                         | secret_sauce          | Epic sadface: Username is required                                        |
      | random_nonexistant_user | secret_sauce          | Epic sadface: Username and password do not match any user in this service |
      | standard_user           | random_wrong_password | Epic sadface: Username and password do not match any user in this service |
