using System.Collections.Generic;
using UnityEngine;

public enum AudioClipType
{
    PlayerJump = 0,
    PlayerHurt = 1,
    PlayerThrow = 2,
    PlayerEat = 3,
    TigerRoar = 4,
    BoarRoar = 5,
    PenquinNoot = 6,
    SpearThud = 7,
    SpearHit = 8,
}

public enum SingleAudioClips
{
    GameMusic = 0,
    SuspenseMusic = 1,
    EndThoom = 2,
}

[CreateAssetMenu(fileName = "AudioClipsSO", menuName = "ScriptableObjects/AudioClipManager", order = 1)]
public class AudioClipsSO : ScriptableObject
{
    public TempAudioSource instantiatedAudioSource;

    [Range(0, 1)]
    public float volume;

    public List<AudioClip> player_jump_clips;
    public List<AudioClip> player_hurt_clips;
    public List<AudioClip> player_throw_clips;
    public List<AudioClip> player_eat_clips;
    public List<AudioClip> tiger_roar_clips;
    public List<AudioClip> boar_roar_clips;
    public List<AudioClip> penquin_noot_clips;
    public List<AudioClip> spear_hit_clips;
    public List<AudioClip> spear_thud_clips;
    public AudioClip gameMusicLoop;
    public AudioClip suspenseMusic;
    public AudioClip endThoom;

    public AudioClip GetAudioClip(AudioClipType clipType)
    {
        AudioClip clip;

        switch (clipType)
        {
            default:
            case AudioClipType.PlayerJump:
                clip = player_jump_clips[Random.Range(0, player_jump_clips.Count - 1)];
                break;
            case AudioClipType.PlayerHurt:
                clip = player_hurt_clips[Random.Range(0, player_hurt_clips.Count - 1)];
                break;
            case AudioClipType.PlayerThrow:
                clip = player_throw_clips[Random.Range(0, player_throw_clips.Count - 1)];
                break;
            case AudioClipType.PlayerEat:
                clip = player_eat_clips[Random.Range(0, player_eat_clips.Count - 1)];
                break;
            case AudioClipType.TigerRoar:
                clip = tiger_roar_clips[Random.Range(0, tiger_roar_clips.Count - 1)];
                break;
            case AudioClipType.BoarRoar:
                clip = boar_roar_clips[Random.Range(0, boar_roar_clips.Count - 1)];
                break;
            case AudioClipType.PenquinNoot:
                clip = penquin_noot_clips[Random.Range(0, penquin_noot_clips.Count - 1)];
                break;
            case AudioClipType.SpearThud:
                clip = spear_thud_clips[Random.Range(0, spear_thud_clips.Count - 1)];
                break;
            case AudioClipType.SpearHit:
                clip = spear_hit_clips[Random.Range(0, spear_hit_clips.Count - 1)];
                break;
        }

        return clip;
    }

    public AudioClip GetAudioClip(SingleAudioClips audioClipType)
    {
        AudioClip clip;

        switch (audioClipType)
        {
            default:
            case SingleAudioClips.GameMusic:
                clip = gameMusicLoop;
                break;
            case SingleAudioClips.SuspenseMusic:
                clip = suspenseMusic;
                break;
            case SingleAudioClips.EndThoom:
                clip = endThoom;
                break;
        }

        return clip;
    }

    public void PlayRandomAudioClipAtPoint(Transform sourcePosition, AudioClipType clipType)
    {
        AudioSource.PlayClipAtPoint(GetAudioClip(clipType), sourcePosition.position + Vector3.forward * 0.15f);
    }

    public void PlayRandomAudioClipAtNewAudioSource(Transform sourcePosition, AudioClipType clipType)
    {
        GameObject go = Instantiate(instantiatedAudioSource.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        TempAudioSource audioSource = go.GetComponent<TempAudioSource>();
        audioSource.PlayClip(clipType, volume);
    }

    public void PlayAudioClipAtNewAudioSource(SingleAudioClips selectedAudioClip)
    {
        GameObject go = Instantiate(instantiatedAudioSource.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        TempAudioSource audioSource = go.GetComponent<TempAudioSource>();
        audioSource.PlayClip(GetAudioClip(selectedAudioClip), volume);
    }
}
