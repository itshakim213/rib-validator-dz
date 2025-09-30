namespace Dz.RibRipValidator.Tests;

public class AlgerianRibValidatorTests
{
    [Fact]
    public void ValidateRib_WithValidRibs_ShouldReturnValidResult()
    {
        // Test valid RIBs with correct control keys for different banks
        var agency = "00012";
        var account = "3456789012";
        var controlKey = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        
        // BNA example with correct control key
        var rib = "001" + agency + account + controlKey;
        
        // Act
        var result = AlgerianRibValidator.ValidateRib(rib);
        
        // Assert
        Assert.True(result.IsValid);
        Assert.Equal("001", result.BankCode);
        Assert.Equal(agency, result.AgencyCode);
        Assert.Equal(account, result.AccountNumber);
        Assert.Equal(controlKey, result.ControlKey);
        Assert.Equal(controlKey, result.CalculatedKey);
        Assert.NotNull(result.BankInfo);
        Assert.Equal("BNA", result.BankInfo.Short);
        Assert.Null(result.Error);
    }
    
    [Fact]
    public void ValidateRib_WithValidRibNameAndProperControlKey_ShouldReturnValid()
    {
        // Arrange: Create a valid RIB with a proper control key
        // Using agency "00012" and account "3456789012"
        var agency = "00012";
        var account = "3456789012";
        var controlKey = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        var rib = "001" + agency + account + controlKey; // BNA bank code
        
        // Act
        var result = AlgerianRibValidator.ValidateRib(rib);
        
        // Assert
        Assert.True(result.IsValid);
        Assert.Equal("001", result.BankCode);
        Assert.Equal(agency, result.AgencyCode);
        Assert.Equal(account, result.AccountNumber);
        Assert.Equal(controlKey, result.ControlKey);
        Assert.Equal(controlKey, result.CalculatedKey);
        Assert.Equal("BNA", result.BankInfo.Short);
        Assert.Equal("Banque Nationale d'Algérie", result.BankInfo.Name);
        Assert.Null(result.Error);
    }
    
    [Fact]
    public void ValidateRib_WithInvalidControlKey_ShouldReturnInvalidResult()
    {
        // Arrange: Create a RIB with an incorrect control key
        var rib = "00100012345678901235"; // Last digit is changed to make it invalid
        
        // Act
        var result = AlgerianRibValidator.ValidateRib(rib);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("001", result.BankCode);
        Assert.Equal("00012", result.AgencyCode);
        Assert.Equal("3456789012", result.AccountNumber);
        Assert.Equal("35", result.ControlKey); // The key from the input
        Assert.NotEqual("35", result.CalculatedKey); // The calculated key should be different
        Assert.Equal("BNA", result.BankInfo.Short);
        Assert.Equal("Banque Nationale d'Algérie", result.BankInfo.Name);
        Assert.NotNull(result.Error);
        Assert.Contains("The key part is invalid", result.Error);
    }
    
    [Fact]
    public void ValidateRib_WithNullInput_ShouldReturnInvalidResult()
    {
        // Act
        var result = AlgerianRibValidator.ValidateRib(null);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("", result.BankCode);
        Assert.Equal("", result.AgencyCode);
        Assert.Equal("", result.AccountNumber);
        Assert.Equal("", result.ControlKey);
        Assert.Equal("", result.CalculatedKey);
        Assert.Equal(Utilities.InvalidRibFormat, result.BankInfo);
        Assert.Equal("The RIB should contain exactly 20 characters without spaces", result.Error);
    }
    
    [Fact]
    public void ValidateRib_WithEmptyString_ShouldReturnInvalidResult()
    {
        // Act
        var result = AlgerianRibValidator.ValidateRib("");
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("", result.BankCode);
        Assert.Equal("", result.AgencyCode);
        Assert.Equal("", result.AccountNumber);
        Assert.Equal("", result.ControlKey);
        Assert.Equal("", result.CalculatedKey);
        Assert.Equal(Utilities.InvalidRibFormat, result.BankInfo);
        Assert.Equal("The RIB should contain exactly 20 characters without spaces", result.Error);
    }
    
    [Fact]
    public void ValidateRib_WithWhitespace_RemovalShouldWork()
    {
        // Arrange: Valid RIB with spaces
        var agency = "00012";
        var account = "3456789012";
        var controlKey = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        var ribWithSpaces = "001 00 012 3456 7890 12" + controlKey;
        var expectedRib = "001" + agency + account + controlKey;
        
        // Act
        var result = AlgerianRibValidator.ValidateRib(ribWithSpaces);
        
        // Assert
        Assert.True(result.IsValid);
        Assert.Equal("001", result.BankCode);
        Assert.Equal(agency, result.AgencyCode);
        Assert.Equal(account, result.AccountNumber);
        Assert.Equal(controlKey, result.ControlKey);
        Assert.Equal(controlKey, result.CalculatedKey);
        Assert.Equal("BNA", result.BankInfo.Short);
        Assert.Equal("Banque Nationale d'Algérie", result.BankInfo.Name);
        Assert.Null(result.Error);
    }
    
