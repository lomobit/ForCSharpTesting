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
        Console.WriteLine("==== ORDER =================================");

        var orders = await orderCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
        foreach (var order in orders)
        {
            Console.WriteLine(order);
        }


        return;

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

    public static void ConfigureCOmposeObject()
    {
        // 1. Регистрация вложенного класса OrderUser
        BsonClassMap.RegisterClassMap<OrderUser>(cm =>
        {
            cm.AutoMap(); // Автоматически маппит остальные свойства

            cm.MapMember(c => c.UserId)
                .SetElementName("user_id")
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapMember(c => c.DeliveryAddress)
                .SetElementName("deliveryAdress");
        });

        // 2. Регистрация вложенного класса OrderProduct
        BsonClassMap.RegisterClassMap<OrderProduct>(cm =>
        {
            cm.AutoMap();

            cm.MapMember(c => c.ProductId)
                .SetElementName("product_id")
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapMember(c => c.Name).SetElementName("name");
            cm.MapMember(c => c.Price).SetElementName("price");
            cm.MapMember(c => c.Currency).SetElementName("currency");
        });

        // 3. Регистрация корневого класса Order
        BsonClassMap.RegisterClassMap<Order>(cm =>
        {
            cm.AutoMap();

            // Настройка Id (аналог [BsonId] и [BsonRepresentation])
            cm.MapIdMember(c => c.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIdGenerator(StringObjectIdGenerator.Instance);

            cm.MapMember(c => c.User).SetElementName("user");
            cm.MapMember(c => c.Product).SetElementName("product");
            cm.MapMember(c => c.CreateTime).SetElementName("createTime");
        });
    }
}
