using RibValidatorDz.Models;

namespace RibValidatorDz.Data;

/// <summary>
/// Algerian bank codes database
/// </summary>
public static class BankCodes
{
    /// <summary>
    /// Dictionary of Algerian bank codes with their information
    /// </summary>
    public static readonly Dictionary<string, BankInfo> Banks = new()
    {
        ["001"] = new BankInfo { Code = "001", ShortName = "BNA", FullName = "Banque Nationale d'Algérie" },
        ["002"] = new BankInfo { Code = "002", ShortName = "BEA", FullName = "Banque Extérieur d'Algérie" },
        ["003"] = new BankInfo { Code = "003", ShortName = "BADR", FullName = "Banque de l'Agriculture et du Développement Rural" },
        ["004"] = new BankInfo { Code = "004", ShortName = "CPA", FullName = "Crédit Populaire d'Algérie" },
        ["005"] = new BankInfo { Code = "005", ShortName = "BDL", FullName = "Banque de Développement Local" },
        ["006"] = new BankInfo { Code = "006", ShortName = "BARAKA", FullName = "El Baraka Bank" },
        ["007"] = new BankInfo { Code = "007", ShortName = "CCP", FullName = "Algérie Poste - Compte Courant Postal (RIP)" },
        ["008"] = new BankInfo { Code = "008", ShortName = "TRESOR", FullName = "Trésor Central - Banque d'Algérie" },
        ["010"] = new BankInfo { Code = "010", ShortName = "CNMA", FullName = "Caisse Nationale de Mutualité Agricole" },
        ["011"] = new BankInfo { Code = "011", ShortName = "CNEP", FullName = "Caisse Nationale d'Épargne et de Prévoyance" },
        ["012"] = new BankInfo { Code = "012", ShortName = "CITI", FullName = "City Bank" },
        ["014"] = new BankInfo { Code = "014", ShortName = "ABC", FullName = "Arab Banking Corporation Algeria" },
        ["020"] = new BankInfo { Code = "020", ShortName = "NATIXIS", FullName = "Natixis Banque" },
        ["021"] = new BankInfo { Code = "021", ShortName = "SGA", FullName = "Société Générale Algérie" },
        ["026"] = new BankInfo { Code = "026", ShortName = "ARAB BANK", FullName = "Arab Bank PLC" },
        ["027"] = new BankInfo { Code = "027", ShortName = "BNP", FullName = "BNP Paribas El Djazaïr" },
        ["029"] = new BankInfo { Code = "029", ShortName = "TRUST", FullName = "Trust Bank" },
        ["031"] = new BankInfo { Code = "031", ShortName = "HOUSING", FullName = "Housing Bank Algeria" },
        ["032"] = new BankInfo { Code = "032", ShortName = "AGB", FullName = "Algeria Gulf Bank" },
        ["035"] = new BankInfo { Code = "035", ShortName = "FRANSABANK", FullName = "Fransabank El Djazaïr" },
        ["036"] = new BankInfo { Code = "036", ShortName = "CALYON", FullName = "Calyon Algeria" },
        ["037"] = new BankInfo { Code = "037", ShortName = "HSBC", FullName = "HSBC Algeria" },
        ["038"] = new BankInfo { Code = "038", ShortName = "ALSALAM", FullName = "Al Salam Bank Algeria" },
        ["111"] = new BankInfo { Code = "111", ShortName = "BA", FullName = "Banque d'Algérie" }
    };

    /// <summary>
    /// Get bank information by code
    /// </summary>
    /// <param name="bankCode">3-digit bank code</param>
    /// <returns>Bank information or null if not found</returns>
    public static BankInfo? GetBankInfo(string bankCode)
    {
        return Banks.TryGetValue(bankCode, out var bankInfo) ? bankInfo : null;
    }

    /// <summary>
    /// Get all available bank codes
    /// </summary>
    /// <returns>Dictionary of all bank codes and their information</returns>
    public static Dictionary<string, BankInfo> GetAllBanks()
    {
        return new Dictionary<string, BankInfo>(Banks);
    }
}
