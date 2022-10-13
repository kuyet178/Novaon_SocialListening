using AioCore.Domain.Packages.MongoPackage.Metadata;
using MongoDB.Driver;

namespace AioCore.Domain.Packages.MongoPackage.Abstracts;

public interface IMongoContextBuilder
{
    IMongoDatabase Database { get; }

    void Entity<TEntity>(Action<EntityTypeBuilder<TEntity>> action) where TEntity : class;

    void OnConfiguring(MongoContext context);

    void OnModelCreating(Action action);
}