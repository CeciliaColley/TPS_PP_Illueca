using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public static PlayerPrompts lastPrompt = PlayerPrompts.DEFAULT;
    private static float _playerLife;
    private static string _playerPrompt;
    private static string _playerSpeech;
    private static bool _hasGun = false;


    private float maxLife;
    private float maxPlayerHearts = 3;
    private float lifePerHeart = 10;

    public static float PlayerLife
    {
        get => _playerLife;
        set
        {
            if (_playerLife != value)
            {
                _playerLife = value;
                OnPlayerLivesChanged?.Invoke(_playerLife);
            }
        }
    }

    public static event Action<float> OnPlayerLivesChanged;

    public static string PlayerPrompt
    {
        get => _playerPrompt;
        set
        {
            _playerPrompt = value;
            OnPromptPlayer?.Invoke(_playerPrompt);
        }
    }

    public static event Action<string> OnPromptPlayer;

    public static string PlayerSpeech
    {
        get => _playerSpeech;
        set
        {
            _playerSpeech = value;
            OnPlayerSpeech?.Invoke(_playerSpeech);
        }
    }

    public static event Action<string> OnPlayerSpeech;

    public static bool HasGun
    {
        get => _hasGun;
        set
        {
            _hasGun = value;
            OnAcquireGun?.Invoke(_hasGun);
        }
    }

    public static event Action<bool> OnAcquireGun;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        maxLife = maxPlayerHearts * lifePerHeart;
        _playerLife = maxLife;
    }

    public float GetLifePerHeart()
    {
        return lifePerHeart;
    }
}

public enum PlayerPrompts
{
    GOTOGARDEN,
    GOTOHOUSE,
    INSPECTBRIDGE,
    PICKUPGUN,
    PUDDLERELOAD,
    DEFAULT
}
