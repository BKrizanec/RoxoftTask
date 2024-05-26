Feature: Login Tests
  As a user
  I want to verify that login with different credentials behaves correctly
  So that I can ensure the login process works as expected

  @LoginPageTests
  Scenario Outline: Login with valid credentials
    Given I navigate to the login page
    When I attempt to login the user <username> <password> <isLocked>

    Examples:
    | username           | password | isLocked |
    | standard_user      | secret_sauce | false  |
    | locked_out_user    | secret_sauce | true |
    | problem_user       | secret_sauce | false  |
    | performance_glitch_user | secret_sauce | false  |
    | error_user         | secret_sauce | false  |
    | visual_user        | secret_sauce | false  |