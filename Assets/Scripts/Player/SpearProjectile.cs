using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearProjectile : MonoBehaviour
{
    public float startSpeed = 5.0f;
    public float maxLifeTime = 1f;

    void Start()
    {
        Launch();
        Invoke(nameof(DestroyItself), maxLifeTime);
    }

    public void Launch()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * startSpeed, ForceMode.Impulse);
    }

    private void DestroyItself()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Ground")
        {
            DestroyItself();
        }
    }
}
