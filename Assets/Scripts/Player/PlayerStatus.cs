using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public AudioClipsSO audioClipsSo;

    Health health;
    Hunger hunger;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.TookDamage += TakeDamage;

        hunger = GetComponent<Hunger>();
        hunger.TookFood += EatMeat;
    }

    private void TakeDamage(float value)
    {
        audioClipsSo.PlayRandomAudioClipAtNewAudioSource(transform, AudioClipType.PlayerHurt);
    }

    private void EatMeat()
    {
        audioClipsSo.PlayRandomAudioClipAtNewAudioSource(transform, AudioClipType.PlayerEat);
    }
}
