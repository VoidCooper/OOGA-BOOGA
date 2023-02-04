using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferenceRegisterer : MonoBehaviour
{
    private void Awake()
    {
        GlobalReferenceManager.Instance.Player = gameObject;
    }
}
