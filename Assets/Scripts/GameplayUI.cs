using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Slider HitpointSlider;
    public Image HitpointSliderFill;

    public Slider HungerSlider;
    public Image HungerSliderFill;

    public Gradient SliderGradient;

    private int _count = 0;
    private bool _down = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CalcColor()
    {
        HitpointSliderFill.color = SliderGradient.Evaluate(HitpointSlider.value);
    }
}
