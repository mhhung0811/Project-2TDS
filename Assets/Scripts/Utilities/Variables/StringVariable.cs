using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/String Variable")]
public class StringVariable : ScriptableObject
{
    public string DefaultValue;
    
    private string _currentValue;

    public string CurrentValue
    {
        get => _currentValue;
        set => _currentValue = value;
    }

    private void OnEnable()
    {
        _currentValue = DefaultValue;
    }
}
