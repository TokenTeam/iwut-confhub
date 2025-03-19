using System.Text.Json.Serialization;

namespace TokenTeam.ConfigCenter.Model;

#nullable disable

public class Author
{
    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("user_name")]
    public string UserName { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class Commit
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("tree_id")]
    public string TreeId { get; set; }

    [JsonPropertyName("distinct")]
    public bool Distinct { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("author")]
    public Author Author { get; set; }

    [JsonPropertyName("committer")]
    public Committer Committer { get; set; }

    [JsonPropertyName("added")]
    public List<string> Added { get; set; }

    [JsonPropertyName("removed")]
    public List<string> Removed { get; set; }

    [JsonPropertyName("modified")]
    public List<string> Modified { get; set; }
}

public class Committer
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("user_name")]
    public string UserName { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class Enterprise
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class Owner
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("login")]
    public string Login { get; set; }

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("site_admin")]
    public bool SiteAdmin { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("user_name")]
    public string UserName { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class Repository
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("path")]
    public string Path { get; set; }

    [JsonPropertyName("full_name")]
    public string FullName { get; set; }

    [JsonPropertyName("owner")]
    public Owner Owner { get; set; }

    [JsonPropertyName("private")]
    public bool Private { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("fork")]
    public bool Fork { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("pushed_at")]
    public DateTime PushedAt { get; set; }

    [JsonPropertyName("git_url")]
    public string GitUrl { get; set; }

    [JsonPropertyName("ssh_url")]
    public string SshUrl { get; set; }

    [JsonPropertyName("clone_url")]
    public string CloneUrl { get; set; }

    [JsonPropertyName("svn_url")]
    public string SvnUrl { get; set; }

    [JsonPropertyName("git_http_url")]
    public string GitHttpUrl { get; set; }

    [JsonPropertyName("git_ssh_url")]
    public string GitSshUrl { get; set; }

    [JsonPropertyName("git_svn_url")]
    public string GitSvnUrl { get; set; }

    [JsonPropertyName("homepage")]
    public object Homepage { get; set; }

    [JsonPropertyName("stargazers_count")]
    public int StargazersCount { get; set; }

    [JsonPropertyName("watchers_count")]
    public int WatchersCount { get; set; }

    [JsonPropertyName("forks_count")]
    public int ForksCount { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("has_issues")]
    public bool HasIssues { get; set; }

    [JsonPropertyName("has_wiki")]
    public bool HasWiki { get; set; }

    [JsonPropertyName("has_pages")]
    public bool HasPages { get; set; }

    [JsonPropertyName("license")]
    public object License { get; set; }

    [JsonPropertyName("open_issues_count")]
    public int OpenIssuesCount { get; set; }

    [JsonPropertyName("default_branch")]
    public string DefaultBranch { get; set; }

    [JsonPropertyName("namespace")]
    public string Namespace { get; set; }

    [JsonPropertyName("name_with_namespace")]
    public string NameWithNamespace { get; set; }

    [JsonPropertyName("path_with_namespace")]
    public string PathWithNamespace { get; set; }
}

public class GiteePushHookRequest
{
    [JsonPropertyName("hook_name")]
    public string HookName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("hook_id")]
    public int HookId { get; set; }

    [JsonPropertyName("hook_url")]
    public string HookUrl { get; set; }

    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; }

    [JsonPropertyName("sign")]
    public string Sign { get; set; }

    [JsonPropertyName("ref")]
    public string Ref { get; set; }

    [JsonPropertyName("before")]
    public string Before { get; set; }

    [JsonPropertyName("after")]
    public string After { get; set; }

    [JsonPropertyName("created")]
    public bool Created { get; set; }

    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }

    [JsonPropertyName("compare")]
    public string Compare { get; set; }

    [JsonPropertyName("commits")]
    public List<Commit> Commits { get; set; }

    [JsonPropertyName("head_commit")]
    public Commit HeadCommit { get; set; }

    [JsonPropertyName("total_commits_count")]
    public int TotalCommitsCount { get; set; }

    [JsonPropertyName("commits_more_than_ten")]
    public bool CommitsMoreThanTen { get; set; }

    [JsonPropertyName("repository")]
    public Repository Repository { get; set; }

    [JsonPropertyName("sender")]
    public Sender Sender { get; set; }

    [JsonPropertyName("enterprise")]
    public Enterprise Enterprise { get; set; }
}

public class Sender
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("login")]
    public string Login { get; set; }

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("site_admin")]
    public bool SiteAdmin { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("user_name")]
    public string UserName { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

#nullable restore