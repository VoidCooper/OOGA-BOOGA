using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartText : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameMaster.Instance.StartTheGameAlready();
        gameObject.SetActive(false);
    }
}
