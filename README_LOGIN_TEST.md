# Login Test - Page Object Model (POM)

## Overview
This project contains a complete Page Object Model (POM) implementation for testing the login functionality of the e-commerce website: https://ecommerce-playground.lambdatest.io/index.php?route=account/login

## Project Structure

### 1. **Page Object Model** (`Pages/LoginPage.cs`)
Contains all the locators and methods for interacting with the login page:
- **Locators**: Email input, password input, login button, error messages, etc.
- **Methods**: 
  - `NavigateToLoginPage()` - Navigate to the login page
  - `EnterEmail(string email)` - Enter email in the input field
  - `EnterPassword(string password)` - Enter password in the input field
  - `ClickLoginButton()` - Click the login button
  - `Login(string email, string password)` - Complete login flow
  - `IsMyAccountHeaderDisplayed()` - Verify successful login
  - `IsErrorMessageDisplayed()` - Check for error messages
  - And more helper methods...

### 2. **Feature File** (`Features/Login.feature`)
Contains Gherkin scenarios for login testing:
- **Scenario 1**: Successful login with valid credentials
- **Scenario 2**: Verify login page elements are present

### 3. **Step Definitions** (`StepDefinitions/LoginSteps.cs`)
Contains the C# implementation of the Gherkin steps:
- Given steps for navigation
- When steps for actions (enter credentials, click login)
- Then steps for assertions and verifications

## Test Credentials
- **Email**: amwell709@gmail.com
- **Password**: Sellenium123#

## Running the Tests

### Run all login tests:
```bash
dotnet test --filter "FullyQualifiedName~Login"
```

### Run all tests in the project:
```bash
dotnet test
```

### Build the project:
```bash
dotnet build
```

## Test Results Summary

### ✅ Test 2: Verify login page elements - **PASSED**
This test successfully verified that all login page elements are present:
- Email input field
- Password input field
- Login button
- Forgotten password link

### ⚠️ Test 1: Successful login with valid credentials - **FAILED**
This test detected an error message after login attempt. Possible reasons:
- Credentials may be invalid or expired
- Account may not exist or may be locked
- Website may require additional verification

**Note**: The test framework is working correctly - it properly detected the error condition. If you have valid credentials, the test should pass.

## Key Features of This POM Implementation

1. **Separation of Concerns**: Page logic is separated from test logic
2. **Reusability**: Page methods can be reused across multiple tests
3. **Maintainability**: If page elements change, only update the LoginPage class
4. **Explicit Waits**: Uses WebDriverWait for reliable element interactions
5. **Error Handling**: Comprehensive error handling with descriptive messages
6. **BDD Approach**: Uses Gherkin syntax for readable test scenarios

## Adding New Test Scenarios

To add new login test scenarios:

1. Add a new scenario in `Features/Login.feature`
2. If new steps are needed, add them to `StepDefinitions/LoginSteps.cs`
3. If new page interactions are needed, add methods to `Pages/LoginPage.cs`

## Example: Adding a Negative Test

```gherkin
Scenario: Login with invalid credentials
  Given I navigate to the login page
  When I enter email "invalid@email.com"
  And I enter password "wrongpassword"
  And I click the login button
  Then I should see an error message
```

## Troubleshooting

If tests fail:
1. Verify ChromeDriver is compatible with your Chrome browser version
2. Check internet connectivity
3. Verify the website is accessible
4. Ensure credentials are correct and active
5. Check console output for detailed error messages

## Technologies Used
- **Reqnroll**: BDD framework (SpecFlow successor)
- **Selenium WebDriver**: Browser automation
- **NUnit**: Testing framework
- **C# .NET 8.0**: Programming language and framework
