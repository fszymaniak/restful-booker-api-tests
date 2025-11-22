# restful-booker-api-tests

[![CI Pipeline](https://github.com/fszymaniak/restful-booker-api-tests/workflows/CI%20Pipeline/badge.svg)](https://github.com/fszymaniak/restful-booker-api-tests/actions)
[![.NET Version](https://img.shields.io/badge/.NET-3.1-512BD4)](https://dotnet.microsoft.com/)
[![NUnit](https://img.shields.io/badge/NUnit-3.14-22B14C)](https://nunit.org/)
[![RestSharp](https://img.shields.io/badge/RestSharp-110.2-009485)](https://restsharp.dev/)

## Description

Integration test project written in C# for training purpose which tests @mwinteringham API [restful-booker project](https://github.com/mwinteringham/restful-booker).

Restful-booker API Docs can be found [here](https://restful-booker.herokuapp.com/apidoc/index.html).

Trello board with current work can be found [here](https://trello.com/b/Eb5VwCVJ/restful-booker-api-test-project).

## Technology Stack

* [.NET Core 3.1](https://github.com/dotnet/core) - Runtime framework
* [NUnit 3.14](https://github.com/nunit/nunit) - Testing framework
* [NUnit3TestAdapter 4.5](https://github.com/nunit/nunit3-vs-adapter) - Test adapter
* [RestSharp 110.2](https://github.com/restsharp/RestSharp) - HTTP client library
* [Shouldly 4.2](https://github.com/shouldly/shouldly) - Assertion library
* [Coverlet](https://github.com/coverlet-coverage/coverlet) - Code coverage tool

## Features

✨ **Modern Architecture**
- Service-based design with dependency injection
- Separation of concerns (API Client, Request Factory, Test Base)
- Centralized configuration and logging

🧪 **Comprehensive Testing**
- Smoke tests for quick validation
- Regression test suite
- Integration test suite
- Test categorization support

📊 **Quality Assurance**
- Code coverage reporting
- Structured logging (INFO/WARN/ERROR/DEBUG)
- XML documentation for all public APIs
- Response validators for consistent assertions

🚀 **CI/CD Ready**
- GitHub Actions workflow
- Parallel test execution
- Automated dependency updates (Dependabot)
- Test result reporting

## Quick Start

### Prerequisites

- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1)
- IDE: Visual Studio 2019+, VS Code, or Rider

### Clone and Build

```bash
git clone https://github.com/fszymaniak/restful-booker-api-tests.git
cd restful-booker-api-tests
dotnet restore
dotnet build
```

### Run Tests

Run all tests:
```bash
dotnet test
```

Run specific test categories:
```bash
# Smoke tests only (quick validation)
dotnet test --filter "Category=Smoke"

# Regression tests
dotnet test --filter "Category=Regression"

# Integration tests
dotnet test --filter "Category=Integration"
```

Run with code coverage:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Configuration

Tests can be configured via:

**Environment Variables:**
```bash
export RESTFUL_BOOKER_URL="https://restful-booker.herokuapp.com"
export TEST_ENVIRONMENT="Development"
```

**appsettings.json:**
```json
{
  "RestfulBookerUrl": "https://restful-booker.herokuapp.com",
  "DefaultTimeout": 30000,
  "EnableRetry": false
}
```

## Project Structure

```
restful-booker-api-tests/
├── RestfulBooker.ApiTests/
│   ├── Api/                    # Test fixtures
│   ├── Attributes/             # Test category attributes
│   ├── Builders/               # Test data builders
│   ├── Constants/              # Endpoint and header constants
│   ├── Extensions/             # Assertion extensions
│   ├── Factories/              # Request factory
│   ├── Helpers/                # JSON and request helpers
│   ├── Models/                 # Request/Response models
│   ├── Services/               # API client, auth, logging
│   ├── TestData/               # Test data providers
│   ├── Validators/             # Response validators
│   └── BookingTestBase.cs     # Base test class
├── .github/
│   ├── workflows/              # CI/CD workflows
│   └── dependabot.yml          # Dependency automation
├── .editorconfig               # Code style configuration
└── RestfulBooker.sln           # Solution file
```

## CI/CD Pipeline

The project uses GitHub Actions for continuous integration:

1. **Smoke Tests** - Quick validation (5 min timeout)
2. **Build & Full Test Suite** - Complete test run with coverage
3. **Regression Tests** - Parallel execution
4. **Integration Tests** - API integration validation

All test results and coverage reports are uploaded as artifacts.

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## Architecture Highlights

### Service-Based Design
- **ApiConfiguration** - Environment-aware configuration
- **AuthenticationService** - Token management with caching
- **BookingApiClient** - HTTP client with logging
- **BookingRequestFactory** - Centralized request building

### Test Organization
- **Test Categories** - Smoke, Regression, Integration
- **Test Data Factory** - Fresh test instances for isolation
- **Validators** - Reusable assertion logic
- **Logging** - Structured logs for debugging

### Best Practices
- ✅ Async/await with ConfigureAwait(false)
- ✅ Dependency injection pattern
- ✅ XML documentation
- ✅ Code coverage tracking
- ✅ Test isolation and independence

## License

This project is open source and available for training purposes.

## Acknowledgments

- [@mwinteringham](https://github.com/mwinteringham) for the restful-booker API
- NUnit team for the excellent testing framework
- RestSharp team for the HTTP client library
