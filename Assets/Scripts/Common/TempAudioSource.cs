using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAudioSource : MonoBehaviour
{
    public float LifeTime = 5f;

    public AudioClipsSO audioClipsSO;

    float curLifeTime = 0f;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke(nameof(DestroyMe), LifeTime);
    }

    public void PlayClip(AudioClipType clipType, float volume, bool hasRandomPitch = false)
    {
        PlayClip(audioClipsSO.GetAudioClip(clipType), volume, hasRandomPitch);
    }

    public void PlayClip(AudioClip audioClip, float volume, bool hasRandomPitch = false)
    {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = hasRandomPitch ? Random.Range(0.9f, 1.1f) : 1f;
        LifeTime = 2f + audioSource.clip.length;
        audioSource.Play();
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
