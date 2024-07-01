using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    protected float maxLife = 3.0f;
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
            if (_life <= 0)
            {
                _life = 0;
                Die();
            }
            LifeChanged?.Invoke(value);
        }
    }
    public float MaxLife
    {
        get { return maxLife; }
    }

    protected virtual void Die()
    {
        // Throw an exception if not overridden
        throw new NotImplementedException("Die method not implemented in derived class.");
    }
}
