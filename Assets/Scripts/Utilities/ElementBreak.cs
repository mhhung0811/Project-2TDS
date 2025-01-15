using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBreak : MonoBehaviour
{
    private Animator animator;
	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
	void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Break()
    {
        animator.SetBool("IsBreak", true);
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		// Compare tag with "Player" and "Bullet" and "Enemy"
		if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy"))
		{
			Break();
		}
	}
}
