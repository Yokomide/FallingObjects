using _Samples.Scripts;
using DG.Tweening;
using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PlayerInfo
    {

    public int _healthPoint;

    public int _coins;

    public int _damage = 1;

    public int _lastScore;

    public int _bestScore;

    public int _maxHealth = 1;

    public int firstDamageCurrentCostID;
    public int firstDamageUpgradesCount;
    public bool firstDamageIsLocked;

    public int secondDamageCurrentCostID;
    public int secondDamageUpgradesCount;
    public bool secondDamageIsLocked;

    public int thirdDamageCurrentCostID;
    public int thirdDamageUpgradesCount;
    public bool thirdDamageIsLocked;

    public int HealthUpgradesCount;
    public bool HealthIsLocked;

    public int _playedTimes;

    public int currentSkin;

    public bool BlondieSkinOpened;

    public bool AlienSkinOpened;

    public bool CompSkinOpened;

    public bool GManSkinOpened;

    public bool HorrorSkinOpened;

    public bool RobberSkinOpened;

    public bool TVSkinOpened;

    public bool ZombieSkinOpened;

}

public class GameManager : MonoBehaviour
{
    #region Public

    public static GameManager instance;
    public PlayerInfo PlayerInfo;

    [DllImport("__Internal")]
    public static extern void ContinueGameExtern();

    [DllImport("__Internal")]
    public static extern void SetToLeaderboard(int value);

    [DllImport("__Internal")]
    public static extern void UnlockSkinExtern();

    [DllImport("__Internal")]
    public static extern void UpgradeHealthExtern();

    [DllImport("__Internal")]
    public static extern void GetCoinsExtern();

    [DllImport("__Internal")]
    public static extern void ShowAdv();

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    public static extern void LoadExtern();

    #endregion Public

    #region Private

    #region Controllers
    [SerializeField]
    private HealthBarContoller _healthBarContoller;
    [SerializeField]
    private AudioController _audioController;


    #endregion

    #region Text

    [SerializeField]
    private GameObject _coinPrefab;

    [SerializeField]
    private SkinSetter _skinSetter;

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
    private GameObject _continueScreen;

    [SerializeField]
    private GameObject _gameplayUI;

    [SerializeField]
    private GameObject _skibidiToilet;


    [SerializeField]
    private int _coinDropChance;

    public int Damage => PlayerInfo._damage;
    public int Coins => PlayerInfo._coins;
    public int Health => PlayerInfo._maxHealth;

    public int PlayedTimes => PlayerInfo._playedTimes;

    public int LastScore => PlayerInfo._lastScore;

    public int BestScore => PlayerInfo._bestScore;

    public int DamageUpgraderCost => _damageUpgradeCost;

    private int _damageUpgradeCost;

    private SkinConditionChecker skinConditionChecker;
    private HealthButtonController healthController;

    private int _gameOverCount;

    #endregion Private

    public event Action OnAttack;
    public event Action OnDamageRecieve;
    public event Action OnDamageChanged;
    public event Action OnBestScoreChanged;
    public event Action OnLastScoreChanged;
    public event Action OnHealthChanged;
    public event Action OnDamageUpgraderChanged;
    public event Action OnCoinChanged;
    public event Action OnReset;




    #region Initialize

