using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/Bool Variable")]
public class BoolVariable : ScriptableVariable<bool>, IResetScene
{
    public bool isReset { get; set; }
    public void ResetScene()
    {
        _currentValue = DefaultValue;
        OnChanged?.Invoke(_currentValue);
    }
}