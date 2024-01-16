using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class AdManager : MonoBehaviour
{
    // public static AdManager Instance;

    // private Action _actionAD_full_complite;
    // private Action _actionAD_close;
    // private Action _actionAD_error;
    // DateTime _startWatch;

    // void Awake()
    // {
    //     Instance = this;
    // }

    // [SerializeField] private RewardedAd rewardedAd;
    // public static void ShowRewardedAd(string idPlacement, Action actionComplite = null, Action actionClose = null, Action actionError = null) 
    // {
    //     rewardedAd = new RewardedAd(idPlacement); 
    //     var request = new AdRequest.Builder().Build(); 
    //     rewardedAd.LoadAd(request); 
    //     rewardedAd.OnRewardedAdLoaded += HandleRewardedAdLoaded;
    //     rewardedAd.OnRewardedAdFailedToLoad += HandleRewardedAdFailedToLoad;
    //     rewardedAd.OnReturnedToApplication += HandleReturnedToApplication;
    //     rewardedAd.OnLeftApplication += HandleLeftApplication;
    //     rewardedAd.OnRewardedAdShown += HandleRewardedAdShown;
    //     rewardedAd.OnRewardedAdDismissed += HandleRewardedAdDismissed;
    //     rewardedAd.OnImpression += HandleImpression;
    //     rewardedAd.OnRewarded += HandleRewarded;
    //     _actionAD_full_complite = actionComplite;
    //     _actionAD_close = actionClose;
    //     _actionAD_error = actionError;
    // }

    // private void ShowRewardedAd()
    // { 
    //     if (rewardedAd.IsLoaded()) 
    //     {
    //         _startWatch = DateTime.Now; 
    //         rewardedAd.Show(); 
    //     } 
    //     else 
    //     { 

    //     } 
    // }

    // public void HandleRewardedAdLoaded(object sender, EventArgs args) 
    // { 
    //     ShowRewardedAd(); 
    // }
}
