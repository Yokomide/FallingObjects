using System.Collections;
using TMPro;
using UnityEngine;

public class SimpleUISetter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _text;

    [SerializeField]
    private bool _subscribeDamage;

    [SerializeField]
    private bool _subscribeBestScore;

    [SerializeField]
    private bool _subscribeLastScore;

    [SerializeField]
    private bool _subscribeHealth;


    [SerializeField]
    private bool _subscribeCoins;

    [SerializeField]
    private bool _subscribeDamageUpgrader;

    private void OnEnable()
    {
        StartCoroutine(InstanceWaiter());

    }
    private void OnDisable()
    {
        if (_subscribeBestScore)
            GameManager.instance.OnBestScoreChanged -= SetBestScoreValue;

        if (_subscribeLastScore)
            GameManager.instance.OnLastScoreChanged -= SetLastScoreValue;

        if (_subscribeDamage)
            GameManager.instance.OnDamageChanged -= SetDamageValue;

        if(_subscribeDamageUpgrader)
            GameManager.instance.OnDamageUpgraderChanged -= SetDamageUpgraderValue;

        if (_subscribeHealth)
            GameManager.instance.OnHealthChanged -= SetHealthValue;

        if (_subscribeCoins)
            GameManager.instance.OnCoinChanged -= SetCoinValue;
    }

    private void SetDamageValue()
    {
        _text.text = GameManager.instance.Damage.ToString();
    }
    private void SetDamageUpgraderValue()
    {
        _text.text = GameManager.instance.DamageUpgraderCost.ToString();
    }
    private void SetBestScoreValue()
    {
        _text.text = GameManager.instance.BestScore.ToString();
    }
    private void SetLastScoreValue()
    {
        _text.text = GameManager.instance.LastScore.ToString();
    }
    private void SetCoinValue()
    {
        _text.text = GameManager.instance.Coins.ToString();
    }
    private void SetHealthValue()
    {
        _text.text = GameManager.instance.Health.ToString();
    }

    private IEnumerator InstanceWaiter()
    {
        yield return new WaitUntil(() => GameManager.instance != null);

        if (_subscribeBestScore)
        {
            GameManager.instance.OnBestScoreChanged += SetBestScoreValue;
            SetBestScoreValue();
        }

        if (_subscribeDamage)
        {
            GameManager.instance.OnDamageChanged += SetDamageValue;
            SetDamageValue();
        }
        if(_subscribeDamageUpgrader)
        {
            GameManager.instance.OnDamageUpgraderChanged += SetDamageUpgraderValue;
            SetDamageUpgraderValue();
        }
        if (_subscribeHealth)
        {
            GameManager.instance.OnHealthChanged += SetHealthValue;
            SetHealthValue();
        }
        if (_subscribeCoins)
        {
            GameManager.instance.OnCoinChanged += SetCoinValue;
            SetCoinValue();
        }
        if (_subscribeLastScore)
        {
            GameManager.instance.OnLastScoreChanged += SetLastScoreValue;
            SetLastScoreValue();
        }
    }
}
