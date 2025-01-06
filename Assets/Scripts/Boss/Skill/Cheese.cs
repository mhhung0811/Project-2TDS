using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cheese : MonoBehaviour
{
    public Animator animator;
    public GameObject CheeseSummon;
    public int quantityX;
	public int quantityY;
    public float width;
	public float height;

    private float spaceX;
	private float spaceY;

	public GameObject CheeseSlam;
	public int quantityCheese;
	private float spaceAngle;


	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
	void Start()
    {
		spaceX = width / quantityX;
		spaceY = height / quantityY;
		StartCoroutine(SpawnSummonCheese());
	}

    void Update()
    {
        
    }

    public IEnumerator SpawnSummonCheese()
    {
		Vector2 offetRandom = Vector2.zero;
		for(int temp = 0; temp < 3;temp++)
		{

			//Spawn theo chieu ngang
			for (int i = 0; i < quantityX; i++)
			{
				offetRandom = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
				GameObject cheeseSummon1 = Instantiate(CheeseSummon);
				CheeseSummonBullet cheeseSummonBullet1 = cheeseSummon1.GetComponent<CheeseSummonBullet>();
				cheeseSummonBullet1.direction = (new Vector2(-width / 2 + i * spaceX, height / 2) + offetRandom) * -1;
				cheeseSummon1.transform.position = new Vector2(-width / 2 + i * spaceX, height / 2) + (Vector2)this.transform.position + offetRandom;

				offetRandom = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
				GameObject cheeseSummon2 = Instantiate(CheeseSummon);
				CheeseSummonBullet cheeseSummonBullet2 = cheeseSummon2.GetComponent<CheeseSummonBullet>();
				cheeseSummonBullet2.direction = (new Vector2(-width / 2.0f + i * spaceX, -height / 2) + offetRandom) * -1;
				cheeseSummon2.transform.position = new Vector2(-width / 2.0f + i * spaceX, -height / 2) + (Vector2)this.transform.position + offetRandom;
			}

			// Spawn theo chieu doc
			for (int i = 0; i <= quantityY; i++)
			{
				offetRandom = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
				GameObject cheeseSummon1 = Instantiate(CheeseSummon);
				CheeseSummonBullet cheeseSummonBullet1 = cheeseSummon1.GetComponent<CheeseSummonBullet>();
				cheeseSummonBullet1.direction = (new Vector2(width / 2 , -height / 2 + i * spaceY) + offetRandom) * -1;
				cheeseSummon1.transform.position = new Vector2(width / 2 , -height / 2 + i * spaceY) + (Vector2)this.transform.position + offetRandom;

				offetRandom = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
				GameObject cheeseSummon2 = Instantiate(CheeseSummon);
				CheeseSummonBullet cheeseSummonBullet2 = cheeseSummon2.GetComponent<CheeseSummonBullet>();
				cheeseSummonBullet2.direction = (new Vector2(-width / 2, -height / 2 + i * spaceY) + offetRandom) * -1;
				cheeseSummon2.transform.position = new Vector2(-width / 2, -height / 2 + i*spaceY) + (Vector2)this.transform.position + offetRandom;
			}
			yield return new WaitForSeconds(1.5f);
		}

		animator.SetBool("IsSummon", true);
		yield return new WaitForSeconds(1.5f);
		StartCheeseSlam();
	}

	public void StartCheeseSlam()
	{
		animator.SetBool("IsSlam", true);
	}

	public void DestroyGobj()
	{
		Destroy(gameObject);
	}


	public IEnumerator SpawnCheeseSlam()
	{
		Vector2 offetRandom = Vector2.zero;
		spaceAngle = 360 / quantityCheese;
		for (int i = 0; i < quantityCheese; i++)
		{
			offetRandom = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
			GameObject cheeseSlam = Instantiate(CheeseSlam);
			CheeseSlamBullet cheeseSlamBullet = cheeseSlam.GetComponent<CheeseSlamBullet>();
			cheeseSlamBullet.direction = new Vector2(Mathf.Cos(spaceAngle * i * Mathf.Deg2Rad), Mathf.Sin(spaceAngle * i * Mathf.Deg2Rad));
			cheeseSlam.transform.position = (Vector2)this.transform.position + offetRandom;
		}
		yield return null;
	}
	public void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.GetComponent<CheeseSummonBullet>() != null)
        {
            Destroy(collision.gameObject);
		} 
	}
}
