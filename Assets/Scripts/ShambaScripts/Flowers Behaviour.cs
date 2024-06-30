using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FlowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject flowers;
    [SerializeField] private float spawnDuration = 3.0f;
    [SerializeField] private float secondsBeforeFollowPlayer = 1.0f;
    public static Action Spawned;

    private void OnEnable()
    {
        Player.OnAcquireGun += SpawnFlowers;
    }

    private void OnDisable()
    {
        Player.OnAcquireGun -= SpawnFlowers;
    }

    private void SpawnFlowers(bool hasGun)
    {
        StartCoroutine(RepositionAndNavigateCoroutine());
    }

    private IEnumerator RepositionAndNavigateCoroutine()
    {
        if (flowers == null)
        {
            Debug.LogError("Flowers not assigned!");
            yield break;
        }

        flowers.SetActive(true);

        Vector3 startPosition = flowers.transform.position;  // Store the initial position
        float elapsed = 0f;
        while (elapsed < spawnDuration)
        {
            flowers.transform.position = Vector3.Lerp(startPosition, gameObject.transform.position, elapsed / spawnDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        flowers.transform.position = gameObject.transform.position;

        yield return new WaitForSeconds(secondsBeforeFollowPlayer);
        OnSpawnEnded();
    }

    private void OnSpawnEnded()
    {
        Spawned?.Invoke();
    }
}
