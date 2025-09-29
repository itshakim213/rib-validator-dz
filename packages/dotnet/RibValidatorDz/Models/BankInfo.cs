namespace RibValidatorDz.Models;

/// <summary>
/// Represents bank information
/// </summary>
public class BankInfo
{
    /// <summary>
    /// The 3-digit bank code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Short name of the bank
    /// </summary>
    public string ShortName { get; set; } = string.Empty;

    /// <summary>
    /// Full name of the bank
    /// </summary>
    public string FullName { get; set; } = string.Empty;
}
