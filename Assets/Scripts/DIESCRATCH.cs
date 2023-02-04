using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIESCRATCH : MonoBehaviour
{
    public SpriteAnimation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.AnimationEnded += Die;
    }

    void Die()
    {
        anim.Stopped = false;
        gameObject.SetActive(false);
    }
}
