using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/Vector2 Variable")]
public class Vector2Variable : ScriptableVariable<Vector2>, IResetScene
{
	public bool isReset { get; set; }
	public void ResetScene()
	{
		_currentValue = DefaultValue;
		OnChanged?.Invoke(_currentValue);
	}
}
