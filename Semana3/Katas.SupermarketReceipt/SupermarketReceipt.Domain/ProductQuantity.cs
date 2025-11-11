namespace SupermarketReceipt.Domain;

public class ProductQuantity
{
    private const string Format = "{0} Quantity: {1}.";

    public Product Product { get; }
    public int Quantity { get; }

    public ProductQuantity(Product product, int quantity)
    { 
        ValidatePositiveQuantity(quantity);
        
        Product = product;
        Quantity = quantity;
    }

    private static void ValidatePositiveQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                ProductQuantityException.TheQuantityCannotBeZeroOrNegative);
    }

    public override string ToString() => 
        string.Format(Format, Product, Quantity);
}