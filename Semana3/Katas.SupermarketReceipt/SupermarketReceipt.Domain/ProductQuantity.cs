namespace SupermarketReceipt.Domain;

public class ProductQuantity
{
    private const string Format = "{0} Quantity: {1}.";

    private readonly Product _product;
    private readonly int _quantity;

    public ProductQuantity(Product product, int quantity)
    { 
        ValidatePositiveQuantity(quantity);
        
        _product = product;
        _quantity = quantity;
    }
    
    public decimal CalculateTotalPricePerQuantity() => 
        _product.CalculatePrice(_quantity);

    private static void ValidatePositiveQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                ProductQuantityException.TheQuantityCannotBeZeroOrNegative);
    }

    public bool Compare(ProductQuantity productQuantity) => 
        _product.Equals(productQuantity._product);

    public ProductQuantity CloneAddingQuantity(ProductQuantity productQuantity) => 
        new(_product, _quantity + productQuantity._quantity);
    
    public override string ToString() => 
        string.Format(Format, _product, _quantity);
}