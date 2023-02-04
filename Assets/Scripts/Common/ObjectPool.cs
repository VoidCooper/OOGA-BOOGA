using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> Objects;
    public int PoolSize = 10;

    public bool PreFillPool = false;
    public GameObject PrefillObj;

    private void Awake()
    {
        Objects = new List<GameObject>();
    }

    public GameObject Take()
    {
        foreach (GameObject gameObject in Objects)
        {
            if (!gameObject.activeInHierarchy)
                return gameObject;
        }

        var ob = Instantiate(PrefillObj, transform);
        Objects.Add(ob);
        return ob;
    }
}
