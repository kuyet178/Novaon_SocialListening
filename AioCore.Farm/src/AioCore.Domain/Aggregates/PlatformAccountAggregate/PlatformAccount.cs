using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AioCore.Shared.Extensions;
using AioCore.Shared.ModelCores;

namespace AioCore.Domain.Aggregates.PlatformAccountAggregate;

public class PlatformAccount : Entity
{
    public string UniqueId { get; set; } = default!;

    public PlatformType PlatformType { get; set; }

    public string Data { get; set; } = default!;

    public FacebookAccount FacebookAccount =>
        PlatformType.Equals(PlatformType.Facebook) ? Data.ParseTo<FacebookAccount>() : default!;

    public void UpdateFacebook(string cookie, string token)
    {
        FacebookAccount.Cookie = cookie;
        FacebookAccount.Token = token;
        Data = FacebookAccount.ToJson();
    }
}