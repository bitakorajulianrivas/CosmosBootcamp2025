using FluentAssertions;
using SupermarketReceipt.Domain;

namespace SupermarketReceipt.Tests;

public class ProductQuantityTest
{
    [Fact]
    public void ProductQuantity_ShouldAddOneProduct()
    {
        Product product = new ("Toothpaste", 1.99m);
        int quantity = 1;
        
        ProductQuantity productQuantity = new (product, quantity); 
        
        productQuantity.ToString().Should()
            .Be("Name: Toothpaste. Price: € 1.99. Quantity: 1.");
    }
}

public class ProductQuantity
{
    private const string Format = "{0} Quantity: {1}.";
    
    private readonly Product _product;
    private readonly int _quantity;
    public ProductQuantity(Product product, int quantity)
    { 
        _product = product;
        _quantity = quantity;
    }

    public override string ToString() => 
        string.Format(Format, _product, _quantity);
}