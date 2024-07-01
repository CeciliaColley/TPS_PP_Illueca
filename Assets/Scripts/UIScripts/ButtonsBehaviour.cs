using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsBehaviour : MonoBehaviour
{
    public GameObject creditsCanvas;
    public GameObject settingsCanvas;
    public GameObject controlCanvas;
    public Slider audioSlider1;
    public Slider audioSlider2;
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    public void LoadLevel(string sceneToLoad)
    {
        LevelManager.Instance.ChangeLevel(sceneToLoad);
    }

    public void ShowCredits()
    {
        if (creditsCanvas != null)
        {
            creditsCanvas.SetActive(true);
        }
        if (settingsCanvas != null && settingsCanvas.activeSelf == true)
        {
            settingsCanvas.SetActive(false);
        }
        if (controlCanvas != null && controlCanvas.activeSelf == true)
        {
            controlCanvas.SetActive(false);
        }
    }

    public void Close()
    {
        if (creditsCanvas != null && creditsCanvas.activeSelf == true)
        {
            creditsCanvas.SetActive(false);
        }
        if (settingsCanvas != null && settingsCanvas.activeSelf == true)
        {
            settingsCanvas.SetActive(false);
        }
        if (controlCanvas != null && controlCanvas.activeSelf == true)
        {
            controlCanvas.SetActive(false);
        }
    }

    public void ShowSettings()
    {
        if (creditsCanvas != null)
        {
            creditsCanvas.SetActive(false);
        }
        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(true);
        }
        if (controlCanvas != null && controlCanvas.activeSelf == true)
        {
            controlCanvas.SetActive(false);
        }
    }

    public void ShowControls()
    {
        if (creditsCanvas != null)
        {
            creditsCanvas.SetActive(false);
        }
        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false);
        }
        if (controlCanvas != null)
        {
            controlCanvas.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        // This is needed to stop the game in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void Start()
    {
        if (audioSlider1 != null && audioSource1 != null)
        {
            audioSlider1.value = audioSource1.volume;
            audioSlider1.onValueChanged.AddListener(delegate { SetVolume(audioSource1, audioSlider1.value); });
        }

        if (audioSlider2 != null && audioSource2 != null)
        {
            audioSlider2.value = audioSource2.volume;
            audioSlider2.onValueChanged.AddListener(delegate { SetVolume(audioSource2, audioSlider2.value); });
        }
    }

    public void SetVolume(AudioSource audioSource, float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
