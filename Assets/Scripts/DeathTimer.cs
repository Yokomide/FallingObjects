using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime;

    private void Start()
    {
        StartCoroutine(TimeToDeath());
    }
    private IEnumerator TimeToDeath()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }


}
