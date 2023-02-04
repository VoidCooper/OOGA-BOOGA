using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform Target;
    public float MovementSpeed = 0.1f;
    public float contactDistanceSqrt = 0.86f;
    public bool CheckContact = false;
    public event System.Action ContactEvent;
    public bool IsFleeing = false;
    public bool IsStopped = false;

    private void Awake()
    {
        Target = GlobalReferenceManager.Instance.MainCamera.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target != null)
        {
            transform.LookAt(Target);

            if (IsStopped)
                return;

            Vector3 movement = transform.forward * MovementSpeed * Time.deltaTime;
            movement.y = 0;

            if (IsFleeing)
                transform.localPosition -= movement;
            else
                transform.localPosition += movement;

            if (Vector3.SqrMagnitude(transform.position - Target.position) < contactDistanceSqrt)
            {
                ContactEvent?.Invoke();
            }
        }
    }
}
