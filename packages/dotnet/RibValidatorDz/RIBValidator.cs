using RibValidatorDz.Data;
using RibValidatorDz.Models;
using System.Text.RegularExpressions;

namespace RibValidatorDz;

/// <summary>
/// Main class for validating Algerian RIB (Relevé d'Identité Bancaire) and RIP (Relevé d'Identité Postal) numbers
/// </summary>
public static class RIBValidator
{
    /// <summary>
    /// Validates a RIB/RIP according to Algerian Bank Instruction 06-2004
    /// </summary>
    /// <param name="rib">The RIB/RIP to validate (20 digits)</param>
    /// <returns>Detailed validation result</returns>
    public static RIBResult ValidateRIB(string rib)
    {
        if (string.IsNullOrEmpty(rib))
        {
            return new RIBResult
            {
                IsValid = false,
                ErrorMessage = "RIB cannot be null or empty"
            };
        }

        // Remove any spaces
        var cleaned = rib.Replace(" ", "");

        // Check format: exactly 20 digits
        if (!Regex.IsMatch(cleaned, @"^\d{20}$"))
        {
            return new RIBResult
            {
                IsValid = false,
                ErrorMessage = "RIB must contain exactly 20 digits (without spaces)"
            };
        }

        // Extract components
        var bankCode = cleaned.Substring(0, 3);
        var agencyCode = cleaned.Substring(3, 5);
        var accountNumber = cleaned.Substring(8, 10);
        var controlKey = cleaned.Substring(18, 2);

        // Calculate control key using Modulo 97 algorithm
        var calculatedKey = ComputeControlKey(agencyCode, accountNumber);
        var isValid = calculatedKey == controlKey;

        // Get bank information
        var bankInfo = BankCodes.GetBankInfo(bankCode);

        var result = new RIBResult
        {
            IsValid = isValid,
            BankCode = bankCode,
            AgencyCode = agencyCode,
            AccountNumber = accountNumber,
            ControlKey = controlKey,
            CalculatedKey = calculatedKey,
            BankInfo = bankInfo ?? new BankInfo 
            { 
                Code = bankCode, 
                ShortName = "UNKNOWN", 
                FullName = $"Unknown bank (code: {bankCode})" 
            }
        };

        if (!isValid)
        {
            result.ErrorMessage = $"Invalid control key: expected {calculatedKey}";
        }

        return result;
    }

    /// <summary>
    /// Quick validation check
    /// </summary>
    /// <param name="rib">The RIB/RIP to validate</param>
    /// <returns>True if valid, false otherwise</returns>
    public static bool IsValidRIB(string rib)
    {
        var result = ValidateRIB(rib);
        return result.IsValid;
    }

    /// <summary>
    /// Get bank information by code
    /// </summary>
    /// <param name="bankCode">3-digit bank code</param>
    /// <returns>Bank information or null if not found</returns>
    public static BankInfo? GetBankInfo(string bankCode)
    {
        return BankCodes.GetBankInfo(bankCode);
    }

    /// <summary>
    /// Get all available bank codes
    /// </summary>
    /// <returns>Dictionary of all bank codes and their information</returns>
    public static Dictionary<string, BankInfo> GetAllBankCodes()
    {
        return BankCodes.GetAllBanks();
    }

    /// <summary>
    /// Computes the control key using Modulo 97 algorithm
    /// According to Algerian Bank Instruction 06-2004:
    /// 1. Concatenate AGENCY(5) + ACCOUNT(10) → R1
    /// 2. Calculate R2 = R1 × 100
    /// 3. R3 = R2 % 97
    /// 4. Control key = 97 - R3 (if key = 0 → key = 97)
    /// </summary>
    /// <param name="agency">5-digit agency code</param>
    /// <param name="account">10-digit account number</param>
    /// <returns>Calculated control key (2 digits, zero-padded)</returns>
    private static string ComputeControlKey(string agency, string account)
    {
        // Use BigInteger for safety with large values
        var r1 = BigInteger.Parse(agency + account); // 15 digits (5+10)
        var r2 = r1 * 100;
        var remainder = r2 % 97;
        var key = 97 - remainder;
        
        if (key == 0) 
            key = 97; // Convention: key in range [1..97]
            
        return key.ToString().PadLeft(2, '0');
    }
}
