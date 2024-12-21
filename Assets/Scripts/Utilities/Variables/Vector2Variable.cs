using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/Vector2 Variable")]
public class Vector2Variable : ScriptableObject
{
	public Vector2 DefaultValue;

	private Vector2 _currentValue;

	public Vector2 CurrentValue
	{
		get => _currentValue;
		set => _currentValue = value;
	}

	public void SetValue(Vector2 value)
	{
		_currentValue = value;
	}

	public void SetValue(Vector2Variable value)
	{
		_currentValue = value.CurrentValue;
	}

	public void SetValue(Vector3 value)
	{
		_currentValue = value;
	}

	private void OnEnable()
	{
		_currentValue = DefaultValue;
	}
}
