using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/Int Variable")]
public class IntVariable : ScriptableObject
{
    public int DefaultValue;
    
    private int _currentValue;
    
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
    
    public void SetValue(int value)
    {
        _currentValue = value;
    }

    public void SetValue(IntVariable value)
    {
        _currentValue = value.CurrentValue;
    }

    public void ApplyChange(int amount)
    {
        _currentValue += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        _currentValue += amount.CurrentValue;
    }

    private void OnEnable()
    {
        _currentValue = DefaultValue;
    }
}
