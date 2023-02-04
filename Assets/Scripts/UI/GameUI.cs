using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]

public class GameUI : MonoBehaviour
{
    public UI_Slider HitpointSlider;
    public UI_Slider HungerSlider;

    [SerializeField]
    private Canvas _canvas;

    [Range(0f, 1f)]
    [SerializeField]
    private float _lowThreshold = 0.25f;
    private ScaledRepeatingTimer _timer;
    private bool _down = true;

    private void Awake()
    {
        _timer = gameObject.AddComponent<ScaledRepeatingTimer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (_canvas.worldCamera == null)
            _canvas.worldCamera = GetComponent<Camera>();

        _timer.StartTimer(0.2f);
        _timer.OnTimerCompleted += UpdateThing;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void UpdateThing()
    {
        if (_down)
        {
            UpdateSliderValue(HitpointSlider, HitpointSlider.Slider.value - 0.025f);
            if (HitpointSlider.Slider.value <= 0.01f)
                _down = false;
        }
        else
        {
            UpdateSliderValue(HitpointSlider, HitpointSlider.Slider.value + 0.025f);

            if (HitpointSlider.Slider.value >= 0.99f)
                _down = true;
        }
    }

    private void UpdateSliderValue(UI_Slider sli, float value)
    {
        sli.Slider.value = value;

        if (value < _lowThreshold)
            sli.ImageAnim.SetBool("ShakeOn", true);
        else
            sli.ImageAnim.SetBool("ShakeOn", false);

        if (sli.Fill != null)
            CalcColor(sli.Slider, sli.Fill, sli.Gradient);

    }

    public void CalcColor(Slider slider, Image sliderfill, Gradient gradient)
    {
        sliderfill.color = gradient.Evaluate(slider.value);
    }
}
