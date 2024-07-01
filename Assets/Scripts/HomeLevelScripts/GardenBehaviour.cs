using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> happyFlowers = new List<GameObject>();
    [SerializeField] private List<GameObject> sadFlowers = new List<GameObject>();

    private void Start()
    {
        if (Player.Instance != null && Player.Instance.hasWatered)
        {
            ActivateFlowers(happyFlowers, true);
            ActivateFlowers(sadFlowers, false);
        }
    }

    private void ActivateFlowers(List<GameObject> flowers, bool activeState)
    {
        if (flowers.Count > 0)
        {
            foreach (GameObject flower in flowers)
            {
                flower.SetActive(activeState);
            }
        }
    }
}
