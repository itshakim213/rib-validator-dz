namespace Dz.RibRipValidator.Tests;

public class UtilitiesTests
{
    [Fact]
    public void ComputeControlKeyFromAgencyAccount_WithValidInput_ShouldReturnCorrectKey()
    {
        // Arrange
        var agency = "00012";
        var account = "3456789012";
        // Expected key: (000123456789012 * 100) % 97 = rem, then 97 - rem
        // 123456789012 * 100 = 12345678901200
        // 12345678901200 % 97 = 54
        // 97 - 54 = 43
        var expectedKey = "43";
        
        // Act
        var result = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        
        // Assert
        Assert.Equal(expectedKey, result);
    }
    
    [Fact]
    public void ComputeControlKeyFromAgencyAccount_WithAllZeros_ShouldReturnValidKey()
    {
        // Arrange
        var agency = "00000";
        var account = "0000000000";
        // (0 * 100) % 97 = 0, so 97 - 0 = 97, but when remainder is 0, key is 97
        var expectedKey = "97";
        
        // Act
        var result = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        
        // Assert
        Assert.Equal(expectedKey, result);
    }
    
    [Fact]
    public void ComputeControlKeyFromAgencyAccount_WithLargeNumbers_ShouldReturnValidKey()
    {
        // Arrange
        var agency = "99999";
        var account = "9999999999";
        // For this calculation: (99999999999999 * 100) % 97
        // The result should be a 2-digit number
        
        // Act
        var result = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        
        // Assert
        Assert.Matches(@"^\d{2}$", result); // Should be 2 digits
        Assert.InRange(int.Parse(result), 1, 97); // Should be between 1 and 97
    }
    
    [Fact]
    public void BankCodes_DictionaryContainsExpectedBanks()
    {
        // Assert: Check that some known bank codes exist
        Assert.Contains("001", Utilities.BankCodes.Keys);
        Assert.Contains("002", Utilities.BankCodes.Keys);
        Assert.Contains("007", Utilities.BankCodes.Keys);  // CCP (Poste)
        Assert.Contains("027", Utilities.BankCodes.Keys);  // BNP
        
        // Check specific values
        Assert.Equal("BNA", Utilities.BankCodes["001"].Short);
        Assert.Equal("Banque Nationale d'Algérie", Utilities.BankCodes["001"].Name);
        Assert.Equal("CCP", Utilities.BankCodes["007"].Short);
        Assert.Equal("Algérie Poste - Compte Courant Postal (RIP)", Utilities.BankCodes["007"].Name);
    }
    
    [Fact]
    public void UnknowBank_ReturnsCorrectUnknownBankInfo()
    {
        // Act
        var result = Utilities.UnknowBank("999");
        
        // Assert
        Assert.Equal("Unknown", result.Short);
        Assert.Contains("999", result.Name);
        Assert.Contains("The Bank is unknown", result.Name);
    }
}