using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement), typeof(Health))]

public class Enemy : MonoBehaviour
{
    private EnemyMovement m_movement;
    private Health m_health;
    private SpriteAnimation m_animator;
    public float DealDamageSelfKnockBack = 2;
    public float Damage = 10;
    public Sprite HurtSprite;
    public float HurtTime = 0;

    private void Awake()
    {
        m_movement = gameObject.GetComponent<EnemyMovement>();
        m_health = gameObject.GetComponent<Health>();
        m_animator = transform.GetChild(0).GetComponent<SpriteAnimation>();
    }

    private void Start()
    {
        m_movement.Target = GlobalReferenceManager.Instance.Player?.transform;

        if (m_movement.Target == null)
            m_movement.enabled = false;

        m_health.IsDying += IsDying;
        m_movement.ContactEvent += ContactEvent;
        m_health.TookDamage += TookDamage;
    }

    private void OnDestroy()
    {
        m_health.IsDying -= IsDying;
        m_movement.ContactEvent -= ContactEvent;
        m_health.TookDamage -= TookDamage;
    }

    private void Update()
    {
        if (HurtTime > 0)
        {
            HurtTime -= Time.deltaTime;
        }

        if (m_animator && m_animator.OverrideSprite != null && HurtTime <= 0)
        {
            m_movement.IsStopped = false;
            m_animator.OverrideSprite = null;
        }
    }

    private void TookDamage()
    {
        HurtTime = 0.25f;
        m_animator.OverrideSprite = HurtSprite;
        m_movement.IsStopped = true;
    }


    private void IsDying()
    {
        gameObject.SetActive(false);
    }

    private void ContactEvent()
    {
        Vector3 movement = transform.forward * DealDamageSelfKnockBack;
        movement.y = 0;
        transform.localPosition -= movement;
        m_health.DealDamage(1);
        Health targetHealth = m_movement.Target.GetComponent<Health>();
        targetHealth?.DealDamage(Damage);
    }

    public void SpwanInit()
    {
        m_health.CurrentHealth = m_health.MaxHealth;
    }
}
