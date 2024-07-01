using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] sceneClips;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneAudio(scene.name);
    }

    private void PlaySceneAudio(string sceneName)
    {
        foreach (AudioClip clip in sceneClips)
        {
            if (clip.name == sceneName)
            {
                audioSource.clip = clip;
                audioSource.Play();
                return;
            }
        }

        Debug.LogWarning($"No audio clip found for scene {sceneName}");
    }
}