namespace TokenTeam.ConfigCenter.Service;

using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using TokenTeam.ConfigCenter.Config;
using TokenTeam.ConfigCenter.Model;

public class GiteeApiClient
{
    internal HttpClient Client { get; private set; } = null!;

    public GiteeApiClient(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<GiteeApiClient> logger)
    {
        Client = clientFactory.CreateClient();
        _config = configuration.GetSection("ConfigProvider").Get<AppConfigProviderConfig>() ?? new();
        logger.LogInformation("appid: {conf} ", _config.AppId);
        logger.LogInformation("repoName: {conf} ", _config.RepoName);
    }

    private readonly AppConfigProviderConfig _config;
    private string _accessToken = string.Empty;
    private readonly Lock _lockObj = new();

    public async IAsyncEnumerable<string> FetchConfigKeys()
    {
        var url = $"https://gitee.com/api/v5/repos/{_config.RepoOwner}/{_config.RepoName}/contents/config?" +
            $"access_token={_accessToken}&ref={_config.Branch}";
        var req = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url)
        };

        var resp = await SendRequestInternal(req).ConfigureAwait(false);
        var content = resp.Content.ReadFromJsonAsAsyncEnumerable<GiteeContentMetaResp>().ConfigureAwait(false);

        await foreach(var item in content)
        {
            if (item is null) continue;
            if (item.Type != "dir") continue;
            yield return item.Name;
        }
    }

    public async Task<RawConfig> FetchConfig(string configKey)
    {
        var platforms = FetchConfigPlatforms(configKey);

        var result = new RawConfig();

        await foreach (var platform in platforms)
        {
            var path = $"config/{configKey}/{configKey}.{platform}.json";
            var content = await FetchBlob(path).ConfigureAwait(false);
            result.Platforms.Add(platform, content);
        }
        {
            var path = $"config/{configKey}/{configKey}.json";
            var content = await FetchBlob(path).ConfigureAwait(false);
            result.Base = content;
        }

        return result;
    }

    internal async Task<string> FetchBlob(string path)
    {
        var url = $"https://gitee.com/api/v5/repos/{_config.RepoOwner}/{_config.RepoName}/contents/{path}?" +
            $"access_token={_accessToken}&ref={_config.Branch}";
        var req = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url)
        };

        var resp = await SendRequestInternal(req).ConfigureAwait(false);
        var content = await resp.Content.ReadFromJsonAsync<GiteeContentMetaResp>().ConfigureAwait(false);
        if (content is null) return string.Empty;

        var data = Convert.FromBase64String(content.Content!);
        var json = Encoding.UTF8.GetString(data);
        return json;
    }

    internal async IAsyncEnumerable<string> FetchConfigPlatforms(string configKey)
    {
        var url = $"https://gitee.com/api/v5/repos/{_config.RepoOwner}/{_config.RepoName}/contents/config/{configKey}?" +
            $"access_token={_accessToken}&ref={_config.Branch}";
        var req = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url)
        };

        var resp = await SendRequestInternal(req).ConfigureAwait(false);
        var content = resp.Content.ReadFromJsonAsAsyncEnumerable<GiteeContentMetaResp>().ConfigureAwait(false);

        await foreach (var item in content)
        {
            if (item is null) continue;
            if (item.Type != "file") continue;
            var match = Regex.Match(item.Name, @$"{configKey}\.(.+)\.json");
            if (!match.Success) continue;

            var platform = match.Groups[1].Value;
            yield return platform;
        }
    }

    private async Task<HttpResponseMessage> SendRequestInternal(HttpRequestMessage request)
    {
        var resp = await Client.SendAsync(request).ConfigureAwait(false);
        if (resp.StatusCode is System.Net.HttpStatusCode.Unauthorized)
        {
            await RefreshAccessToken().ConfigureAwait(false);
            var newUrl = Regex.Replace(request.RequestUri!.ToString(), @"access_token=.*?&", $"access_token={_accessToken}&");
            var newRequest = new HttpRequestMessage
            {
                Method = request.Method,
                Content = request.Content,
                RequestUri = new Uri(newUrl),
            };

            resp = await Client.SendAsync(newRequest).ConfigureAwait(false);
        }

        resp.EnsureSuccessStatusCode();

        return resp;
    }

    internal async Task RefreshAccessToken()
    {
        var formDict = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "scope", "projects user_info" },
            { "username",  _config.UserName},
            { "password", _config.Password },
            { "client_id", _config.AppId },
            { "client_secret", _config.AppSecret }
        };

        var req = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://gitee.com/oauth/token"),
            Content = new FormUrlEncodedContent(formDict),
        };

        var resp = await Client.SendAsync(req).ConfigureAwait(false);
        var content = await resp.Content.ReadFromJsonAsync<GiteeOAuthResp>().ConfigureAwait(false);

        lock (_lockObj)
        {
            _accessToken = content?.AccessToken ?? _accessToken;
        }
    }
}

internal record GiteeOAuthResp
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = null!;
}

internal record GiteeContentMetaResp
{
    [JsonPropertyName("type")]
    public string Type { get; init; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    [JsonPropertyName("path")]
    public string Path { get; init; } = null!;

    [JsonPropertyName("content")]
    public string? Content { get; init; }
}
