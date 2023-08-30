using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject _vfx;

    [SerializeField]
    private List<Transform> _effectTarget = new List<Transform>();

    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        GameManager.instance.OnAttack += PlayAttackEffect;
    }
    private void OnEnable()
    {
        if (GameManager.instance != null)
            GameManager.instance.OnAttack += PlayAttackEffect;
    }
    private void OnDisable()
    {
        GameManager.instance.OnAttack -= PlayAttackEffect;

    }
    private void PlayAttackEffect()
    {
        if (_animator != null)
            _animator.SetTrigger("Attack");
        for (int i = 0; i < _effectTarget.Count; i++)
        {
            Instantiate(_vfx, _effectTarget[i].position, Quaternion.identity);
        }
    }
}
