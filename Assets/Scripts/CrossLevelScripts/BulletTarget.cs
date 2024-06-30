using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    public static float maxLife = 3.0f;
    private float _life;

    private void Awake()
    {
        _life = maxLife;
    }

    public static Action<float> LifeChanged;
    public float Life
    {
        get { return _life; }
        set 
        { 
            _life = value;
            Debug.Log(value);
            LifeChanged?.Invoke(value);
            Debug.Log("Invoking life changed");
        }
    }
}
