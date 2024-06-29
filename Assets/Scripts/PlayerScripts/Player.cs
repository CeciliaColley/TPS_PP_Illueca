using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private static float _playerLife;

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
