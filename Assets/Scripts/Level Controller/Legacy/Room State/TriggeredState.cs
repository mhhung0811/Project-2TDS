using System.Collections;
using UnityEngine;

public class TriggeredState : IState
{
    private RoomController roomController;
    
    public TriggeredState(RoomController roomController)
    {
        this.roomController = roomController;
    }   
    
    public void Enter()
    {
        roomController.RoomTriggered();
        Debug.Log("Room Triggered Enter");
    }

    public void FrameUpdate()
    {
        
    }

    public void PhysicsUpdate()
    {
        
    }

    public void Exit()
    {
        Debug.Log("Room Triggered Exit");
    }
}