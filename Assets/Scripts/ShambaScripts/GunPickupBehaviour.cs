using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupBehaviour : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second")]
    [SerializeField] private float rotationSpeed = 90.0f;
    [Tooltip("The gun in the player's hand")]
    [SerializeField] private GameObject playersGun;
    [Tooltip("The prompter to pick up the gun.")]
    [SerializeField] private GameObject prompter;


    void OnEnable()
    {
        StartCoroutine(Spin());
        Player.OnAcquireGun += PickUpGun;
    }

    private IEnumerator Spin()
    {
        while (true)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }

    public void PickUpGun(bool hasGun)
    {
        playersGun.SetActive(hasGun);
        StopCoroutine(Spin());
        Destroy(prompter);
        Destroy(gameObject);
    }
}
