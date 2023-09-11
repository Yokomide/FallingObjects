using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewWave", menuName = "Custom/WaveSettings")]

public class Wave : ScriptableObject
{
    public List<GameObject> _fallingObjectsList = new List<GameObject>();
    public float _nextWaveDelay;
    public bool _shouldResetDelay;
}
