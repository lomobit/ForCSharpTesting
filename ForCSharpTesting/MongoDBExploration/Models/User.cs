using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ForCSharpTesting.MongoDBExploration.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("city")]
    public string City { get; set; }


    public override string ToString()
    {
        var tab = "    ";
        return $"{{\n{tab}_id: {Id},\n{tab}email: {Email},\n{tab}name: {Name}\n{tab}city: {City}\n}}";
    }
}
