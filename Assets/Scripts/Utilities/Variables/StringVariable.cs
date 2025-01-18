using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/String Variable")]
public class StringVariable : ScriptableObject, IResetScene
{
    public string DefaultValue;
    
    private string _currentValue;
	public bool isReset { get; set; } = true;
	public void ResetScene()
	{
		_currentValue = DefaultValue;
	}

	public string CurrentValue
    {
        get => _currentValue;
        set
        {
            if (_currentValue == value) return;
            _currentValue = value;
            OnChanged?.Invoke(_currentValue);
        }
    }
    
    public Action<string> OnChanged;

    private void OnEnable()
    {
        _currentValue = DefaultValue;
    }
}
