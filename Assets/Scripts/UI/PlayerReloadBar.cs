using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReloadBar : MonoBehaviour
{
    private Slider _reloadBar;
    public Player player;
    
    public FloatVariable reloadTimeMax;
    public FloatVariable reloadTimeCurrent;
    public BoolVariable isReloading;

    private void Awake()
    {
        _reloadBar = GetComponent<Slider>();
        reloadTimeMax.OnChanged += UpdateSliderMax;
        reloadTimeCurrent.OnChanged += UpdateSliderCurrent;
        isReloading.OnChanged += UpdateSliderActive;
        
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!player.IsFacingRight)
        {
            this.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, -180, 0); 
        }
        else
        {
            GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0); 
        }
    }
    
    private void OnDestroy()
    {
        reloadTimeMax.OnChanged -= UpdateSliderMax;
        reloadTimeCurrent.OnChanged -= UpdateSliderCurrent;
        isReloading.OnChanged -= UpdateSliderActive;
    }
    
    private void UpdateSliderActive(bool value)
    {
        // Debug.Log("UpdateSliderActive");
        gameObject.SetActive(value);
    }

    private void UpdateSliderMax(float value)
    {
        // Debug.Log("UpdateSliderMax");
        _reloadBar.maxValue = value;
    }
    
    private void UpdateSliderCurrent(float value)
    {
        // Debug.Log("UpdateSliderCurrent");
        _reloadBar.value = value;
    }
}