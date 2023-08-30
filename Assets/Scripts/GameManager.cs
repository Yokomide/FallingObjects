using _Samples.Scripts;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    #region Public

    public static GameManager instance;

    public int _score { get; private set; }

    #endregion Public

    #region Private

    #region Controllers
    [SerializeField]
    private HealthBarContoller _healthBarContoller;

    [SerializeField]
    private VolumeController _volumeController;

    [SerializeField]
    private AudioController _audioController;

    [SerializeField]
    private SFXController _sfxController;

    #endregion

    #region Text
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private TextMeshProUGUI _gameOverScoreText;
    #endregion

    [SerializeField]
    private ObjectSpawner _objectSpawner;

    [SerializeField]
    private GameObject _gameOverScreen;

    [SerializeField]
    private GameObject _menuScreen;


    [SerializeField]
    private GameObject _gameplayUI;

    [SerializeField]
    private Transform _scarySprite;

    [SerializeField]
    private int _healthPoint = 5;


    [SerializeField]
    private int _damage;

    public int Damage => _damage;



    #endregion Private

    public event Action OnAttack;

    #region Initialize

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

       // _healthBarContoller.SetHealthHearts(_healthPoint);
    }

    public void StartGame()
    {
        _healthBarContoller.SetHealthHearts(_healthPoint);
        _menuScreen.SetActive(false);
        _gameplayUI.SetActive(true);
        _objectSpawner.isShouldSpawn = true;
    }
    #endregion Initialize

    #region Health
    public void IncreaseHealth()
    {
        _healthBarContoller.AddHeart();
        _healthPoint++;
    }

    public void DecreaseHealth()
    {
        _healthBarContoller.RemoveHeart();
        _healthPoint--;
        if (_healthPoint <= 0)
        {
            Death();
        }
    }

    public void IncreaseDamage(int amount)
    {
        _damage += amount;
    }

    public void Death()
    {
        _healthPoint = 0;

        _objectSpawner.isShouldSpawn = false;
        _objectSpawner.ClearAllFallingObjects();

        _gameOverScoreText.text = _score.ToString();
        _gameOverScreen.SetActive(true);
        DOTween.Sequence()
            .Append(_gameOverScreen.transform.GetChild(0).DOScale(new Vector3(0.51993f, 0.51993f, 0.51993f), 1f))
            .Append(_gameOverScreen.transform.GetChild(1).DOScale(new Vector3(0.72f, 0.72f, 0.72f), 1f))
            .Append(_gameOverScreen.transform.GetChild(2).DOScale(new Vector3(3.0671f, 3.0671f, 3.0671f), 1f));
        _gameplayUI.SetActive(false);

    }

    #endregion

    #region Points
    public void AddPoints(int amount)
    {
        _score += amount;
        _scoreText.text = _score.ToString();
    }

    public void ResetPoints()
    {
        _score = 0;
        _scoreText.text = _score.ToString();
    }
    #endregion

    #region Horror
    public void StartHorrorEvent()
    {
        _objectSpawner.isShouldSpawn = false;
        _objectSpawner.ClearAllFallingObjects();

        _volumeController.isScary = true;

        _scarySprite.gameObject.SetActive(true);
        _scarySprite.DOMoveY(0, 7f).OnComplete(() => EndHorrorEvent());

        _audioController.PlayHorrorSound();

    }
    private void EndHorrorEvent()
    {
        _objectSpawner.isShouldSpawn = true;
        _volumeController.isScary = false;
        _volumeController.ResetSettings();

        _scarySprite.gameObject.SetActive(false);

        _audioController.PlayNormalSound();
    }
    #endregion

    #region Sound
    public void PlayInteractCorrectSound()
    {
        _sfxController.PlayCorrectSound();
    }

    public void PlayInteractWrongSound()
    {
        _sfxController.PlayWrongSound();
    }
    #endregion
    public void AttackEvent()
    {
        OnAttack?.Invoke();
    }
}