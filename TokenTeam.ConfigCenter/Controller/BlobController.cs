namespace TokenTeam.ConfigCenter.Controller;

using Microsoft.AspNetCore.Mvc;
using TokenTeam.ConfigCenter.Model;
using TokenTeam.ConfigCenter.Service;

[Route("[controller]")]
[ApiController]
public class BlobController(AppConfigProvider appConfigProvider) : ControllerBase
{

    [HttpGet("{key}")]
    [Produces("application/json")]
    public IActionResult GetConfig([FromHeader(Name = "iwut-platform")] string platform, [FromRoute] string key)
    {
        var content = appConfigProvider.GetConfig(key, platform);
        if (string.IsNullOrEmpty(content))
        {
            return Problem("指定的配置不存在");
        }

        return new RawJsonResult(content);
    }
}
