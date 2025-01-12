public class IdleState : IState
{
    private RoomController roomController;
    private RoomStateMachine roomStateMachine;
    
    public IdleState(RoomController roomController, RoomStateMachine roomStateMachine)
    {
        this.roomController = roomController;
        this.roomStateMachine = roomStateMachine;
    }
    
    public void Enter()
    {
        
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