using System.Text.RegularExpressions;

namespace Dz.RibRipValidator;

/// <summary>
/// Provides methods to validate an Algerian RIB (Relevé d'Identité Bancaire).
/// </summary>
public static partial class AlgerianRibValidator
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex MatchMultipleWhitespaceRegex();

    [GeneratedRegex(@"^\d{20}$")]
    private static partial Regex MatchExactTwentyDigitsRegex();

    /// <summary>
    /// Validates an Algerian RIB (Relevé d'Identité Bancaire) or RIP (Relevé d'Identité Postal).
    /// </summary>
    /// <remarks>
    /// This function validates RIB/RIP compliance according to instruction 06-2004.
    /// </remarks>
    /// <param name="rib">The RIB/RIP to validate (can contain spaces which will be removed)</param>
    /// <returns>An object containing the RIB/RIP details and the validation result.</returns>
    public static RibDetails ValidateRib(string? rib)
    {
        var cleaned = string.IsNullOrEmpty(rib) ? string.Empty : MatchMultipleWhitespaceRegex().Replace(rib, "");
        if (!MatchExactTwentyDigitsRegex().IsMatch(cleaned))
        {
            return new RibDetails
            {
                BankCode = "",
                AgencyCode = "",
                AccountNumber = "",
                ControlKey = "",
                CalculatedKey = "",
                IsValid = false,
                Error = "The RIB should contain exactly 20 characters without spaces",
                BankInfo = Utilities.InvalidRibFormat
            };
        }

        var bankCode = cleaned[..3];
        if (!Utilities.BankCodes.TryGetValue(bankCode, out var bankInfo)) bankInfo = Utilities.UnknowBank(bankCode);

        var agencyCode = cleaned[3..8];
        var accountNumber = cleaned[8..18];
        var controlKey = cleaned[18..20];
        var calculatedKey = Utilities.ComputeControlKeyFromAgencyAccount(agencyCode, accountNumber);
        var isValid = calculatedKey == controlKey;

        return new RibDetails
        {
            BankCode = bankCode,
            AgencyCode = agencyCode,
            AccountNumber = accountNumber,
            ControlKey = controlKey,
            CalculatedKey = calculatedKey,
            IsValid = isValid,
            BankInfo = bankInfo,
            Error = isValid ? null : $"The key part is invalid - calculated key is {calculatedKey}"
        };
    }
    
    /// <summary>
    /// Validates a list of Algerian RIBs (Relevé d'Identité Bancaire) or RIPs (Relevé d'Identité Postal).
    /// </summary>
    /// <remarks>
    /// This function validates each RIB/RIP in the list according to instruction 06-2004.
    /// </remarks>
    /// <param name="ribs">The list of RIBs/RIPs to validate (each can contain spaces which will be removed)</param>
    /// <returns>A dictionary mapping each input RIB/RIP string to its corresponding validation result.</returns>
    public static Dictionary<string, RibDetails> ValidateRibs(ICollection<string> ribs)
    {
        var results = new Dictionary<string, RibDetails>();
        
        foreach (var rib in ribs)
        {
            results[rib] = ValidateRib(rib);
        }
        
        return results;
    }
}