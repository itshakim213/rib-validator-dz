# RibCli Example

A minimal console app to validate Algerian RIB/RIP using the Dz.RibRipValidator library. This example is for contributors only and is not part of the solution or CI.

## Prerequisites
- .NET 8 SDK

## Setup
From the repository root:
```bash
cd dotnet-package/examples/RibCli
dotnet restore
```

If you created this project manually and need a reference:
```bash
dotnet add reference ../../Dz.RibRipValidator/Dz.RibRipValidator.csproj
```

## Run
Pass a 20-digit RIB (spaces allowed) as an argument:
```bash
dotnet run -- 00100234567890123472
```

Sample output:

isValid: True
bankCode: 001
agencyCode: 00234
accountNumber: 5678901234
controlKey: 72
calculatedKey: 72
bank: BNA - Banque Nationale d'Algérie
error:



## Notes
- This example is excluded from the solution and CI on purpose.
- Do not publish or package this project.
- Use it locally to test changes to the validator.