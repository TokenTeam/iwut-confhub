namespace TokenTeam.ConfigCenter.Service;

using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

public class AppConfigProvider
{
    public AppConfigProvider(GiteeApiClient giteeClient, IMemoryCache cache)
    {
        _giteeApiClient = giteeClient;
        _cahce = cache;
    }

    private readonly GiteeApiClient _giteeApiClient;
    private readonly IMemoryCache _cahce;
    public const string CacheKey = "AppConfig";

    public async Task RefreshConfig()
    {
        var configKeys = _giteeApiClient.FetchConfigKeys().ConfigureAwait(false);
        var configDict = new Dictionary<string, string>();

        await foreach(var configKey in configKeys)
        {
            var config = await _giteeApiClient.FetchConfig(configKey).ConfigureAwait(false);
            var baseJobj = JObject.Parse(config.Base);

            foreach (var kv in config.Platforms)
            {
                var platformJobj = JObject.Parse(kv.Value);
                platformJobj.Merge(baseJobj, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union,
                    MergeNullValueHandling = MergeNullValueHandling.Ignore
                });

                configDict.Add($"{configKey}.{kv.Key}", platformJobj.ToString(Newtonsoft.Json.Formatting.None));
            }

            configDict.Add($"{configKey}", baseJobj.ToString());
        }

        _cahce.Set(CacheKey, configDict);
    }

    public string? GetConfig(string key, string platform)
    {
        var dict = _cahce.Get<Dictionary<string, string>>(CacheKey);
        if (dict is null) return null;
        var success = dict.TryGetValue($"{key}.{platform}", out var config);
        if (!success)
        {
            success = dict.TryGetValue(key, out config);
        }

        return success ? config : null;
    }
}
