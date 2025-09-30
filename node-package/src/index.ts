/**
 * Dictionnaire des codes banque algériennes
 * Code banque → abréviation + nom complet
 */
export const bankCodes: Record<string, { short: string; name: string }> = {
  "001": { short: "BNA", name: "Banque Nationale d'Algérie" },
  "002": { short: "BEA", name: "Banque Extérieur d'Algérie" },
  "003": { short: "BADR", name: "Banque de l'Agriculture et du Développement Rural" },
  "004": { short: "CPA", name: "Crédit Populaire d'Algérie" },
  "005": { short: "BDL", name: "Banque de Développement Local" },
  "006": { short: "BARAKA", name: "El Baraka Bank" },
  "007": { short: "CCP", name: "Algérie Poste - Compte Courant Postal (RIP)" },
  "008": { short: "TRESOR", name: "Trésor Central - Banque d'Algérie" },
  "010": { short: "CNMA", name: "Caisse Nationale de Mutualité Agricole" },
  "011": { short: "CNEP", name: "Caisse Nationale d'Épargne et de Prévoyance" },
  "012": { short: "CITI", name: "City Bank" },
  "014": { short: "ABC", name: "Arab Banking Corporation Algeria" },
  "020": { short: "NATIXIS", name: "Natixis Banque" },
  "021": { short: "SGA", name: "Société Générale Algérie" },
  "026": { short: "ARAB BANK", name: "Arab Bank PLC" },
  "027": { short: "BNP", name: "BNP Paribas El Djazaïr" },
  "029": { short: "TRUST", name: "Trust Bank" },
  "031": { short: "HOUSING", name: "Housing Bank Algeria" },
  "032": { short: "AGB", name: "Algeria Gulf Bank" },
  "035": { short: "FRANSABANK", name: "Fransabank El Djazaïr" },
  "036": { short: "CALYON", name: "Calyon Algeria" },
  "037": { short: "HSBC", name: "HSBC Algeria" },
  "038": { short: "ALSALAM", name: "Al Salam Bank Algeria" },
  "111": { short: "BA", name: "Banque d'Algérie" }
};

/**
 * Structure d'un RIB algérien selon l'instruction 06-2004
 * Format : 20 chiffres répartis comme suit :
 * - 3 chiffres : code banque
 * - 5 chiffres : code agence  
 * - 10 chiffres : numéro de compte
 * - 2 chiffres : clé de contrôle (modulo 97)
 */
export interface RIBDetails {
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

/**
 * Calcule la clé de contrôle d'un RIB algérien selon l'algorithme Modulo 97
 * 
 * Algorithme conforme à l'instruction 06-2004 (méthode Modulo 97 - annexe) :
 * 1. Concaténer AGENCE(5) + COMPTE(10) => R1
 * 2. Calculer R2 = R1 * 100
 * 3. R3 = R2 % 97
 * 4. clé = 97 - R3 (si clé == 0 => clé = 97)
 * 
 * @param agency - Code agence (5 chiffres)
 * @param account - Numéro de compte (10 chiffres)
 * @returns Clé de contrôle calculée (2 chiffres, formatée avec zéros)
 */
function computeControlKeyFromAgencyAccount(agency: string, account: string): string {
  // Utiliser BigInt pour la sûreté sur les grandes valeurs
  const r1 = BigInt(agency + account); // 15 chiffres (5+10)
  const r2 = r1 * BigInt(100);
  const rem = r2 % BigInt(97);
  let key = BigInt(97) - rem;
  if (key === BigInt(0)) key = BigInt(97); // convention : clé dans [1..97]
  return key.toString().padStart(2, "0");
}

/**
 * Valide un RIB (Relevé d'Identité Bancaire) ou RIP (Relevé d'Identité Postal) algérien
 * 
 * Cette fonction vérifie la conformité d'un RIB/RIP selon l'instruction 06-2004 :
 * - Format : 20 chiffres exactement
 * - Structure : BANQUE(3) + AGENCE(5) + COMPTE(10) + CLÉ(2)
 * - Pour Algérie Poste (007) : AGENCE est toujours "99999"
 * - Validation de la clé de contrôle par l'algorithme Modulo 97
 * 
 * @param rib - Le RIB/RIP à valider (peut contenir des espaces qui seront supprimés)
 * @returns Objet contenant les détails du RIB/RIP et le résultat de la validation
 * 
 * @example
 * ```typescript
 * const result = validateRIB("00100234567890123456");
 * console.log(result.isValid); // true
 * console.log(result.bankCode); // "004"
 * ```
 */
export function validateRIB(rib: string): RIBDetails {
  const cleaned = (rib || "").replace(/\s+/g, "");

  if (!/^\d{20}$/.test(cleaned)) {
    return {
      bankCode: "",
      agencyCode: "",
      accountNumber: "",
      controlKey: "",
      calculatedKey: "",
      isValid: false,
      error: "Le RIB doit contenir exactement 20 chiffres (sans espaces).",
      bankInfo: { short: "ERREUR", name: "Format invalide" }
    };
  }

  const bankCode = cleaned.substring(0, 3);
  const agencyCode = cleaned.substring(3, 8);
  const accountNumber = cleaned.substring(8, 18);
  const controlKey = cleaned.substring(18, 20);

  // sécurité : vérifier longueur des morceaux
  if (agencyCode.length !== 5 || accountNumber.length !== 10) {
    return {
      bankCode,
      agencyCode,
      accountNumber,
      controlKey,
      calculatedKey: "",
      isValid: false,
      error: "Format interne invalide : agency (5) / account (10).",
      bankInfo: bankCodes[bankCode] || { short: "INCONNUE", name: `Banque inconnue (code: ${bankCode})` }
    };
  }

  const calculatedKey = computeControlKeyFromAgencyAccount(agencyCode, accountNumber);
  const isValid = calculatedKey === controlKey;
  const bankInfo = bankCodes[bankCode] || { short: "INCONNUE", name: `Banque inconnue (code: ${bankCode})` };

  const result: RIBDetails = {
    bankCode,
    agencyCode,
    accountNumber,
    controlKey,
    calculatedKey,
    isValid,
    bankInfo
  };

  if (!isValid) {
    result.error = `Clé invalide : attendue ${calculatedKey}`;
  }

  return result;
}