    private void Awake()
    {

        LoadExtern();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        //   DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
       // LoadExtern();
        ShowAdv();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PlayerInfo = new PlayerInfo();
            Save();
            LoadExtern();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            LoadExtern();
        }
    }
    public void StartGame()
    {
        _healthBarContoller.SetHealthHearts(PlayerInfo._maxHealth);
        PlayerInfo._healthPoint = PlayerInfo._maxHealth;
        _menuScreen.SetActive(false);
        _gameplayUI.SetActive(true);
        _objectSpawner.isShouldSpawn = true;
        _objectSpawner.StartFalling();
        PlayerInfo._lastScore = 0;
        OnLastScoreChanged?.Invoke();
    }
    #endregion Initialize

    #region Health
    public void IncreaseHealth()
    {
       //_healthBarContoller.AddHeart();
        PlayerInfo._maxHealth++;
       // Save();
        OnHealthChanged?.Invoke();
    }

    public void DecreaseHealth()
    {
        _healthBarContoller.RemoveHeart(); 
        PlayerInfo._healthPoint--;
        OnDamageRecieve?.Invoke();

        if (PlayerInfo._healthPoint <= 0)
        {
            Death();
        }
    }

    public void IncreaseDamage(int amount)
    {
        PlayerInfo._damage += amount;
        Save();
        OnDamageChanged?.Invoke();
    }

    public void IncreaseDamageUpgradeCost()
    {
        _damageUpgradeCost += 10;
        OnDamageUpgraderChanged?.Invoke();
    }
    public void Death()
    {
        _objectSpawner.isShouldSpawn = false;
        _objectSpawner.ClearAllFallingObjects();
        if (_gameOverCount == 0)
        {
            _gameOverCount++;
        _continueScreen.SetActive(true);
            DOTween.Sequence()
          .Append(_continueScreen.transform.GetChild(0).DOScale(new Vector3(1, 1, 1), 0.5f))
          .Append(_continueScreen.transform.GetChild(1).DOScale(new Vector3(1, 1, 1), 1.5f));
            return;
        }
        _continueScreen.SetActive(false);
        Save();
        SetToLeaderboard(PlayerInfo._bestScore);
        PlayerInfo._healthPoint = 0;

        _gameOverScoreText.text = PlayerInfo._lastScore.ToString();
        _gameOverScreen.SetActive(true);
        DOTween.Sequence()
            .Append(_gameOverScreen.transform.GetChild(0).DOScale(new Vector3(1, 1, 1), 0.5f))
            .Append(_gameOverScreen.transform.GetChild(1).DOScale(new Vector3(1, 1, 1), 0.5f))
            .Append(_gameOverScreen.transform.GetChild(2).DOScale(new Vector3(1, 1, 1), 0.5f))
            .Append(_gameOverScreen.transform.GetChild(3).DOScale(new Vector3(1, 1, 1), 0.5f));
        _gameplayUI.SetActive(false);

    }

    #endregion

    public void AddPlayedTimes()
    {
        PlayerInfo._playedTimes++;
        Save();

    }
    #region Points
    public void AddPoints(int amount)
    {
        PlayerInfo._lastScore += amount;
        if (PlayerInfo._lastScore > PlayerInfo._bestScore)
        {
            PlayerInfo._bestScore = PlayerInfo._lastScore;

        //    PlayerPrefs.SetInt("BestScore", PlayerInfo._bestScore);
            OnBestScoreChanged?.Invoke();
        }
       // Save();

        //PlayerPrefs.SetInt("LastScore", PlayerInfo._lastScore);
        OnLastScoreChanged?.Invoke();
        //TryAddCoins();

    }

    public void TryAddCoins(Vector3 spawnPosition)
    {
        if (Random.Range(0, 100) < _coinDropChance)
        {
           var coin = Instantiate(_coinPrefab, spawnPosition, Quaternion.identity);
            coin.transform.localScale = Vector3.zero;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(coin.transform.DOScale(new Vector3(1, 1, 1), 0.6f)).Append(coin.transform.DOMove(_skibidiToilet.transform.position, 1f)).Join(coin.transform.DOScale(Vector3.zero, 1)).OnComplete(() => Destroy(coin));
            AddCoins(1);
        }
    }
    public void AddCoins(int amount)
    {
        PlayerInfo._coins += amount;

       // PlayerPrefs.SetInt("Coins", PlayerInfo._coins);
        OnCoinChanged?.Invoke();
    }


    public void AddRewardCoins()
    {
        AddCoins(15);
        Save();
    }
    public bool CheckCoins(int amount)
    {
        if (PlayerInfo._coins >= amount)
            return true;
        else
            return false;
    }
    public void RemoveCoins(int amount)
    {
        PlayerInfo._coins -= amount;
        OnCoinChanged?.Invoke();
        Save();

      //  PlayerPrefs.SetInt("Coins", PlayerInfo._coins);

    }
    #endregion


    public void SetSkin(int id)
    {
        PlayerInfo.currentSkin = id;
        _skinSetter.ChangeSkin(PlayerInfo.currentSkin);
        Save();
    }

    #region Sound
    public void PlayInteractCorrectSound()
    {
        _audioController.PlayCorrectSound();
    }

    public void PlayInteractWrongSound()
    {
        _audioController.PlayBadSound();
    }

    public void PlayClickSound()
    {
        _audioController.PlayClickSound();
    }
    #endregion
    public void AttackEvent()
    {
        OnAttack?.Invoke();
    }
    private int BoolToInt(bool boolean)
    {
        if (boolean)
            return 1;
        else
            return 0;
    }
    private bool IntToBool(int num)
    {
        if (num == 1)
            return true;
        else
            return false;
    }
    public void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonString);
    }

    public void SetPlayerInfo(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
    }

    public bool GetSkinStatus(SkinName skinName)
    {
        switch(skinName)
        {
            case SkinName.Standart:
                return true;

            case SkinName.Alien:
                return PlayerInfo.AlienSkinOpened;

            case SkinName.Blondie:
                return PlayerInfo.BlondieSkinOpened;

            case SkinName.Comp:
                return PlayerInfo.CompSkinOpened;

            case SkinName.GMan:
                return PlayerInfo.GManSkinOpened;

            case SkinName.Robber:
                return PlayerInfo.RobberSkinOpened;

            case SkinName.Horror:
                return PlayerInfo.HorrorSkinOpened;

            case SkinName.TV:
                return PlayerInfo.TVSkinOpened;

            case SkinName.Zombie:
                return PlayerInfo.ZombieSkinOpened;

            default: return false;
        }
    }
    public void SetSkinStatus(SkinName skinName)
    {
        switch (skinName)
        {
            case SkinName.Standart:
                break;

            case SkinName.Alien:
                PlayerInfo.AlienSkinOpened = true;
                break;

            case SkinName.Blondie:
                 PlayerInfo.BlondieSkinOpened = true;
                break;

            case SkinName.Comp:
                 PlayerInfo.CompSkinOpened = true;
                break;

            case SkinName.GMan:
                 PlayerInfo.GManSkinOpened = true;
                break;

            case SkinName.Robber:
                 PlayerInfo.RobberSkinOpened = true;
                break;

            case SkinName.Horror:
                 PlayerInfo.HorrorSkinOpened = true;
                break;

            case SkinName.TV:
                 PlayerInfo.TVSkinOpened = true;
                break;

            case SkinName.Zombie:
                 PlayerInfo.ZombieSkinOpened = true;
                break;

            default: break;
        }
        Save();
    }
    public void SaveUpgradeProgress(Upgraders type, int cost, int upgrades, bool locked)
    {
        switch(type)
        {
            case Upgraders.FirstAttack:
                PlayerInfo.firstDamageCurrentCostID = cost;
                PlayerInfo.firstDamageUpgradesCount = upgrades;
                PlayerInfo.firstDamageIsLocked = locked;
                break;
            case Upgraders.SecondAttack:
                PlayerInfo.secondDamageCurrentCostID = cost;
                PlayerInfo.secondDamageUpgradesCount = upgrades;
                PlayerInfo.secondDamageIsLocked = locked;
                break;
            case Upgraders.ThirdAttack:
                PlayerInfo.thirdDamageCurrentCostID = cost;
                PlayerInfo.thirdDamageUpgradesCount = upgrades;
                PlayerInfo.thirdDamageIsLocked = locked;
                break;
            case Upgraders.Health:
                PlayerInfo.HealthUpgradesCount = upgrades;
                PlayerInfo.HealthIsLocked = locked;
                break;
        }
        Save();
    }
    public void ShowSimpleAd()
    {
        ShowAdv();
    }

    public void TryUnlockSkinByAdv(SkinConditionChecker checker)
    {
        skinConditionChecker = checker;
        UnlockSkinExtern();
        Pause();
    }
    public void TryUpgradeHealth(HealthButtonController controller)
    {
       
        healthController = controller;
        UpgradeHealthExtern();
        Pause();
    }
    private void UpgradeHealth()
    {
        healthController.UpgradeHealth();
    }
    public void SetSkinUnlock()
    {
        skinConditionChecker.AddWatchedAdv();
    }
    public void TryContinue()
    {
        ContinueGameExtern();
        Pause();
    }
    
    public void Continue()
    {
        _continueScreen.SetActive(false);
        _healthBarContoller.SetHealthHearts(PlayerInfo._maxHealth);
        PlayerInfo._healthPoint = PlayerInfo._maxHealth;
        _objectSpawner.isShouldSpawn = true;
        _objectSpawner.StartFalling();
    }

    public void TryAddRewardCoins()
    {
        GetCoinsExtern();
        Pause();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}