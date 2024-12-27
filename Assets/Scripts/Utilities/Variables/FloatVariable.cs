using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/Float Variable")]
public class FloatVariable : ScriptableObject
{ 
    public float DefaultValue;
    
    private float _currentValue;
    
    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            if (Mathf.Approximately(_currentValue, value)) return;
            _currentValue = value;
            OnChanged?.Invoke(_currentValue);
        }
    }
    
    public Action<float> OnChanged;
    
    public void SetValue(float value)
    {
        _currentValue = value;
    }

    public void SetValue(FloatVariable value)
    {
        _currentValue = value.CurrentValue;
    }

    public void ApplyChange(float amount)
    {
        _currentValue += amount;
    }

    public void ApplyChange(FloatVariable amount)
    {
        _currentValue += amount.CurrentValue;
    }

    private void OnEnable()
    {
        _currentValue = DefaultValue;
    }
}
