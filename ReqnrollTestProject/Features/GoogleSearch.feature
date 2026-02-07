Feature: Google Search

  Scenario: Open Google home page
    Given I open the browser
    When I navigate to "https://www.google.com"
    Then the page title should contain "Google"
