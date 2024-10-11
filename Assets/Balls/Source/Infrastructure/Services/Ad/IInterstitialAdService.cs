using System;

public interface IInterstitialAdService
{
    public event Action Opened;
    public event Action Closed;
    public event Action<string> Error;

    public bool IsIntestitial { get; }

    public void OpenInterstitialAd();
    public bool CanOpenIntestitialAd();
}