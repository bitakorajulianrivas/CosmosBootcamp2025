using FluentAssertions;
using SupermarketReceipt.Domain;

namespace SupermarketReceipt.Tests;

public class ProductTest
{
    [Fact]
    public void Product_ShouldHaveNameAndPrice()
    {
        Product product = new("Brushteeth", 1);
        
        product.ToString().Should()
            .Be("Name: Brushteeth. Price: € 1.");
    }

    [Fact]
    public void Product_ShouldAllowDecimalPrices()
    {
        Product product = new("Toothpaste", 1.99m);
        
        product.ToString().Should()
            .Be("Name: Toothpaste. Price: € 1.99.");
    }

    [Fact]
    public void Product_ThrowException_WhenPriceIsNegative()
    {
        Action action = () => new Product("Apple", -0.99m);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(ProductException.PriceShouldBePositive);
    }
}