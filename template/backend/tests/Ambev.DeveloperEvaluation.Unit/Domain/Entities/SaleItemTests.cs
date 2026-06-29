using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    private SaleItem CreateItem(int quantity) => new SaleItem
    {
        Id = Guid.NewGuid(),
        SaleId = Guid.NewGuid(),
        ProductId = Guid.NewGuid(),
        ProductName = "Test Product",
        Quantity = quantity,
        UnitPrice = 100m
    };

    [Fact(DisplayName = "Given quantity below 4 When applying discount Then no discount applied")]
    public void ApplyDiscount_QuantityBelow4_NoDiscount()
    {
        // Given
        var item = CreateItem(3);

        // When
        item.ApplyDiscount();

        // Then
        item.Discount.Should().Be(0m);
        item.TotalAmount.Should().Be(300m);
    }

    [Theory(DisplayName = "Given quantity between 4 and 9 When applying discount Then 10% discount applied")]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(9)]
    public void ApplyDiscount_QuantityBetween4And9_10PercentDiscount(int quantity)
    {
        // Given
        var item = CreateItem(quantity);

        // When
        item.ApplyDiscount();

        // Then
        item.Discount.Should().Be(0.10m);
        item.TotalAmount.Should().Be(100m * quantity * 0.90m);
    }

    [Theory(DisplayName = "Given quantity between 10 and 20 When applying discount Then 20% discount applied")]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    public void ApplyDiscount_QuantityBetween10And20_20PercentDiscount(int quantity)
    {
        // Given
        var item = CreateItem(quantity);

        // When
        item.ApplyDiscount();

        // Then
        item.Discount.Should().Be(0.20m);
        item.TotalAmount.Should().Be(100m * quantity * 0.80m);
    }

    [Fact(DisplayName = "Given quantity above 20 When applying discount Then throws DomainException")]
    public void ApplyDiscount_QuantityAbove20_ThrowsDomainException()
    {
        // Given
        var item = CreateItem(21);

        // When
        var act = () => item.ApplyDiscount();

        // Then
        act.Should().Throw<DomainException>()
           .WithMessage("Cannot sell more than 20 identical items.");
    }

    [Fact(DisplayName = "Given item When cancelled Then IsCancelled is true")]
    public void Cancel_WhenCalled_SetsIsCancelledTrue()
    {
        // Given
        var item = CreateItem(3);

        // When
        item.Cancel();

        // Then
        item.IsCancelled.Should().BeTrue();
    }
}