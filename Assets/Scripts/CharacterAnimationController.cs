using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]    
   // private List<RuntimeAnimatorController> _controllers = new List<RuntimeAnimatorController>();

    private void OnEnable()
    {
       StartCoroutine(InstanceWaiter());
    }

    private void OnDisable()
    {
        GameManager.instance.OnAttack -= PlayAttack;
        GameManager.instance.OnDamageRecieve -= PlayHurt;
    }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttack()
    {
        _animator.SetTrigger("Attack");
    }
    public void PlayHurt()
    {
        _animator.SetTrigger("Hurt");
    }
    // public void SetAnimatorController(int index)
    // {
    //     _animator.runtimeAnimatorController = _controllers[index];
    // }
    public void SetAnimatorController(RuntimeAnimatorController controller)
    {
        _animator.runtimeAnimatorController = controller;
    }
    private IEnumerator InstanceWaiter()
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        GameManager.instance.OnAttack += PlayAttack;
        GameManager.instance.OnDamageRecieve += PlayHurt;
    }
}
