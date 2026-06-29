using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleItemCommand> itemFaker =
        new Faker<CreateSaleItemCommand>()
            .RuleFor(i => i.ProductId, f => f.Random.Guid())
            .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())
            .RuleFor(i => i.Quantity, f => f.Random.Int(1, 3)) // < 4 = sem desconto
            .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(10, 500));

    private static readonly Faker<CreateSaleCommand> saleCommandFaker =
        new Faker<CreateSaleCommand>()
            .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(8).ToUpper())
            .RuleFor(s => s.SaleDate, f => f.Date.Recent())
            .RuleFor(s => s.CustomerId, f => f.Random.Guid())
            .RuleFor(s => s.CustomerName, f => f.Company.CompanyName())
            .RuleFor(s => s.BranchId, f => f.Random.Guid())
            .RuleFor(s => s.BranchName, f => f.Address.City())
            .RuleFor(s => s.Items, f => itemFaker.Generate(2));

    public static CreateSaleCommand GenerateValidCommand()
        => saleCommandFaker.Generate();

    public static CreateSaleCommand GenerateCommandWithQuantity(int quantity)
    {
        var item = new CreateSaleItemCommand
        {
            ProductId = Guid.NewGuid(),
            ProductName = new Faker().Commerce.ProductName(),
            Quantity = quantity,
            UnitPrice = 100m
        };

        var cmd = saleCommandFaker.Generate();
        cmd.Items = new List<CreateSaleItemCommand> { item };
        return cmd;
    }
}