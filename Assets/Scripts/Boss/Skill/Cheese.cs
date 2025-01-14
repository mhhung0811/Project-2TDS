using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cheese : MonoBehaviour
{
    public Animator animator;
	public SpriteRenderer spriteRenderer;
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
	private bool isPreSummon = false;


	private void Awake()
	{
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
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
		SoundManager.Instance.PlaySound("BossCheeseSummon");
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
		SoundManager.Instance.PlaySound("BossJumpPreSlam");
		spriteRenderer.sortingOrder = 2;
		animator.SetBool("IsSlam", true);
		//StartCoroutine(SoundExplodeDelay(0.2f));
	}

	public IEnumerator SoundExplodeDelay(float time)
	{
		yield return new WaitForSeconds(time);
		SoundManager.Instance.PlaySound("BossCheeseSlam");
	}

	public void DestroyGobj()
	{
		Destroy(gameObject);
	}


	public IEnumerator SpawnCheeseSlam()
	{
		SoundManager.Instance.PlaySound("BossCheeseSlam");
		Vector2 offetRandom = Vector2.zero;
		spaceAngle = 360f / quantityCheese;
		for (int i = 0; i < quantityCheese ; i++)
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
			if (!isPreSummon)
			{
				isPreSummon = true;
				StartCoroutine(SetAnimationPreSummon());
			}
		} 
	}

	public IEnumerator SetAnimationPreSummon()
	{
		isPreSummon = true;
		yield return new WaitForSeconds(0.1f);
		animator.SetBool("IsPreSummon", true);
	}
}
