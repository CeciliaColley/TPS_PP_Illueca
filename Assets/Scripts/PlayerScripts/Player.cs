using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private static int _playerLives;
    public static int PlayerLives
    {
        get => _playerLives;
        set
        {
            if (_playerLives != value)
            {
                _playerLives = value;
                OnPlayerLivesChanged?.Invoke(_playerLives);
            }
        }
    }

    public static event Action<int> OnPlayerLivesChanged;

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
        _playerLives = 3;
    }
}
