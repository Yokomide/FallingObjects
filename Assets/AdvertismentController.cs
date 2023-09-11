using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertismentController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(InstanceWaiter());
    }
    private IEnumerator InstanceWaiter()
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        GameManager.instance.ShowSimpleAd();
    }
}
