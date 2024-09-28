using System;
using UnityEngine;
using GoogleMobileAds.Api;

// Ödüllü geçiş reklamı; ödüllü reklamdan farklı olarak sahne geçişlerinde geçiş reklamı yerine kullanılabilir izlendikden sonra ödül verir.

public class RewardedInterstitialAdController : MonoBehaviour
{
#if UNITY_ANDROID
    private const string _adUnitId = "ca-app-pub-3940256099942544/5354046379";
#elif UNITY_IPHONE
        private const string _adUnitId = "ca-app-pub-3940256099942544/6978759866";
#else
        private const string _adUnitId = "unused";
#endif

    private RewardedInterstitialAd _rewardedInterstitialAd;

    private void Start()
    {
        MobileAds.Initialize((InitializationStatus _initStatus) =>
        {
            Debug.Log("RewardedInterstitialAd mobil Ad. " + _initStatus.ToString());
        });

        LoadAd();
    }

    public void LoadAd()
    {
        if (_rewardedInterstitialAd != null)
        {
            DestroyAd();
        }

        Debug.Log("Loading rewarded interstitial ad.");

        var adRequest = new AdRequest();

        RewardedInterstitialAd.Load(_adUnitId, adRequest,(RewardedInterstitialAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.LogError("Rewarded interstitial ad failed to load an ad with error : " + error);
                return;
            }
            if (ad == null)
            {
            Debug.LogError("Unexpected error: Rewarded interstitial load event fired with null ad and null error.");
            return;
            }

            Debug.Log("Rewarded interstitial ad loaded with response : " + ad.GetResponseInfo());
            _rewardedInterstitialAd = ad;              
        });
        RegisterEventHandlers(_rewardedInterstitialAd);
    }
    public void ShowAd()
    {
        if (_rewardedInterstitialAd != null && _rewardedInterstitialAd.CanShowAd())
        {
            _rewardedInterstitialAd.Show((Reward reward) =>
            {
                Debug.Log("Rewarded interstitial ad rewarded : " + reward.Amount);
            });
        }
        else
        {
            Debug.LogError("Rewarded interstitial ad is not ready yet.");
        }
    }
    public void DestroyAd()
    {
        if (_rewardedInterstitialAd != null)
        {
            Debug.Log("Destroying rewarded interstitial ad.");
            _rewardedInterstitialAd.Destroy();
            _rewardedInterstitialAd = null;
        }
    }
    protected void RegisterEventHandlers(RewardedInterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded interstitial ad paid {0} {1}.", adValue.Value, adValue.CurrencyCode));
        };
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded interstitial ad recorded an impression.");
        };
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded interstitial ad was clicked.");
        };
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content opened.");
        };
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content closed.");
            LoadAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded interstitial ad failed to open full screen content" + " with error : " + error);
            LoadAd();
        };
    }
}
