using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupBehaviour : MonoBehaviour
{
    [Tooltip("The gun in the player's hand")]
    [SerializeField] private GameObject playersGun;
    [Tooltip("The prompter to pick up the gun.")]
    [SerializeField] private GameObject prompter;


    void OnEnable()
    {
        Player.OnAcquireGun += PickUpGun;
    }

    private void OnDisable()
    {
        Player.OnAcquireGun -= PickUpGun;
    }

    public void PickUpGun(bool hasGun)
    {
        playersGun.SetActive(hasGun);
        Destroy(prompter);
    }
}
