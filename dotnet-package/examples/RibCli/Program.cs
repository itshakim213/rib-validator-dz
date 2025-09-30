using Dz.RibRipValidator;

if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run -- <RIB20digits>");
    return;
}

var rib = args[0];
var result = AlgerianRibValidator.ValidateRib(rib);
Console.WriteLine($"isValid: {result.IsValid}");
Console.WriteLine($"bankCode: {result.BankCode}");
Console.WriteLine($"agencyCode: {result.AgencyCode}");
Console.WriteLine($"accountNumber: {result.AccountNumber}");
Console.WriteLine($"controlKey: {result.ControlKey}");
Console.WriteLine($"calculatedKey: {result.CalculatedKey}");
Console.WriteLine($"bank: {result.BankInfo.Short} - {result.BankInfo.Name}");
Console.WriteLine($"error: {result.Error}");