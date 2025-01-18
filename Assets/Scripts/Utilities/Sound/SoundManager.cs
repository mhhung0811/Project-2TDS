using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
	public SoundDB soundDatabase;
	private Dictionary<string, AudioClip[]> _soundDictionary;
	private Queue<AudioSource> _audioSourcePool;
	private AudioSource _audioSource;
	public AudioClip musicBackGround;
	private Coroutine _musicDownCoroutine;

	public override void Awake()
	{
		base.Awake();
		_audioSource = GetComponent<AudioSource>();

		_soundDictionary = new Dictionary<string, AudioClip[]>();
		foreach (Sound sound in soundDatabase.sounds)
		{
			_soundDictionary.Add(sound.key, sound.clips);
		}
	}

	public void Start()
	{
		_audioSourcePool = new Queue<AudioSource>();
		Prepare(5);
		//PlayMusic();
	}

	public void Prepare(int amount = 10)
	{
		for (int i = 0; i < amount; i++)
		{
			GameObject go = new GameObject("AudioSource");
			go.transform.SetParent(transform);
			AudioSource audioSource = go.AddComponent<AudioSource>();
			audioSource.gameObject.SetActive(false);
			_audioSourcePool.Enqueue(audioSource);
		}
	}

	public void PlaySound(string key)
	{
		if (!_soundDictionary.ContainsKey(key))
		{
			Debug.LogWarning("Sound key not found: " + key);
			return;
		}


		if (_soundDictionary.ContainsKey(key))
		{
			AudioClip clip = _soundDictionary[key][Random.Range(0, _soundDictionary[key].Length)];
			if (clip != null)
			{
				if (_audioSourcePool.Count == 0)
				{
					Prepare(5);
				}
				AudioSource audioSource = _audioSourcePool.Dequeue();
				audioSource.gameObject.SetActive(true);
				audioSource.clip = clip;
				audioSource.volume = 0.5f;
				audioSource.Play();
				StartCoroutine(Return(audioSource));
			}
		}
	}

	public void StopAllSounds()
	{
		foreach (AudioSource audioSource in _audioSourcePool)
		{
			audioSource.Stop();
			audioSource.gameObject.SetActive(false);
		}

		_audioSource.Stop();
	}

	public void PauseAllSounds()
	{
		foreach (AudioSource audioSource in _audioSourcePool)
		{
			audioSource.Pause(); 
		}

		_audioSource.Pause();
	}

	public void ResumeAllSounds()
	{
		foreach (AudioSource audioSource in _audioSourcePool)
		{
			if(audioSource.isPlaying == false)
			{
				audioSource.Play();
			}
		}

		_audioSource.Play();
	}

	public void StopMusic()
	{
		_musicDownCoroutine = StartCoroutine(DownToStopMusic());
	}

	public IEnumerator DownToStopMusic()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			if (_audioSource.volume > 0)
			{
				_audioSource.volume -= 0.025f;
			}
			else
			{
				_audioSource.Stop();
				break;
			}
		}
	}

	public void PlayMusic()
	{
		if(_musicDownCoroutine != null)
		{
			StopCoroutine(_musicDownCoroutine);
		}
		_audioSource.clip = musicBackGround;
		_audioSource.loop = true;
		_audioSource.volume = 0f;
		_audioSource.Play();
		StartCoroutine(InCreaseVolume(0.5f));
	}

	public IEnumerator InCreaseVolume(float maxVolume = 1f)
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			if (_audioSource.volume < maxVolume)
			{
				_audioSource.volume += 0.1f;
			}
			else
			{
				_audioSource.volume = maxVolume;
				break;
			}
		}
	}

	public IEnumerator Return(AudioSource audioSource)
	{
		yield return new WaitWhile(() => audioSource.isPlaying);
		audioSource.gameObject.SetActive(false);
		_audioSourcePool.Enqueue(audioSource);
	}
}
