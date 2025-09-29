namespace RibValidatorDz.Models;

/// <summary>
/// Represents the result of RIB validation
/// </summary>
public class RIBResult
{
    /// <summary>
    /// Indicates whether the RIB is valid
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// The 3-digit bank code
    /// </summary>
    public string BankCode { get; set; } = string.Empty;

    /// <summary>
    /// The 5-digit agency code
    /// </summary>
    public string AgencyCode { get; set; } = string.Empty;

    /// <summary>
    /// The 10-digit account number
    /// </summary>
    public string AccountNumber { get; set; } = string.Empty;

    /// <summary>
    /// The 2-digit control key from the RIB
    /// </summary>
    public string ControlKey { get; set; } = string.Empty;

    /// <summary>
    /// The calculated control key using Modulo 97 algorithm
    /// </summary>
    public string CalculatedKey { get; set; } = string.Empty;

    /// <summary>
    /// Bank information for the given bank code
    /// </summary>
    public BankInfo? BankInfo { get; set; }

    /// <summary>
    /// Error message if validation failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}
