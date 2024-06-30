using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject scope;

    private void OnEnable()
    {
        Player.OnAcquireGun += ActivateScope;
    }

    private void OnDisable()
    {
        Player.OnAcquireGun -= ActivateScope;
    }

    private void ActivateScope(bool hasGun)
    {
        scope.SetActive(hasGun);
    }
}
