namespace TokenTeam.ConfigCenter.Model;

using Microsoft.AspNetCore.Mvc;

public class RawJsonResult(string rawString) : IActionResult
{
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var resp = context.HttpContext.Response;
        resp.ContentType = "application/json";
        await resp.WriteAsync(rawString).ConfigureAwait(false);
    }
}
