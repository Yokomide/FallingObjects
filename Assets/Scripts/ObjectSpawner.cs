using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    #region Public

    public bool isShouldSpawn = true;

    #endregion Public

    #region Private

    private float _startDelay;

    [SerializeField]
    private float _delay = 2f;


    [SerializeField]
    private float _minDelay = 0.2f;

    [SerializeField]
    private float _minRange;

    [SerializeField]
    private float _maxRange;

    [SerializeField]
    private float _delayStep = 0.02f;

    [SerializeField]
    private List<GameObject> instantiatedFallingObjects = new List<GameObject>();

    [SerializeField]
    private List<Wave> _waveList = new List<Wave>();

    private Wave _currentWave;

    #endregion Private

    private void Start()
    {
        _startDelay = _delay;
    }
    public void StartFalling()
    {
        StartCoroutine(WaveSwitcher());
        StartCoroutine(SpawnCoroutine());
    }
    public void ClearAllFallingObjects()
    {
        for (int i = 0; i < instantiatedFallingObjects.Count; i++)
        {
            Destroy(instantiatedFallingObjects[i]);
        }
        instantiatedFallingObjects.Clear();
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            if (isShouldSpawn == true)
            {

                instantiatedFallingObjects
                     .Add(Instantiate(_currentWave._fallingObjectsList[Random.Range(0, _currentWave._fallingObjectsList.Count)], new Vector2(Random.Range(_minRange, _maxRange), transform.position.y), Quaternion.identity));
              

                if (_delay > _minDelay)
                {
                    _delay -= _delayStep;
                    if (_delay < _minDelay)
                    {
                        _delay = _minDelay;
                    }
                }
                yield return new WaitForSeconds(_delay);
            }
            yield return null;
        }
    }
    private void CheckShouldResetDelay()
    {
        if (_currentWave._shouldResetDelay)
            ResetDelay();
    }
    private void ResetDelay()
    {
        _delay = _startDelay;
    }
    private IEnumerator WaveSwitcher()
    {
        while (true)
        {
            if (isShouldSpawn == true)
            {
                if (_currentWave == null)
                {
                    _currentWave = _waveList[0];
                    if (_currentWave._shouldResetDelay)
                        CheckShouldResetDelay();
                }
                else
                {
                    if (_currentWave != _waveList.Last())
                    {
                        _currentWave = _waveList[_waveList.IndexOf(_currentWave) + 1];
                        CheckShouldResetDelay();

                    }
                    else
                        yield return null;
                }
                yield return new WaitForSeconds(_currentWave._nextWaveDelay);
            }

        }
    }
}