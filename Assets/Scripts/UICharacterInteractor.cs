using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UICharacterInteractor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Animator _anim;

    public void OnPointerClick(PointerEventData eventData)
    {
        _anim.SetTrigger("Attack");
    }
}
