using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ImageSaveAndReadMongoDb.Models;

public class Employee
{
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("photo")]
    public byte[] Photo { get; set; }
}