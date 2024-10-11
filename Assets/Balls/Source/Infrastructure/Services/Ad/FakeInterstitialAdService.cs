using Balls.Source.Infrastructure.Services.Log;
using System;

public class FakeInterstitialAdService : IInterstitialAdService
{
    private readonly ILogService _logService;

    public event Action Opened;
    public event Action Closed;
    public event Action<string> Error;

    public FakeInterstitialAdService(ILogService logService)
    {
        _logService = logService;
    }

    public bool IsIntestitial => throw new System.NotImplementedException();

    public void OpenInterstitialAd()
    {
        _logService.Log("Interstitial opened");
        Opened?.Invoke();
        _logService.Log("Interstitial closed");
        Closed?.Invoke();
    }

    public bool CanOpenIntestitialAd()
    {
        return true;
    }
}