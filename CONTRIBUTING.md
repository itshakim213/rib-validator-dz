# Contributing to rib-validator-dz

Thank you for your interest in contributing to the rib-validator-dz project! This document outlines how to contribute to the project, especially for adding new language implementations.

## Project Goals

This project aims to provide RIB/RIP validation for Algerian bank accounts across multiple programming languages, starting with TypeScript/JavaScript and expanding to other ecosystems.

## Current Status

- **TypeScript/JavaScript**: Complete and published to npm
- **C#/.NET**: In development

## Contributing a .NET Package

### Prerequisites

- .NET 6.0 or later
- Visual Studio or VS Code with C# extension
- Git

### Technical Requirements

#### Target Framework
- **Primary**: .NET 6.0+ (for broad compatibility)
- **Package Manager**: NuGet
- **Package ID**: `RibValidatorDz` (or similar)

#### API Design Requirements

The .NET API should closely mirror the TypeScript implementation:

```csharp
// Main validation function
public static RIBResult ValidateRIB(string rib)

// Quick validation
public static bool IsValidRIB(string rib)

// Bank information lookup
public static BankInfo GetBankInfo(string bankCode)

// All bank codes
public static Dictionary<string, BankInfo> GetAllBankCodes()
```

#### Data Models

```csharp
public class RIBResult
{
    public bool IsValid { get; set; }
    public string BankCode { get; set; }
    public string AgencyCode { get; set; }
    public string AccountNumber { get; set; }
    public string ControlKey { get; set; }
    public string CalculatedKey { get; set; }
    public BankInfo BankInfo { get; set; }
    public string ErrorMessage { get; set; }
}

public class BankInfo
{
    public string Code { get; set; }
    public string ShortName { get; set; }
    public string FullName { get; set; }
}
```

### Validation Algorithm

The validation must implement the **Modulo 97** algorithm as specified in the Algerian Bank Instruction 06-2004:

1. **Concatenation**: AGENCY(5) + ACCOUNT(10) → R1
2. **Multiplication**: R2 = R1 × 100
3. **Modulo**: R3 = R2 % 97
4. **Control Key**: key = 97 - R3 (if key = 0 → key = 97)

### Bank Codes

Include all 40+ Algerian bank codes from the TypeScript implementation. The list should be identical to ensure consistency across language implementations.

### Project Structure

```
packages/dotnet/
├── RibValidatorDz/
│   ├── RibValidatorDz.csproj
│   ├── RIBValidator.cs
│   ├── Models/
│   │   ├── RIBResult.cs
│   │   └── BankInfo.cs
│   └── Data/
│       └── BankCodes.cs
├── RibValidatorDz.Tests/
│   ├── RibValidatorDz.Tests.csproj
│   └── RIBValidatorTests.cs
└── README.md
```

### Testing Requirements

- Unit tests for all public methods
- Test cases for valid and invalid RIBs
- Edge cases (empty strings, null values, wrong formats)
- Performance tests for large datasets
- Test coverage should be > 90%

### Documentation Requirements

- XML documentation for all public APIs
- README.md with usage examples
- Code comments explaining the Modulo 97 algorithm
- Changelog for version tracking

### Code Style

- Follow C# naming conventions
- Use meaningful variable names
- Add XML documentation comments
- Handle exceptions gracefully
- Use nullable reference types where appropriate

## Getting Started

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/dotnet-package`
3. **Set up the .NET project structure** in `packages/dotnet/`
4. **Implement the validation logic** based on the TypeScript version
5. **Add comprehensive tests**
6. **Update documentation**
7. **Submit a pull request**

## Pull Request Guidelines

### Before Submitting

- [ ] Code compiles without warnings
- [ ] All tests pass
- [ ] Code follows C# conventions
- [ ] Documentation is complete
- [ ] README includes usage examples
- [ ] Changelog is updated

### PR Description Template

```markdown
## Description
Brief description of the .NET package implementation

## Changes
- [ ] Core validation logic
- [ ] Bank codes data
- [ ] Unit tests
- [ ] Documentation
- [ ] NuGet package configuration

## Testing
- [ ] All tests pass
- [ ] Manual testing completed
- [ ] Performance testing done

## Checklist
- [ ] Code follows project conventions
- [ ] Documentation updated
- [ ] No breaking changes to existing
```

## Success Criteria

A successful .NET contribution should:

1. **Functionality**: Pass all validation tests from the TypeScript version
2. **Usability**: Simple, intuitive
3. **Documentation**: Clear examples and comprehensive docs
4. **Maintainability**: Clean, well-commented code

## 📞 Questions?

If you have questions about the contribution process or technical requirements, please:

1. Check existing issues and discussions
2. Create a new issue with the `question` label
3. Contact the maintainers

## 🏆 Recognition

Contributors will be:
- Listed in the project README
- Mentioned in release notes
- Credited in the NuGet package metadata

Thank you for contributing to the rib-validator-dz project!
