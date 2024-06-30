using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float maxAmmo;
    private float _ammo;

    private void Awake()
    {
        _ammo = maxAmmo;
    }

    public Action<float> AmmoChange;

    public float Ammo
    {
        get { return _ammo; }
        set
        {
            _ammo = value;
            AmmoChange?.Invoke(_ammo);
        }
    }

    public void Refill()
    {
        Ammo = maxAmmo;
    }

}
