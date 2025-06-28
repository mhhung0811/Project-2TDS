using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleState : PlayerState
{
	public PlayerTeleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
	{

	}

	public override void Enter()
	{
		Debug.Log("PlayerTeleState: Enter");
		base.Enter();
		Player.IsRolling = true;
		Player.GetComponent<Collider2D>().enabled = false;
		Player.StartCoroutine(Teleport());
		Player.HoldGun.SetActive(false);
	}

	public override void Exit()
	{
		base.Exit();
		Player.GetComponent<Collider2D>().enabled = true;
		Player.IsRolling = false;
		Player.myRb.velocity = Vector2.zero;
		Player.HoldGun.SetActive(true);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
	{
		base.AnimationTriggerEvent(triggerType);

	}

	private IEnumerator Teleport()
	{
		// Dissolve
		Player.StartVerDissolve(0.2f);
		yield return new WaitForSeconds(0.2f);
		Player.myRb.velocity = Vector2.zero;
		Player.trailTele.transform.position = Player.transform.position;
		// Move
		Player.trailTele.SetActive(true);
		Player.StartCoroutine(MoveToPosition(Player.transform, (Vector2)Player.GetMousePosition(), 0.1f));
		yield return new WaitForSeconds(0.1f);

		// Appear
		Player.trailTele.SetActive(false);
		ChangeState();
		Player.StartVerAppear(0.2f);
		yield return new WaitForSeconds(0.2f);
	}

	IEnumerator MoveToPosition(Transform obj, Vector3 target, float duration)
	{
		Vector3 start = obj.position;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			obj.position = Vector3.Lerp(start, target, elapsed / duration);
			elapsed += Time.deltaTime;
			yield return null;
		}

		obj.position = target; // Đảm bảo đến đúng vị trí
	}

	private void ChangeState()
	{
		if (Player.IsPressWASD)
		{
			PlayerStateMachine.ChangeState(Player.MoveState);
		}
		else
		{
			PlayerStateMachine.ChangeState(Player.IdleState);
		}
	}

}
