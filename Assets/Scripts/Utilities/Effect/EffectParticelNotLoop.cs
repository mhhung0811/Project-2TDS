using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectParticelNotLoop : MonoBehaviour
{
	public float destroyTime; // Giá trị mặc định nếu quên gán
	private ParticleSystem particleSystemEf;

	private void Awake()
	{
		particleSystemEf = GetComponent<ParticleSystem>();
		if (particleSystemEf != null)
		{
			particleSystemEf.Simulate(0, true, true); // Simulate 1 frame để cập nhật hệ thống hạt
			particleSystemEf.Play();
		}
	}

	private void Start()
	{
		StartCoroutine(DestroyAfterTime(destroyTime));
	}

	private IEnumerator DestroyAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
