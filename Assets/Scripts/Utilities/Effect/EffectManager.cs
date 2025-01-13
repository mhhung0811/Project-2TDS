using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
	public EffectDB effectDatabase;

	public override void Awake()
	{
		base.Awake();
		Application.targetFrameRate = 60;
	}

	public GameObject PlayEffect(EffectType effectType, Vector3 position, Quaternion rotation)
	{
		GameObject prefab = effectDatabase.GetEffectPrefab(effectType);
		if (prefab != null)
		{
			return Instantiate(prefab, position, rotation);
		}
		return null;
	}
}

