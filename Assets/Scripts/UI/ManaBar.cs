using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public IntVariable maxMana;
	public IntVariable currentMana;
    public Slider manaBar;

	private void Awake()
	{
		manaBar = GetComponent<Slider>();
	}
	void Start()
    {
        manaBar.maxValue = maxMana.CurrentValue;
		manaBar.value = currentMana.CurrentValue;
		currentMana.OnChanged += SetValueManaBar;
	}

    void Update()
    {
        
    }

	public void SetValueManaBar(int curMana)
	{
		manaBar.value = curMana;
	}
}
