public class TriggeredState : IState
{
    private RoomController roomController;
    private RoomStateMachine roomStateMachine;
    
    public TriggeredState(RoomController roomController, RoomStateMachine roomStateMachine)
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
        roomStateMachine.TransitionTo(roomStateMachine.activeState);
    }
}