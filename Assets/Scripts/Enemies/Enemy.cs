using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement), typeof(Health))]

public class Enemy : MonoBehaviour
{
    private EnemyMovement m_movement;
    private Health m_health;
    public float DealDamageSelfKnockBack = 2;
    public float Damage = 10;

    private void Awake()
    {
        m_movement = gameObject.GetComponent<EnemyMovement>();
        m_health = gameObject.GetComponent<Health>();

        m_movement.Target = GlobalReferenceManager.Instance.Player.transform;

        m_health.IsDying += IsDying;
        m_movement.ContactEvent += ContactEvent;
    }

    private void OnDestroy()
    {
        m_health.IsDying -= IsDying;
        m_movement.ContactEvent -= ContactEvent;
    }

    private void IsDying()
    {
        Destroy(gameObject);
    }

    private void ContactEvent()
    {
        Vector3 movement = transform.forward * DealDamageSelfKnockBack;
        movement.y = 0;
        transform.localPosition -= movement;


        Health targetHealth = m_movement.Target.GetComponent<Health>();
        targetHealth?.DealDamage(Damage);
    }
}
