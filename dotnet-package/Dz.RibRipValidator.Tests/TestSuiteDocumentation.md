# Algerian RIB/RIP Validator - Test Suite Documentation

This document provides a detailed explanation of each test in the test suite for the Algerian RIB/RIP Validator library.

## AlgerianRibValidatorTests

### Valid RIB Validation Tests

#### `ValidateRib_WithValidRibs_ShouldReturnValidResult()`
Validates a RIB with correct format and proper control key to ensure it returns a valid result. This test creates a RIB with a proper control key calculated using the algorithm and verifies that the validator returns a `RibDetails` object with `IsValid` set to true, proper bank code, and no error message.

#### `ValidateRib_WithValidRibNameAndProperControlKey_ShouldReturnValid()`
Tests a RIB with a valid format and correct control key, verifying that the validation passes completely. This test uses a specific agency code "00012" and account number "3456789012", calculates the proper control key using the algorithm, and confirms all fields in the result are correct.

#### `ValidateRib_WithInvalidControlKey_ShouldReturnInvalidResult()`
Creates a RIB with an incorrect control key to verify that the validation correctly identifies and reports it as invalid. This test ensures that when the control key doesn't match the calculated key, the validator returns `IsValid = false` with an appropriate error message.

#### `ValidateRib_WithValidRip_ShouldWork()`
Tests validation of a valid RIP (Relevé d'Identité Postal) which uses the CCP bank code. This ensures that postal account numbers (CCP) are properly validated using the same algorithm as bank RIBs.

### Edge Case Tests

#### `ValidateRib_WithNullInput_ShouldReturnInvalidResult()`
Validates the behavior when a null string is passed to the validator. This test ensures that the validator handles null input gracefully, returning an invalid result with an appropriate error message indicating the RIB format is invalid.

#### `ValidateRib_WithEmptyString_ShouldReturnInvalidResult()`
Tests the validator's behavior when an empty string is provided. Similar to the null test, this verifies proper handling of empty input with appropriate error reporting.

#### `ValidateRib_WithWhitespace_RemovalShouldWork()`
Tests the validator's ability to handle RIB strings containing whitespace. The validator should remove all whitespace characters before processing and validate the resulting 20-digit string.

#### `ValidateRib_WithLessThan20Digits_ShouldReturnInvalidResult()`
Validates the handling of RIB strings with fewer than 20 digits. This test ensures that the validator correctly identifies format violations and returns appropriate error information.

#### `ValidateRib_WithMoreThan20Digits_ShouldReturnInvalidResult()`
Validates the handling of RIB strings with more than 20 digits. This test ensures that the validator correctly identifies format violations and returns appropriate error information.

#### `ValidateRib_WithNonDigitCharacters_ShouldReturnInvalidResult()`
Tests the validator's behavior when non-numeric characters are present in the RIB string. This ensures that the validator correctly identifies format violations.

### Bank Code Tests

#### `ValidateRib_WithUnknownBankCode_ShouldReturnUnknownBankInfo()`
Tests the handling of RIBs with unrecognized bank codes. This test verifies that when an unknown bank code is encountered, the validator still processes the RIB structurally but returns appropriate "Unknown Bank" information.

#### `ValidateRib_WithKnownBankCodes_ShouldReturnCorrectBankInfo(string bankCode, string expectedShort, string expectedName)`
Parameterized test that verifies proper bank information is returned for known bank codes. This test runs with multiple bank codes (e.g., "001" for BNA, "007" for CCP) and verifies that the correct short name and full name are returned.

## AlgerianRibValidatorListTests

### Multiple RIB Validation Tests

#### `ValidateRibs_WithValidRibsList_ShouldReturnCorrectResults()`
Tests the validation of a list containing multiple valid RIBs. This test creates a collection of valid RIBs with proper control keys and verifies that each RIB in the collection is validated correctly, returning a dictionary where each RIB maps to its corresponding validation result.

#### `ValidateRibs_WithMixedValidInvalidRibs_ShouldReturnCorrectResults()`
Validates a collection containing a mix of valid and invalid RIBs. This test ensures that the `ValidateRibs` method correctly processes each RIB individually, returning appropriate validation results for both valid and invalid RIBs in the same collection.

#### `ValidateRibs_WithEmptyList_ShouldReturnEmptyDictionary()`
Tests the behavior when an empty collection is passed to the `ValidateRibs` method. Verifies that an empty dictionary is returned when no RIBs are provided for validation.

#### `ValidateRibs_WithEmptyCollection_ShouldReturnEmptyDictionary()`
Tests the behavior when a null collection is passed to the `ValidateRibs` method. Verifies that an empty dictionary is returned when the input collection is null.

#### `ValidateRibs_WithDuplicateRibs_ShouldReturnCorrectResults()`
Tests the validation of a collection containing duplicate RIB strings. This test ensures that the dictionary correctly handles duplicate keys by maintaining unique entries in the result dictionary.

#### `ValidateRibs_WithWhitespaceContainingRibs_ShouldHandleCorrectly()`
Tests the validation of RIBs containing whitespace in a collection. This ensures that each RIB in the collection is properly processed for whitespace removal and validation, maintaining the original RIB string as the key in the result dictionary.

## UtilitiesTests

### Control Key Calculation Tests

#### `ComputeControlKeyFromAgencyAccount_WithValidInput_ShouldReturnCorrectKey()`
Tests the control key calculation algorithm with a specific agency "00012" and account "3456789012". This ensures the Modulo 97 algorithm is working correctly by verifying the calculated control key matches the expected value.

#### `ComputeControlKeyFromAgencyAccount_WithAllZeros_ShouldReturnValidKey()`
Tests the control key calculation with all-zero agency and account numbers. This edge case ensures that the algorithm handles zero values correctly and returns a valid control key.

#### `ComputeControlKeyFromAgencyAccount_WithLargeNumbers_ShouldReturnValidKey()`
Tests the control key calculation with the largest possible values for agency and account numbers. This ensures the algorithm works correctly with large numbers and the result is in the proper 2-digit format.

### Bank Information Tests

#### `BankCodes_DictionaryContainsExpectedBanks()`
Verifies that the bank codes dictionary contains expected entries for major Algerian banks. This test ensures that the predefined bank codes are available and properly configured.

#### `UnknowBank_ReturnsCorrectUnknownBankInfo()`
Tests the function that creates unknown bank information for unrecognized bank codes. Verifies that the function returns the correct "Unknown" indicator and appropriate descriptive text with the bank code.

## Test Coverage Summary

The test suite provides comprehensive coverage including:

- **Format validation**: Tests for correct 20-digit format, handling of whitespace, and rejection of invalid formats
- **Control key validation**: Tests for proper key calculation, validation, and error reporting
- **Bank code handling**: Tests for known bank codes, unknown bank codes, and appropriate information retrieval
- **Edge cases**: Tests for null/empty input, malformed inputs, and special character handling
- **Multiple RIB validation**: Tests for validating collections of RIBs with various combinations of valid and invalid entries
- **Integration**: Tests that validate the end-to-end functionality of the RIB validator

## Testing Approach

The test suite follows these principles:
- **Boundary testing**: Tests inputs at the limits of acceptable values
- **Error condition testing**: Tests how invalid inputs are handled
- **Positive testing**: Tests valid inputs to ensure correct functionality
- **Parameterized tests**: Uses theories to test multiple inputs for the same validation logic
- **Integration testing**: Tests the interaction between different components of the validation process

This comprehensive test suite ensures that the Algerian RIB validator functions correctly across all expected use cases while handling errors gracefully.