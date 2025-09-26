# rib-validator-dz

[![npm version](https://badge.fury.io/js/rib-validator-dz.svg)](https://badge.fury.io/js/rib-validator-dz)
[![TypeScript](https://img.shields.io/badge/TypeScript-Ready-blue.svg)](https://www.typescriptlang.org/)

Validateur de RIB (Relevé d'Identité Bancaire) et RIP (Relevé d'Identité Postal) algérien conforme à l'[Instruction 06-2004](https://www.bank-of-algeria.dz/stoodroa/2021/03/instruction062004.pdf) de la Banque d'Algérie.

## 📋 À propos

Ce package TypeScript/JavaScript permet de valider les RIB et RIP algériens en utilisant l'algorithme de contrôle Modulo 97. Il vérifie la conformité du format et calcule la clé de contrôle selon les spécifications officielles.

### Structure d'un RIB/RIP algérien

Un RIB/RIP algérien est composé de **20 chiffres** répartis comme suit :
- **3 chiffres** : Code banque
- **5 chiffres** : Code agence (toujours "99999" pour Algérie Poste)
- **10 chiffres** : Numéro de compte
- **2 chiffres** : Clé de contrôle (Modulo 97)

**Note spéciale pour Algérie Poste (CCP)** : Le code agence est toujours "99999" et le document s'appelle RIP (Relevé d'Identité Postal).

## 📦 Installation

```bash
npm install rib-validator-dz
```

## 🚀 Utilisation

### Importation

```typescript
import { validateRIB, RIBDetails, bankCodes } from 'rib-validator-dz';
```

### Validation simple

```typescript
// Exemple avec un RIB générique (remplacez par votre RIB)
const result = validateRIB("VOTRE_RIB_ICI");

console.log(result.isValid); // true
console.log(result.bankCode); // "004"
console.log(result.agencyCode); // "12345"
console.log(result.accountNumber); // "0123456789"
console.log(result.controlKey); // "12"
console.log(result.calculatedKey); // "12"
console.log(result.bankInfo); // { short: "CPA", name: "Crédit Populaire d'Algérie" }
```

### Gestion des erreurs

```typescript
const result = validateRIB("00100234567890123456"); // RIB de test invalide

if (!result.isValid) {
  console.log("RIB invalide:", result.error);
  // "RIB invalide: Clé invalide : attendue 72"
}
```

### Nettoyage automatique

Le validateur supprime automatiquement les espaces :

```typescript
// Exemple avec espaces (remplacez par votre RIB) "001 12345 1234567890 12"
const result = validateRIB("VOTRE_RIB_AVEC_ESPACES");
// Fonctionne parfaitement, les espaces sont ignorés
```

### Accès au dictionnaire des banques

```typescript
// Accès direct au dictionnaire
console.log(bankCodes["004"]); // { short: "CPA", name: "Crédit Populaire d'Algérie" }

// Ou via le résultat de validation
const result = validateRIB("VOTRE_RIB_ICI");
console.log(`Banque: ${result.bankInfo.short} - ${result.bankInfo.name}`);
// "Banque: CPA - Crédit Populaire d'Algérie"

// Pour une banque non reconnue
const unknownResult = validateRIB("99901234567890123456");
console.log(unknownResult.bankInfo);
// { short: "INCONNUE", name: "Banque inconnue (code: 999)" }
```

## 📖 API

### `validateRIB(rib: string): RIBDetails`

Valide un RIB ou RIP algérien et retourne les détails de validation.

**Paramètres :**
- `rib` (string) : Le RIB/RIP à valider (20 chiffres, espaces optionnels)

**Retour :**
- `RIBDetails` : Objet contenant les détails du RIB/RIP et le résultat de la validation

### Interface `RIBDetails`

```typescript
interface RIBDetails {
  /** Code banque (3 chiffres) */
  bankCode: string;
  /** Code agence (5 chiffres) */
  agencyCode: string;
  /** Numéro de compte (10 chiffres) */
  accountNumber: string;
  /** Clé de contrôle fournie dans le RIB (2 chiffres) */
  controlKey: string;
  /** Clé de contrôle calculée par l'algorithme (2 chiffres) */
  calculatedKey: string;
  /** Indique si le RIB est valide */
  isValid: boolean;
  /** Message d'erreur si le RIB est invalide */
  error?: string | undefined;
  /** Informations sur la banque (toujours présent, "INCONNUE" si code non reconnu) */
  bankInfo: { short: string; name: string };
}
```

### Dictionnaire `bankCodes`

```typescript
const bankCodes: Record<string, { short: string; name: string }>
```

Dictionnaire contenant les codes banque algériennes avec leurs abréviations et noms complets.

## 🏦 Codes banque algériennes

| Code | Abréviation | Nom complet |
|------|-------------|-------------|
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

## 🔧 Algorithme de validation

L'algorithme utilisé est conforme à l'Instruction 06-2004 (méthode Modulo 97) :

1. **Concaténation** : AGENCE(5) + COMPTE(10) → R1
2. **Multiplication** : R2 = R1 × 100
3. **Modulo** : R3 = R2 % 97
4. **Clé de contrôle** : clé = 97 - R3 (si clé = 0 → clé = 97)

## 🧪 Exemples de test

```typescript
// RIB invalide (exemple de test)
validateRIB("00100234567890123456"); // ❌ Clé incorrecte
```

## 🛠️ Développement

### Prérequis
- Node.js
- TypeScript

### Installation des dépendances

```bash
npm install
```

### Compilation

```bash
npm run build
```

### Tests

```bash
npm run test
```

## 🚀 Perspectives

### Fonctionnalités prévues

- **Validator SWIFT algérien** : Support pour les codes SWIFT algériens
- **Logos des banques** : Ajout d'un champ `bankLogo` avec URL vers les logos officiels
- **Fonction de lookup** : Export de `getBankInfo(code)` pour recherche rapide
- **Compatibilité ESM/CommonJS** : Support complet pour React et Node.js
- **Tests unitaires** : Suite de tests complète avec Jest ou Vitest
- **Génération PDF** : Fonctionnalité pour télécharger les relevés d'identité en PDF

### Roadmap

```typescript
// Exemples des futures fonctionnalités

// Validator SWIFT
validateSWIFT("CPALDZAL213");

// Lookup rapide
const bankInfo = getBankInfo("004");

// Génération PDF
generateRIBReport(ribDetails);
```

## 📄 Licence

MIT © [HxK1m](https://github.com/itshakim213) | [LinkedIn](https://www.linkedin.com/in/sid-ali-ikhlef99/)

## 🔗 Références

- [Instruction 06-2004 - Banque d'Algérie](https://www.bank-of-algeria.dz/stoodroa/2021/03/instruction062004.pdf)
