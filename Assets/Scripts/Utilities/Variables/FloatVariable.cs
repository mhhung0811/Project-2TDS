using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/Float Variable")]
public class FloatVariable : ScriptableVariable<float>, IResetScene
{ 
    public override float CurrentValue
    {
        get => _currentValue;
        set
        {
            if (Mathf.Approximately(_currentValue, value)) return;
            _currentValue = value;
            OnChanged?.Invoke(_currentValue);
        }
    }
    
    public bool isReset { get; set; }
    public void ResetScene()
    {
        _currentValue = DefaultValue;
        OnChanged?.Invoke(_currentValue);
    }
}
