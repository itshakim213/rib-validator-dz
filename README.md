# Algerian RIB & RIP Validator

A lightweight package to validate any Algerian RIB (Relevé d'Identité Bancaire) or RIP (Relevé d'Identité Postal) code
according to instruction 06-2004 using a pre-defined list of banks.

This repository contains several implementations of the same validator in different programming languages.

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

## Javascript/TypeScript for Nodes

The folder `node-package` contains the complete implementation of the validator in TypeScript/JavaScript for
Node.js environments.
Checkout the [README.md](./node-package/readme.md) file for more details.

Package is published, find it on [npm](https://www.npmjs.com/package/rib-validator-dz).

## C# for .NET

The folder `dotnet-package` contains a complete implementation of the validator in C# for .NET environments.
Checkout the [README.md](./dotnet-package/README.md) file for more details.

Package will be published soon.

### Upcoming implementations

- Python
- Dart
- Java
- Go
- Kotlin