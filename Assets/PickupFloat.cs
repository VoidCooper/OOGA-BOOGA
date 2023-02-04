using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFloat : MonoBehaviour
{
    public float floatSpeed = 5f;
    public float floatHeight = 10f;
    public float pickupRange = 4f;
    public float pickupSpeed = 5.5f;
    public Transform floatingTransform;

    float curFloatAmount = 0f;
    float curPickupSpeed = 0f;
    Rigidbody rb;
    bool isPickedUp = false;

    Transform playerTransform;
    BoxCollider boxCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

        if (floatingTransform == null)
            floatingTransform = transform;
    }

    private void Start()
    {
        playerTransform = GlobalReferenceManager.Instance.Player?.transform;
    }

    void Update()
    {
        Vector3 playerPos = playerTransform.position;
        if (!isPickedUp)
        {
            curFloatAmount += Time.deltaTime * floatSpeed;

            floatingTransform.Rotate(new Vector3(0f, 1f, 0f));
            floatingTransform.localPosition = new Vector3(0f, Mathf.Sin(curFloatAmount) * floatHeight, 0f);


            if (Vector3.Distance(playerPos, transform.position) < pickupRange)
            {
                StartPickup();
            }
        }
        else
        {
            curPickupSpeed += Time.deltaTime * pickupSpeed;
            transform.position = Vector3.MoveTowards(transform.position, playerPos, curPickupSpeed);
        }   
    }

    public void StartPickup()
    {
        isPickedUp = true;
        boxCollider.isTrigger = true;
        Destroy(rb);
    }
}
