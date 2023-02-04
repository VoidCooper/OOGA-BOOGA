using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearProjectile : MonoBehaviour
{
    public float startSpeed = 5.0f;
    public float maxLifeTime = 10f;
    public float damage = 50f;
    public bool isPearcing = false;

    private Rigidbody rb;
    private BoxCollider boxCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

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
            Destroy(rb);
            boxCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.SendMessage("DealDamage", damage);

            if (!isPearcing)
            {
                transform.parent = collider.transform;
                Destroy(rb);
                boxCollider.enabled = false;
            }
        }
    }
}
