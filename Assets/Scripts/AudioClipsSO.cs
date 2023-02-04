using System.Collections;
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
}

[CreateAssetMenu(fileName = "AudioClipsSO", menuName = "ScriptableObjects/AudioClipManager", order = 1)]
public class AudioClipsSO : ScriptableObject
{ 
    public List<AudioClip> player_jump_clips;
    public List<AudioClip> player_hurt_clips;
    public List<AudioClip> player_throw_clips;
    public List<AudioClip> player_eat_clips;
    public List<AudioClip> tiger_roar_clips;
    public List<AudioClip> boar_roar_clips;
    public List<AudioClip> penquin_noot_clips;

    public void PlayRandomAudioClip(Transform sourcePosition, AudioClipType clipType)
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
        }

        AudioSource.PlayClipAtPoint(clip, sourcePosition.position + Vector3.forward * 0.15f);
    }
}
