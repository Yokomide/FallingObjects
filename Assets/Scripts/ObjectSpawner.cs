using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    #region Public

    public bool isShouldSpawn = true;

    #endregion Public

    #region Private

    [SerializeField]
    private float _delay = 2f;


    [SerializeField]
    private float _minDelay = 0.2f;

    [SerializeField]
    private float _delayStep = 0.02f;

    [SerializeField]
    private List<GameObject> goodFallingObjects = new List<GameObject>();

    [SerializeField]
    private List<GameObject> badFallingObjects = new List<GameObject>();

    [SerializeField]
    private List<GameObject> instantiatedFallingObjects = new List<GameObject>();

    #endregion Private

    private void Start()
    {
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

                if (Random.Range(0, 100) <= 80)
                    instantiatedFallingObjects
                         .Add(Instantiate(goodFallingObjects[Random.Range(0, goodFallingObjects.Count)], new Vector2(Random.Range(-7.5f, 7.5f), transform.position.y), Quaternion.identity));
                else
                    instantiatedFallingObjects
                        .Add(Instantiate(badFallingObjects[Random.Range(0, badFallingObjects.Count)], new Vector2(Random.Range(-7.5f, 7.5f), transform.position.y), Quaternion.identity));

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
}