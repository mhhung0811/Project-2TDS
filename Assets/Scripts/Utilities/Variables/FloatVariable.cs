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

    private void OnEnable()
    {
        _currentValue = DefaultValue;
    }
}
