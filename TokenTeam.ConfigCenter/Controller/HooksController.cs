namespace TokenTeam.ConfigCenter.Controller;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TokenTeam.ConfigCenter.Model;
using TokenTeam.ConfigCenter.Service;


[ApiController]
[Route("[controller]")]
public class HooksController(AppConfigProvider configProvider) : ControllerBase
{
    [HttpPost("Gitee/Push")]
    public async Task<IActionResult> GiteePushHook([FromBody] GiteePushHookRequest request)
    {
        await configProvider.RefreshConfig();
        return Ok();
    }
}
