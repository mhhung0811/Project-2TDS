using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCenterBoss : MonoBehaviour
{
    public Vector2Variable posCenter;
	public VoidEvent onBossEnter;
	public VoidEvent onBossExit;
	void Start()
    {
        posCenter.CurrentValue = (Vector2)transform.position;
	}

    void Update()
    {
        
    }

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Boss"))
		{
			Void @void = new Void();
			onBossEnter.Raise(@void);
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Boss"))
		{
			Void @void = new Void();
			onBossExit.Raise(@void);
		}
	}
}
