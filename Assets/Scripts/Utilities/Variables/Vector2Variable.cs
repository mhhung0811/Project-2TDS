using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables/Vector2 Variable")]
public class Vector2Variable : ScriptableObject, IResetScene
{
	public Vector2 DefaultValue;

	private Vector2 _currentValue;

	public bool isReset { get; set; } = true;
	public void ResetScene()
	{
		_currentValue = DefaultValue;
	}

	public Vector2 CurrentValue
	{
		get => _currentValue;
		set
		{
			if (_currentValue == value) return;
			_currentValue = value;
			OnChanged?.Invoke(_currentValue);
		}
	}

	public Action<Vector2> OnChanged;

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
