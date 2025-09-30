# Algerian RIB/RIP Validator

A lightweight .NET package to validate Algerian RIB (Relevé d'Identité Bancaire) or RIP (Relevé d'Identité Postal) code according to instruction 06-2004 using a pre-defined list of banks.

## Features
- Validates RIB/RIP format according to Algerian banking standards
- Checks control key using Modulo 97 algorithm
- Supports all major Algerian banks
- Supports both bank RIBs and postal RIPs (CCP)
- Validate single or multiple RIBs at once

## Supported Banks

The validator includes a comprehensive list of Algerian banks and financial institutions:

| Bank Code | Abbreviation | Full Name |
|-----------|--------------|-----------|
| 001 | BNA | Banque Nationale d'Algérie |
| 002 | BEA | Banque Extérieur d'Algérie |
| 003 | BADR | Banque de l'Agriculture et du Développement Rural |
| 004 | CPA | Crédit Populaire d'Algérie |
| 005 | BDL | Banque de Développement Local |
| 006 | BARAKA | El Baraka Bank |
| 007 | CCP | Algérie Poste - Compte Courant Postal (RIP) |
| 008 | TRESOR | Trésor Central - Banque d'Algérie |
| 010 | CNMA | Caisse Nationale de Mutualité Agricole |
| 011 | CNEP | Caisse Nationale d'Épargne et de Prévoyance |
| 012 | CITI | City Bank |
| 014 | ABC | Arab Banking Corporation Algeria |
| 020 | NATIXIS | Natixis Banque |
| 021 | SGA | Société Générale Algérie |
| 026 | ARAB BANK | Arab Bank PLC |
| 027 | BNP | BNP Paribas El Djazaïr |
| 029 | TRUST | Trust Bank |
| 031 | HOUSING | Housing Bank Algeria |
| 032 | AGB | Algeria Gulf Bank |
| 035 | FRANSABANK | Fransabank El Djazaïr |
| 036 | CALYON | Calyon Algeria |
| 037 | HSBC | HSBC Algeria |
| 038 | ALSALAM | Al Salam Bank Algeria |
| 111 | BA | Banque d'Algérie |

## Usage

### Validating a Single RIB

```csharp
using Dz.RibRipValidator;

var result = AlgerianRibValidator.ValidateRib("00100012345678901234");
if (result.IsValid)
{
    Console.WriteLine($"Valid RIB for {result.BankInfo.Name}");
}
else
{
    Console.WriteLine($"Invalid RIB: {result.Error}");
}
```

### Validating Multiple RIBs

```csharp
using Dz.RibRipValidator;
using System.Collections.Generic;

var ribs = new List<string>
{
    "00100012345678901234",
    "00200012345678901234",
    "00300012345678901234"
};

var results = AlgerianRibValidator.ValidateRibs(ribs);
foreach (var kvp in results)
{
    var rib = kvp.Key;
    var validationResult = kvp.Value;
    
    if (validationResult.IsValid)
    {
        Console.WriteLine($"Valid RIB {rib} for {validationResult.BankInfo.Name}");
    }
    else
    {
        Console.WriteLine($"Invalid RIB {rib}: {validationResult.Error}");
    }
}
```

The `ValidateRibs` method takes an `ICollection<string>` of RIBs and returns a `Dictionary<string, RibDetails>` where each key is the original RIB string and the value is its corresponding validation result.