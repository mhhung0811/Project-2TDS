using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
	public Material dissolveMaterial;
    public float dissolveTime = 0.75f;
	public float outlineThickness = 0.1f;

	private int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");
    private int _horizontalDissolve = Shader.PropertyToID("_HorizontalDissolve");
	private int _outLineThickness = Shader.PropertyToID("_OutlineThickness");

	private IEnumerator Vanish(bool useDissolve, bool useHorizontal)
    {
		dissolveMaterial.SetFloat(_outLineThickness, outlineThickness);
		float elcapsetime = 0;
        while (elcapsetime < dissolveTime)
        {
            elcapsetime += Time.deltaTime;

            float lerpDissvolve = Mathf.Lerp(0f, 1f, elcapsetime / dissolveTime);
            float lerpHorizontal = Mathf.Lerp(0f, 1.1f, elcapsetime / dissolveTime);

            if(useDissolve)
				dissolveMaterial.SetFloat(_dissolveAmount, lerpDissvolve);

			if (useHorizontal)
				dissolveMaterial.SetFloat(_horizontalDissolve, lerpHorizontal);

			yield return null;
		}
    }

	private IEnumerator Appear(bool useDissolve, bool useHorizontal)
	{
		dissolveMaterial.SetFloat(_outLineThickness, outlineThickness);
		float elcapsetime = 0;
		while (elcapsetime < dissolveTime)
		{
			elcapsetime += Time.deltaTime;

			float lerpDissvolve = Mathf.Lerp(1f, 0f, elcapsetime / dissolveTime);
			float lerpHorizontal = Mathf.Lerp(1.1f, 0f, elcapsetime / dissolveTime);

			if (useDissolve)
				dissolveMaterial.SetFloat(_dissolveAmount, lerpDissvolve);

			if (useHorizontal)
				dissolveMaterial.SetFloat(_horizontalDissolve, lerpHorizontal);

			yield return null;
		}
		Debug.Log(elcapsetime);
	}

	public void Vanish()
	{
		StartCoroutine(Vanish(false, true));
	}

	public void Appear()
	{
		StartCoroutine(Appear(false, true));
	}
}
