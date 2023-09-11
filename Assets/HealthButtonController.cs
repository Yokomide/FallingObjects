using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthButtonController : MonoBehaviour
{
    [SerializeField]
    private Upgraders _upgradeType;

    [SerializeField]
    private ShopItemData _shopItemData;

    [SerializeField]
    private GameObject _lockImage;

    [SerializeField]
    private GameObject _advIcon;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        Initialize();
    }
    private void Initialize()
    {
        _shopItemData.upgradesCount = GameManager.instance.PlayerInfo.HealthUpgradesCount;
        _shopItemData.isLocked = GameManager.instance.PlayerInfo.HealthIsLocked;

        if (GameManager.instance.PlayerInfo.HealthIsLocked)
        {
            LockButton();
            return;
        }
    }
    public void TryUpgradeHealth()
    {
        GameManager.instance.TryUpgradeHealth(this);
    }
    public void UpgradeHealth()
    {
        _shopItemData.upgradesCount++;
        GameManager.instance.PlayerInfo.HealthUpgradesCount++;
        GameManager.instance.IncreaseHealth();
        if (GameManager.instance.PlayerInfo.HealthUpgradesCount >= _shopItemData.maxUpgrades)
        {
            LockButton();
        }
        GameManager.instance.Save();
    }
    private void LockButton()
    {
        GameManager.instance.PlayerInfo.HealthIsLocked = true;
        _button.interactable = false;
        _lockImage.SetActive(true);
        _advIcon.SetActive(false);
        _shopItemData.isLocked = true;
    }

    private void Unlock()
    {
        _advIcon.SetActive(true);

        _button.interactable = true;
        _lockImage.SetActive(false);
    }
}
