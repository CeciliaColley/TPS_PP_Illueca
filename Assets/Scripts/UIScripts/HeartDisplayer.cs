using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    public GameObject heartPrefab;  // Prefab for the heart image
    public Transform heartContainer;  // Container to hold the heart images
    public float spaceBetweeenHearts = 50;

    private void OnEnable()
    {
        Player.OnPlayerLivesChanged += UpdateHearts;
    }

    private void OnDisable()
    {
        Player.OnPlayerLivesChanged += UpdateHearts;
    }

    void UpdateHearts(int playerLives)
    {
        // Clear existing hearts
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }

        // Create new hearts
        for (int i = 0; i < playerLives; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            newHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * spaceBetweeenHearts, 0);
        }
    }

    
}

