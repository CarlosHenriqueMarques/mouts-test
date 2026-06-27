using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; private set; }
    public bool IsCancelled { get; set; }

    public decimal TotalAmount => UnitPrice * Quantity * (1 - Discount);

    // Chamado automaticamente quando Quantity é setado
    public void ApplyDiscount()
    {
        if (Quantity > 20)
            throw new Domain.Exceptions.DomainException("Cannot sell more than 20 identical items.");

        Discount = Quantity switch
        {
            < 4 => 0m,
            < 10 => 0.10m,
            _ => 0.20m
        };
    }

    public void Cancel() => IsCancelled = true;
}