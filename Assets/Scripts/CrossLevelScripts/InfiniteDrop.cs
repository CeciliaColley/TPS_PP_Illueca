using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteDrop : MonoBehaviour
{
    [SerializeField] float fallDamage = 2.5f;
    [SerializeField] GameObject respawnPositionReference;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player") && respawnPositionReference != null)
        {
            Player.PlayerLife -= fallDamage;
            StartCoroutine(ResetPlayersPosition(other.gameObject));
        }
        else
        {
            Debug.Log("The infinite drop didn't work because the object that triggered the collider either didn't have a collider, wasn't tagged as player, or the game object for the respawn position reference wasn't assigned.");
        }
    }

    private IEnumerator ResetPlayersPosition(GameObject player)
    {
        while (player.transform.position.y < respawnPositionReference.transform.position.y-1)
        {
            player.transform.position = new Vector3(respawnPositionReference.transform.position.x, respawnPositionReference.transform.position.y, respawnPositionReference.transform.position.z);
            yield return null;
        }
    }
}
