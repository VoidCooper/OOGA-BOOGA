using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{
    private Transform _parent;
    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_parent.position.x, transform.position.y, _parent.position.z);
    }
}
