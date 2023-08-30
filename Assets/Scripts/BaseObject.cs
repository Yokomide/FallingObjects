using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI.Table;
using System;
public class BaseObject : MonoBehaviour
{
    [SerializeField]
    protected int _points;

    [SerializeField]
    protected int _health;

    [SerializeField]
    protected bool _isBadObject;

    [SerializeField]
    protected float _speed = 2f;

    [SerializeField]
    protected GameObject _vfx;
    
    public event Action OnHealthChanged;

    public int Health => _health;
    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(0, 0, -60), 1f)).Append(transform.DORotate(new Vector3(0, 20, 60), 1f));
        sequence.SetLoops(-1, LoopType.Yoyo);
        OnHealthChanged?.Invoke();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - _speed * Time.deltaTime);
    }

    public void PickUp()
    {
        if (!_isBadObject)
        {
            GameManager.instance.AddPoints(_points);
        }
        else
        {
            DamagePlayer();
        }
        DestroyObject();
    }

    public void DestroyObject()
    {
        StartCoroutine(DestroyCoroutine());
    }

    public void TakeDamage()
    {
        GameManager.instance.PlayInteractCorrectSound();
        GameManager.instance.AttackEvent();
        var effect = Instantiate(_vfx, gameObject.transform.position, Quaternion.identity);
        effect.transform.parent = gameObject.transform;
        _health -= GameManager.instance.Damage;
        OnHealthChanged?.Invoke();
        if(_health <= 0)
        {
            PickUp();
                
        }
    }

    public void DamagePlayer()
    {
        GameManager.instance.PlayInteractWrongSound();
        GameManager.instance.DecreaseHealth();
    }

    private IEnumerator DestroyCoroutine()
    {
        var fallingObjectTranform = gameObject.transform;
        fallingObjectTranform.GetComponent<Collider2D>().enabled = false;

        //fallingObjectTranform.GetChild(0).gameObject.SetActive(true);

        yield return DOTween.Sequence()
            .Append(fallingObjectTranform.DOScale(new Vector3(fallingObjectTranform.localScale.x * 1.2f, fallingObjectTranform.localScale.y * 1.2f, fallingObjectTranform.localScale.z * 1.2f), 0.2f))
            .Append(fallingObjectTranform.DOScale(Vector3.zero, 0.2f)).WaitForCompletion();

        Destroy(gameObject);
    }
}