using FluentAssertions;

namespace SupermarketReceipt.Tests;

public class ProductTest
{
    [Fact]
    public void Product_ShouldHaveNameAndPrice()
    {
        Product product = new(name: "Brushteeth", price: 1);
        product.ToString().Should().Be("Name: Brushteeth. Price: € 1.");
    }
    
}

public class Product(string name, int price)
{
    private const string ProductFormat = 
        "Name: {0}. Price: € {1}.";

    public override string ToString() => 
        string.Format(ProductFormat, name, price);
}