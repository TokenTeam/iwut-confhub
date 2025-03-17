namespace TokenTeam.ConfigCenter.Model;

public class RawConfig
{
    public string Base { get; set; } = null!;
    public Dictionary<string, string> Platforms { get; private set; } = [];
}
