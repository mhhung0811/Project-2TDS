using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BombSheeBulletState : EnemyState
{
	private BombShee bombShee => (BombShee)base.Enemy;
	private float speed = 20f;
	private bool hasReverse = false;
	private Vector2 posTarget = Vector2.zero;

	public BombSheeBulletState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
	{

	}

	public override void Enter()
	{
		bombShee.RB.drag = 0;
		bombShee.animator.SetBool("IsBullet", true);
		bombShee.RB.velocity = new Vector2(0, speed);
		bombShee.StartCoroutine(Reverse());
	}

	public override void Exit()
	{
		bombShee.animator.SetBool("IsBullet", false);
	}

	public override void FrameUpdate()
	{
		if(posTarget == Vector2.zero && hasReverse)
		{
			posTarget = bombShee.PlayerPos.CurrentValue;
			bombShee.RB.velocity = new Vector2(0, -speed);
			bombShee.transform.position = new Vector2(posTarget.x, bombShee.transform.position.y);
		}

		if(hasReverse && bombShee.transform.position.y <= posTarget.y)
		{
			bombShee.RB.velocity = Vector2.zero;
			bombShee.StateMachine.ChangeState(bombShee.DieState);
		}
	}

	public override void PhysicsUpdate()
	{

	}

	public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
	{

	}

	private IEnumerator Reverse()
	{
		yield return new WaitForSeconds(1f);
		hasReverse = true;
	}
}
