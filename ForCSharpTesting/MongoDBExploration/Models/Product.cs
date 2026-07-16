using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ForCSharpTesting.MongoDBExploration.Models;

public class Product
{
    public string Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Currency { get; set; }

    public override string ToString()
    {
        var tab = "    ";
        return $"{{\n{tab}_id: {Id},\n{tab}name: {Name}\n{tab}price: {Price}\n{tab}currency: {Currency}\n}}";
    }
}
