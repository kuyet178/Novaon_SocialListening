using AioCore.Domain.Packages.MongoPackage;

namespace AioCore.Domain.Common;

public class AppSettings
{
    public string AssemblyName { get; set; } = default!;

    public string? AssemblyPath { get; set; }

    public MongoConfigs MongoConfigs { get; set; } = default!;

    public ConnectionStrings ConnectionStrings { get; set; } = default!;

    public DefaultUser DefaultUser { get; set; } = default!;

    public List<DefaultRole> DefaultRoles { get; set; } = default!;

    public void Update(string assemblyName, string? assemblyPath = "")
    {
        AssemblyName = assemblyName;
        AssemblyPath = assemblyPath;
    }
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; } = default!;
}

public class DefaultUser
{
    public string Email { get; set; } = default!;

    public string FullName { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;
}

public class DefaultRole
{
    public string Name { get; set; } = default!;
}