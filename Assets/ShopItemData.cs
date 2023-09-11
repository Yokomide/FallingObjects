using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioPreset", menuName = "Custom/ShopItemPreset")]

public class ShopItemData : ScriptableObject
{

    public List<int> costList = new List<int>();

    public int maxUpgrades;

    public int upAmount;

    public int currentCostID;
    public int upgradesCount;

    public bool isLocked;

    public void Reset()
    {
        currentCostID = 0;
        upgradesCount = 0;
        isLocked = false;
    }
}
