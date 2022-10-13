using AioCore.Domain.Packages.MongoPackage.Abstracts;
using MongoDB.Driver;

namespace AioCore.Domain.Packages.MongoPackage;

public class MongoContext
{
    protected readonly IMongoContextBuilder ModelBuilder;
    public IMongoDatabase Database => ModelBuilder.Database;

    protected MongoContext(IMongoContextBuilder modelBuilder)
    {
        ModelBuilder = modelBuilder;
        modelBuilder.OnConfiguring(this);
        modelBuilder.OnModelCreating(OnModelCreating);
    }

    protected virtual void OnModelCreating()
    {
    }
}