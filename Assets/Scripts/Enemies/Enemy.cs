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
    public GameObject pickupObject;
    public GameObject BloodParticles;
    public AudioClipsSO audioClipsSO;
    public AudioClipType audioType = AudioClipType.None;

    private float m_currentDelayForAudio = 5f;

    private void Awake()
    {
        m_movement = gameObject.GetComponent<EnemyMovement>();
        m_health = gameObject.GetComponent<Health>();
        m_animator = transform.GetChild(0).GetComponent<SpriteAnimation>();
        m_currentDelayForAudio += Random.Range(2f, 15f);
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

        m_currentDelayForAudio -= Time.deltaTime;
        if (m_currentDelayForAudio < 0)
        {
            m_currentDelayForAudio += Random.Range(20f, 40f);
            audioClipsSO.PlayRandomAudioClipAtPoint(transform, audioType);
        }
    }

    private void TookDamage(float value)
    {
        HurtTime = 0.25f;
        m_animator.OverrideSprite = HurtSprite;
        m_movement.IsStopped = true;
        Instantiate(BloodParticles, transform.position, Quaternion.identity);
    }


    private void IsDying()
    {
        if (pickupObject != null)
        {
            GameObject.Instantiate(pickupObject, transform.position, Quaternion.identity);
        }

        Instantiate(BloodParticles, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void ContactEvent()
    {
        Vector3 movement = transform.forward * DealDamageSelfKnockBack;
        movement.y = 0;
        transform.localPosition -= movement;
        Health targetHealth = m_movement.Target.GetComponent<Health>();
        targetHealth?.DealDamage(Damage);
    }

    public void SpwanInit()
    {
        m_health.CurrentHealth = m_health.MaxHealth;

        int childCount = transform.childCount;

        for (int i = childCount - 1; i > 0; i--)
        {
            Transform child = transform.GetChild(i);
            if (child.tag == "Projectile")
                Destroy(child.gameObject);
        }
    }

    public void StartFleeing()
    {
        m_movement.IsFleeing = true;
        m_movement.MovementSpeed *= 2;
        m_animator.TimeOnFrame /= 2;
    }
}
