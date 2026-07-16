using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Xml.Linq;

namespace ForCSharpTesting.MongoDBExploration.Models;

public static class Constants
{
    public const string Tab = "  ";
    public const string SubTab = "    ";
}

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("user")]
    public OrderUser User { get; set; }

    [BsonElement("product")]
    public OrderProduct Product { get; set; }

    [BsonElement("createTime")]
    public DateTime CreateTime { get; set; }

    public override string ToString()
    {
        return $"{{" +
               $"\n{Constants.Tab}_id: {Id}," +
               $"\n{Constants.Tab}user: {User}," +
               $"\n{Constants.Tab}product: {Product}," +
               $"\n{Constants.Tab}createTime: {CreateTime}" +
               $"\n}}";
    }
}

public class OrderUser
{
    [BsonElement("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("deliveryAdress")]
    public string DeliveryAddress { get; set; }

    public override string ToString()
    {
        return $"{Constants.Tab}" +
               $"{{\n{Constants.SubTab}user_id: {UserId}," +
               $"\n{Constants.SubTab}deliveryAdress: {DeliveryAddress}" +
               $"\n{Constants.Tab}}}";
    }
}

public class OrderProduct
{
    [BsonElement("product_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProductId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("currency")]
    public string Currency { get; set; }

    public override string ToString()
    {
        return $"{Constants.Tab}" +
               $"{{\n{Constants.SubTab}product_id: {ProductId}," +
               $"\n{Constants.SubTab}name: {Name}," +
               $"\n{Constants.SubTab}price: {Price}," +
               $"\n{Constants.SubTab}currency: {Currency}" +
               $"\n{Constants.Tab}}}";
    }
}