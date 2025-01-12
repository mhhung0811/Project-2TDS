using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState
{
	protected Boss Boss;
	protected BossStateMachine BossStateMachine;

	public BossState(Boss boss, BossStateMachine bossStateMachine)
	{
		Boss = boss;
		BossStateMachine = bossStateMachine;
	}

	public virtual void Enter() { }
	public virtual void Exit() { }
	public virtual void FrameUpdate() { }
	public virtual void PhysicsUpdate() { }
	public virtual void AnimationTriggerEvent(Boss.AnimationTriggerType triggerType) { }
}
