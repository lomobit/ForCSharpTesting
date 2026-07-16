using ForCSharpTesting.MongoDBExploration.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace ForCSharpTesting.MongoDBExploration;

public static class MongoTest
{
    public static async Task Test()
    {


        var mongoClient = new MongoClient("mongodb://localhost:27017");
        var explorationDb = mongoClient.GetDatabase("exploration");

        var userCollection = explorationDb.GetCollection<User>("user");
        var productCollection = explorationDb.GetCollection<Product>("product");
        var orderCollection = explorationDb.GetCollection<Order>("order");

        

        Console.WriteLine("==== USER ===========================================");

        var filter = Builders<User>.Filter.Empty;
        var users = await userCollection.Find(filter).ToListAsync();

        foreach (var user in users)
        {
            Console.WriteLine(user);
        }

        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("==== PRODUCT ========================================");

        var filterProduct = Builders<Product>.Filter.Empty;
        var products = await productCollection.Find(filterProduct).ToListAsync();

        foreach (var product in products)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("==== RANDOM_QUERIES =================================");

        var filter3 = Builders<User>.Filter.Eq(u => u.City, "Москва");
        var filter4 = Builders<User>.Filter.Where(u => u.City == u.Name);
        var usersMoscow = await userCollection.Find(filter3).ToListAsync();



        var newUser = new User
        {
            Name = "Петров Иван Иванович",
            Email = "test1@test.city",
            City = "Москва"
        };
        await userCollection.InsertOneAsync(newUser);



        var updateFilter = Builders<User>.Filter.Eq(u => u.Id, newUser.Id);
        var update = Builders<User>.Update.Set(u => u.City, "Санкт-Петербург");

        await userCollection.UpdateOneAsync(updateFilter, update);



        var deleteFilter = Builders<User>.Filter.Eq(u => u.Id, newUser.Id);
        await userCollection.DeleteOneAsync(deleteFilter);
    }

    public static void Configure()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(Product)))
        {
            BsonClassMap.RegisterClassMap<Product>(cm =>
            {
                cm.MapIdProperty(p => p.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));

                cm.MapProperty(p => p.Name)
                    .SetElementName("name");

                cm.MapProperty(p => p.Price)
                    .SetElementName("price");

                cm.MapProperty(p => p.Currency)
                    .SetElementName("currency");
            });
        }
    }
}
