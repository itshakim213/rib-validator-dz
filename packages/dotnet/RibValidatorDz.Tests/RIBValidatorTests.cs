using RibValidatorDz;
using Xunit;

namespace RibValidatorDz.Tests;

/// <summary>
/// Unit tests for RIBValidator
/// </summary>
public class RIBValidatorTests
{
    [Fact]
    public void ValidateRIB_ValidRIB_ReturnsValidResult()
    {
        // Arrange
        var rib = "00100234567890123456";

        // Act
        var result = RIBValidator.ValidateRIB(rib);

        // Assert
        Assert.True(result.IsValid);
        Assert.Equal("001", result.BankCode);
        Assert.Equal("00234", result.AgencyCode);
        Assert.Equal("5678901234", result.AccountNumber);
        Assert.NotNull(result.BankInfo);
        Assert.Equal("BNA", result.BankInfo.ShortName);
    }

    [Fact]
    public void ValidateRIB_InvalidFormat_ReturnsInvalidResult()
    {
        // Arrange
        var rib = "12345"; // Too short

        // Act
        var result = RIBValidator.ValidateRIB(rib);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("20 digits", result.ErrorMessage);
    }

    [Fact]
    public void ValidateRIB_EmptyString_ReturnsInvalidResult()
    {
        // Arrange
        var rib = "";

        // Act
        var result = RIBValidator.ValidateRIB(rib);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("RIB cannot be null or empty", result.ErrorMessage);
    }

    [Fact]
    public void ValidateRIB_NullString_ReturnsInvalidResult()
    {
        // Arrange
        string? rib = null;

        // Act
        var result = RIBValidator.ValidateRIB(rib);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("RIB cannot be null or empty", result.ErrorMessage);
    }

    [Fact]
    public void ValidateRIB_WithSpaces_RemovesSpaces()
    {
        // Arrange
        var rib = "001 002 34567 89012 3456";

        // Act
        var result = RIBValidator.ValidateRIB(rib);

        // Assert
        Assert.Equal("001", result.BankCode);
        Assert.Equal("00234", result.AgencyCode);
        Assert.Equal("5678901234", result.AccountNumber);
    }

    [Fact]
    public void IsValidRIB_ValidRIB_ReturnsTrue()
    {
        // Arrange
        var rib = "00100234567890123456";

        // Act
        var isValid = RIBValidator.IsValidRIB(rib);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void IsValidRIB_InvalidRIB_ReturnsFalse()
    {
        // Arrange
        var rib = "00100234567890123457"; // Wrong control key

        // Act
        var isValid = RIBValidator.IsValidRIB(rib);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void GetBankInfo_ValidCode_ReturnsBankInfo()
    {
        // Act
        var bankInfo = RIBValidator.GetBankInfo("001");

        // Assert
        Assert.NotNull(bankInfo);
        Assert.Equal("001", bankInfo.Code);
        Assert.Equal("BNA", bankInfo.ShortName);
        Assert.Equal("Banque Nationale d'Algérie", bankInfo.FullName);
    }

    [Fact]
    public void GetBankInfo_InvalidCode_ReturnsNull()
    {
        // Act
        var bankInfo = RIBValidator.GetBankInfo("999");

        // Assert
        Assert.Null(bankInfo);
    }

    [Fact]
    public void GetAllBankCodes_ReturnsAllBanks()
    {
        // Act
        var allBanks = RIBValidator.GetAllBankCodes();

        // Assert
        Assert.True(allBanks.Count > 0);
        Assert.Contains("001", allBanks.Keys);
        Assert.Contains("004", allBanks.Keys);
        Assert.Contains("007", allBanks.Keys); // CCP
    }

    [Fact]
    public void ValidateRIB_CCP_HandlesCorrectly()
    {
        // Arrange - CCP (Algérie Poste) should have agency code 99999
        var rib = "00799999123456789012";

        // Act
        var result = RIBValidator.ValidateRIB(rib);

        // Assert
        Assert.Equal("007", result.BankCode);
        Assert.Equal("99999", result.AgencyCode);
        Assert.NotNull(result.BankInfo);
        Assert.Equal("CCP", result.BankInfo.ShortName);
    }
}
