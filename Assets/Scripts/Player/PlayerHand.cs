using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [Header("Projectile variables")]
    public float startVelocity = 5f;
    public float spearThrowCooldown = 0.2f;
    public float spearThrowDelay = 0.02f;
    public GameObject spearPrefabObj;
    public Transform projectileSpawner;
    public GameObject handModel;

    public AudioClipsSO audioClipsSo;

    private bool _isThrowOnCooldown;
    private Animator _animationComponent;
    private bool _disabled = false;

    public void Awake()
    {
        _animationComponent = handModel.GetComponent<Animator>();
    }

    public void ShootSpear()
    {
        if (_disabled)
            return;

        if (_isThrowOnCooldown)
            return;

        _animationComponent.SetTrigger("ThrowSpearTrigger");

        audioClipsSo.PlayRandomAudioClip(transform, AudioClipType.PlayerThrow);

        _isThrowOnCooldown = true;
        Invoke(nameof(InstantiateSpear), spearThrowDelay);
        Invoke(nameof(ResetThrowCooldown), spearThrowCooldown);
    }

    public void Disable()
    {
        _disabled = true;
    }

    private void InstantiateSpear()
    {
        GameObject spearObj = GameObject.Instantiate(spearPrefabObj, projectileSpawner.position, projectileSpawner.rotation);
        spearObj.GetComponent<SpearProjectile>().Launch();
    }

    private void ResetThrowCooldown()
    {
        _isThrowOnCooldown = false;
    }
}
