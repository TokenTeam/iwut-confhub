using Microsoft.AspNetCore.Mvc;
using TokenTeam.ConfigCenter.Model;
using TokenTeam.ConfigCenter.Service;

namespace TokenTeam.ConfigCenter.Controller;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    public TestController(GiteeApiClient giteeClient, AppConfigProvider appConfigProvider)
    {
        _giteeClient = giteeClient;
        _appConfigProvider = appConfigProvider;
    }

    private readonly GiteeApiClient _giteeClient;
    private readonly AppConfigProvider _appConfigProvider;

    [HttpGet("ConfigKeys")]
    public IAsyncEnumerable<string> FetchConfigKeys()
    {
        return _giteeClient.FetchConfigKeys();
    }

    [HttpGet("Config/{key}")]
    public async Task<RawConfig> FetchConfig([FromRoute]string key)
    {
        return await _giteeClient.FetchConfig(key).ConfigureAwait(false);
    }

    [HttpGet("RefreshConfig")]
    public async Task<IActionResult> RefreshConfig()
    {
        await _appConfigProvider.RefreshConfig().ConfigureAwait(false);
        return Ok("ok");
    }
}
