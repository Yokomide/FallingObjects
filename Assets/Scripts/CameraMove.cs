using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 _pos;
    private Vector3 _startPos;

    public float modifier;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        var pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
        transform.position = new Vector3((pos.x), (pos.y));
    }
}