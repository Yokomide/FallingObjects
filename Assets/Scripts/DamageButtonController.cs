using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DamageButtonController : MonoBehaviour
{
    [SerializeField]
    private Upgraders _upgradeType;

    [SerializeField]
    private ShopItemData _shopItemData;

    [SerializeField]
    private TextMeshProUGUI _costText;

    [SerializeField]
    private TextMeshProUGUI _upText;

    [SerializeField]
    private GameObject _lockImage;
    private Button _button;


  
    private void OnDestroy()
    {
        GameManager.instance.OnReset -= ResetData;
    }
    private void Start()
    {
        GameManager.instance.OnReset += ResetData;
        _button = GetComponent<Button>();
        //if (_shopItemData.isLocked)
        //{
        //    LockButton();
        //    return;
        //}
        Initialize();
    }
    private void Initialize()
    {
        switch(_upgradeType)
        {
            case Upgraders.FirstAttack:
                _shopItemData.currentCostID = GameManager.instance.PlayerInfo.firstDamageCurrentCostID;
                _shopItemData.upgradesCount = GameManager.instance.PlayerInfo.firstDamageUpgradesCount;
                _shopItemData.isLocked = GameManager.instance.PlayerInfo.firstDamageIsLocked;
                break;
            case Upgraders.SecondAttack:
                _shopItemData.currentCostID = GameManager.instance.PlayerInfo.secondDamageCurrentCostID;
                _shopItemData.upgradesCount = GameManager.instance.PlayerInfo.secondDamageUpgradesCount;
                _shopItemData.isLocked = GameManager.instance.PlayerInfo.secondDamageIsLocked;
                break;
            case Upgraders.ThirdAttack:
                _shopItemData.currentCostID = GameManager.instance.PlayerInfo.thirdDamageCurrentCostID;
                _shopItemData.upgradesCount = GameManager.instance.PlayerInfo.thirdDamageUpgradesCount;
                _shopItemData.isLocked = GameManager.instance.PlayerInfo.thirdDamageIsLocked;
                break;
        }
        if(_shopItemData.isLocked)
        {
            LockButton();
            return;
        }
        _costText.text = _shopItemData.costList[_shopItemData.currentCostID].ToString() + "Ì";
        _upText.text = "+" + _shopItemData.upAmount.ToString() + " ÀÒÊ";
    }
    public void TryBuy()
    {
        if (GameManager.instance.CheckCoins(_shopItemData.costList[_shopItemData.currentCostID]))
        {
            GameManager.instance.IncreaseDamage(_shopItemData.upAmount);
            GameManager.instance.RemoveCoins(_shopItemData.costList[_shopItemData.currentCostID]);
            _shopItemData.currentCostID++;
            _shopItemData.upgradesCount++;
            if (_shopItemData.upgradesCount >= _shopItemData.maxUpgrades)
            {
                LockButton();
                GameManager.instance.SaveUpgradeProgress(_upgradeType, _shopItemData.currentCostID, _shopItemData.upgradesCount, _shopItemData.isLocked);
                return;
            }
            GameManager.instance.SaveUpgradeProgress(_upgradeType, _shopItemData.currentCostID, _shopItemData.upgradesCount, _shopItemData.isLocked);
            _costText.text = _shopItemData.costList[_shopItemData.currentCostID].ToString() + "Ì";

        }
    }
    private void LockButton()
    {
        _button.interactable = false;
        _lockImage.SetActive(true);
        _costText.gameObject.SetActive(false);
        _upText.gameObject.SetActive(false);
        _shopItemData.isLocked = true;
    }
    private void ResetData()
    {
        _shopItemData.Reset();
        Unlock();
        Initialize();
    }
    private void Unlock()
    {
        _button.interactable = true;
        _lockImage.SetActive(false);
        _costText.gameObject.SetActive(true);
        _upText.gameObject.SetActive(true);
    }
}
