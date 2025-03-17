namespace TokenTeam.ConfigCenter.Service;

public class InitializeConfigService : BackgroundService
{
    public InitializeConfigService(AppConfigProvider appConfigProvider)
    {
        _appConfigProvider = appConfigProvider;
    }

    private readonly AppConfigProvider _appConfigProvider;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _appConfigProvider.RefreshConfig();
    }
}
