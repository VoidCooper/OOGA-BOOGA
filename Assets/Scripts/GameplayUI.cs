using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Slider HitpointSlider;
    public Image HitpointSliderFill;
    public Gradient HitpointGradient;

    private int _count = 0;
    private bool _down = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_count > 30 )
        {
            _count = 0;

            if (_down)
            {
                HitpointSlider.value -= 0.05f;
                if (HitpointSlider.value <= 0)
                {
                    _down = false;
                    HitpointSlider.value = 0;
                }
            }
            else
            {
                HitpointSlider.value += 0.05f;
                if (HitpointSlider.value >= 1)
                {
                    _down = true;
                    HitpointSlider.value = 1;
                }
            }
        }
        _count++;
    }

    public void CalcColor()
    {
        HitpointSliderFill.color = HitpointGradient.Evaluate(HitpointSlider.value);
    }
}
