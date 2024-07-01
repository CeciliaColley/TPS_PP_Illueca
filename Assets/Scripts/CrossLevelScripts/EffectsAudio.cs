using UnityEngine;

public class EffectsAudioManager : MonoBehaviour
{
    public AudioSource effectsAudioSource;
    public AudioClip shootingClip;
    public AudioClip winClip;

    private void Awake()
    {
        if (effectsAudioSource == null)
        {
            effectsAudioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayShootingSound()
    {
        effectsAudioSource.PlayOneShot(shootingClip);
    }
}