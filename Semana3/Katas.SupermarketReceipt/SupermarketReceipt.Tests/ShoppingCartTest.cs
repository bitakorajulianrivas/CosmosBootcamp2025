using FluentAssertions;

namespace SupermarketReceipt.Tests;

public class ShoppingCartTest
{
    [Fact]
    public void ShoppingCart_ShouldBeEmpty_WhenCreated()
    {
        ShoppingCart shoppingCart = new();
        
        shoppingCart.GetStringProducts().Should().BeEmpty();
    }
}

public class ShoppingCart
{
    public IReadOnlyCollection<string> GetStringProducts() => [];
}