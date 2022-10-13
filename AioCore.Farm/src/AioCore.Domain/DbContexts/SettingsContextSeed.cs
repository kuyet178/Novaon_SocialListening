using AioCore.Domain.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace AioCore.Domain.DbContexts;

public class SettingsContextSeed
{
    public static async Task SeedAsync(
        AppSettings appSettings,
        ILogger<IdentityContextSeed>? logger)
    {
        var policy = CreatePolicy(logger, nameof(IdentityContextSeed));

        await policy.ExecuteAsync(async () => { await Task.CompletedTask; });
    }

    private static AsyncRetryPolicy CreatePolicy(ILogger? logger, string prefix, int retries = 3)
    {
        return Policy.Handle<SqlException>().WaitAndRetryAsync(
            retryCount: retries,
            sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
            onRetry: (exception, timeSpan, retry, ctx) =>
            {
                logger?.LogWarning(exception,
                    "[{Prefix}] Exception {ExceptionType} with message {Message} detected on attempt {Retry} of {Retries}",
                    prefix, exception.GetType().Name, exception.Message, retry, retries);
            }
        );
    }
}