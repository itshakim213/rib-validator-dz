namespace Dz.RibRipValidator.Tests;

public class AlgerianRibValidatorListTests
{
    [Fact]
    public void ValidateRibs_WithValidRibsList_ShouldReturnCorrectResults()
    {
        // Arrange
        var agency = "00012";
        var account = "3456789012";
        var controlKey = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        var validRib1 = "001" + agency + account + controlKey; // BNA
        var validRib2 = "002" + agency + account + controlKey; // BEA

        var ribs = new List<string> { validRib1, validRib2 };

        // Act
        var results = AlgerianRibValidator.ValidateRibs(ribs);

        // Assert
        Assert.Equal(2, results.Count);
        Assert.Contains(validRib1, results.Keys);
        Assert.Contains(validRib2, results.Keys);

        var result1 = results[validRib1];
        Assert.True(result1.IsValid);
        Assert.Equal("001", result1.BankCode);
        Assert.Equal("BNA", result1.BankInfo.Short);

        var result2 = results[validRib2];
        Assert.True(result2.IsValid);
        Assert.Equal("002", result2.BankCode);
        Assert.Equal("BEA", result2.BankInfo.Short);
    }

    [Fact]
    public void ValidateRibs_WithMixedValidInvalidRibs_ShouldReturnCorrectResults()
    {
        // Arrange
        var agency = "00012";
        var account = "3456789012";
        var controlKey = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        var validRib = "001" + agency + account + controlKey; // Valid RIB
        var invalidRib = "00100012345678901235"; // Invalid control key
        var malformedRib = "12345678901234567890"; // 20 digits but invalid control key

        var ribs = new List<string> { validRib, invalidRib, malformedRib };

        // Act
        var results = AlgerianRibValidator.ValidateRibs(ribs);

        // Assert
        Assert.Equal(3, results.Count);
        Assert.Contains(validRib, results.Keys);
        Assert.Contains(invalidRib, results.Keys);
        Assert.Contains(malformedRib, results.Keys);

        // Valid RIB
        var validResult = results[validRib];
        Assert.True(validResult.IsValid);

        // Invalid control key RIB
        var invalidResult = results[invalidRib];
        Assert.False(invalidResult.IsValid);
        Assert.Contains("The key part is invalid", invalidResult.Error);

        // Malformed RIB with invalid control key
        var malformedResult = results[malformedRib];
        Assert.False(malformedResult.IsValid);
        Assert.Contains("The key part is invalid", malformedResult.Error);
    }

    [Fact]
    public void ValidateRibs_WithEmptyList_ShouldReturnEmptyDictionary()
    {
        // Arrange
        var ribs = new List<string>();

        // Act
        var results = AlgerianRibValidator.ValidateRibs(ribs);

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void ValidateRibs_WithEmptyCollection_ShouldReturnEmptyDictionary()
    {
        // Arrange
        var ribs = new List<string>();

        // Act
        var results = AlgerianRibValidator.ValidateRibs(ribs);

        // Assert
        Assert.Empty(results);
    }


    [Fact]
    public void ValidateRibs_WithDuplicateRibs_ShouldReturnCorrectResults()
    {
        // Arrange
        var agency = "00012";
        var account = "3456789012";
        var controlKey = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        var validRib = "001" + agency + account + controlKey; // Valid RIB

        var ribs = new List<string> { validRib, validRib, validRib }; // Three identical RIBs

        // Act
        var results = AlgerianRibValidator.ValidateRibs(ribs);

        // Assert
        Assert.Single(results); // Dictionary will have one entry since keys are unique
        Assert.Contains(validRib, results.Keys);

        var result = results[validRib];
        Assert.True(result.IsValid);
        Assert.Equal("001", result.BankCode);
        Assert.Equal("BNA", result.BankInfo.Short);
    }

    [Fact]
    public void ValidateRibs_WithWhitespaceContainingRibs_ShouldHandleCorrectly()
    {
        // Arrange
        var agency = "00012";
        var account = "3456789012";
        var controlKey = Utilities.ComputeControlKeyFromAgencyAccount(agency, account);
        var ribWithoutSpaces = "001" + agency + account + controlKey; // Valid RIB without spaces
        var ribWithSpaces = "001 00 012 3456 7890 12" + controlKey; // Same RIB with spaces

        var ribs = new List<string> { ribWithoutSpaces, ribWithSpaces };

        // Act
        var results = AlgerianRibValidator.ValidateRibs(ribs);

        // Assert
        Assert.Equal(2, results.Count);
        Assert.Contains(ribWithoutSpaces, results.Keys);
        Assert.Contains(ribWithSpaces, results.Keys);

        var resultWithoutSpaces = results[ribWithoutSpaces];
        var resultWithSpaces = results[ribWithSpaces];

        // Both should have the same validation result (valid)
        Assert.Equal(resultWithoutSpaces.IsValid, resultWithSpaces.IsValid);
        Assert.Equal(resultWithoutSpaces.BankCode, resultWithSpaces.BankCode);
        Assert.Equal(resultWithoutSpaces.AgencyCode, resultWithSpaces.AgencyCode);
        Assert.Equal(resultWithoutSpaces.AccountNumber, resultWithSpaces.AccountNumber);
        Assert.Equal(resultWithoutSpaces.ControlKey, resultWithSpaces.ControlKey);
    }
}