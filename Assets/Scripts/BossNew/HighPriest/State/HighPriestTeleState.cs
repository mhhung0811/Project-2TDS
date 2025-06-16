using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestTeleState : HighPriestState
{
	private List<Vector2Variable> posTeles = new List<Vector2Variable>();
	public HighPriestTeleState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{
		posTeles.Add(highPriest.posCenter);
		foreach(var pos in boss.posTele)
		{
			posTeles.Add(pos);
		}
	}

	public override void Enter()
	{
		base.Enter();
		boss.col.enabled = false;
		SortByDistance(boss.playerPos.CurrentValue);
		boss.StartCoroutine(Tele());
	}

	public override void Exit()
	{
		base.Exit();
		boss.col.enabled = true;
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private void SortByDistance(Vector2 posTarget)
	{
		posTeles.Sort((a, b) => Vector2.Distance(a.CurrentValue, posTarget).CompareTo(Vector2.Distance(b.CurrentValue, posTarget)));
	}

	private Vector2Variable FindPosNearest(Vector2 posTarget)
	{
		Vector2Variable nearest = posTeles[0];
		float minDistance = Vector2.Distance(nearest.CurrentValue, posTarget);

		foreach (var pos in posTeles)
		{
			float distance = Vector2.Distance(pos.CurrentValue, posTarget);
			if (distance < minDistance)
			{
				minDistance = distance;
				nearest = pos;
			}
		}

		return nearest;
	}

	private IEnumerator Tele()
	{
		boss.StartVerDissolve(0.25f);
		yield return new WaitForSeconds(0.25f);
		
		if(FindPosNearest(boss.transform.position) == posTeles[0])
		{
			boss.transform.position = posTeles[1].CurrentValue;
		}
		else
		{
			boss.transform.position = posTeles[0].CurrentValue;
		}

		boss.StartVerAppear(0.25f);
		yield return new WaitForSeconds(0.5f);
		stateMachine.ChangeState(boss.attackState);
	}
}

