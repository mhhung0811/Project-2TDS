using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBurning : MonoBehaviour
{
    private Animator animator;
    public Transform Ground;
    public float type;
    public float timeBurning;
	private void Awake()
    {
        animator = GetComponent<Animator>();
	}

    void Start()
    {
        animator.SetFloat("Type", type);
        StartCoroutine(Burning(timeBurning));
	}

    void Update()
    {

    }

    public void FinishStateStart()
    {
        animator.SetBool("IsStarted", true);
    }
    
    public void FinishStateEnd()
    {
		Destroy(this.gameObject);
	}

    public IEnumerator Burning(float time)
	{
		yield return new WaitForSeconds(time);
		animator.SetBool("IsEnd", true);
		
	}
}
