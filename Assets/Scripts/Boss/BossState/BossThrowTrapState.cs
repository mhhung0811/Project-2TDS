using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowTrapState : BossState
{
	private int quantityX = 4;
	private int quantityY = 4;
	private float width = 14f;
	private float height = 8f;
	private float spaceX;
	private float spaceY;
	public BossThrowTrapState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		//Boss.Animator.SetBool("IsThrowTrap", true);
		Boss.Animator.SetBool("IsMove", true);
		SoundManager.Instance.PlaySound("BossThrowTrap");
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsThrowTrap", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();

		// Neu da quang trap thi khong quang nua
		if (Boss.isUsedThrowTrap)
		{
			return;
		}

		if (Boss.isStayPosCenter && !Boss.isUsedThrowTrap)
		{
			Boss.RB.velocity = Vector2.zero;
			Boss.isUsedThrowTrap = true;
			Boss.StartCoroutine(ThrowTrap());
		}
		else
		{
			Boss.MoveToPosCenter();
			Boss.UpdateAnimationByPosCenter();
		}
	}

	public override void AnimationTriggerEvent(Boss.AnimationTriggerType triggerType)
	{

	}

	public IEnumerator ThrowTrap()
	{
		Boss.Animator.SetBool("IsMove", false);
		Boss.Animator.SetBool("IsThrowTrap", true);
		spaceX = width / quantityX;
		spaceY = height / quantityY;

		Vector2 offetRandom = Vector2.zero;
		// Spawn theo chieu ngang
		for (int i = 0; i < quantityX; i++)
		{
			offetRandom = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
			GameObject trapgobj1 = GameObject.Instantiate(Boss.Trap);
			Trap trap1 = trapgobj1.GetComponent<Trap>();
			trap1.direction = (new Vector2(-width / 2 + i * spaceX, height / 2) + offetRandom);
			trap1.transform.position = Boss.transform.position;
			trap1.StartFly();

			offetRandom = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
			GameObject trapgobj2 = GameObject.Instantiate(Boss.Trap);
			Trap trap2 = trapgobj2.GetComponent<Trap>();
			trap2.direction = (new Vector2(-width / 2.0f + i * spaceX, -height / 2) + offetRandom);
			trap2.transform.position = Boss.transform.position;
			trap2.StartFly();
		}

		// Spawn theo chieu doc
		for (int i = 0; i <= quantityY; i++)
		{
			offetRandom = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
			GameObject trapgobj1 = GameObject.Instantiate(Boss.Trap);
			Trap trap1 = trapgobj1.GetComponent<Trap>();
			trap1.direction = (new Vector2(width / 2, -height / 2 + i * spaceY) + offetRandom);
			trap1.transform.position = Boss.transform.position;
			trap1.StartFly();

			offetRandom = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
			GameObject trapgobj2 = GameObject.Instantiate(Boss.Trap);
			Trap trap2 = trapgobj2.GetComponent<Trap>();
			trap2.direction = (new Vector2(-width / 2, -height / 2 + i * spaceY) + offetRandom);
			trap2.transform.position = Boss.transform.position;
			trap2.StartFly();
		}

		yield return new WaitForSeconds(2f);
		BossStateMachine.ChangeState(Boss.IdleState);
	}
}