    [Fact]
    public void ValidateRib_WithLessThan20Digits_ShouldReturnInvalidResult()
    {
        // Arrange
        var rib = "0010001234567890123"; // Only 19 characters
        
        // Act
        var result = AlgerianRibValidator.ValidateRib(rib);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("", result.BankCode);
        Assert.Equal("", result.AgencyCode);
        Assert.Equal("", result.AccountNumber);
        Assert.Equal("", result.ControlKey);
        Assert.Equal("", result.CalculatedKey);
        Assert.Equal(Utilities.InvalidRibFormat, result.BankInfo);
        Assert.Equal("The RIB should contain exactly 20 characters without spaces", result.Error);
    }
    
    [Fact]
    public void ValidateRib_WithMoreThan20Digits_ShouldReturnInvalidResult()
    {
        // Arrange
        var rib = "001000123456789012345"; // 21 characters
        
        // Act
        var result = AlgerianRibValidator.ValidateRib(rib);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("", result.BankCode);
        Assert.Equal("", result.AgencyCode);
        Assert.Equal("", result.AccountNumber);
        Assert.Equal("", result.ControlKey);
        Assert.Equal("", result.CalculatedKey);
        Assert.Equal(Utilities.InvalidRibFormat, result.BankInfo);
        Assert.Equal("The RIB should contain exactly 20 characters without spaces", result.Error);
    }
    
    [Fact]
    public void ValidateRib_WithNonDigitCharacters_ShouldReturnInvalidResult()
    {
        // Arrange
        var rib = "0010001234567890123A"; // Contains a letter
        
        // Act
        var result = AlgerianRibValidator.ValidateRib(rib);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("", result.BankCode);
        Assert.Equal("", result.AgencyCode);
        Assert.Equal("", result.AccountNumber);
        Assert.Equal("", result.ControlKey);
        Assert.Equal("", result.CalculatedKey);
        Assert.Equal(Utilities.InvalidRibFormat, result.BankInfo);
        Assert.Equal("The RIB should contain exactly 20 characters without spaces", result.Error);
    }
    
    [Fact]
    public void ValidateRib_WithUnknownBankCode_ShouldReturnUnknownBankInfo()
    {
        // Arrange: Use an unknown bank code
        var agency = "00012";
        var account = "3456789012";
        var controlKey = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        var rib = "999" + agency + account + controlKey; // Unknown bank code
        
        // Act
        var result = AlgerianRibValidator.ValidateRib(rib);
        
        // Assert
        Assert.True(result.IsValid); // Should be valid structurally but unknown bank
        Assert.Equal("999", result.BankCode);
        Assert.Equal(agency, result.AgencyCode);
        Assert.Equal(account, result.AccountNumber);
        Assert.Equal(controlKey, result.ControlKey);
        Assert.Equal(controlKey, result.CalculatedKey);
        Assert.Equal("Unknown", result.BankInfo.Short);
        Assert.Contains("The Bank is unknown (code: 999)", result.BankInfo.Name);
        Assert.Null(result.Error);
    }
    
    [Theory]
    [InlineData("001", "BNA", "Banque Nationale d'Algérie")]
    [InlineData("002", "BEA", "Banque Extérieur d'Algérie")]
    [InlineData("007", "CCP", "Algérie Poste - Compte Courant Postal (RIP)")]
    [InlineData("027", "BNP", "BNP Paribas El Djazaïr")]
    public void ValidateRib_WithKnownBankCodes_ShouldReturnCorrectBankInfo(string bankCode, string expectedShort, string expectedName)
    {
        // Arrange
        var agency = "00012";
        var account = "3456789012";
        var controlKey = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        var rib = bankCode + agency + account + controlKey;
        
        // Act
        var result = AlgerianRibValidator.ValidateRib(rib);
        
        // Assert
        Assert.True(result.IsValid);
        Assert.Equal(bankCode, result.BankCode);
        Assert.Equal(expectedShort, result.BankInfo.Short);
        Assert.Equal(expectedName, result.BankInfo.Name);
    }
    
    [Fact]
    public void ValidateRib_WithValidRip_ShouldWork()
    {
        // Arrange: Valid RIP (Poste) with correct control key
        var agency = "00012";
        var account = "3456789012";
        var controlKey = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        var rib = "007" + agency + account + controlKey; // CCP (Poste) bank code
        
        // Act
        var result = AlgerianRibValidator.ValidateRib(rib);
        
        // Assert
        Assert.True(result.IsValid);
        Assert.Equal("007", result.BankCode);
        Assert.Equal(agency, result.AgencyCode);
        Assert.Equal(account, result.AccountNumber);
        Assert.Equal(controlKey, result.ControlKey);
        Assert.Equal(controlKey, result.CalculatedKey);
        Assert.Equal("CCP", result.BankInfo.Short);
        Assert.Equal("Algérie Poste - Compte Courant Postal (RIP)", result.BankInfo.Name);
        Assert.Null(result.Error);
    }
}