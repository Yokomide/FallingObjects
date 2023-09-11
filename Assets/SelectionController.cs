using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public GameObject selection;
    public int skinID;

    private void OnEnable()
    {
        StartCoroutine(InstanceWaiter());
    }
    private IEnumerator InstanceWaiter()
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        if (GameManager.instance.PlayerInfo.currentSkin == skinID)
            selection.SetActive(true);
    }
}
