using FluentAssertions;
using SupermarketReceipt.Domain;

namespace SupermarketReceipt.Tests;

public class ShoppingCartTest
{
    [Fact]
    public void ShoppingCart_ShouldBeEmpty_WhenCreated()
    {
        ShoppingCart shoppingCart = new();
        
        shoppingCart.GetStringProducts().Should().BeEmpty();
    }

    [Fact]
    public void ShoppingCart_ShouldAddSingleProduct()
    {
        ProductQuantity productQuantity = new (
                product: new Product("Apple", 0.99m), 
                quantity: 1);
        
        ShoppingCart shoppingCart = new();
        
        shoppingCart.AddProductItem(productQuantity);

        shoppingCart.GetStringProducts().Should().BeEquivalentTo([
            "Name: Apple. Price: € 0.99. Quantity: 1."]);
    }

    [Fact]
    public void ShoppingCart_ShouldAddMultipleDifferentProducts()
    {
        ShoppingCart shoppingCart = new();
        
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Apple", 0.99m), quantity: 2));
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Banana", 1.99m), quantity: 3));
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Pear", 0.99m), quantity: 4));

        shoppingCart.GetStringProducts().Should().BeEquivalentTo(
            "Name: Apple. Price: € 0.99. Quantity: 2.",
            "Name: Banana. Price: € 1.99. Quantity: 3.",
            "Name: Pear. Price: € 0.99. Quantity: 4.");
    }

    [Fact]
    public void ShoppingCart_ShouldAccumulateQuantity_WhenAddingSameProduct()
    {
        ShoppingCart shoppingCart = new();
        
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Apple", 0.99m), quantity: 3));
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Banana", 1.99m), quantity: 3));
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Pear", 0.99m), quantity: 4));
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Banana", 1.99m), quantity: 2));
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Pear", 0.99m), quantity: 2));
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Apple", 0.99m), quantity: 4));
        
        shoppingCart.GetStringProducts().Should().BeEquivalentTo(
            "Name: Apple. Price: € 0.99. Quantity: 7.",
            "Name: Banana. Price: € 1.99. Quantity: 5.",
            "Name: Pear. Price: € 0.99. Quantity: 6.");
    }

    [Fact]
    public void ShoppingCart_ShouldReturnTotalPriceWithoutDiscounts()
    {
        decimal expectedTotalPrice = 2.97m + 5.97m + 3.96m + 3.98m;
        ShoppingCart shoppingCart = new();
        
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Apple", 0.99m), quantity: 3));
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Banana", 1.99m), quantity: 3));
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Pear", 0.99m), quantity: 4));
        shoppingCart.AddProductItem(new ProductQuantity(product: new Product("Banana", 1.99m), quantity: 2));

        decimal totalPrice = shoppingCart.GetTotalPrice();
        
        totalPrice.Should().Be(expectedTotalPrice);
    }
    
}

public class ShoppingCart
{
    private List<ProductQuantity> _products = [];
    
    public IReadOnlyCollection<string> GetStringProducts() => 
        _products.Select(product => product.ToString()).ToList();

    public void AddProductItem(ProductQuantity productQuantity)
    {
        ProductQuantity? existingProduct = _products.FirstOrDefault(product =>
            product.Product.Equals(productQuantity.Product));

        if (existingProduct != null) 
            EditProductQuantity(productQuantity, existingProduct);
        else
            _products.Add(productQuantity);
    }

    private void EditProductQuantity(ProductQuantity productQuantity, ProductQuantity existingProduct)
    {
        int newQuantity = existingProduct.Quantity + productQuantity.Quantity;
        _products.Remove(existingProduct);
        _products.Add(new ProductQuantity(existingProduct.Product, newQuantity));
    }

    public decimal GetTotalPrice()
    {
        return _products.Sum(product => product.Product
            .CalculatePrice(product.Quantity));
    }
}