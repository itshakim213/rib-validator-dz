using System.Numerics;

namespace Dz.RibRipValidator;

internal static class Utilities
{
    public static readonly BankInfo InvalidRibFormat = new("Error", "RIB Format is invalid");
    public static BankInfo UnknowBank(string bankCode) => new("Unknown", $"The Bank is unknown (code: {bankCode})");

    /// <summary>
    /// Dictionary of Algerian bank codes
    /// Bank code → abbreviation + full name
    /// </summary>
    public static readonly IReadOnlyDictionary<string, BankInfo> BankCodes = new Dictionary<string, BankInfo>
    {
        ["001"] = new("BNA", "Banque Nationale d'Algérie"),
        ["002"] = new("BEA", "Banque Extérieur d'Algérie"),
        ["003"] = new("BADR", "Banque de l'Agriculture et du Développement Rural"),
        ["004"] = new("CPA", "Crédit Populaire d'Algérie"),
        ["005"] = new("BDL", "Banque de Développement Local"),
        ["006"] = new("BARAKA", "El Baraka Bank"),
        ["007"] = new("CCP", "Algérie Poste - Compte Courant Postal (RIP)"),
        ["008"] = new("TRESOR", "Trésor Central - Banque d'Algérie"),
        ["010"] = new("CNMA", "Caisse Nationale de Mutualité Agricole"),
        ["011"] = new("CNEP", "Caisse Nationale d'Épargne et de Prévoyance"),
        ["012"] = new("CITI", "City Bank"),
        ["014"] = new("ABC", "Arab Banking Corporation Algeria"),
        ["020"] = new("NATIXIS", "Natixis Banque"),
        ["021"] = new("SGA", "Société Générale Algérie"),
        ["026"] = new("ARAB BANK", "Arab Bank PLC"),
        ["027"] = new("BNP", "BNP Paribas El Djazaïr"),
        ["029"] = new("TRUST", "Trust Bank"),
        ["031"] = new("HOUSING", "Housing Bank Algeria"),
        ["032"] = new("AGB", "Algeria Gulf Bank"),
        ["035"] = new("FRANSABANK", "Fransabank El Djazaïr"),
        ["036"] = new("CALYON", "Calyon Algeria"),
        ["037"] = new("HSBC", "HSBC Algeria"),
        ["038"] = new("ALSALAM", "Al Salam Bank Algeria"),
        ["111"] = new("BA", "Banque d'Algérie")
    };

    /// <summary>
    /// Calculates the control key of an Algerian RIB using the Modulo 97 algorithm.
    /// </summary>
    /// <param name="agency">The 5-digit agency code</param>
    /// <param name="account">The 10-digit account number</param>
    /// <returns>The 2-digit control key calculated using the Modulo 97 algorithm</returns>
    public static string ComputeControlKeyFromAgencyAccount(string agency, string account)
    {
        var r1 = BigInteger.Parse(agency + account);
        var r2 = r1 * 100;
        var rem = r2 % 97;
        var key = 97 - rem;
        if (key == 0) key = 97;

        return key.ToString().PadLeft(2, '0');
    }
}