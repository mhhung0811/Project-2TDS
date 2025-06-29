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
		Player.StartCoroutine(Teleport());
		Player.HoldGun.SetActive(false);
		Player.OnUseSkillTele?.Raise(new Void());
		Player.IsPlayerInteractable = false; 
		Player.isInvulnerable = true;
	}

	public override void Exit()
	{
		base.Exit();
		Player.IsRolling = false;
		Player.myRb.velocity = Vector2.zero;
		Player.IsPlayerInteractable = true;
		Player.HoldGun.SetActive(true);
		Player.isInvulnerable = false;
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

	private void Tele(float duration)
	{
		Vector2 targetPos = (Vector2)Player.GetMousePosition();
		Vector2 direction = targetPos - (Vector2)Player.transform.position;
		float distance = direction.magnitude;

		RaycastHit2D[] hits = Physics2D.RaycastAll(Player.transform.position, direction.normalized, distance, Player.wallLayer);

		foreach(var hit in hits)
		{
			if(hit.collider != null && hit.collider.CompareTag("Wall"))
			{
				targetPos = hit.point - direction.normalized * 0.75f;
				break;
			}
		}

		Player.StartCoroutine(MoveToPosition(Player.transform, targetPos, duration));
	}

	private IEnumerator Teleport()
	{
		// Dissolve
		Player.StartVerDissolve(0.2f);
		yield return new WaitForSeconds(0.2f);

		// Move
		EffectManager.Instance.PlayEffect(EffectType.EfRollLand, Player.transform.position, Quaternion.identity);
		Player.myRb.velocity = Vector2.zero;
		Player.trailTele.transform.position = Player.transform.position;
		Tele(0.1f);
		Player.trailTele.SetActive(true);
		yield return new WaitForSeconds(0.1f);

		// Appear
		EffectManager.Instance.PlayEffect(EffectType.EfRollLand, Player.transform.position, Quaternion.identity);
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
