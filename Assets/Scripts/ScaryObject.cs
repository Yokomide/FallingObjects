using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryObject : BaseObject
{
    private void OnMouseDown()
    {
       // GameManager.instance.StartHorrorEvent();
        TakeDamage();
        DestroyObject();
    }
}