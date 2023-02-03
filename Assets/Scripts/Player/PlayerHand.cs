using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [Header("Projectile variables")]
    public float startVelocity = 5f;
    public float spearThrowCooldown = 0.2f;
    public GameObject spearPrefabObj;
    public Transform projectileSpawner;

    private bool _isThrowOnCooldown;

    public void ShootSpear()
    {
        if (_isThrowOnCooldown)
            return;

        GameObject spearObj = GameObject.Instantiate(spearPrefabObj, projectileSpawner.position, projectileSpawner.rotation);
        spearObj.GetComponent<SpearProjectile>().Launch();

        _isThrowOnCooldown = true;
        Invoke(nameof(ResetThrowCooldown), spearThrowCooldown);
    }

    private void ResetThrowCooldown()
    {
        _isThrowOnCooldown = false;
    }
}
