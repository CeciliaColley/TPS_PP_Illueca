using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

    [SerializeField] private Transform waterSplash;
    [SerializeField] private float speed = 50f;

    private Rigidbody bulletRigidbody;

    private void Awake() 
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() 
    {
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) 
    {
        Instantiate(waterSplash, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}