using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadObject : BaseObject
{
    private void OnMouseDown()
    {
        TakeDamage();
    }
}