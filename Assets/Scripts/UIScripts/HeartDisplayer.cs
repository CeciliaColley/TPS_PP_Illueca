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
        UpdateHearts(Player.PlayerLife);
    }

    private void OnEnable()
    {
        Player.OnPlayerLivesChanged += UpdateHearts;
    }

    private void OnDisable()
    {
        Player.OnPlayerLivesChanged += UpdateHearts;
    }

    void UpdateHearts(float playerLife)
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

        float lifeRemainder = playerLife % Player.Instance.GetLifePerHeart();
        float fullHearts = (playerLife - lifeRemainder) / Player.Instance.GetLifePerHeart();
        int heartsCounter;
        
        // Create new hearts
        for (heartsCounter = 0; heartsCounter < fullHearts; heartsCounter++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            newHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2((heartsCounter * (spaceBetweeenHearts + heartWidth)), 0);
        }

        if (lifeRemainder > 0)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            newHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(heartsCounter * (spaceBetweeenHearts + heartWidth), 0);
            Image heartImage = newHeart.GetComponent<Image>();
            if (heartImage != null)
            {
                float fillAmount = (float)lifeRemainder / Player.Instance.GetLifePerHeart();
                heartImage.fillAmount = fillAmount;
            }
        }
    }

    
}

