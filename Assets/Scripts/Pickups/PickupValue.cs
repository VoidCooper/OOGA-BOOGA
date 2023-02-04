using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Food = 0,
    Ammo = 1
}

public class PickupValue : MonoBehaviour
{
    public float ValueAmount = 3f;
    public PickupType pickupType = PickupType.Food;
}
