namespace Dz.RibRipValidator;

/// <summary>
/// Represents immutable information about a bank.
/// </summary>
/// <param name="Short">The bank's abbreviation (e.g., "BNA").</param>
/// <param name="Name">The bank's full name.</param>
public record BankInfo(string Short, string Name);

/// <summary>
/// Structure of an Algerian RIB according to instruction 06-2004.
/// Represents the detailed breakdown and validation status of an Algerian RIB.
/// </summary>
public record RibDetails
{
    /// <summary>
    /// Bank code (3 digits)
    /// </summary>
    public required string BankCode { get; init; }

    /// <summary>
    /// Agency code (5 digits)
    /// </summary>
    public required string AgencyCode { get; init; }

    /// <summary>
    /// Account number (10 digits)
    /// </summary>
    public required string AccountNumber { get; init; }

    /// <summary>
    /// Control key provided in the RIB (2 digits)
    /// </summary>
    public required string ControlKey { get; init; }

    /// <summary>
    /// Control key calculated by the algorithm (2 digits)
    /// </summary>
    public required string CalculatedKey { get; init; }

    /// <summary>
    /// Indicates if the RIB is valid
    /// </summary>
    public required bool IsValid { get; init; }

    /// <summary>
    /// Error message if the RIB is invalid. Null if valid.
    /// </summary>
    public string? Error { get; init; }

    /// <summary>
    /// Bank information (always present, "UNKNOWN" if code not recognized)
    /// </summary>
    public required BankInfo BankInfo { get; init; }
}