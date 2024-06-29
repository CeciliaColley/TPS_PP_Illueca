using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartContainer;
    [SerializeField] private float spaceBetweeenHearts = 50;

    private float heartWidth;

    private void Start()
    {
        UpdateHearts(Player.PlayerLives);
    }

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

        RectTransform heartRectTransform = heartPrefab.GetComponent<RectTransform>();
        if (heartRectTransform != null)
        {
            heartWidth = heartRectTransform.rect.width;
        }

        // Create new hearts
        for (int i = 0; i < playerLives; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            newHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2((i * (spaceBetweeenHearts + heartWidth)), 0);
        }
    }

    
}

