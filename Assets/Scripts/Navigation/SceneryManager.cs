using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneryManager : MonoBehaviour
{
    /**************************************************************   VARIABLES   *****************************************************************/

    // VARIABLES EXPOSED TO THE EDITOR
    [SerializeField] private Image loadingBarFill;
    [SerializeField] private Canvas loadingScreen;
    [SerializeField] private float fillTime = 0.2f;

    // STATIC VARIABLES
    public static SceneryManager Instance;

    /**************************************************************   METHODS   *****************************************************************/

    // UNITY EXECUTION DEPENDANT METHODS
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        loadingScreen.enabled = false;
    }

    // CLASS METHODS
    public void EnableLoadScreen()
    {
        loadingBarFill.fillAmount = 0;
        loadingScreen.enabled = true;
    }

    public void DisableLoadScreen()
    {
        loadingScreen.enabled = false;
    }

    public IEnumerator ModifyFillBar(float targetFillAmount)
    {
        float elapsedTime = 0f;
        float startingFillAmount = loadingBarFill.fillAmount;
        targetFillAmount += startingFillAmount;

        while (elapsedTime < fillTime)
        {
            Debug.Log(loadingBarFill.fillAmount);
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fillTime;
            loadingBarFill.fillAmount = Mathf.Lerp(startingFillAmount, targetFillAmount, progress);
            yield return null;
        }

        loadingBarFill.fillAmount = targetFillAmount;

        if (loadingBarFill.fillAmount >= 1)
        {
            LevelManager.Instance.LevelLoaded = true;
            DisableLoadScreen();
        }
    }

}
