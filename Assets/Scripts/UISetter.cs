using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISetter : MonoBehaviour
{
    [SerializeField]
    BaseObject _baseObject;

    [SerializeField]
    TextMeshProUGUI _text;
    
    


    private void OnEnable()
    {
        _baseObject.OnHealthChanged += SetValue;
    }
    private void OnDisable()
    {
        _baseObject.OnHealthChanged -= SetValue;
    }

    private void SetValue()
    {
        _text.text = _baseObject.Health.ToString();
    }
}
