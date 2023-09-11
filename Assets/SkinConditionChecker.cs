using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkinConditionChecker : MonoBehaviour
{

    [SerializeField]
    private GameObject _lockScreen;

    [SerializeField]
    private Button _skinSelector;


    public bool adUnlock;
    public bool coinUnlock;
    public bool scoreUnlock;
    public bool playedTimesUnlock;

    public bool isAdWatched;
    public int adCost;
    public int coinCost;
    public int scoreCost;
    public int playedTimesCost;

    public int currentWatchedAd;

    public GameObject selectFrame;

    public TextMeshProUGUI currentCountText;
    public SkinName skinName;


    private void Start()
    {
        CheckStatus();
    }
    public void TryUnlock()
    {
        if (adUnlock)
            CheckAd();

        if (coinUnlock)
            CheckCoin();

        if(scoreUnlock)
            CheckScore();

        if(playedTimesUnlock)
            CheckPlayedTimes();
    }
    private void CheckAd()
    {
        GameManager.instance.TryUnlockSkinByAdv(this);
    }

    private void CheckCoin()
    {
        if (GameManager.instance.Coins >= coinCost)
        {
            GameManager.instance.RemoveCoins(coinCost);
            UnlockSkin();
        }
    }

    private void CheckScore()
    {

        if (GameManager.instance.BestScore >= scoreCost)
        {
            UnlockSkin();
        }
    }

    private void CheckPlayedTimes()
    {
        if (GameManager.instance.PlayedTimes >= playedTimesCost)
        {
            UnlockSkin();
        }
    }

    private void CheckStatus()
    {
        if (GameManager.instance.GetSkinStatus(skinName))
        {
            UnlockSkin();
        }
    }

    public void AddWatchedAdv()
    {
        currentWatchedAd++;
        currentCountText.text = (adCost - currentWatchedAd).ToString() + "x";
        if(currentWatchedAd >= adCost)
        {
            UnlockSkin();
        }
    }
    private void UnlockSkin()
    {
        _skinSelector.enabled = true;
        _lockScreen.SetActive(false);
        GameManager.instance.SetSkinStatus(skinName);
    }
}
