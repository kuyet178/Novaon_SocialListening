using AioCore.Domain.Packages.MongoPackage.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace AioCore.Domain.Packages.MongoPackage;

public class MongoDocument
{
    [BsonId] [MongoKey] public Guid Id { get; set; } = Guid.NewGuid();
}