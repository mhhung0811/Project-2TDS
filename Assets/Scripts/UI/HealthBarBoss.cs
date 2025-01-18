using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss : MonoBehaviour
{
	public FloatVariable maxHealth;
	public FloatVariable currentHealth;
    public Slider healthBar;
	private void Awake()
	{
        healthBar = GetComponent<Slider>();
		
	}
	void Start()
    {
		healthBar.maxValue = maxHealth.CurrentValue;
		healthBar.value = currentHealth.CurrentValue;
		currentHealth.OnChanged += SetValueHealthBar;
	}

    void Update()
    {
        
    }

    void OnDestroy()
    {
	    maxHealth.OnChanged -= SetValueHealthBar;
	    currentHealth.OnChanged -= SetValueHealthBar;
    }

    public void SetValueHealthBar(float currentHp)
    {
		healthBar.value = currentHp;
	}
}
