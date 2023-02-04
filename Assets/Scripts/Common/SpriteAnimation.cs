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

    public Sprite OverrideSprite;
    public bool Loop = true;
    public bool Stopped = false;

    public event System.Action AnimationEnded;

    private void Awake()
    {
        m_renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Stopped)
        {
            return;
        }

        if (OverrideSprite != null)
        {
            m_renderer.sprite = OverrideSprite;
            return;
        }

        m_elapsedTime += Time.deltaTime;

        if (m_elapsedTime >= TimeOnFrame)
        {
            m_elapsedTime = 0;
            m_renderer.sprite = AnimationSprites[m_currentIndex];
            m_currentIndex++;

            if (m_currentIndex == AnimationSprites.Length)
            {
                if (Loop)
                    m_currentIndex = 0;
                else if (!Stopped)
                {
                    Stopped = true;
                    AnimationEnded?.Invoke();
                    m_currentIndex = AnimationSprites.Length - 1;
                }
            }
        }
    }
}
