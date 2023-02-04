using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]

public class GameUI : MonoBehaviour
{
    public UI_Slider HitpointSlider;
    public UI_Slider HungerSlider;

    public GameObject ScratchPrefab;

    private ObjectPool _pool;
    private Health _playerHealth;
    private Hunger _playerHunger;

    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private RectTransform _scratchZone;
    [SerializeField]
    private float ScratchMaxSize, ScratchMinSize;
    [SerializeField]
    private TMPro.TextMeshProUGUI _TimerText;

    [Range(0f, 1f)]
    [SerializeField]
    private float _lowThreshold = 0.25f;

    private void Awake()
    {
        _pool = gameObject.AddComponent<ObjectPool>();
        _pool.PrefillObj = ScratchPrefab;
        _pool.PreFillPool = true;
    }

    // Start is called before the first frame update
    private void Start() 
    {
        _playerHealth = GlobalReferenceManager.Instance.Player.GetComponent<Health>();
        _playerHunger = GlobalReferenceManager.Instance.Player.GetComponent<Hunger>();

        if (_canvas.worldCamera == null)
            _canvas.worldCamera = GetComponent<Camera>();

        CalcColor(HungerSlider.Slider, HungerSlider.Fill, HungerSlider.Gradient);
        CalcColor(HitpointSlider.Slider, HitpointSlider.Fill, HitpointSlider.Gradient);
        HealPlayer(_playerHealth.GetNormalizedHealth());
        Hunger(_playerHunger.GetNormalizedHunger());

        _playerHealth.Healed += HealPlayer;
        _playerHealth.TookDamage += DamagePlayer;
        _playerHealth.TookDamageByOtherMeans += DamagePlayerWithoutSratch;
        _playerHealth.IsDying += PlayerDead;
    }

    private void Update()
    {
        Hunger(_playerHunger.CurrentHunger);
        float minutes = Mathf.Round(GameManager.Instance.RemainingTime / 60);
        _TimerText.text = $"{minutes:00}:{GameManager.Instance.RemainingTime - minutes * 60:00}";
    }

    public void DamagePlayer(float normHP)
    {
        UpdateSliderValue(HitpointSlider, normHP);
        SpawnScratch();
    }

    public void DamagePlayerWithoutSratch(float normHP)
    {
        UpdateSliderValue(HitpointSlider, normHP);
    }

    public void HealPlayer(float normHP)
    {
        UpdateSliderValue(HitpointSlider, normHP);
    }

    public void Hunger(float normHunger)
    {
        UpdateSliderValue(HungerSlider, normHunger);
    }

    private void PlayerDead()
    {
        HitpointSlider.Slider.value = 0f;
        _playerHealth.Healed -= HealPlayer;
        _playerHealth.TookDamage -= DamagePlayer;
        _playerHealth.IsDying -= PlayerDead;
    }

    private void SpawnScratch()
    {
        GameObject scratch = _pool.Take();
        bool flipped = Random.Range(1, 10) > 5 ? true : false;
        Vector2 pos = new(Random.Range(_scratchZone.rect.xMin, _scratchZone.rect.xMax), Random.Range(_scratchZone.rect.yMin, _scratchZone.rect.yMax));
        Vector2 size = new(Random.Range(ScratchMinSize, ScratchMaxSize), Random.Range(ScratchMinSize, ScratchMaxSize));

        if (flipped)
            size.x = -size.x;

        scratch.transform.SetParent(_scratchZone.transform);
        scratch.transform.localPosition = pos;
        scratch.transform.localScale = size;
        scratch.SetActive(true);
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
