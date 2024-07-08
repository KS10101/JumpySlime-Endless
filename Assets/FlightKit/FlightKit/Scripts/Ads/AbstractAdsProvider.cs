using System;
using UnityEngine;

namespace FlightKit
{
    /// <summary>
    /// Possible outcomes of showing a rewarded ad.
    /// </summary>
    public enum AdShowResult
    {
        Finished,
        Skipped,
        Failed
    }
    
    /// <summary>
    /// A universal interface for any rewarded ad provider. 
    /// </summary>
    public abstract class AbstractAdsProvider : MonoBehaviour
    {
        public virtual void ShowRewardedAd(Action<AdShowResult> resultCallback) {}
    }
}