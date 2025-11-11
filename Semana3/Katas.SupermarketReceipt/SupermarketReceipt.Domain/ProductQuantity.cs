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
    
    public decimal CalculateTotalPricePerQuantity() => 
        Product.CalculatePrice(Quantity);

    private static void ValidatePositiveQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                ProductQuantityException.TheQuantityCannotBeZeroOrNegative);
    }

    public ProductQuantity CloneAddingQuantity(ProductQuantity productQuantity) => 
        new(Product, Quantity + productQuantity.Quantity);

    public override string ToString() => 
        string.Format(Format, Product, Quantity);
}