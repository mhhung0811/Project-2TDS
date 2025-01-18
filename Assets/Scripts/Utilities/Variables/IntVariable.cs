using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/Int Variable")]
public class IntVariable : ScriptableObject, IResetScene
{
    public int DefaultValue;
    
    private int _currentValue;
	public bool isReset { get; set; } = true;
	public void ResetScene()
	{
		_currentValue = DefaultValue;
	}

	public int CurrentValue
    {
        get => _currentValue;
        set
        {
            if (_currentValue == value) return;
            _currentValue = value;
            OnChanged?.Invoke(_currentValue);
        }
    }
    
    public event Action<int> OnChanged;

    private void OnEnable()
    {
        _currentValue = DefaultValue;
    }
}
