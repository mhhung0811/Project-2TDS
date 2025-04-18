using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/Int Variable")]
public class IntVariable : ScriptableVariable<int>, IResetScene
{
    public bool isReset { get; set; }
    public void ResetScene()
    {
        _currentValue = DefaultValue;
        OnChanged?.Invoke(_currentValue);
    }
}
