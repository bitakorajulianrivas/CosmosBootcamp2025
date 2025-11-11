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

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ProductQuantity_ShouldThrowException_WhenQuantityIsZeroOrNegative(int quantity)
    {
        Product product = new ("Pineapple", 2.99m);
        
        Action action = () => new ProductQuantity(product, quantity);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(ProductQuantityException.TheQuantityCannotBeZeroOrNegative);
    }
}