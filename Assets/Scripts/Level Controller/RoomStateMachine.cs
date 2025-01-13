public class RoomStateMachine
{
    public IState CurrentState { get; private set; }
    
    public IdleState idleState { get; }
    public ActiveState activeState { get; }
    public TriggeredState triggeredState { get; }
    
    public RoomStateMachine(RoomController roomController)
    {
        idleState = new IdleState(roomController);
        activeState = new ActiveState(roomController);
        triggeredState = new TriggeredState(roomController);
    }
    
    public void Initialize(bool isActiveRoom)
    {
        CurrentState = isActiveRoom ? activeState : idleState;
        CurrentState.Enter();
    }
    
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
    
    public void FrameExecute()
    {
        CurrentState.FrameUpdate();
    }
    
    public void PhysicsExecute()
    {
        CurrentState.PhysicsUpdate();
    }
}