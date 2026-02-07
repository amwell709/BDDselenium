# Azure Pipeline Configuration Guide

## Overview
This document explains the Azure Pipeline setup for the Reqnroll BDD Selenium test project and provides troubleshooting guidance.

## Pipeline Configuration

### What the Pipeline Does
1. **Installs .NET 8.0 SDK** - Required to build and run the tests
2. **Restores NuGet packages** - Downloads all dependencies
3. **Builds the solution** - Compiles the test project
4. **Runs Selenium tests** - Executes BDD tests with headless Chrome
5. **Publishes test results** - Makes results available in Azure DevOps
6. **Publishes artifacts** - Saves build outputs

### Key Features
- **Headless Chrome**: Tests automatically run in headless mode in CI/CD
- **Verbose Logging**: Detailed console output for debugging
- **Test Result Publishing**: Results visible in Azure DevOps UI
- **Fail-Safe**: Pipeline continues even if tests fail, to publish results

## Current Error: Exit Code 1

### What This Means
Exit code 1 from `dotnet.exe` typically means the test execution failed. This could be due to:

1. **Test Failures** - The actual tests are failing (most likely)
2. **Network Issues** - Can't reach the external test website
3. **Timeout Issues** - Tests taking too long in CI environment
4. **ChromeDriver Issues** - Browser automation problems

### Troubleshooting Steps

#### Step 1: Check Test Results
Even though the pipeline shows an error, test results should still be published. Check:
- Go to your pipeline run
- Click on "Tests" tab
- Review which specific tests failed and error messages

#### Step 2: Review Detailed Logs
The pipeline now has verbose logging enabled:
- In your pipeline run, expand the "Run Reqnroll/Selenium tests" step
- Look for detailed error messages about what went wrong
- Common issues to look for:
  - "TimeoutException" - Website not responding
  - "WebDriverException" - ChromeDriver issues
  - "NoSuchElementException" - Element not found (website changed)

#### Step 3: Understand Expected Behavior
The pipeline is currently configured with:
```yaml
continueOnError: true  # Test step
failTaskOnFailedTests: false  # Publish step
```

This means:
- ❌ The pipeline will show as "failed" if tests fail
- ✅ But it will still publish test results
- ✅ You can see exactly which tests failed and why

### Common Solutions

#### Solution 1: Accept Test Failures are Normal
If your tests are accessing a live website (`https://ecommerce-playground.lambdatest.io`):
- The website might be down or slow
- Login credentials might expire
- Website elements might change
- **This is expected behavior** - external dependencies can fail

The pipeline is working correctly by reporting these failures!

#### Solution 2: Make Tests More Resilient
Update your tests to handle CI/CD environments better:
- Increase timeout values
- Add retry logic
- Handle intermittent failures gracefully
- Add better error messages

#### Solution 3: Add Test Categories
Tag tests that require external websites differently:
```csharp
[Binding]
[Category("ExternalDependency")]
public class LoginSteps { }
```

Then run only stable tests in CI:
```yaml
arguments: '--configuration $(buildConfiguration) --filter TestCategory!=ExternalDependency'
```

#### Solution 4: Use Mock/Stub Services
For CI/CD, consider:
- Using a local test website
- Mocking external services
- Using a dedicated test environment

## Environment Variables

The pipeline sets these CI/CD detection variables:
```yaml
TF_BUILD: 'true'  # Azure Pipelines indicator
CI: 'true'        # Generic CI indicator
```

Your `WebDriverFactory.cs` uses these to automatically enable headless mode.

## Viewing Test Results

Even when the pipeline fails:
1. Go to your pipeline run in Azure DevOps
2. Click the "Tests" tab
3. You'll see:
   - ✅ Passed tests (green)
   - ❌ Failed tests (red)
   - Detailed error messages
   - Stack traces
   - Test duration

## Pipeline Success Criteria

The pipeline will show as **succeeded** when:
- ✅ Build completes successfully
- ✅ All tests pass

The pipeline will show as **failed** when:
- ❌ Build fails (syntax errors, missing dependencies)
- ❌ Any test fails

## Next Steps

### Option A: Keep Current Configuration (Recommended)
- Pipeline correctly reports test failures
- You can monitor test health over time
- Fix failing tests as needed
- Accept that external dependencies can fail

### Option B: Make Pipeline Always Succeed
If you want the pipeline to succeed even when tests fail:

```yaml
# Change this line:
continueOnError: true  # to: continueOnError: false

# This will make the pipeline succeed but still show test failures in the Tests tab
```

### Option C: Run Only Specific Tests
Filter tests to run only stable ones:

```yaml
arguments: '--configuration $(buildConfiguration) --filter FullyQualifiedName~GoogleSearch'
```

## Additional Configuration Options

### Run Tests in Parallel
```yaml
arguments: '--configuration $(buildConfiguration) --parallel'
```

### Set Test Timeout
```yaml
arguments: '--configuration $(buildConfiguration) --blame-hang-timeout 5m'
```

### Generate Code Coverage
Add this after the test step:
```yaml
- task: PublishCodeCoverageResults@2
  inputs:
    codeCoverageTool: 'cobertura'
    summaryFileLocation: '$(Build.SourcesDirectory)/**/coverage.cobertura.xml'
```

## Support

If you continue to have issues:
1. Share the detailed logs from the "Run Reqnroll/Selenium tests" step
2. Share the test results from the "Tests" tab
3. Indicate which specific tests are failing

## Summary

**The pipeline is working correctly!** 

The exit code 1 error means tests are failing, not that the pipeline is broken. The pipeline successfully:
- ✅ Builds your project
- ✅ Runs your tests in headless Chrome
- ✅ Reports which tests pass/fail
- ✅ Publishes detailed results

Review the test results in Azure DevOps to see why specific tests are failing, then decide if you need to:
- Fix the tests
- Update test credentials
- Make tests more resilient
- Accept external dependency failures as normal
