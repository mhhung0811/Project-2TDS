public class SetupState : IState
{
    private RoomController roomController;
    private RoomStateMachine roomStateMachine;
    
    public SetupState(RoomController roomController, RoomStateMachine roomStateMachine)
    {
        this.roomController = roomController;
        this.roomStateMachine = roomStateMachine;
    }
    
    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void FrameUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}