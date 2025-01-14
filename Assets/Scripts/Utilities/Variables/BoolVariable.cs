using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/Bool Variable")]
public class BoolVariable : ScriptableObject
{
    public bool DefaultValue;
    
    private bool _currentValue;
    
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
    
    public void SetValue(bool value)
    {
        _currentValue = value;
    }

    public void SetValue(BoolVariable value)
    {
        _currentValue = value.CurrentValue;
    }

    private void OnEnable()
    {
        _currentValue = DefaultValue;
    }
}