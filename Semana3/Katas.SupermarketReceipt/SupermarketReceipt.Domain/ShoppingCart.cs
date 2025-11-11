using SupermarketReceipt.Domain;

namespace SupermarketReceipt.Tests;

public class ShoppingCart 
{
    private readonly List<ProductQuantity> _products = [];
    
    public IReadOnlyCollection<string> GetStringProducts() => 
        _products.Select(product => product.ToString()).ToList();

    public void AddProductItem(ProductQuantity productQuantity)
    {
        ProductQuantity? existingProduct = _products.FirstOrDefault(product =>
            product.Compare(productQuantity));

        if (existingProduct != null) 
            EditProductQuantity(productQuantity, existingProduct);
        else
            _products.Add(productQuantity);
    }

    private void EditProductQuantity(ProductQuantity productQuantity, ProductQuantity existingProduct)
    {
        _products.Remove(existingProduct);
        _products.Add(existingProduct.CloneAddingQuantity(productQuantity));
    }

    public decimal GetTotalPrice() =>  .Sum(product => product.CalculateTotalPricePerQuantity());
}