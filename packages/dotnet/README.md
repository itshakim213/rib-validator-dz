# RIB Validator for .NET

A .NET library for validating Algerian RIB (Relevé d'Identité Bancaire) and RIP (Relevé d'Identité Postal) numbers according to the Algerian Bank Instruction 06-2004.

## Development Status

This package is currently in development.

## Features

- ✅ RIB/RIP validation using Modulo 97 algorithm
- ✅ Support for all Algerian bank codes
- ✅ Detailed validation results
- ✅ Bank information lookup
- 🚧 Comprehensive test suite
- 🚧 NuGet package publishing

## Quick Start

```csharp
using RibValidatorDz;

// Validate a RIB
var result = RIBValidator.ValidateRIB("00100234567890123456");

if (result.IsValid)
{
    Console.WriteLine($"Bank: {result.BankInfo.FullName}");
    Console.WriteLine($"Account: {result.AccountNumber}");
}
```

## 📦 Installation

```bash
# Once published to NuGet
dotnet add package RibValidatorDz
```

## 🔧 Development

### Prerequisites
- .NET 6.0 or later
- Visual Studio 2022 or VS Code

### Building
```bash
dotnet build
```

### Testing
```bash
dotnet test
```

## API Reference

### RIBValidator Class

#### ValidateRIB(string rib)
Validates a RIB/RIP and returns detailed information.

**Parameters:**
- `rib` (string): The RIB/RIP to validate (20 digits)

**Returns:**
- `RIBResult`: Detailed validation result

#### IsValidRIB(string rib)
Quick validation check.

**Parameters:**
- `rib` (string): The RIB/RIP to validate

**Returns:**
- `bool`: True if valid, false otherwise

#### GetBankInfo(string bankCode)
Get bank information by code.

**Parameters:**
- `bankCode` (string): 3-digit bank code

**Returns:**
- `BankInfo`: Bank information or null if not found

## Testing

The library includes comprehensive tests covering:
- Valid RIB validation
- Invalid RIB detection
- Edge cases (empty, null, wrong format)
- Bank code lookup
- Performance benchmarks

## 📄 License

MIT License - see the main project LICENSE file.

## 🤝 Contributing

See the main project [CONTRIBUTING.md](../../CONTRIBUTING.md) for contribution guidelines.

## 🔗 Related Projects

- [Main Project Repository](https://github.com/itshakim213/rib-validator-dz)
