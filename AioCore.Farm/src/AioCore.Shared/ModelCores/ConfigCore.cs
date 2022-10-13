using AioCore.Shared.Extensions;

namespace AioCore.Shared.ModelCores
{
    public class ConfigCore : Entity
    {
        public ConfigKeyCore Key { get; set; }

        public string Value { get; set; } = default!;

        public FacebookSurfingNewsFeed FacebookSurfingNewsFeedConfigs =>
            Key.Equals(ConfigKeyCore.FacebookSurfingNewsFeed)
                ? Value.ParseTo<FacebookSurfingNewsFeed>()
                : default!;

        public class FacebookSurfingNewsFeed
        {
            public WatchVideo WatchVideoConfigs { get; set; } = default!;

            public class WatchVideo
            {
                public int Count { get; set; }

                public int Delay { get; set; }
            }
        }
    }

    public enum ConfigKeyCore
    {
        Undefined = 0,
        FacebookSurfingNewsFeed,
    }
}