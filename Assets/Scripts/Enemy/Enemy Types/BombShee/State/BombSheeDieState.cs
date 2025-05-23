using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSheeDieState : EnemyState
{
    private BombShee bombShee => (BombShee)base.Enemy;

	public BombSheeDieState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
	{

	}

	public override void Enter()
	{
		Debug.Log("BombShee: Enter DieState");
		bombShee.animator.SetBool("IsDie", true);
		SoundManager.Instance.PlaySound("Explode");
		EffectManager.Instance.PlayEffect(EffectType.EfExplode, bombShee.transform.position, Quaternion.identity);
		bombShee.StartCoroutine(WaitDie());
	}

	public override void Exit()
	{
		bombShee.animator.SetBool("IsDie", false);
		bombShee.RB.velocity = Vector2.zero;
	}

	public override void FrameUpdate()
	{

	}

	public override void PhysicsUpdate()
	{

	}

	public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
	{

	}

	private IEnumerator WaitDie()
	{
		yield return new WaitForSeconds(0.5f);
		bombShee.gameObject.SetActive(false);
	}
}
