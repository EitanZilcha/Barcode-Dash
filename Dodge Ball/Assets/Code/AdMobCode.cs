using System.Collections;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine;

public class AdMobCode : MonoBehaviour
{
    public BannerView bannerView;
    public InterstitialAd interstitial;
    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-8162835261816145~4284545399";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
        //this.RequestBanner();
        if (PlayerPrefs.GetInt("AUS") % 3 == 0 && PlayerPrefs.GetInt("AUS") > 5)
        {
            this.RequestInterstitial();
        }
        PlayerPrefs.SetInt("AUS", PlayerPrefs.GetInt("AUS") + 1);
    }
    public void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-8162835261816145/7925365364";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif
        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoadedBanner;
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-8162835261816145/3515293163";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Called when an ad request has successfully loaded.
        interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }
    public void HandleOnAdLoaded(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        if (interstitial.IsLoaded())
        {
            //interstitial.Show();
        }
    }
    public void HandleOnAdLoadedBanner(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        
            bannerView.Show();
        
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender,System. EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeftApplication(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

}