using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second")]
    [SerializeField] private float rotationSpeed = 90.0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        while (true)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }

    public void PickUpGun()
    {
        StopCoroutine(Spin());
        gameObject.SetActive(false);
    }
}
