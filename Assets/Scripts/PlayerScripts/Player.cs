using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private static float _playerLife;
    private static string _playerPrompt;

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
