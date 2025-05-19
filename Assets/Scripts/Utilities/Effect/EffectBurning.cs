using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBurning : MonoBehaviour
{
    private Animator animator;
    public Transform Ground;
    public float type;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        //type = Random.Range(0, 3);
	}

    void Start()
    {
        animator.SetFloat("Type", type);
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
}
