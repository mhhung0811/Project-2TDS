using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/Bool Variable")]
public class BoolVariable : ScriptableObject, IResetScene
{
    public bool DefaultValue;
    
    private bool _currentValue;
	public bool isReset { get; set; } = true;
	public void ResetScene()
	{
		_currentValue = DefaultValue;
	}

	public bool CurrentValue
    {
        get => _currentValue;
        set
        {
            if (_currentValue == value) return;
            _currentValue = value;
            OnChanged?.Invoke(_currentValue);
        }
    }
    
    public Action<bool> OnChanged;

    private void OnEnable()
    {
        _currentValue = DefaultValue;
    }
}