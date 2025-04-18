using System;
using UnityEngine;

public class ScriptableVariable<T> : ScriptableObject
{
    public T DefaultValue;
    protected T _currentValue;
    
    public virtual T CurrentValue
    {
        get => _currentValue;
        set
        {
            if (Equals(_currentValue, value)) return;
            _currentValue = value;
            OnChanged?.Invoke(_currentValue);
        }
    }
    
    public Action<T> OnChanged;
    
    protected virtual void OnEnable()
    {
        _currentValue = DefaultValue;
    }
}