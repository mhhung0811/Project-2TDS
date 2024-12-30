using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectDB", menuName = "EffectSO/EffectDB")]
public class EffectDB : ScriptableObject
{
	public Effect[] effects;

	[Serializable]
	public class Effect
	{
		public EffectType effectType;
		public GameObject effectPref;
	}

	public GameObject GetEffectPrefab(EffectType effectType)
	{
		foreach (Effect effect in effects)
		{
			if (effect.effectType == effectType)
			{
				return effect.effectPref;
			}
		}
		Debug.LogError("Effect not found in database: " + effectType.effectName);
		return null;
	}
}
