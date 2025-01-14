using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
	public string key;
	public AudioClip[] clips;
}

[CreateAssetMenu(fileName = "SoundDB", menuName = "Utilities/SoundDB", order = 1)]
public class SoundDB : ScriptableObject
{
	public Sound[] sounds;

	public AudioClip GetClip(string key)
	{
		foreach (Sound sound in sounds)
		{
			if (sound.key == key)
			{
				return sound.clips[Random.Range(0, sound.clips.Length)];
			}
		}
		return null;
	}
}
