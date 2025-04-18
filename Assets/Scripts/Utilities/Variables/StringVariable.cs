using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/String Variable")]
public class StringVariable : ScriptableVariable<string>, IResetScene
{
    public bool isReset { get; set; }
    public void ResetScene()
    {
        _currentValue = DefaultValue;
        OnChanged?.Invoke(_currentValue);
    }
}
