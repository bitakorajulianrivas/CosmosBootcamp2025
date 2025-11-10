using FluentAssertions;

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
            .WithMessage(Product.PriceShouldBePositive);
    }
    
}

public class Product
{
    public const string? PriceShouldBePositive = "The price should be positive.";
    
    private readonly string _name;
    private readonly decimal _price;

    public Product(string name, decimal price)
    {
        ValidatePositivePrice(price);
        
        _name = name;
        _price = price;
    }

    private static void ValidatePositivePrice(decimal price)
    {
        if(price < 0)
            throw new ArgumentException(PriceShouldBePositive);
    }

    private const string ProductFormat = 
        "Name: {0}. Price: € {1}.";

    public override string ToString() => 
        string.Format(ProductFormat, _name, _price);
}