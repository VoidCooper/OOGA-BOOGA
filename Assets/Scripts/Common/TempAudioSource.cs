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

    public void PlayClip(AudioClipType clipType, float volume)
    {
        audioSource.clip = audioClipsSO.GetAudioClip(clipType);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
