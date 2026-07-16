using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ForCSharpTesting.MongoDBExploration.Models;

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
}

public class OrderUser
{
    [BsonElement("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("deliveryAdress")]
    public string DeliveryAddress { get; set; }
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
}