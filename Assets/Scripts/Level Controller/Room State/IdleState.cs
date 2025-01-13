using UnityEngine;

public class IdleState : IState
{
    private RoomController roomController;
    
    public IdleState(RoomController roomController)
    {
        this.roomController = roomController;
    }
    
    public void Enter()
    {
        roomController.RoomDeactivated();
        Debug.Log("Idle State Enter");
    }

    public void FrameUpdate()
    {
        
    }

    public void PhysicsUpdate()
    {
        
    }

    public void Exit()
    {
        
    }
}