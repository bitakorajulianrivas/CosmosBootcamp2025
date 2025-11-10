namespace SupermarketReceipt.Domain;

public class Product
{
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
            throw new ArgumentException(ProductException.PriceShouldBePositive);
    }

    private const string ProductFormat = 
        "Name: {0}. Price: € {1}.";

    public override string ToString() => 
        string.Format(ProductFormat, _name, _price);
}