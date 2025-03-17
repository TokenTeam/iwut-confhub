namespace TokenTeam.ConfigCenter.Config
{
    public class AppConfigProviderConfig
    {
        public string RepoOwner { get; set; } = null!;
        public string RepoName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Branch { get; set; } = null!;
        public string AppId { get; set; } = null!;
        public string AppSecret { get; set; } = null!;
        public string[] Platforms { get; set; } = [];
    }
}
