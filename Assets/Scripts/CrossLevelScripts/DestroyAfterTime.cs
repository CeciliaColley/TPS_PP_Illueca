using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float seconds;

    private void Awake()
    {
        StartCoroutine(WaitThenDie());
    }

    private IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);

    }
}