using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player Player;
    protected PlayerStateMachine PlayerStateMachine;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine)
    {
        this.Player = player;
        this.PlayerStateMachine = playerStateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Player.AnimationTriggerType triggerType) { }
}
