using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class SimpleAds : MonoBehaviour
{
    void Start()
    {
        Advertisement.Initialize("24dbb682-bbe7-40e2-95a3-e522fbe52f2b", true);
    }
    public void ShowAd()
    {
        StartCoroutine(ShowAdWhenReady());
    }
    IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady())
            yield return null;

        Advertisement.Show();
    }
}