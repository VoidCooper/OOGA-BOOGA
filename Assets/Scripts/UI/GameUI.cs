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

    [SerializeField]
    private Canvas _canvas;

    private void Awake()
    {
        if (_canvas == null)
        {
            _canvas = GetComponent<Canvas>();

            if (_canvas == null)
                Debug.LogError("GameUI canvas missing");
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (_canvas.worldCamera == null)
            _canvas.worldCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void UpdateSliderValue(Slider slider, float value, Gradient gradient, Image sliderimage = null)
    {
        slider.value = value;

        if (sliderimage != null)
            CalcColor(slider, sliderimage, gradient);

    }

    public void CalcColor(Slider slider, Image sliderfill, Gradient gradient)
    {
        sliderfill.color = gradient.Evaluate(slider.value);
    }
}
