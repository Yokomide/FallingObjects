using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSSettings : MonoBehaviour
{
    [SerializeField][Min(10)] private int _frameRate = 60;
    [SerializeField][Min(0)] private int _vSyncCount = 0;

    private void Start()
    {
        Application.targetFrameRate = _frameRate;
        QualitySettings.vSyncCount = _vSyncCount;
    }
}
