using UnityEngine;

public class ActiveState : IState
{
    private RoomController roomController;
    
    public ActiveState(RoomController roomController)
    {
        this.roomController = roomController;
    }
    
    public void Enter()
    {
        // Debug.Log("Active State Enter");
        roomController.RoomSetUp();
    }

    public void FrameUpdate()
    {
        
    }

    public void PhysicsUpdate()
    {
        
    }

    public void Exit()
    {
        Debug.Log("Active State Exit");
    }
}