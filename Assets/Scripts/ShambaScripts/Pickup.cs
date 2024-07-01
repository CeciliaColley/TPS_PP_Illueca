using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second")]
    [SerializeField] private float rotationSpeed = 90.0f;
    
    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        ThirdPersonShooterController player = other.GetComponent<ThirdPersonShooterController>();
        if (player != null)
        {
            GardenInput.pickUp = gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ThirdPersonShooterController player = other.GetComponent<ThirdPersonShooterController>();
        if (player != null)
        {
            GardenInput.pickUp = null;
        }
    }
}
