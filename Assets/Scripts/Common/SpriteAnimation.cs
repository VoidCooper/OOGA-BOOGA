using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] AnimationSprites;
    private SpriteRenderer m_renderer;

    public float TimeOnFrame = 1;
    private float m_elapsedTime = 0;
    private int m_currentIndex = 0;

    private void Awake()
    {
        m_renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        m_elapsedTime += Time.deltaTime;

        if (m_elapsedTime >= TimeOnFrame)
        {
            m_elapsedTime = 0;
            m_renderer.sprite = AnimationSprites[m_currentIndex];
            m_currentIndex++;

            if (m_currentIndex == AnimationSprites.Length)
                m_currentIndex = 0;
        }
    }
}
