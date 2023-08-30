using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GoodObject goodObject))
        {
            goodObject.DamagePlayer();
            goodObject.DestroyObject();
        }
        else if (collision.gameObject.TryGetComponent(out BadObject badObject))
        {
            if (badObject != null)
            {
                badObject.DestroyObject();
            }
        }
        else if (collision.gameObject.TryGetComponent(out ScaryObject scaryObject))
        {
            if (scaryObject != null)
            {
                scaryObject.DestroyObject();
            }
        }
    }
}