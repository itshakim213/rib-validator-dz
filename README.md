# Algerian RIB & RIP Validator

A lightweight package to validate any Algerian RIB (Relevé d'Identité Bancaire) or RIP (Relevé d'Identité Postal) code
according to instruction 06-2004 using a pre-defined list of banks.

This repository contains several implementations of the same validator in different programming languages.

## Structure Overview

```
rib-validator-dz/
├── .gitignore
├── LICENSE
├── README.md
├── node-package/                             # Node.js/TypeScript implementation
│   ├── .gitignore                            # Node package specific git ignores
│   ├── LICENSE                               # License file for the node package
│   ├── package.json                          # NPM package configuration
│   ├── package-lock.json                     # NPM dependency lock file
│   ├── readme.md                             # Node package documentation
│   ├── tsconfig.json                          # TypeScript configuration
│   ├── src/                                  # Source code directory
│   │   ├── index.ts                          # Main validator implementation
│   │   └── test.ts                           # Test implementation
├── dotnet-package/                           # .NET/C# implementation
│   ├── .gitignore
│   ├── Dz.RibRipValidator.sln                # Visual Studio solution file
│   ├── LICENSE
│   ├── README.md
│   ├── Dz.RibRipValidator/                   # Main .NET package
│   │   ├── AlgerianRibValidator.cs
│   │   ├── RibDetails.cs
│   │   ├── Utilities.cs
│   │   ├── Dz.RibRipValidator.csproj         # Project & Package configuration
│   │   ├── LICENSE                           # .NET package main License file
│   │   ├── README.md                         # .NET package main readme file
│   └── Dz.RibRipValidator.Tests/             # .NET unit tests
│       ├── AlgerianRibValidatorListTests.cs
│       ├── AlgerianRibValidatorTests.cs
│       ├── UtilitiesTests.cs
│       ├── TestSuiteDocumentation.md         # Test documentation
│       ├── CoverageReport.md                 # Test coverage report
│       ├── Dz.RibRipValidator.Tests.csproj   # Test project configuration
```

## Validation Algorithm

The algorithm used complies with Instruction 06-2004 (Modulo 97 method):

1. **Concatenation** : BRANCH CODE(5) + ACCOUNT NUMBER(10) → R1
2. **Multiplication** : R2 = R1 × 100
3. **Modulo** : R3 = R2 % 97
4. **Check digit** : = 97 - R3 (if check digit = 0 → check digit = 97)

## Supported Algerian Banks

| Code | Abbreviation | Full Name                                         |
|------|--------------|---------------------------------------------------|
| 001  | BNA          | Banque Nationale d'Algérie                        |
| 002  | BEA          | Banque Extérieur d'Algérie                        |
| 003  | BADR         | Banque de l'Agriculture et du Développement Rural |
| 004  | CPA          | Crédit Populaire d'Algérie                        |
| 005  | BDL          | Banque de Développement Local                     |
| 006  | BARAKA       | El Baraka Bank                                    |
| 007  | CCP          | Algérie Poste - Compte Courant Postal (RIP)       |
| 008  | TRESOR       | Trésor Central - Banque d'Algérie                 |
| 010  | CNMA         | Caisse Nationale de Mutualité Agricole            |
| 011  | CNEP         | Caisse Nationale d'Épargne et de Prévoyance       |
| 012  | CITI         | City Bank                                         |
| 014  | ABC          | Arab Banking Corporation Algeria                  |
| 020  | NATIXIS      | Natixis Banque                                    |
| 021  | SGA          | Société Générale Algérie                          |
| 026  | ARAB BANK    | Arab Bank PLC                                     |
| 027  | BNP          | BNP Paribas El Djazaïr                            |
| 029  | TRUST        | Trust Bank                                        |
| 031  | HOUSING      | Housing Bank Algeria                              |
| 032  | AGB          | Algeria Gulf Bank                                 |
| 035  | FRANSABANK   | Fransabank El Djazaïr                             |
| 036  | CALYON       | Calyon Algeria                                    |
| 037  | HSBC         | HSBC Algeria                                      |
| 038  | ALSALAM      | Al Salam Bank Algeria                             |
| 111  | BA           | Banque d'Algérie                                  |

## Javascript/TypeScript for Node.js

The folder `node-package` contains the complete implementation of the validator in TypeScript/JavaScript for
Node.js environments.
Checkout the [README.md](./node-package/readme.md) file for more details.

Package is published, find it on [npm](https://www.npmjs.com/package/rib-validator-dz).

### Node Package Structure
- `src/index.ts`: Main export file containing the `validateRIB` function and bank codes
- `src/test.ts`: Test implementation
- `package.json`: NPM package configuration with build/test scripts
- `tsconfig.json`: TypeScript compilation configuration
- `dist/`: Compiled JavaScript output (generated after build)

### Features
- Validates RIB/RIP format according to Algerian banking standards
- Checks control key using Modulo 97 algorithm
- Supports all major Algerian banks
- Supports both bank RIBs and postal RIPs (CCP)
- Automatic whitespace removal
- TypeScript type definitions included

## C# for .NET

The folder `dotnet-package` contains a complete implementation of the validator in C# for .NET environments.
Checkout the [README.md](./dotnet-package/README.md) file for more details.

Package will be published soon.

### .NET Package Structure
- `Dz.RibRipValidator/`: Main library project
  - `AlgerianRibValidator.cs`: Core validation logic
  - `RibDetails.cs`: Structure for validation results
  - `Utilities.cs`: Supporting utility functions
  - `Dz.RibRipValidator.csproj`: Project configuration file
- `Dz.RibRipValidator.Tests/`: Unit tests project
  - Comprehensive test suite for validation logic
  - Test documentation and coverage reports

### Features
- Validates RIB/RIP format according to Algerian banking standards
- Checks control key using Modulo 97 algorithm
- Supports all major Algerian banks
- Supports both bank RIBs and postal RIPs (CCP)
- Validate single or multiple RIBs at once

## Upcoming implementations

- Python
- Dart
- Java
- Go
- Kotlin

## Acknowledgements

Special thanks to @joeloudjinz for contributing the .NET implementation, restructuring the repository, and adding a comprehensive test suite and documentation. Your work significantly improved the project quality and developer experience.

## Contributing

Contributions are welcome! If you’d like to add features, improve documentation, or bring new language implementations:
- Open an issue to discuss your proposal
- Submit a pull request targeting the `dev` branch
- For .NET changes, CI and CODEOWNERS will request reviews from maintainers (including @joeloudjinz)
