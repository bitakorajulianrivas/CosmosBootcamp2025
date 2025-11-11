namespace SupermarketReceipt.Domain;

public class Product : IEquatable<Product>
{
    private const string ProductFormat = 
        "Name: {0}. Price: € {1}.";
    
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
    
    public override string ToString() => 
        string.Format(ProductFormat, _name, _price);

    public bool Equals(Product? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _name == other._name && _price == other._price;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Product)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_name, _price);
    }

    public decimal CalculatePrice(int productQuantity)
    {
        return _price * productQuantity;
    }
}